"use strict"

class ParlayMaxPayout {
    private $: JQueryStatic;
    private $btnSubmit: JQuery;
    private $divMessageContainer: JQuery;
    private $divMessageText: JQuery;

    private parlayMaxPayoutService: ParlayMaxPayoutService;

    private custIdList: string;
    private custNameList: string;
    private updateSuccessMessage: string;

    constructor(
        $: JQueryStatic,
        parlayMaxPayoutService: ParlayMaxPayoutService) {
        this.$ = $;
        this.parlayMaxPayoutService = parlayMaxPayoutService;
    }

    public Init(): void {
        this.cacheDomElements();
        this.cacheData();
        this.registerEvents();
    }

    private cacheDomElements(): void {
        this.$btnSubmit = this.$("#btn-submit");
        this.$divMessageContainer = this.$("#message-container");
        this.$divMessageText = this.$("#message-text");
    }

    private cacheData(): void {
        this.custIdList = this.$("#tb-custidlist").val();
        this.custNameList = this.$("#tb-custnamelist").val();
        this.updateSuccessMessage = this.$("#tb-updatesuccess-msg").val();
    }

    private registerEvents(): void {
        this.$btnSubmit.off("click").on("click", this.onSubmit);
        this.$(".cb-setting").off("change").on("change", this.onChangeUnlimitted);
    }

    private onSubmit = (): void => {
        const self = this;

        let maxPayoutLimit = "<ROOT>";
        self.$(".sport-name").each(function () {
            let $this = self.$(this),
                betTypeId = $this.data("bettypeid"),
                sportTypeId = $this.data("sporttypeid");

            let item = `<MaxPayoutLimit SportType="${sportTypeId}" BetType="${betTypeId}"`;
            self.$(`.tb-setting[data-sporttypeid="${sportTypeId}"]`).each(function () {
                item += ` ${self.$(this).data("settingname")}="${Helper.ToNumber(this.value)}"`;
            });

            maxPayoutLimit += `${item} />`;
        });

        maxPayoutLimit += "</ROOT>";

        let params = JSON.stringify({
            CustIdList: this.custIdList,
            MaxPayoutLimit: maxPayoutLimit
        });

        this.parlayMaxPayoutService.UpdateParlayMaxPayout(params, this.onUpdateSuccess);
    };

    private onChangeUnlimitted = (event): void => {
        const el = event.currentTarget,
            tbSettingId = this.$(el).data("tbsettingid"),
            tbSetting = this.$(`#${tbSettingId}`);

        tbSetting.val(el.checked ? Constants.Unlimited : tbSetting.data("currentvalue"));
        tbSetting.prop("disabled", el.checked);
    };

    private onUpdateSuccess = (data): void => {
        if (data && data.Code === 0) {
            this.showMessage(this.updateSuccessMessage);
        }

        if (data && data.length > 0) {
            const custNameList = this.custNameList.split(/\,/gi);

            let messages = "";
            data.forEach((item, index) => {
                let custName = custNameList[index];
                let message = item.Code === 0
                    ? this.updateSuccessMessage
                    : item.Message;
                let className = item.Code === 0
                    ? "message-success"
                    : "message-error";

                messages +=
                    `<div class="message ${className} message-icon">
                        ${custName}: ${message}
                    </div>`;
            });

            this.showMessages(messages);
        }

        this.reloadAfterSuccess();
    };

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
        }, 3000);
    }
}

$(function () {
    const parlayMaxPayout = new ParlayMaxPayout(
        $,
        new ParlayMaxPayoutService($));
    parlayMaxPayout.Init();
});