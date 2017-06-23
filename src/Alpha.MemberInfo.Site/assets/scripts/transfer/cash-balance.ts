"use strict";

class CashBalance {
    $: JQueryStatic;
    topWindow: any;

    availableAmountFrom: number;
    availableAmountTo: number;
    oldAmount: number = 0;
    availableRangeTransfer: string;
    confirmTransfer: string;
    transferAmountIs: string;

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
        this.$token = this.$("input[name=\"__RequestVerificationToken\"]", this.$formTransfer);

        this.availableAmountFrom = parseFloat(this.$("#availableAmountFrom").val());
        this.availableAmountTo = parseFloat(this.$("#availableAmountTo").val());
        this.availableRangeTransfer = this.$("#availableRangeTransfer").val();
        this.confirmTransfer = this.$("#confirmTransfer").val();
        this.transferAmountIs = this.$("#transferAmountIs").val();

        this.$amount.focus();
    };

    registerEvents = () => {
        this.$formTransfer.submit(this.doTransfer);
        this.$amount.keydown(this.amountKeyDown)
            .keyup(this.amountKeyUp);
    };

    amountKeyDown = (e: any) => {
        // allow: backspace, delete, tab, escape, enter, - and .
        if (this.$.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190, 231, 189, 109]) !== -1 ||
            // allow: Ctrl/cmd+A
            (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // allow: Ctrl/cmd+C
            (e.keyCode === 67 && (e.ctrlKey === true || e.metaKey === true)) ||
            // allow: Ctrl/cmd+X
            (e.keyCode === 88 && (e.ctrlKey === true || e.metaKey === true)) ||
            // allow: home, end, left, right
            (e.keyCode >= 35 && e.keyCode <= 39)) {
            // let it happen, don't do anything
            return;
        }

        // ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    };

    amountKeyUp = (e: any) => {
        let amountValue: string = this.$amount.val();

        if (amountValue.length === 0 || amountValue === "-") {
            return;
        }

        let amount: number = parseFloat(amountValue.replace(/\,/g, ""));

        if (!isNaN(amount) && amount !== this.oldAmount) {
            this.oldAmount = amount;
            this.$amount.val(this.toString4Digits(amount));
        }
    };

    validateAmount = (amount: number): boolean => {
        return this.availableAmountFrom <= amount && amount <= this.availableAmountTo && amount !== 0;
    };

    showMessage = (code: number, message: string) => {
        this.$msgContent.removeClass().html(message);

        if (code === 0) {
            this.$msgContent.addClass("msg msg-success");
        } else {
            this.$msgContent.addClass("msg msg-error");
        }

        this.$msgContainer.removeClass("hidden");
    };

    hideMessage = () => {
        this.$msgContent.removeClass().html("");
        this.$msgContainer.addClass("hidden");
    };

    countDecimals = (value: number): number => {
        return Math.floor(value) === value ?
            0 :
            value.toString().split(".")[1].length || 0;
    };

    amountEqualTo = (amount: number, comparedValue: number): boolean => {
        if (comparedValue !== 0 && amount !== comparedValue && this.countDecimals(amount) === 2) {
            return comparedValue > 0 ?
                Math.round(amount * 100) === Math.round(comparedValue * 100) :
                Math.ceil(amount * 100) === Math.ceil(comparedValue * 100) + 1;
        }

        return false;
    };

    toString4Digits = (value: number): string => {
        return value.toLocaleString("en-US", { maximumFractionDigits: 4 });
    };

    getValidationObject = (): any => {
        let validationObject: any = {};

        let confirmMessage: string = "";
        let amountValue: string = this.$amount.val();
        let amount: number = amountValue.length === 0 ? 0 : parseFloat(amountValue.replace(/\,/g, ""));

        if (this.amountEqualTo(amount, this.availableAmountFrom)) {
            amount = this.availableAmountFrom;
            confirmMessage = this.transferAmountIs + " " + this.toString4Digits(amount) + "\n";
        } else if (this.amountEqualTo(amount, this.availableAmountTo)) {
            amount = this.availableAmountTo;
            confirmMessage = this.transferAmountIs + " " + this.toString4Digits(amount) + "\n";
        }

        if (confirmMessage.length === 0 && !this.validateAmount(amount)) {
            validationObject.code = 400;
            validationObject.message = this.availableRangeTransfer;
        } else {
            validationObject.code = 0;

            confirmMessage += this.confirmTransfer;
            validationObject.message = confirmMessage;

            validationObject.amount = amount;
        }

        return validationObject;
    };

    afterAddTransferSuccessfully = (data: any) => {
        this.showMessage(data.Code, data.Message);

        if (data.Code === 0) {
            let self = this;

            setTimeout(function () {
                self.topWindow.main.location.reload(true);
                self.topWindow.popupManager.close();
            }, 3000);
        }
    };

    doTransfer = (e: any) => {
        e.preventDefault();

        let self: any = this;
        self.$.blockUI();

        let validationObject: any = self.getValidationObject();

        if (validationObject.code !== 0) {
            this.showMessage(validationObject.code, validationObject.message);
            self.$.unblockUI();
            return;
        }

        this.hideMessage();

        if (!confirm(validationObject.message)) {
            self.$.unblockUI();
            return;
        }

        let postParams: any = {
            __RequestVerificationToken: this.$token.val(),
            custId: self.$custId.val(),
            amount: validationObject.amount
        };

        self.$.post(self.$formTransfer.attr("action"), postParams, self.afterAddTransferSuccessfully, "json")
            .always(function () {
                self.$.unblockUI();
            });
    };
}

$(() => {
    let cashBalance: any = new CashBalance($, window.top);
    cashBalance.init();
});