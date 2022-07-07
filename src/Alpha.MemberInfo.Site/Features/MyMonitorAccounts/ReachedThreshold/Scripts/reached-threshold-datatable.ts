class ReachedThresholdDataTable {
    private $: JQueryStatic;

    private service: ReachedThresholdService;
    private viewModel: ReachedThresholdViewModel;
    private topWindow: any;

    private $divTable: JQuery;
    private $txtUsername: JQuery;
    private $fromDate: JQuery;
    private $toDate: JQuery;
    private $selStatus: JQuery;
    private $selLimitType: JQuery;
    private $btnSubmit: JQuery;

    private $table: JQuery;

    constructor(
        $: JQueryStatic,
        service: ReachedThresholdService,
        viewModel: ReachedThresholdViewModel,
        topWindow: any
    ) {
        this.$ = $;
        this.service = service;
        this.viewModel = viewModel;
        this.topWindow = topWindow;
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
    }

    public bindingEvent = () => {
        this.initDataTable();

        this.$divTable.on("click", ".reach-count", event => {
            const row = this.$table.row(this.$(event.currentTarget).parent()).data();

            const data = {
                username: row.Username,
                selectedCustId: row.CustId,
                fromDate: this.$fromDate.val(),
                toDate: this.$toDate.val(),
                limitType: this.$selLimitType.val()
            }

            const url = this.viewModel.MemberInfoUrl + `ReachedThreshold/GetReachHistory?${this.$.param(data)}`;
            this.topWindow.popupManager.open(url, undefined, 750, this.topWindow.alpha.isMobile ? 430 : 500);
        });
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

        this.$table = this.$divTable.DataTable({
            serverSide: true,
            ajax: (data, callback) => {
                console.log("data: ", data);
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
                        SortType: data.order[0].column,
                        IsDesc: data.order[0].dir === 'desc',
                        PageIndex: (data.start / data.length) + 1,
                        PageSize: data.length
                    }
                    this.service.getReachlistThreshold(params, callback);
                }
            },
            columns: this.buildDataTableColumns(),
            columnDefs: [
                {
                    orderable: false,
                    targets: 0
                }
            ],
            order: [[1, 'asc']],
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
                "data": "ReachedCnt",
                "render": item => `<a class="reach-count" href="#">${item}</a>`
            },
        );

        return columns;
    }

    private drawCallback = () => {
        this.$divTable.removeClass("hidden");

        this.$btnSubmit.off().click(() => {
            this.$table.ajax.reload();
        });
    }
}