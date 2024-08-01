"use strict"

declare var Chart;
declare var ChartDataLabels;
declare var TipsyHelper;

class MemberBettingBehavior {
    private $: JQueryStatic;
    private topWindow: any;
    private customerProfile: JQuery;
    private statisticBlock: JQuery;
    private summaryStatistic: JQuery;
    private exportExcelForm: JQuery;
    private txtUsername: JQuery;
    private isDarkMode: JQuery;
    private dateRange: SingleSelectionDropdownComponent;
    private turnoverBySportChart: TurnoverBySportChart;
    private turnoverByBetTypeChart: TurnoverByBetTypeChart;
    private turnoverWinLossByDateChart: TurnoverWinLossByDateChart;

    constructor(
        $: JQueryStatic,
        dataRange: SingleSelectionDropdownComponent,
        topWindow: any,
    ) {
        this.$ = $;
        this.dateRange = dataRange;
        this.topWindow = topWindow;
    }

    init() {
        this.cacheDOMElements();
        this.bindEvents();
        this.initCharts();

        this.dateRange.render(
            new SingleSelectionDropdownOption("DateRangeId", "DateRangeLevelId"));

        setTimeout(() => {
            this.topWindow.indexManager.initFeedbackFunction(9, undefined, 500);
        }, 1000);
    }

    cacheDOMElements(): void {
        this.exportExcelForm = this.$("#export-excel-form");
        this.customerProfile = this.$("#customer-profile");
        this.statisticBlock = this.$("#statisticId");
        this.summaryStatistic = this.$("#summary-statistic");
        this.txtUsername = this.$('#txt-username');
        this.isDarkMode = this.$('#isDarkMode');
    }

    bindEvents(): void {
        this.txtUsername
            .keyup(e => e.which === 13 && this.handleSubmit())
            .on("input", () => TipsyHelper.clear(this.txtUsername));

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
        this.$("#txt-username-hidden").val(encodeURIComponent(username));
        this.$("#date-range-hidden").val(this.dateRange.selectedValue);
        this.exportExcelForm.submit();
    };

    initCharts() {
        Chart.defaults.plugins.legend.position = 'bottom';
        Chart.defaults.plugins.legend.labels.color = '#333';
        Chart.defaults.plugins.title.color = '#333';
        Chart.defaults.plugins.title.display = true;
        Chart.defaults.plugins.title.font.size = 16;
        Chart.defaults.plugins.tooltip.boxPadding = 5;

        const isDarkMode = this.isDarkMode.val() === "true";
        this.turnoverBySportChart = new TurnoverBySportChart(isDarkMode);
        this.turnoverByBetTypeChart = new TurnoverByBetTypeChart(isDarkMode);
        this.turnoverWinLossByDateChart = new TurnoverWinLossByDateChart(isDarkMode);
    }

