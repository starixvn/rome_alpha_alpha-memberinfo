"use strict";

interface Window {
    reachHistoryData: any;
}

class ReachHistory {
    private $: JQueryStatic;
    private service: ReachedThresholdService;

    private $divTable: JQuery;
    private $selectedCustId: JQuery;
    private $fromDate: JQuery;
    private $toDate: JQuery;
    private $limitType: JQuery;

    private reachHistoryData: any;

    constructor(
        $: JQueryStatic,
        service: ReachedThresholdService,
        reachHistoryData: any) {
        this.$ = $;
        this.service = service;
        this.reachHistoryData = reachHistoryData;
        this.cacheDom();
    }

    private cacheDom = () => {
        this.$divTable = this.$('#tbl-reach-history');
        this.$selectedCustId = this.$('#selected-cust-id');
        this.$fromDate = this.$('#from-date');
        this.$toDate = this.$('#to-date');
        this.$limitType = this.$('#limit-type');
    }

    public init = () => {
        const sortDefinitions = {
            DateTime: 1,
            ThresholdType: 2,
            WinningLosingAmount: 3,
            WinningThreshold: 4,
            LosingThreshold: 5,
        }

        this.$divTable.DataTable({
            serverSide: true,
            ajax: (data, callback) => {
                if (this.reachHistoryData) {
                    callback({
                        data: this.reachHistoryData,
                    });
                    delete this.reachHistoryData;
                } else {
                    const columnName = data.columns[data.order[0].column].name;
                    const sortType = sortDefinitions[columnName];
                    const params = {
                        selectedCustId: this.$selectedCustId.val(),
                        fromDate: this.$fromDate.val(),
                        toDate: this.$toDate.val(),
                        limitType: this.$limitType.val(),
                        sortType,
                        isDesc: data.order[0].dir === 'desc',
                    }
                    this.service.getReachHistoryList(params, callback);
                }
            },
            columns: this.buildDataTableColumns(),
            order: [[1, 'desc']],
            stripeClasses: [],
            ordering: true,
            paging: false,
            searching: false,
            bInfo: false,
            fixedHeader: true,
            processing: false,
            language: {
                emptyTable: this.$("#no-info").val()
            },
            drawCallback: this.drawCallback,
            isSubmit: false
        });
    }

    private buildDataTableColumns = () => {
        let columns = [];

        columns.push(
            {
                "className": 'center',
                "data": "RowNumber",
                "orderable": false,
            },
            {
                "className": 'center',
                "data": "CreatedDateString",
                "name": "CreatedDate"
            },
            {
                "className": 'left',
                "data": "TranslatedLimitTypeReach",
                "name": "ThresholdType"
            },
            {
                "className": 'right',
                "name": "WinningLosingAmount",
                "data": item => `<div class="${item.ReachType === 1 ? 'winning-reach' : 'losing-reach'}">${item.WinLossAmountString}</div>`,
            },
            {
                "className": 'right',
                "name": "WinningThreshold",
                "data": item => `<div class="${item.ReachType === 1 ? 'winning-reach' : ''}">${item.WinningThresholdString}</div>`
            },
            {
                "className": 'right',
                "name": "LosingThreshold",
                "data": item => `<div class="${item.ReachType === 2 ? 'losing-reach' : ''}">${item.LosingThresholdString}</div>`
            },
        );

        return columns;
    }

    private drawCallback = () => {
        this.$divTable.removeClass("hidden");
    }

}

$(function () {
    const service = new ReachedThresholdService($);
    const reachHistory = new ReachHistory($, service, window.reachHistoryData);
    reachHistory.init();
});