"use strict";

class ManageGlobalThreshold extends ThresholdInputsBase {
    private service: MyWatchlistAccountService;

    private reloadTimeout = 3000;

    constructor(
        $: JQueryStatic,
        service: MyWatchlistAccountService,
    ) {
        super($);
        this.service = service;
    }

    public init = () => {
        this.bindingThresholdTxtInput();
        this.bindingThresholdCheckboxs();
        this.$("#btn-update").off().click(this.handleUpdate);
    }

    private handleUpdate = () => {
        this.alert.hide();

        if (!this.validateThresholdInput() || !this.validateEditThresholdNoChangeUpdate())
            return;

        const thresholdInputs = this.getThresholdInputs();
        this.service.updateGlobalThreshold(thresholdInputs, result => {
            if (result.IsSuccessful) {
                this.alert.showSuccess(this.$("#update-successfully").val());
                WatchlistHelper.reloadMainFrameAfterSuccess(this.reloadTimeout);
            } else if (result.Code === 1) {
                this.alert.showError(result.Message);
            }
        });
    }
}

$(function () {
    const service = new MyWatchlistAccountService($);
    const manageGlobalThreshold = new ManageGlobalThreshold($, service);

    manageGlobalThreshold.init();
})