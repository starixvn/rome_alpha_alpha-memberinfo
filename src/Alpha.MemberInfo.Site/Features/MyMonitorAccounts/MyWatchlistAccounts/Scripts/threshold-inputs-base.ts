"use strict";

abstract class ThresholdInputsBase {
    protected $: JQueryStatic;
    protected alert: MyWatchlistAccountsAlert;
    protected $ckbWinningUnlimited: JQuery;
    protected $txtWinningThreshold: JQuery;
    protected $ckbLosingUnlimited: JQuery;
    protected $txtLosingThreshold: JQuery;
    protected $oldWinningThreshold: JQuery;
    protected $oldLosingThreshold: JQuery;

    constructor($: JQueryStatic) {
        this.$ = $;
        this.cacheDom();
    }

    private cacheDom = () => {
        this.$ckbWinningUnlimited = this.$('input[name="winningUnlimited"]');
        this.$txtWinningThreshold = this.$('input[name="winningThreshold"]');
        this.$ckbLosingUnlimited = this.$('input[name="losingUnlimited"]');
        this.$txtLosingThreshold = this.$('input[name="losingThreshold"]');
        this.$oldWinningThreshold = this.$("#old-winning-threshold");
        this.$oldLosingThreshold = this.$("#old-losing-threshold");
        this.alert = new MyWatchlistAccountsAlert(this.$);
    }

    private handleInputThresholdChange = event => {
        const el = event.currentTarget;
        el.value = WatchlistHelper.ToMoneyFormat(el.value);
    }

    protected bindingThresholdTxtInput = () => {
        this.$txtWinningThreshold.off().on("keyup", this.handleInputThresholdChange);
        this.$txtLosingThreshold.off().on("keyup", this.handleInputThresholdChange);
    }

    protected bindingThresholdCheckboxs = () => {
        this.$ckbWinningUnlimited.off().on("change", () =>
            this.$txtWinningThreshold.prop("disabled", this.$ckbWinningUnlimited.is(":checked")));
        this.$ckbLosingUnlimited.off().on("change", () =>
            this.$txtLosingThreshold.prop("disabled", this.$ckbLosingUnlimited.is(":checked")));
    }

    protected validateThresholdInput = () => {
        if (!((this.$ckbWinningUnlimited.is(":checked") || this.$txtWinningThreshold.val() !== "")
            && (this.$ckbLosingUnlimited.is(":checked") || this.$txtLosingThreshold.val() !== ""))) {
            this.alert.showError("Must enter threshold or check on Unlimited");
            return false;
        }

        return true;
    }

    protected validateEditThresholdNoChangeUpdate = () => {
        const oldWinningThreshold = this.$oldWinningThreshold.val() !== "" ? WatchlistHelper.ToNumber(this.$oldWinningThreshold.val()) : null;
        const oldLosingThreshold = this.$oldLosingThreshold.val() !== "" ? WatchlistHelper.ToNumber(this.$oldLosingThreshold.val()) : null;

        const thresholdInputs = this.getThresholdInputs();
        if (oldWinningThreshold === thresholdInputs.winningThreshold && oldLosingThreshold === thresholdInputs.losingThreshold) {
            this.alert.showError("There is no change to update.");
            return false;
        }

        return true;
    }

    protected getThresholdInputs = () => ({
        winningThreshold: !this.$ckbWinningUnlimited.is(":checked") ? WatchlistHelper.ToNumber(this.$txtWinningThreshold.val()) : null,
        losingThreshold: !this.$ckbLosingUnlimited.is(":checked") ? WatchlistHelper.ToNumber(this.$txtLosingThreshold.val()) : null
    });
}