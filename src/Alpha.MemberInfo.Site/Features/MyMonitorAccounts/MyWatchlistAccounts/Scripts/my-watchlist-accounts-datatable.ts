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

    private initDataTable = () => {
        const maxPageSize = 500;
        const lengthMenu = `Page Size <select>` +
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
                info: `Page _INPUT_ Of _TOTAL_`
        };

        this.$table = this.$('#tbl-my-watchlist-accounts').DataTable({
            serverSide: true,
            ajax: (data, callback) => {
                console.log("data: ", data);
                if (this.viewModel.WatchlistAccountData) {
                    callback({
                        data: this.viewModel.WatchlistAccountData.WatchlistAccounts,
                        recordsTotal: this.viewModel.WatchlistAccountData.TotalItems,
                        recordsFiltered: this.viewModel.WatchlistAccountData.TotalItems
                    });
                    delete this.viewModel.WatchlistAccountData;
                } else {
                    const params = {
                        SortType: data.order[0].column - 2,
                        IsDesc: data.order[0].dir === 'desc',
                        PageIndex: (data.start / data.length) + 1,
                        PageSize: data.length,
                        Username: this.username,
                    }
                    this.service.getWatchlistAccounts(params, callback);
                }
            },
            columns: this.buildDataTableColumns(),
            columnDefs: [
                {
                    orderable: false,
                    targets: 0
                },
                {
                    orderable: false,
                    targets: 1
                },
                {
                    orderable: false,
                    targets: 2
                },
            ],
            order: [[3, 'asc']],
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
                emptyTable: "There is no data" // Get from resource
            },
            drawCallback: this.drawCallback,
            isSubmit: false
        })
    }

    private buildDataTableColumns = () => {
        let columns = [];

        columns.push(
            {
                "className": 'center',
                "data": "RowNumber",
                "width": "60px"
            },
            {
                "className": 'center',
                "data": null,
                "render": item => this.renderCheckboxColumn(item)
            },
            {
                "className": 'center',
                "data": null,
                "render": item => this.renderEditColumn(item)
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
    }
}