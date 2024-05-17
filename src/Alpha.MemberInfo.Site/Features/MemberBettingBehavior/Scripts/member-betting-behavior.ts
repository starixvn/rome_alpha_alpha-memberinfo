"use strict"

declare var Chart;
declare var ChartDataLabels;
declare var TipsyHelper;

class MemberBettingBehavior {
    private $: JQueryStatic;
    private $exportExcelForm: JQuery;
    private turnoverBySportChart: TurnoverBySportChart;
    private turnoverByBetTypeChart: TurnoverByBetTypeChart;
    private turnoverWinLossByDateChart: TurnoverWinLossByDateChart;

    constructor(
        $: JQueryStatic
    ) {
        this.$ = $
    }

    init() {
        this.cacheDOMElements();
        this.bindEvents();
        this.initCharts();
    }

    cacheDOMElements(): void {
        this.$exportExcelForm = this.$("#export-excel-form");
    }

    bindEvents(): void {
        this.$("#icon-excel").on("click", this.exportExcel);
        const $txtUsername = this.$('#txt-username');
        //$txtUsername.on('input', () => {
        //    this.$.trim($txtUsername.val()).length
        //        ? TipsyHelper.clear($txtUsername)
        //        : TipsyHelper.showError($txtUsername, 'Please enter member username');
        //});

        this.$('#btn-submit').click(() => {
            this.getCustomerProfile();
            //const username = this.$.trim($txtUsername.val());
            //if (!username.length) {
            //    TipsyHelper.showError($txtUsername, 'Please enter member username');
            //    return;
            //}

            //TipsyHelper.clear($txtUsername);
            const getRandomInt = (min, max) => {
                min = Math.ceil(min);
                max = Math.floor(max);
                return Math.floor(Math.random() * (max - min + 1)) + min;
            };

            const sportTypes = ['Soccer', 'Basketball', 'Saba Soccer', 'Allbet', 'Others'];
            const turnovers = [];
            for (let i = 0; i < sportTypes.length; i++) {
                turnovers.push(getRandomInt(10, 1000));
            }
            this.turnoverBySportChart.draw(sportTypes, turnovers);

            const betTypes = ['Over/Under', 'Handicap', '1H Over/Under', 'Others'];
            const lives = [];
            const nonLives = [];
            const mixes = [];
            for (let i = 0; i < betTypes.length; i++) {
                lives.push(getRandomInt(10, 1000));
                nonLives.push(getRandomInt(10, 1000));
                mixes.push(getRandomInt(10, 100));
            }
            this.turnoverByBetTypeChart.draw(betTypes, lives, nonLives, mixes);

            const dates = [];
            const turnover1s = [];
            const winloss = [];
            for (let i = 1; i <= 29; i++) {
                dates.push(`2/${i}`)
                if (i === 1 || i === 5 || i === 12) {
                    turnover1s.push(getRandomInt(0, 24000));
                    winloss.push(getRandomInt(-5000, 5000));
                } else {
                    turnover1s.push(0);
                    winloss.push(0);
                }
            }
            for (let i = 1; i <= 31; i++) {
                dates.push(`3/${i}`)
                if (i === 15 || i === 16 || i === 17) {
                    turnover1s.push(getRandomInt(0, 24000));
                    winloss.push(getRandomInt(-5000, 5000));
                } else {
                    turnover1s.push(0);
                    winloss.push(0);
                }
            }
            for (let i = 1; i <= 30; i++) {
                if (i === 20 || i === 25 || i === 30) {
                    turnover1s.push(getRandomInt(0, 24000));
                    winloss.push(getRandomInt(-5000, 5000));
                } else {
                    turnover1s.push(0);
                    winloss.push(0);
                }
            }

            this.turnoverWinLossByDateChart.draw(dates, turnover1s, winloss);
        });
    }

    exportExcel = () => {
        this.$exportExcelForm.submit();
    };

    initCharts() {
        Chart.defaults.plugins.legend.position = 'bottom';
        Chart.defaults.plugins.legend.labels.color = '#333';
        Chart.defaults.plugins.title.color = '#333';
        Chart.defaults.plugins.title.display = true;
        Chart.defaults.plugins.title.font.size = 16;
        Chart.defaults.plugins.tooltip.boxPadding = 5;

        this.turnoverBySportChart = new TurnoverBySportChart();
        this.turnoverByBetTypeChart = new TurnoverByBetTypeChart();
        this.turnoverWinLossByDateChart = new TurnoverWinLossByDateChart();
    }

    callAjax = (url, type, params, onSuccess) => {
        this.$.blockUI();
        const xhr = this.callAjaxWithoutBlocking(url, type, params, onSuccess);
        xhr.error = this.$.unblockUI;
    }

    callAjaxWithoutBlocking = (url, type, params, onSuccess): JQueryXHR => {
        return this.$.ajax({
            url: url,
            dataType: "json",
            type: type,
            headers: {
                __RequestVerificationToken: this.$("input[name='__RequestVerificationToken']").val(),
                'X-Requested-With': 'XMLHttpRequest'
            },
            data: params,
            success: onSuccess
        });
    }

    getCustomerProfile() {
        const url = this.$('#member-info-url').val() + "MemberBettingBehavior/CustomerProfile";
        this.callAjax(
            url,
            'GET',
            {
                username: "u"
            },
            data => {
                this.$.unblockUI();
                console.log(data);
            }
        )
    }
}

$(function () {
    const memberBettingBehavior = new MemberBettingBehavior($);
    memberBettingBehavior.init();
})