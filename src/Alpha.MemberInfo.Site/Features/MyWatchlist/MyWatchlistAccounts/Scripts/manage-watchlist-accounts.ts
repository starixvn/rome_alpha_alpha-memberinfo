"use strict";

class ManageWatchlistAccounts extends ThresholdInputsBase {
    private topWindow: any;
    private service: MyWatchlistAccountService;
    private presetDropdown: SingleSelectionDropdownComponent;

    private $txtAreaUsernames: JQuery;

    private reloadTimeout = 3000;

    constructor(
        $: JQueryStatic,
        topWindow: any,
        service: MyWatchlistAccountService
    ) {
        super($);
        this.topWindow = topWindow;
        this.service = service;
        this.presetDropdown = new SingleSelectionDropdownComponent($)
        this.cacheManageWatchlistAccountsDom();
    }

    private cacheManageWatchlistAccountsDom = () => {
        this.$txtAreaUsernames = this.$('textarea[name="usernames"]');
    }

    public init = () => {
        this.presetDropdown.render(
            new SingleSelectionDropdownOption(
                "preset-dropdown",
                "presetId",
                () => {
                    const selectedItems = this.presetDropdown.getSelectedItem();
                    if (selectedItems === "") return;

                    this.resetInputs();

                    const thresholds = selectedItems.split(" - ");
                    this.$txtWinningThreshold.val(thresholds[0]);
                    this.$txtLosingThreshold.val(thresholds[1]);
                },
                true,
                true));

        this.bindingThresholdTxtInput();
        this.bindingThresholdCheckboxs();
        this.$("#btn-add").off().click(this.handleAdd);
        this.$("#btn-update").off().click(this.handleUpdate);
    }

    private handleAdd = () => {
        this.alert.hide();

        const usernameArr = this.$txtAreaUsernames.val()
            .split(",")
            .map(u => u.trim())
            .filter(u => u !== "");

        if (!this.validateUsernamesInput(usernameArr) || !this.validateThresholdInput()) return;

        const thresholdInputs = this.getThresholdInputs();

        const params = {
            usernames: usernameArr.join(","),
            winningThreshold: thresholdInputs.winningThreshold,
            losingThreshold: thresholdInputs.losingThreshold
        }

        this.service.addPersonalThreshold(params, result => {
            if (!result) return;

            if (result.Code === 1) {
                this.alert.showError(result.Message);
                return;
            }

            if (result.length > 0) {
                result.forEach(res => {
                    switch (res.Code) {
                        case 0:
                            this.alert.showMultipleSuccess(res.Name, this.$("#add-successfully").val());
                            break;
                        case 101:
                            this.alert.showMultipleError(res.Name, this.$("#is-not-member-error").val());
                            break;
                        case 103:
                            this.alert.showMultipleError(res.Name, this.$("#was-added-watchlist-already-error").val());
                            break;
                        default:
                            break;
                    }
                });

                if (result.find(r => r.IsFailed) && result.find(r => r.IsSuccessful)) {
                    this.topWindow.reloadDataTable();
                    return;
                }

                if (!result.find(r => r.IsFailed)) {
                    WatchlistHelper.reloadMainFrameAfterSuccess(this.reloadTimeout);
                    return;
                }
            }
        });
    }

    private handleUpdate = () => {
        this.alert.hide();

        const custIds = this.$("#custids").val();
        const usernames = this.$("#usernames").val();

        if (!this.validateThresholdInput() || (custIds.length === 1 && !this.validateEditThresholdNoChangeUpdate()))
            return;

        const thresholdInputs = this.getThresholdInputs();

        const params = {
            custIds,
            usernames,
            winningThreshold: thresholdInputs.winningThreshold,
            losingThreshold: thresholdInputs.losingThreshold
        }

        this.service.updateMultiplePersonalThreshold(params, result => {
            if (!result) return;

            if (result.Code === 1) {
                this.alert.showError(result.Message);
                return;
            }

            if (result.length > 0) {
                result.forEach(res => {
                    switch (res.Code) {
                        case 0:
                            this.alert.showMultipleSuccess(res.Name, this.$("#update-successfully").val());
                            break;
                        case 102:
                            this.alert.showMultipleError(res.Name, this.$("#invalid-account-error").val());
                            break;
                        case 103:
                            this.alert.showMultipleError(res.Name, this.$("#not-in-watchlist-any-longer-error").val());
                            break;
                        default:
                            break;
                    }
                });

                if (result.find(r => r.IsFailed) && result.find(r => r.IsSuccessful)) {
                    this.topWindow.reloadDataTable();
                    return;
                }

                if (!result.find(r => r.IsFailed)) {
                    WatchlistHelper.reloadMainFrameAfterSuccess(this.reloadTimeout);
                    return;
                }
            }
        });
    }

    private resetInputs = () => {
        this.$txtWinningThreshold.prop("disabled", false).val("");
        this.$txtLosingThreshold.prop("disabled", false).val("");
        this.$ckbWinningUnlimited.prop("checked", false);
        this.$ckbLosingUnlimited.prop("checked", false);
    }

    private validateUsernamesInput = usernameArr => {
        if (usernameArr.length === 0) {
            this.alert.showError(this.$("#must-enter-username-error").val());
            return false;
        }

        if (usernameArr.length > 50) {
            this.alert.showError(this.$("#more-usernames-error").val());
            return false;
        }

        return true;
    }
}

$(function () {
    const service = new MyWatchlistAccountService($);
    const manageWatchlistAccounts = new ManageWatchlistAccounts($, window.top, service);
    manageWatchlistAccounts.init();
})