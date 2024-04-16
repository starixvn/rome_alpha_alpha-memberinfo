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

    render(summary) {
        this.$activeMembers.text(summary.ActiveMembers.toLocaleString());
        this.$monitoredMembers.text(summary.MonitoredMembers.toLocaleString());
        this.$reachedDailyLimitMembers.text(summary.ReachedDailyLimitMembers.toLocaleString());
        this.$unlimitedThresholdMembers.text(summary.UnlimitedThresholdMembers.toLocaleString());
        this.$unlimitedThresholdMembers
            .removeAttr("data-usernames")
            .removeAttr("data-title")
            .off("click");

        if (summary.UnlimitedThresholdUsers) {
            this.$unlimitedThresholdMembers.attr("data-usernames", summary.UnlimitedThresholdUsers);
            this.$unlimitedThresholdMembers.attr("data-title", this.unlimitedThresholdMembersTitle);
            this.$unlimitedThresholdMembers.click(() => {
                this.topWindow.popupManager.open(
                    `${this.baseUrl}MyWatchlistAccounts/ManageWatchlistAccounts?usernames=${summary.UnlimitedThresholdUsers}`,
                    undefined,
                    820,
                    550,
                    "modal-mobile-friendly");
            });
        }
    }
}