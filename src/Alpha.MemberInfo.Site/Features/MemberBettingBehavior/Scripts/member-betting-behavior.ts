"use strict"

declare var Chart;
declare var ChartDataLabels;
declare var TipsyHelper;

class MemberBettingBehavior {
    private $: JQueryStatic;
    private $customerProfile: JQuery;
    private $summaryStatistic: JQuery;
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
        this.$customerProfile = this.$("#customer-profile");
        this.$summaryStatistic = this.$("#summary-statistic");
    }

    bindEvents(): void {
        const $txtUsername = this.$('#txt-username');
        //$txtUsername.on('input', () => {
        //    this.$.trim($txtUsername.val()).length
        //        ? TipsyHelper.clear($txtUsername)
        //        : TipsyHelper.showError($txtUsername, 'Please enter member username');
        //});

        this.$('#btn-submit').click(() => {
            const username = this.$.trim($txtUsername.val());
            if (!username.length) {
                TipsyHelper.showError($txtUsername, 'Please enter member username');
                return;
            }
            //TipsyHelper.clear($txtUsername);

            this.getMemberBettingBehaviorData(username);
        });

        this.$("#icon-excel").on("click", this.exportExcel);
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

    getMemberBettingBehaviorData(username: string) {
        const url = this.$('#member-info-url').val() + "MemberBettingBehavior/MemberBettingBehavior";
        this.callAjax(
            url,
            'GET',
            {
                username
            },
            data => {
                this.$.unblockUI();
                this.renderCustomerProfile(data.CustomerProfile);
                this.renderSummaryStatistic(data.MemberBettingStatistic.SummaryStatistic);
                this.renderTurnoverBySportChart(data.MemberBettingStatistic.TurnOverBySports);
                this.renderTurnoverByBettypeChart(data.MemberBettingStatistic.TurnoverByBettypes);
                this.renderTurnoverWinLossByDatesChart(data.MemberBettingStatistic.TurnoverWinLossByDates)
            }
        )
    }

    renderCustomerProfile(data) {
        if (data === null) {
            this.$customerProfile.hide();
            return;
        }

        this.$("#customer-status").text(data.Status);
        this.$("#customer-level").text(data.Level);
        this.$("#customer-related-account").text(data.RelatedToOtherAccount ? "Yes" : "No");
        this.$("#monitored-member").text(data.IsMonitoredMember ? "Yes" : "No");
        this.$("#problem-account").text(data.IsProblemAccount ? "Yes" : "No");
        this.$customerProfile.show();
    }

    renderSummaryStatistic(data) {
        if (data === null) {
            this.$summaryStatistic.hide();
            return;
        }

        this.$("#summary-turnover").text(data.Turnover);
        this.$("#summary-margin").text(data.Margin);
        this.$("#summary-winloss").text(data.WinLoss);
        this.$("#summary-betcount").text(data.BetCount);
        this.$summaryStatistic.addClass("value");
        this.$summaryStatistic.show();
    }

    renderTurnoverBySportChart(data) {
        if (data.length < 1) {
            return;
        }
        const sportTypes = data.map((item) => item.SportType);
        const turnovers = data.map((item) => item.MemberTurnover);
        this.turnoverBySportChart.draw(sportTypes, turnovers);
    }

    renderTurnoverByBettypeChart(data) {
        if (data.length < 1) {
            return;
        }
        const betTypes = data.map((item) => item.Bettype);
        const lives = data.map((item) => item.LiveTurnover);
        const nonLives = data.map((item) => item.NonLiveTurnover);
        const mixes = data.map((item) => item.MixTurnover);
        this.turnoverByBetTypeChart.draw(betTypes, lives, nonLives, mixes);
    }

    renderTurnoverWinLossByDatesChart(data) {
        if (data.length < 1) {
            return;
        }
        const dates = data.map((item) => item.WinLostDate.length >= 5 ? item.WinLostDate.substring(0, 5) : item.WinLostDate);
        const turnover1s = data.map((item) => item.Turnover);
        const winloss = data.map((item) => item.WinLoss);
        this.turnoverWinLossByDateChart.draw(dates, turnover1s, winloss);
    }
}

$(function () {
    const memberBettingBehavior = new MemberBettingBehavior($);
    memberBettingBehavior.init();
})