/// <reference path="../../../assets/scripts/shared/top-windows-interface.ts" />
"use strict";

declare var clipboard: any;
declare var numeral: any;

interface Window {
    alpha: any,
    updateGivenCreditPopup: any,
    updateInformationPopup: any,
    ageWnd: any
}

class ShowQRCode {
    $: JQueryStatic;
    topWindow: any;
    username: string;

    constructor(
        $: JQueryStatic,
        topWindow: any) {
        this.$ = $;
        this.topWindow = topWindow;
   }

    resizePopup = () => {
        if (top.alpha.isScreenLower768) {
            const modalElement = window.top.document.querySelector('[id^="page-popup-"]');
            if (modalElement) {
                modalElement.classList.add('modal-account-info');
            }
        }
    };

    givenPopupOnclosing = (username = null, updateSuccessfully = false, credit = 0) => {
        if (updateSuccessfully === true) {
            const creditFormat = numeral(credit);
            this.$('#credit-label').text(creditFormat.format('0,0.##'));
            setTimeout(() => {
                window.top.updateGivenCreditPopup.close();
            }, 1000);
        }
        else {
            window.top.updateGivenCreditPopup.close();
        }
    };

    informationPopupOnclosing = (updateSuccessfully = false, firstName = '') => {
        if (updateSuccessfully === true) {
            this.$('#first-name-label').text(firstName);
            setTimeout(() => {
                window.top.updateInformationPopup.close();
            }, 1000);
        }
        else {
            window.top.updateInformationPopup.close();
        }
    };

    init = () => {
        if (JSON.parse(this.$('#CreateMode').val().toLowerCase())) {
            this.$.ajax({
                method: "GET",
                url: this.$('#CorrectDataUrl').val(),
                cache: false,
                success: () => { }
            });
        }

        const memberNameCopyFrom = this.$('#MemberNameCopyFrom').val();
        localStorage.setItem("memberNameCopyFrom", memberNameCopyFrom);

        this.username = this.$('.username-label').html();

        this.$('#btn-copy-link').click(() => {
            clipboard.writeText(this.$('#QRCodeLink').val());
        });

        this.$('#domains').change(() => {
            this.$.blockUI();

            var chooseUsername = this.$('#UsernameCopyFrom').val();
            this.username = chooseUsername;

            const params = {
                SearchAccountName: this.$('.username-label').text(),
                Domain: this.$('#domains').val()
            };

            this.$.ajax({
                method: "POST",
                data: params,
                url: "GenerateQrCode",
                cache: false,
                success: (result) => {
                    this.$('#qr-code-image').attr('src', result);
                    this.$('#QRCodeLink').val(params.Domain);
                    this.$.unblockUI();
                }
            });
        });

        this.$('#btn-update-information').click(() => {
            if (window.top.updateInformationPopup === null || window.top.updateInformationPopup === undefined) {
                window.top.updateInformationPopup = window.top.ageWnd.createInstance();
            }

            window.top.updateInformationPopup.onClosing = this.informationPopupOnclosing;
            window.top.popupManager.openWithHanlder(
                top.updateInformationPopup,
                this.$('#UpdateInformationUrl').val(),
                `${this.$('#update-infor-text').val()} - ${this.username}`,
                700,
                400);

            this.resizePopup();
        });

        this.$('#btn-update-credit').click(() => {
            if (window.top.updateGivenCreditPopup === null || window.top.updateGivenCreditPopup === undefined) {
                window.top.updateGivenCreditPopup = window.top.ageWnd.createInstance();
            }

            window.top.updateGivenCreditPopup.onClosing = this.givenPopupOnclosing;
            window.top.popupManager.openWithHanlder(
                top.updateGivenCreditPopup,
                this.$('#UpdateCreditUrl').val(),
                `${this.$('#update-credit-text').val()} - ${this.username}`,
                500,
                300);

            this.resizePopup();
        });
    };
}

const showQRCode = new ShowQRCode($, window.top);

$(() => {
    showQRCode.init();
});
