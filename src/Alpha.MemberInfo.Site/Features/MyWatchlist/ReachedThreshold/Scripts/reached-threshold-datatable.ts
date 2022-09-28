"use strict";

class ReachedThresholdDataTable {
    private $: JQueryStatic;

    private service: ReachedThresholdService;
    private viewModel: ReachedThresholdViewModel;
    private iframePopup: IframePopup;

    private $divTable: JQuery;
    private $txtUsername: JQuery;
    private $fromDate: JQuery;
    private $toDate: JQuery;
    private $selStatus: JQuery;
    private $selLimitType: JQuery;
    private $btnSubmit: JQuery;
    private $btnExcel: JQuery;

    private $table: JQuery;

    constructor(
        $: JQueryStatic,
        service: ReachedThresholdService,
        viewModel: ReachedThresholdViewModel,
        iframePopup: IframePopup,
    ) {
        this.$ = $;
        this.service = service;
        this.viewModel = viewModel;
        this.iframePopup = iframePopup;
        this.cacheDom();
    }

    private cacheDom = () => {
        this.$txtUsername = this.$("#txt-username");
        this.$fromDate = this.$("#from-date");
        this.$toDate = this.$("#to-date");
        this.$selStatus = this.$("#status-dropdown");
        this.$selLimitType = this.$("#limit-type-dropdown");
        this.$divTable = this.$('#tbl-reached-threshold');
        this.$btnSubmit = this.$("#btn-submit");
        this.$btnExcel = this.$("#icon-excel");
    }

    public bindingEvent = () => {
        this.initDataTable();
        this.$divTable.on("click", ".reach-count", this.handleOpenReachHistory);
        this.$btnExcel.off().click(this.handleExportExcel);
    }

    private initDataTable = () => {
        const maxPageSize = this.viewModel.IsMobile ? 200 : 500;
        const lengthMenu = `${this.$("#page-size").val()} <select>` +
            '<option value="10">10</option>' +
            '<option value="50">50</option>' +
            '<option value="100">100</option>' +
            `<option value="${maxPageSize}">${maxPageSize}</option>` +
            '</select>';
        const oPaginate = {
            sFirst: '',
            sPrevious: '',
            sNext: '',
            sLast: '',
            info: `${this.$("#page").val()} _INPUT_ ${this.$("#of").val()} _TOTAL_`
        };

        const sortDefinitions = {
            Username: 1,
            LastLogin: 2,
            Status: 3,
            LastReach: 4,
            LastLimitTypeReach: 5,
            WinningLosingAmount: 6,
            LastReachWinningThreshold: 7,
            LastReachLosingThreshold: 8,
            NoOfReach: 9
        }

        this.$table = this.$divTable.DataTable({
            serverSide: true,
            ajax: (data, callback) => {
                if (this.viewModel.ReachlistAccountData) {
                    callback({
                        data: this.viewModel.ReachlistAccountData.ReachlistAccounts,
                        recordsTotal: this.viewModel.ReachlistAccountData.TotalItems,
                        recordsFiltered: this.viewModel.ReachlistAccountData.TotalItems
                    });
                    delete this.viewModel.ReachlistAccountData;
                } else {
                    const columnName = data.columns[data.order[0].column].name;
                    const sortType = sortDefinitions[columnName];
                    const params = {
                        username: this.$txtUsername.val().trim(),
                        fromDate: this.$fromDate.val(),
                        toDate: this.$toDate.val(),
                        status: this.$selStatus.val(),
                        limitType: this.$selLimitType.val(),
                        sortType,
                        isDesc: data.order[0].dir === 'desc',
                        pageIndex: (data.start / data.length) + 1,
                        pageSize: data.length
                    }
                    this.service.getReachlistThreshold(params, callback);
                }
            },
            columns: this.buildDataTableColumns(),
            responsive: {
                details: {
                    type: 'column',
                    target: -1,
                    auto: false,
                    minWidth: "100px"
                }
            },
            columnDefs: [
                {
                    className: 'control',
                    data: null,
                    orderable: false,
                    defaultContent: '',
                    targets: -1
                },
            ],
            createdRow: (row, data) => {
                this.$(row).addClass(this.getRowClass(data));
            },
            order: [[this.viewModel.IsMobile ? 0 : 1, 'asc']],
            stripeClasses: [],
            ordering: true,
            paging: true,
            pagingType: "input",
            pageLength: 100,
            searching: false,
            bInfo: false,
            fixedHeader: true,
            processing: false,
            dom: 'rt<"bottom"iflp<"clear">>',
            language: {
                processing: '',
                lengthMenu,
                oPaginate,
                emptyTable: this.$("#no-info").val()
            },
            drawCallback: this.drawCallback,
            isSubmit: false
        });

        this.$("#tbl-reached-threshold").find('tbody').on('click', 'td.control', event => this.doAdjustHeader(event));

        this.$("thead tr th.control").off().click(event => this.collapseExpandAll(event));
    }

