"use strict"

class ParlayMaxPayout {
    private $: JQueryStatic;
    private $btnSubmit: JQuery;
    private $divMessageContainer: JQuery;
    private $divMessageText: JQuery;
    private $divPmpContainer: JQuery;
    private $divHint: JQuery;
    private $divHintText: JQuery;
    private $divHintPointer: JQuery;

    private parlayMaxPayoutService: ParlayMaxPayoutService;

    private custIdList: string;
    private custNameList: string;
    private updateSuccessMessage: string;
    private invalidMaxPayoutValueText: string;
    private greaterThanMaxValueText: string;

    private hideHintTimeout = 3000;
    private reloadTimeout = 1000;

    constructor(
        $: JQueryStatic,
        parlayMaxPayoutService: ParlayMaxPayoutService) {
        this.$ = $;
        this.parlayMaxPayoutService = parlayMaxPayoutService;
    }

    public Init(): void {
        this.cacheData();
        this.cacheDomElements();
        this.registerEvents();
    }

    private cacheDomElements(): void {
        this.$btnSubmit = this.$("#btn-submit");
        this.$divMessageContainer = this.$("#message-container");
        this.$divMessageText = this.$("#message-text");
        this.$divPmpContainer = this.$("#pmp-container");
        this.$divHint = this.$("#hint");
        this.$divHintText = this.$("#hint-text");
        this.$divHintPointer = this.$("#hint-pointer");
    }

    private cacheData(): void {
        this.custIdList = this.$("#tb-custidlist").val();
        this.custNameList = this.$("#tb-custnamelist").val();
        this.updateSuccessMessage = this.$("#tb-updatesuccess-msg").val();
        this.invalidMaxPayoutValueText = this.$("#tb-invalidvalue-msg").val();
        this.greaterThanMaxValueText = this.$("#tb-gtmaxvalue-msg").val();
    }

    private registerEvents(): void {
        this.$btnSubmit.off("click").on("click", this.onSubmit);
        this.$(".cb-setting").off("change").on("change", this.changeSettingValue);
        this.$(".tb-setting").off("keyup").on("keyup", this.formatSettingValue);
    }

    private onSubmit = (): void => {
        const self = this;

        let isValid = true;
        let maxPayoutLimit = "<ROOT>";
        self.$(".sport-name").each(function () {
            let $this = self.$(this),
                betTypeId = $this.data("bettypeid"),
                betTypeName = $this.data("bettypename"),
                sportTypeId = $this.data("sporttypeid");

            let item = `<MaxPayoutLimit SportType="${sportTypeId}" BetType="${betTypeId}"`;
            self.$(`.tb-setting[data-sporttypeid="${sportTypeId}"]`).each(function () {
                let $el = self.$(this),
                    columnName = $el.data("columnname"),
                    maxValue = `${$el.data("maxvalue")}`,
                    value = `${$el.val()}`;

                let result = self.validateSettingValue(value, maxValue);
                if (result !== ValidateResult.ValidValue) {
                    isValid = false;

                    let message = result === ValidateResult.InvalidValue
                        ? self.invalidMaxPayoutValueText
                        : self.greaterThanMaxValueText
                            .replace("<BetType>", betTypeName)
                            .replace("<Column>", columnName);
                    self.showHint(this, message);

                    return false;
                }

                item += ` ${$el.data("settingname")}="${Helper.ToNumberFormat(value)}"`;
            });

            maxPayoutLimit += `${item} />`;

            return isValid;
        });

        maxPayoutLimit += "</ROOT>";

        if (isValid) {
            let params = JSON.stringify({
                CustIdList: this.custIdList,
                MaxPayoutLimit: maxPayoutLimit
            });

            this.parlayMaxPayoutService.UpdateParlayMaxPayout(params, this.onUpdateSuccess);
        }
    };

