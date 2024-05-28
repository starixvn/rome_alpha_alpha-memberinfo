"use strict"

declare var Chart;
declare var ChartDataLabels;
declare var TipsyHelper;

class MemberBettingBehavior {
    private $: JQueryStatic;
    private customerProfile: JQuery;
    private statisticBlock: JQuery;
    private summaryStatistic: JQuery;
    private exportExcelForm: JQuery;
    private txtUsername: JQuery;
    private dateRange: SingleSelectionDropdownComponent;
    private turnoverBySportChart: TurnoverBySportChart;
    private turnoverByBetTypeChart: TurnoverByBetTypeChart;
    private turnoverWinLossByDateChart: TurnoverWinLossByDateChart;

    constructor(
        $: JQueryStatic,
        dataRange: SingleSelectionDropdownComponent,
    ) {
        this.$ = $;
        this.dateRange = dataRange;
    }

    init() {
        this.cacheDOMElements();
        this.bindEvents();
        this.initCharts();

        this.dateRange.render(
            new SingleSelectionDropdownOption("DateRangeId", "DateRangeLevelId"));
    }

    cacheDOMElements(): void {
        this.exportExcelForm = this.$("#export-excel-form");
        this.customerProfile = this.$("#customer-profile");
        this.statisticBlock = this.$("#statisticId");
        this.summaryStatistic = this.$("#summary-statistic");
        this.txtUsername = this.$('#txt-username');
    }

    bindEvents(): void {
        this.txtUsername.keypress((e) => {
            if (e.which == 13) {
                this.handleSubmit();
            }
        });

        this.$('#btn-submit').click(() => this.handleSubmit());

        this.$("#DateRangeId").on("change", () => {
            const username = this.checkMemberUsername();
            if (username) {
                this.getMemberStatistic(username, Number(this.dateRange.selectedValue));
            }
        });

        this.$("#icon-excel").on("click", () => {
            const username = this.checkMemberUsername();
            if (username) {
                this.exportExcel(username)
            }
        });
    }

    handleSubmit = () => {
        const username = this.checkMemberUsername();
        if (username) {
            this.getMemberProfile(username);
            this.getMemberStatistic(username, Number(this.dateRange.selectedValue));
        }
    }

    checkMemberUsername = () => {
        const username = this.$.trim(this.$(("#txt-username")).val());
        if (!username.length) {
            const mustEnterUsernameText = this.$("#mustEnterUserNameText").val();
            TipsyHelper.showError(this.txtUsername, mustEnterUsernameText);
            return "";
        }

        return username;
    }

    exportExcel = (username: string) => {
        this.$("#txt-username-hidden").val(username);
        this.$("#date-range").val(this.dateRange.selectedValue);
        this.exportExcelForm.submit();
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

    getMemberProfile(username: string) {
        const url = this.$('#member-info-url').val() + "MemberBettingBehavior/MemberProfile";
        AJAXHelper.callAjax(
            url,
            'GET',
            {
                username
            },
            memberProfile => {
                if (memberProfile.IsFailed) {
                    this.customerProfile.hide();
                    const memberUsernameInvalidText = this.$("#memberUserNameIsInvalidText").val();
                    TipsyHelper.showError(this.txtUsername, memberUsernameInvalidText);
                    this.$.unblockUI();
                    return;
                }
                this.renderCustomerProfile(memberProfile);
                this.$.unblockUI();
            }
        )
    }

    getMemberStatistic(username: string, dateRange?: number) {
        const url = this.$('#member-info-url').val() + "MemberBettingBehavior/MemberBettingStatistic";
        AJAXHelper.callAjax(
            url,
            'GET',
            {
                username,
                dateRange
            },
            memberStatistic => {
                if (memberStatistic.IsFailed) {
                    this.statisticBlock.hide();
                    const memberUsernameInvalidText = this.$("#memberUserNameIsInvalidText").val();
                    TipsyHelper.showError(this.txtUsername, memberUsernameInvalidText);
                    this.$.unblockUI();
                    return;
                }
                this.statisticBlock.show();
                this.renderSummaryStatistic(memberStatistic.SummaryStatistic);
                this.renderTurnoverBySportChart(memberStatistic.TurnOverBySports);
                this.renderTurnoverByBettypeChart(memberStatistic.TurnoverByBettypes);
                this.renderTurnoverWinLossByDatesChart(memberStatistic.TurnoverWinLossByDates)
                this.$.unblockUI();
            }
        )
    }

    renderCustomerProfile(memberProfile) {
        const yesText = this.$("#yesText").val();
        const noText = this.$("#noText").val();
        this.$("#customer-status").text(memberProfile.Status);
        this.$("#customer-level").text(memberProfile.Level);
        this.$("#customer-related-account").text(memberProfile.RelatedToOtherAccount ? yesText : noText);
        if (memberProfile.IsMonitoredMember === null) {
            this.$("#monitored-item").hide();
        } else {
            this.$("#monitored-member").text(memberProfile.IsMonitoredMember ? yesText : noText);
            this.$("#monitored-item").show();
        }
        this.$("#problem-account").text(memberProfile.IsProblemAccount ? yesText : noText);
        this.customerProfile.show();
    }

    renderSummaryStatistic(data) {
        this.$("#summary-turnover").text(`${SiteHelper.FormatCompactNumber(data.Turnover)}`);
        this.$("#summary-margin").text(`${data.Margin}%`);
        this.$("#summary-winloss").text(`${SiteHelper.FormatCompactNumber(data.WinLoss)}`);
        this.$("#summary-betcount").text(data.BetCount);
        this.summaryStatistic.addClass("value");
        this.summaryStatistic.show();
    }

    renderTurnoverBySportChart(data) {
        if (data.length > 0) {
            const sportTypes = [];
            const turnovers = [];
            data.forEach((item) => {
                sportTypes.push(item.SportType);
                turnovers.push(item.MemberTurnover);
            })
            this.turnoverBySportChart.draw(sportTypes, turnovers);
        }
    }

    renderTurnoverByBettypeChart(data) {
        if (data.length > 0) {
            const betTypes = [];
            const lives = [];
            const nonLives = [];
            const mixes = [];
            data.forEach((item) => {
                betTypes.push(item.Bettype);
                lives.push(item.LiveTurnover);
                nonLives.push(item.NonLiveTurnover);
                mixes.push(item.MixTurnover);
            })
            this.turnoverByBetTypeChart.draw(betTypes, lives, nonLives, mixes);
        }
    }

    renderTurnoverWinLossByDatesChart(data) {
        if (data.length > 0) {
            const dates = [];
            const turnover1s = [];
            const winloss = [];
            data.forEach((item) => {
                dates.push(item.WinLostDate.length >= 5 ? item.WinLostDate.substring(0, 5) : item.WinLostDate);
                turnover1s.push(item.Turnover);
                winloss.push(item.WinLoss);
                this.turnoverWinLossByDateChart.draw(dates, turnover1s, winloss);
            })
        }
    }
}

$(function () {
    const memberBettingBehavior = new MemberBettingBehavior($, new SingleSelectionDropdownComponent($));
    memberBettingBehavior.init();
})                                         