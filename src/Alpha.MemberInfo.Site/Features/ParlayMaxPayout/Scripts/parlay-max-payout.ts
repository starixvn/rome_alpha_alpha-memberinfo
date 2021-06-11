"use strict"

class ParlayMaxPayout {
    private $: JQueryStatic;
    private $btnSubmit: JQuery;

    private alertService: AlertService;
    private parlayMaxPayoutService: ParlayMaxPayoutService;

    private custIdList: string;
    private custNameList: string;
    private updateSuccessMessage: string;
    private invalidMaxPayoutValueText: string;
    private greaterThanMaxValueText: string;

    private reloadTimeout = 1000;

    constructor(
        $: JQueryStatic,
        alertService: AlertService,
        parlayMaxPayoutService: ParlayMaxPayoutService) {
        this.$ = $;
        this.alertService = alertService;
        this.parlayMaxPayoutService = parlayMaxPayoutService;
    }

    public Init(): void {
        this.alertService.Init();

        this.cacheData();
        this.cacheDomElements();
        this.registerEvents();
    }

    private cacheDomElements(): void {
        this.$btnSubmit = this.$("#btn-submit");
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
                    self.alertService.showHint(this, message);

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

        if (el.checked) {
            tbSetting.attr("data-cachedvalue", tbSetting.val());
        }

        tbSetting.val(el.checked ? Constants.Unlimited : tbSetting.attr("data-cachedvalue"));
        tbSetting.prop("disabled", el.checked);
    };

    private validateSettingValue(valueText: string, maxValueText: string): ValidateResult {
        const value = Helper.ToNumber(valueText),
            maxValue = Helper.ToNumber(maxValueText);

        if (valueText === Constants.Unlimited && maxValueText !== Constants.Unlimited
            || valueText !== Constants.Unlimited
            && (isNaN(value) || value <= Constants.ZeroValue || Helper.IsFloatingPointNumber(value))) {
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
        if (this.$.isEmptyObject(data)) {
            return this.alertService.showMessage("Server error(s).", true);
        }

        if (data.Code && data.Code !== StatusCode.Success) {
            return this.alertService.showMessage(data.Message, true);
        }

        if (data.Code === StatusCode.Success) {
            this.alertService.showMessage(this.updateSuccessMessage);
        }

        if (data.length > 0) {
            if (data[0].Code === StatusCode.NotAuthorized) {
                return this.alertService.showMessage(data[0].Message, true);
            }

            const custNameList = this.custNameList.split(/\,/g);

            let hasError = false;
            let messages = "";
            data.forEach((item, index) => {
                let custName = custNameList[index];
                let message = item.Code === StatusCode.Success
                    ? this.updateSuccessMessage
                    : item.Message;
                let className = item.Code === StatusCode.Success
                    ? "message-success"
                    : "message-error";
                if (!hasError && item.Code !== StatusCode.Success) {
                    hasError = true;
                }

                messages +=
                    `<div class="message ${className} message-icon">
                        ${custName}: ${message}
                    </div>`;
            });

            this.alertService.showMessages(messages);
            if (hasError) {
                return;
            }
        }

        this.reloadAfterSuccess();
    };

    private reloadAfterSuccess(): void {
        setTimeout(function () {
            window.location.reload();
        }, this.reloadTimeout);
    }
}

$(function () {
    const parlayMaxPayout = new ParlayMaxPayout(
        $,
        new AlertService($),
        new ParlayMaxPayoutService($));
    parlayMaxPayout.Init();
});