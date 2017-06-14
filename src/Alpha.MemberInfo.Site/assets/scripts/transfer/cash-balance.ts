"use strict";

class CashBalance {
    $: JQueryStatic;
    topWindow: any;

    availableAmountFrom: number;
    availableAmountTo: number;
    note: string;
    oldAmount: number = 0;
    transferNote: string;
    confirmTransfer: string;

    $msgContainer: JQuery;
    $msgContent: JQuery;
    $formTransfer: JQuery;
    $amount: JQuery;
    $custId: JQuery;
    $token: JQuery;

    constructor($: JQueryStatic, topWindow: any) {
        this.$ = $;
        this.topWindow = topWindow;
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

        this.availableAmountFrom = parseFloat(this.$("#availableAmountFrom").val());
        this.availableAmountTo = parseFloat(this.$("#availableAmountTo").val());
        this.transferNote = this.$("#transferNote").val();
        this.confirmTransfer = this.$("#confirmTransfer").val();
    };

    registerEvents = () => {
        this.$formTransfer.submit(this.doTransfer);
        this.$amount.keydown(this.amountKeyDown)
            .keyup(this.amountKeyUp);
    };

    amountKeyDown = (e) => {
        // Allow: backspace, delete, tab, escape, enter, - and .
        if (this.$.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190, 189, 109]) !== -1 ||
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

        if (amountValue.length === 0 || amountValue === "-") {
            return;
        }

        let amount = parseFloat(amountValue.replace(/\,/g, ""));

        if (!isNaN(amount) && amount !== this.oldAmount) {
            this.oldAmount = amount;
            this.$amount.val(this.toString4Digits(amount));
        }
    };

    validateAmount = (amount): boolean => {
        return this.availableAmountFrom <= amount && amount <= this.availableAmountTo;
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

    toFloat2Digits = (value): number => {
        if (value >= 0) {
            return Math.round(value * 100) / 100;
        }

        return (Math.ceil(value * 100) / 100) + 0.01;
    };

    toString4Digits = (value): string => {
        return value.toLocaleString("en-US", { maximumFractionDigits: 4 })
    };

    doTransfer = (e) => {
        e.preventDefault();

        let self = this;
        self.$.blockUI();

        let confirmMessage = "";
        let amountValue = self.$amount.val();
        let amount = amountValue.length === 0 ? 0 : parseFloat(amountValue.replace(/\,/g, ""));

        if (self.availableAmountFrom !== 0 && amount === self.toFloat2Digits(self.availableAmountFrom)) {
            amount = self.availableAmountFrom;
            confirmMessage = "Transfer amount is " + self.toString4Digits(amount) + "\n";
        }
        else if (self.availableAmountTo !== 0 && amount === self.toFloat2Digits(self.availableAmountTo)) {
            amount = self.availableAmountTo;
            confirmMessage = "Transfer amount is " + self.toString4Digits(amount) + "\n";
        }

        if (confirmMessage.length === 0 && !self.validateAmount(amount)) {
            this.showMessage(403, this.transferNote);
            self.$.unblockUI();
            return;
        }

        this.hideMessage();
        confirmMessage += self.confirmTransfer;

        if (!confirm(confirmMessage)) {
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

            if (data.Code === 0) {
                setTimeout(function () { location.reload(); }, 3000);
            }
        };

        self.$.post(self.$formTransfer.attr("action"), postParams, successFunction, "json")
            .always(function () {
                self.$.unblockUI();
            });
    };
}

$(() => {
    let cashBalance = new CashBalance($, window.top);
    cashBalance.init();
});