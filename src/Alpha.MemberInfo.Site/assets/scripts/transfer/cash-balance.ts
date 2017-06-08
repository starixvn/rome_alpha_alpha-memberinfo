"use strict";

class CashBalance {
    $: JQueryStatic;
    topWindow: any;

    availableAmountFrom: number;
    availableAmountTo: number;
    oldAmount: number = 0;

    $msgContainer: JQuery;
    $msgContent: JQuery;
    $formTransfer: JQuery;
    $amount: JQuery;
    $custId: JQuery;
    $token: JQuery;

    constructor(
        $: JQueryStatic,
        topWindow: any,
        amountFrom: number,
        amountTo: number) {
        this.$ = $;
        this.topWindow = topWindow;
        this.availableAmountFrom = amountFrom;
        this.availableAmountTo = amountTo;
    }

    init = () => {
        this.cacheDOMElements();
        this.registerEvents();
    };

    cacheDOMElements = () => {
        this.$msgContainer = this.$("#status-message");
        this.$msgContent = this.$("#status-message-content");
        this.$formTransfer = this.$("#form-transfer");
        this.$amount = this.$("#amount");
        this.$custId = this.$("#custId");
        this.$token = this.$('input[name="__RequestVerificationToken"]', this.$formTransfer)
    };

    registerEvents = () => {
        this.$formTransfer.submit(this.doTransfer);
        this.$amount.keydown(this.amountKeyDown)
            .keyup(this.amountKeyUp);
    };

    amountKeyDown = (e) => {
        // Allow: backspace, delete, tab, escape, enter and .
        if (this.$.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            // Allow: Ctrl/cmd+A
            (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: Ctrl/cmd+C
            (e.keyCode == 67 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: Ctrl/cmd+X
            (e.keyCode == 88 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right
            (e.keyCode >= 35 && e.keyCode <= 39)) {
            // let it happen, don't do anything
            return;
        }

        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    };

    amountKeyUp = (e) => {
        let amountValue = this.$amount.val();

        if (amountValue.length === 0) {
            return;
        }

        let amount = parseFloat(amountValue.replace(/\,/g, ""));

        if (amount !== this.oldAmount) {
            this.oldAmount = amount;
            this.$amount.val(amount.toLocaleString());
        }
    };

    validateAmountAndShowErrorMessage = (amount): boolean => {
        let isValidAmount = amount !== 0 &&
            this.availableAmountFrom <= amount && amount <= this.availableAmountTo;

        if (isValidAmount) {
            this.hideMessage();
        }
        else {
            this.showMessage(500, "You only allow to transfer from " +
                this.availableAmountFrom.toFixed(2).toLocaleString() +
                " to " +
                this.availableAmountTo.toFixed(2).toLocaleString());
        }

        return isValidAmount;
    };

    showMessage = (code, message) => {
        this.$msgContent.removeClass().html(message);

        if (code === 0) {
            this.$msgContent.addClass("msg msg-success");
        }
        else {
            this.$msgContent.addClass("msg msg-error");
        }

        this.$msgContainer.removeClass("hidden");
    };

    hideMessage = () => {
        this.$msgContent.removeClass().html("");
        this.$msgContainer.addClass("hidden");
    };

    doTransfer = (e) => {
        e.preventDefault();

        let self = this;
        self.$.blockUI();
        let amountValue = self.$amount.val();
        let amount = amountValue.length === 0 ? 0 : parseFloat(amountValue.replace(/\,/g, ""));
        
        if (!self.validateAmountAndShowErrorMessage(amount)) {
            self.$.unblockUI();
            return;
        }

        let postParams = {
            __RequestVerificationToken: this.$token.val(),
            custId: self.$custId.val(),
            amount: amount
        };

        let successFunction = function (data) {
            self.showMessage(data.Code, data.Message);
            setTimeout(function () { location.reload(); }, 3000);
        };

        self.$.post(self.$formTransfer.attr("action"), postParams, successFunction, "json")
            .always(function () {
                self.$.unblockUI();
            });
    };
}

$(() => {
    let amountFrom = parseFloat($("#availableAmountFrom").val());
    let amountTo = parseFloat($("#availableAmountTo").val());
    let cashBalance = new CashBalance($, window.top, amountFrom, amountTo);
    cashBalance.init();
});