    getMemberProfile(username: string) {
        const url = this.$('#member-info-url').val() + "MemberBettingBehavior/MemberProfile";
        AJAXHelper.callAjax(
            url,
            'GET',
            {
                username: encodeURIComponent(username)
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
                username: encodeURIComponent(username),
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

                if (memberStatistic.SummaryStatistic.BetCount === 0) {
                    this.statisticBlock.show();
                    this.$("#no-infomation").show();
                    this.$("#statistic-chart-id").hide();
                    this.$.unblockUI();
                    return;
                }
                                                    
                this.statisticBlock.show();
                this.$("#no-infomation").hide();
                this.$("#statistic-chart-id").show();
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
        this.showMonitoredMember(memberProfile.IsMonitoredMember,() => this.handleClosePopup());
        this.$("#problem-account").text(memberProfile.IsProblemAccount ? yesText : noText);
        this.customerProfile.show();
    }

    showMonitoredMember(isMonitoredMember, handleClosePopup) {
        if (isMonitoredMember === null) {
            this.$("#monitored-item").hide();                                                           
            return;
        }

        if (isMonitoredMember) {
            this.$("#monitored-member-yes").show();
            this.$("#monitored-member-no").hide();
            return;
        }

        this.$("#monitored-member-yes").hide();
        this.$("#monitored-member-no").show();
        this.$("#btn-add-watchlist").on("click", () => {
            let addMyWatchList = window.top['addMyWatchListPopup'];
            if (window.top['addMyWatchListPopup'] === null || window.top['addMyWatchListPopup'] === undefined) {
                addMyWatchList = window.top.ageWnd.createInstance();
                addMyWatchList.onClosing = function () {
                    handleClosePopup()
                }
            }
            window.top['addMyWatchListPopup'] = addMyWatchList;
            window.top["reloadMyWatchlistDataTable"] = () => {
                window.top['addMyWatchListPopup'].close();
            };
            const url = `${window.top.alpha.urls.memberInfoRootPath}/MyWatchlistAccounts/ManageWatchlistAccounts?usernames=${this.txtUsername.val()}`;
            window.top["popupManager"].openWithHanlder(window.top['addMyWatchListPopup'], url, undefined, 820, 550, "modal-mobile-friendly");      
        })
        this.$("#monitored-item").show();
    }

    handleClosePopup() {
        this.getMemberProfile(this.checkMemberUsername());
    }

    renderSummaryStatistic(data) {
        this.$("#summary-turnover")
            .text(`${SiteHelper.FormatCompactNumber(data.Turnover)}`)
            .addClass(data.Turnover < 0 ? "redText" : "")
            .removeClass(data.Turnover >= 0 ? "redText" : "");

        this.$("#summary-margin")
            .text(`${data.Margin}%`)
            .addClass(data.Margin < 0 ? "redText" : "")
            .removeClass(data.Margin >= 0 ? "redText" : "");

        this.$("#summary-winloss")
            .text(`${SiteHelper.FormatCompactNumber(data.WinLoss)}`)
            .addClass(data.WinLoss < 0 ? "redText" : "")
            .removeClass(data.WinLoss >= 0 ? "redText" : "");

        this.$("#summary-betcount")
            .text(data.BetCount)
            .addClass(data.BetCount < 0 ? "redText" : "")
            .removeClass(data.BetCount >= 0 ? "redText" : "");

        this.summaryStatistic.addClass("value");
        this.summaryStatistic.show();
    }

    renderTurnoverBySportChart(data) {
        if (data.length > 0) {
            this.$("#turnover-by-sport-chart-id").show();
            const sportTypes = [];
            const turnovers = [];
            data.forEach((item) => {
                sportTypes.push(item.SportType);
                turnovers.push(item.MemberTurnover);
            })
            this.turnoverBySportChart.draw(sportTypes, turnovers);
        } else {
            this.$("#turnover-by-sport-chart-id").hide();
        }
    }

    renderTurnoverByBettypeChart(data) {
        if (data.length > 0) {
            this.$("#turnover-by-bettype-chart-id").show();
            const betTypes = [];
            const lives = [];
            const nonLives = [];
            const mixes = [];
            data.forEach((item) => {
                betTypes.push([item.Bettype, item.ProductName ? `(${item.ProductName})` : '']);
                lives.push(item.LiveTurnover);
                nonLives.push(item.NonLiveTurnover);
                mixes.push(item.MixTurnover);
            })
            this.turnoverByBetTypeChart.draw(betTypes, lives, nonLives, mixes);
        } else {
            this.$("#turnover-by-bettype-chart-id").hide();
        }
    }

    renderTurnoverWinLossByDatesChart(data) {
        if (data.length > 0) {
            this.$("#turnover-winloss-by-date-chart-id").show();
            const dates = [];
            const turnover1s = [];
            const winloss = [];
            data.forEach((item) => {
                dates.push(item.WinLostDate.length >= 10 ? item.WinLostDate.substring(0, 10) : item.WinLostDate);
                turnover1s.push(item.Turnover);
                winloss.push(item.WinLoss);
                this.turnoverWinLossByDateChart.draw(dates, turnover1s, winloss);
            })
        } else {
            this.$("#turnover-winloss-by-date-chart-id").hide();
        }
    }
}

$(function () {
    const memberBettingBehavior = new MemberBettingBehavior($, new SingleSelectionDropdownComponent($), window.top);
    memberBettingBehavior.init();
})                                         