"use strict";

class ReachedThresholdDataTable {
    private $: JQueryStatic;

    private service: ReachedThresholdService;
    private viewModel: ReachedThresholdViewModel;
    private iframePopup: IframePopup;
    private topWindow: any;

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
        topWindow: any
    ) {
        this.$ = $;
        this.service = service;
        this.viewModel = viewModel;
        this.topWindow = topWindow;
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
                    const params = {
                        Username: this.$txtUsername.val(),
                        FromDate: this.$fromDate.val(),
                        ToDate: this.$toDate.val(),
                        Status: this.$selStatus.val(),
                        LimitType: this.$selLimitType.val(),
                        SortType: this.viewModel.IsMobile ? data.order[0].column + 1 : data.order[0].column,
                        IsDesc: data.order[0].dir === 'desc',
                        PageIndex: (data.start / data.length) + 1,
                        PageSize: data.length
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
                "className": 'text-left',
                "data": "Username",
            },
            {
                "className": 'center',
                "data": "LastLoggedString",
            },
            {
                "className": 'center',
                "data": "Status",
            },
            {
                "className": 'center',
                "data": "LastReachedDateString",
            },
            {
                "className": 'center',
                "data": "LastLimitTypeReach",
            },
            {
                "className": 'text-right',
                "data": "WinLossAmountString",
            },
            {
                "className": 'text-right',
                "data": "WinningThresholdString",
            },
            {
                "className": 'text-right',
                "data": "LosingThresholdString",
            },
            {
                "className": 'text-right',
                "data": null,
                "render": item => `<a class="reach-count" href="#" data-custid="${item.CustId}" data-username="${item.Username}">${item.ReachedCnt}</a>`
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

        const url = this.viewModel.URL + "/GetReachHistory";
        this.iframePopup.open("800px", "600px", username + " - " + this.$("#reach-history-popup-title").val(), url, params);
    }

    private handleExportExcel = () => {
        this.$("#Username").val(this.$txtUsername.val());
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