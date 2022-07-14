"use strict";

class MyWatchlistAccountsDataTable {
    private $: JQueryStatic;
    private service: MyWatchlistAccountService;
    private viewModel: MyWatchlistAccountsViewModel;
    private editSettingPopup: EditSettingPopup;
    private username: string;
    private $table: JQuery;

    constructor(
        $: JQueryStatic,
        service: MyWatchlistAccountService,
        viewModel: MyWatchlistAccountsViewModel,
        editSettingPopup: EditSettingPopup
    ) {
        this.$ = $;
        this.service = service;
        this.viewModel = viewModel;
        this.editSettingPopup = editSettingPopup;
    }

    public bindingEvent = () => {
        this.initDataTable();
    }

    public reloadData = () => {
        this.$table.ajax.reload();
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

        this.$table = this.$('#tbl-my-watchlist-accounts').DataTable({
            serverSide: true,
            ajax: (data, callback) => {
                if (this.viewModel.WatchlistAccountData) {
                    callback({
                        data: this.viewModel.WatchlistAccountData.WatchlistAccounts,
                        recordsTotal: this.viewModel.WatchlistAccountData.TotalItems,
                        recordsFiltered: this.viewModel.WatchlistAccountData.TotalItems
                    });
                    delete this.viewModel.WatchlistAccountData;
                } else {
                    const sortType = !this.viewModel.IsMobile ? data.order[0].column - 2 : data.order[0].column - 1;
                    const params = {
                        sortType,
                        isDesc: data.order[0].dir === 'desc',
                        pageIndex: (data.start / data.length) + 1,
                        pageSize: data.length,
                        username: this.username,
                    }
                    this.service.getWatchlistAccounts(params, callback);
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
            order: [[this.viewModel.IsMobile ? 2 : 3, 'asc']],
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

        this.$("#tbl-my-watchlist-accounts").find('tbody').on('click', 'td.control', event => this.doAdjustHeader(event));

        this.$("thead tr th.control").off().click(event => this.collapseExpandAll(event));
    }

    private buildDataTableColumns = () => {
        let columns = [];

        if (!this.viewModel.IsMobile) {
            columns.push({
                "className": 'center',
                "data": "RowNumber",
                "orderable": false,
            });
        }

        columns.push(
            {
                "className": 'center',
                "data": null,
                "render": item => this.renderCheckboxColumn(item),
                "orderable": false,
            },
            {
                "className": 'center',
                "data": null,
                "render": item => this.renderEditColumn(item),
                "orderable": false,
            },
            {
                "className": 'center',
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
                "data": "WinningThresholdString",
            },
            {
                "className": 'center',
                "data": "LosingThresholdString",
            },
            {
                "data": null,
            }
        );

        return columns;
    }

    private renderCheckboxColumn = item => {
        const attrs = {
            'class': 'checkbox',
            'data-custid': item.CustId,
            'data-username': item.Username,
            'data-winningthreshold': item.WinningThreshold,
            'data-losingthreshold': item.LosingThreshold
        };

        return `<input type="checkbox" ${this.buildAttributes(attrs)} />`;
    }

    private renderEditColumn = item => {
        const attrs = {
            'class': 'edit icon-table-edit',
            'id': `setting-${item.CustId}`,
            'data-custid': item.CustId,
            'data-username': item.Username,
            'data-winningthreshold': item.WinningThreshold,
            'data-losingthreshold': item.LosingThreshold
            //'title': this.$('#editSettingText').val()
        };

        return `<div ${this.buildAttributes(attrs)}/>`;
    }

    private buildAttributes = attrs => this.$.map(attrs, (value, key) => `${key}='${value}'`).join(' ');

    private drawCallback = () => {
        this.editSettingPopup.bindingEditEvent();
        this.editSettingPopup.bindingEditMultipleEvent();
        this.editSettingPopup.bindingModalEvent();

        this.$("#tbl-my-watchlist-accounts").removeClass("hidden");

        this.$('#btn-submit').off().on('click', () => {
            this.username = this.$('#txt-username').val();
            this.$('.select-all').prop("checked", false);
            this.$table.ajax.reload();
        });

        this.$('tbody .checkbox').off().on("click", () => {
            this.$('.select-all').prop("checked", this.$('tbody .checkbox').length === this.$('tbody .checkbox:checked').length);
        });

        this.$('thead tr th.control').parent().removeClass("parent");
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