    private changeSettingValue = (event): void => {
        const el = event.currentTarget,
            tbSettingId = this.$(el).data("tbsettingid"),
            tbSetting = this.$(`#${tbSettingId}`);

        tbSetting.val(el.checked ? Constants.Unlimited : Constants.ZeroValue);
        tbSetting.prop("disabled", el.checked);
    };

    private validateSettingValue(valueText: string, maxValueText: string): ValidateResult {
        const value = Helper.ToNumber(valueText),
            maxValue = Helper.ToNumber(maxValueText);

        if (valueText === Constants.Unlimited && maxValueText !== Constants.Unlimited
            || valueText !== Constants.Unlimited
            && (isNaN(value) || value < Constants.ZeroValue || Helper.IsFloatingPointNumber(value))) {
            return ValidateResult.InvalidValue;
        }

        if (maxValueText !== Constants.Unlimited && value > maxValue) {
            return ValidateResult.GreaterThanMaxValue;
        }

        return ValidateResult.ValidValue;
    }

    private formatSettingValue = (event): void => {
        const el = event.currentTarget;
        el.value = Helper.ToMoneyFormat(el.value);
    };

    private onUpdateSuccess = (data): void => {
        if (data && data.Code === StatusCode.Success) {
            this.showMessage(this.updateSuccessMessage);
        }

        if (data && data.length > 0) {
            const custNameList = this.custNameList.split(/\,/g);

            let messages = "";
            data.forEach((item, index) => {
                let custName = custNameList[index];
                let message = item.Code === StatusCode.Success
                    ? this.updateSuccessMessage
                    : item.Message;
                let className = item.Code === StatusCode.Success
                    ? "message-success"
                    : "message-error";

                messages +=
                    `<div class="message ${className} message-icon">
                        ${custName}: ${message}
                    </div>`;
            });

            this.showMessages(messages);
        }

        if (!data || data.length < 1) {
            return this.showMessage("Server error(s).", true);
        }

        if (data.Code && data.Code !== StatusCode.Success) {
            return this.showMessage(data.Message, true);
        }

        this.reloadAfterSuccess();
    };

    private showHint(el: HTMLElement, message: string): void {
        this.$divHintText.text(message);
        this.$divHint.css("display", "block");

        this.adjustHintPosition(el);
        this.setHideHint();
    }

    private adjustHintPosition(el: HTMLElement): void {
        const hintHeight = 30,
            hintPointerHeight = 12,
            inputWidth = 120,
            hintWidth = this.$divHint.width(),
            elOffset = this.$(el).offset(),
            containerOffset = this.$divPmpContainer.offset(),
            containerWidth = this.$divPmpContainer.width(),
            top = elOffset.top - containerOffset.top - hintHeight - hintPointerHeight;

        if (elOffset.left + hintWidth > containerWidth) {
            const right = containerOffset.left + containerWidth - elOffset.left - inputWidth;
            this.$divHint.css({ top, right, left: "unset" });
            this.$divHintPointer.css({ "margin-left": "auto", "margin-right": "unset" });
        } else {
            this.$divHint.css({ top, right: "unset", left: elOffset.left });
            this.$divHintPointer.css({ "margin-left": "unset", "margin-right": "auto" });
        }
    }

    private setHideHint(): void {
        setTimeout(() => {
            this.$divHint.css("display", "none");
        }, this.hideHintTimeout);
    }

    private showMessage(message: string, isError: boolean = false): void {
        this.$divMessageText.text(message);
        this.$divMessageText.addClass(isError ? "message-error" : "message-success");
        this.$divMessageContainer.css("display", "block");
    }

    private showMessages(messages: string) {
        this.$divMessageContainer.empty();
        this.$divMessageContainer.html(messages);
        this.$divMessageContainer.css("display", "block");
    }

    private reloadAfterSuccess(): void {
        setTimeout(function () {
            window.location.reload();
        }, this.reloadTimeout);
    }
}

$(function () {
    const parlayMaxPayout = new ParlayMaxPayout(
        $,
        new ParlayMaxPayoutService($));
    parlayMaxPayout.Init();
});