    private getRowClass = (data) => {
        if (data.Status === "Disabled") {
            return reachedThresholdStatusClasses.Disabled.Css;
        }

        if (data.Status === "Suspended") {
            return reachedThresholdStatusClasses.Suspended.Css;
        }

        if (data.ClosedReason !== 0) {
            return reachedThresholdStatusClasses.Closed.filter(closedReason => data.ClosedReason === closedReason.Id)[0].Css;
        }

        if (data.Locked !== 0) {
            return reachedThresholdStatusClasses.Locked.filter(locked => data.Locked === locked.Id)[0].Css;
        }

        return "";
    }

    private buildDataTableColumns = () => {
        let columns = [];

        if (!this.viewModel.IsMobile) {
            columns.push({
                "className": 'center',
                "data": "RowNumber",
                "width": "60px",
                "orderable": false,
            });
        }

        columns.push(
            {
                "className": 'left',
                "data": "Username",
                "name": "Username"
            },
        );

        if (this.viewModel.IsShowLastLoginColumn) {
            columns.push({
                "className": 'center',
                "data": "LastLoggedString",
                "name": "LastLogin"
            });
        }

        columns.push(
            {
                "className": 'center',
                "data": "TranslatedStatus",
                "name": "Status"
            },
            {
                "className": 'center',
                "data": "LastReachedDateString",
                "name": "LastReach"
            },
            {
                "className": 'center',
                "data": "TranslatedLastLimitTypeReach",
                "name": "LastLimitTypeReach"
            },
            {
                "className": 'right',
                "data": "WinLossAmountString",
                "name": "WinningLosingAmount"
            },
            {
                "className": 'right',
                "data": "WinningThresholdString",
                "name": "LastReachWinningThreshold"
            },
            {
                "className": 'right',
                "data": "LosingThresholdString",
                "name": "LastReachLosingThreshold"
            },
            {
                "className": 'right',
                "data": null,
                "render": item => {
                    var reachCount = `${item.ReachedCnt} `;

                    if (item.WinningReachedCnt > 0 && item.LosingReachedCnt > 0) {
                        reachCount += `(${item.WinningReachedCnt}W, ${item.LosingReachedCnt}L)`;
                    } else if (item.WinningReachedCnt > 0) {
                        reachCount += `(${item.WinningReachedCnt}W)`;
                    } else if (item.LosingReachedCnt > 0) {
                        reachCount += `(${item.LosingReachedCnt}L)`;
                    }

                    return `<a class="reach-count" href="#" data-custid="${item.CustId}" data-username="${item.Username}">${reachCount}</a>`;
                },
                "name": "NoOfReach"
            },
            {
                "data": null,
            }
        );

        return columns;
    }

    private drawCallback = () => {
        this.$divTable.removeClass("hidden");

        this.$btnSubmit.off().click(() => {
            this.$table.ajax.reload();
        });
    }

    private handleOpenReachHistory = event => {
        const custId = this.$(event.currentTarget).data("custid");
        const username = this.$(event.currentTarget).data("username");

        const params = {
            selectedCustId: custId,
            fromDate: this.$fromDate.val(),
            toDate: this.$toDate.val(),
            limitType: this.$selLimitType.val()
        }

        const url = this.viewModel.URL + "/ReachHistoryIndex";
        this.iframePopup.open("900px", "600px", username + " - " + this.$("#reach-history-popup-title").val(), url, params);
    }

    private handleExportExcel = () => {
        this.$("#Username").val(this.$txtUsername.val().trim());
        this.$("#FromDate").val(this.$fromDate.val());
        this.$("#ToDate").val(this.$toDate.val());
        this.$("#Status").val(this.$selStatus.val());
        this.$("#LimitType").val(this.$selLimitType.val());

        this.$("#export-excel-form").submit();
    }

    private collapseExpandAll = event => {
        this.$.blockUI();
        setTimeout(() => {
            const targetParent = this.$(event.currentTarget).parent();

            if (targetParent.hasClass("parent")) {
                targetParent.removeClass("parent");
                this.triggerClickDetailWhenSwitchAll(this.$('tbody tr.parent'));
                this.$(event.currentTarget).attr('title', this.$('#expandAllText').val());
            }
            else {
                targetParent.addClass("parent");
                this.triggerClickDetailWhenSwitchAll(this.$('tbody tr').not(".parent"));
                this.$(event.currentTarget).attr('title', this.$('#collapseAllText').val());
            }

            this.$.unblockUI();
        }, 100);
    }

    private triggerClickDetailWhenSwitchAll = controlArray => {
        controlArray.each((index, detailButton) => this.$(detailButton).find('.control').click());
        this.$table.fixedHeader.adjust();
    }

    private doAdjustHeader = event => {
        if (!event.isTrigger) {
            this.$table.fixedHeader();
        }
    };
}