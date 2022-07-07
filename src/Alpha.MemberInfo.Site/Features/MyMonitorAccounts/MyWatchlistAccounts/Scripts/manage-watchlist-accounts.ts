"use strict";

class ManageWatchlistAccounts extends ThresholdInputsBase {
    private topWindow: any;
    private service: MyWatchlistAccountService;
    private presetDropdown: SingleSelectionDropdownComponent;

    private $txtAreaUsernames: JQuery;

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
            this.$.unblockUI();
            if (result && result.length > 0) {
                result.forEach(res => {
                    switch (res.ErrCode) {
                        case 0:
                            this.alert.showMultipleSuccess(res.Name, "Add successfully");
                            break;
                        case 101:
                            this.alert.showMultipleError(res.Name, "Are not member");
                            break;
                        case 103:
                            this.alert.showMultipleError(res.Name, "Added into watchlist already");
                            break;
                        default:
                            break;
                    }
                });

                //if (result.find(r => r.ErrCode === 0) && result.find(r => r.ErrCode !== 0)) {
                //    WatchlistHelper.reloadGrid();
                //}

                if (result.filter(r => r.ErrCode === 0).length === result.length) {
                    this.topWindow.closePopupAndReloadMainFrameAfterUpdateSuccess(3000);
                }
            }
        });
    }

    private handleUpdate = () => {
        this.alert.hide();

        if (!this.validateThresholdInput() || !this.validateEditThresholdNoChangeUpdate())
            return;

        const users = [];
        const custIds = this.$("#custids").val().split(",");
        const usernames = this.$("#usernames").val().split(",");
        for (let i = 0; i < custIds.length; i++) {
            users.push({
                custId: custIds[i],
                username: usernames[i].trim()
            })
        }

        const thresholdInputs = this.getThresholdInputs();

        const params = {
            users,
            winningThreshold: thresholdInputs.winningThreshold,
            losingThreshold: thresholdInputs.losingThreshold
        }

        this.service.updateMultiplePersonalThreshold(params, result => {
            if (result && result.length > 0) {
                result.forEach(res => {
                    switch (res.ErrCode) {
                        case 0:
                            this.alert.showMultipleSuccess(res.Name, "Update successfully");
                            break;
                        case 102:
                            this.alert.showMultipleError(res.Name, "Invalid account");
                            break;
                        case 103:
                            this.alert.showMultipleError(res.Name, "This member is not in watchlist account any longer");
                            break;
                        default:
                            break;
                    }
                });

                //if (result.find(r => r.ErrCode === 0) && result.find(r => r.ErrCode !== 0)) {
                //    WatchlistHelper.reloadGrid();
                //}

                if (result.filter(r => r.ErrCode === 0).length === result.length) {
                    this.topWindow.closePopupAndReloadMainFrameAfterUpdateSuccess(3000);
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
            this.alert.showError("Must enter member username");
            return false;
        }

        if (usernameArr.length > 50) {
            this.alert.showError("They're more than 50 member usernames");
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