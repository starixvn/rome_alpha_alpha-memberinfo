"use strict";

class MyWatchlistSummary {
    $: JQueryStatic;
    topWindow: any;
    baseUrl: string;
    $activeMembers: JQuery;
    $monitoredMembers: JQuery;
    $reachedDailyLimitMembers: JQuery;
    $unlimitedThresholdMembers: JQuery;
    unlimitedThresholdMembersTitle: string;

    constructor($: JQueryStatic, topWindow: any, baseUrl: string) {
        this.topWindow = topWindow;
        this.baseUrl = baseUrl;
        this.$activeMembers = $("#summary-active-members");
        this.$monitoredMembers = $("#summary-monitored-members");
        this.$reachedDailyLimitMembers = $("#summary-reached-daily-limit-members");
        this.$unlimitedThresholdMembers = $("#summary-unlimited-threshold-members");
        this.unlimitedThresholdMembersTitle = $("#click-to-set-thresholds-text").val();
    }

    render(summary, canUpdateAccount: boolean) {
        this.$activeMembers.text(summary.ActiveMembers.toLocaleString());
        this.$monitoredMembers.text(summary.MonitoredMembers.toLocaleString());
        this.$reachedDailyLimitMembers.text(summary.ReachedDailyLimitMembers.toLocaleString());
        this.$unlimitedThresholdMembers.text(summary.UnlimitedThresholdMembers.toLocaleString());

        if (canUpdateAccount) {
            this.$unlimitedThresholdMembers
                .removeAttr("data-usernames")
                .removeAttr("data-title")
                .css("cursor", "")
                .off("click");

            if (summary.UnlimitedThresholdUsers && summary.UnlimitedThresholdCustId) {
                this.$unlimitedThresholdMembers.attr("data-usernames", summary.UnlimitedThresholdUsers);
                this.$unlimitedThresholdMembers.attr("data-title", this.unlimitedThresholdMembersTitle);
                this.$unlimitedThresholdMembers.css("cursor", "pointer");
                this.$unlimitedThresholdMembers.click(() => {
                    const queryString = `custIds=${summary.UnlimitedThresholdCustId}&usernames=${summary.UnlimitedThresholdUsers}&winningThreshold=&losingThreshold=&isEdit=true`;
                    this.topWindow.popupManager.open(
                        `${this.baseUrl}MyWatchlistAccounts/ManageWatchlistAccounts?${queryString}`,
                        undefined,
                        820,
                        550,
                        "modal-mobile-friendly");
                });
            }
        }
    }
}