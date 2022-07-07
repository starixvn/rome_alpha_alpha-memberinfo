"use strict";

class ManageGlobalThreshold extends ThresholdInputsBase {
    private topWindow: any;
    private service: MyWatchlistAccountService;

    constructor(
        $: JQueryStatic,
        topWindow: any,
        service: MyWatchlistAccountService,
    ) {
        super($);
        this.topWindow = topWindow;
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
                this.alert.showSuccess("Update successfully");
                this.topWindow.closePopupAndReloadMainFrameAfterUpdateSuccess(3000);
            }
        });
    }
}

$(function () {
    const service = new MyWatchlistAccountService($);
    const manageGlobalThreshold = new ManageGlobalThreshold($, window.top, service);

    manageGlobalThreshold.init();
})