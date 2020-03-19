"use strict";

declare var clipboard: any;

interface Window {
    popupManager: {
        open: Function
    },
    alpha: any
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

    init = () => {
        this.username = this.$('.username-label').html();

        this.$('#btn-copy-link').click(() => {
            clipboard.writeText(this.$('#qrcode-link').val());
        });

        this.$('#btn-update-information').click(() => {
            window.top.popupManager.open(this.$('#update-infor-link').val(), `Update Infomation - ${this.username}`, 700, 400);
            this.resizePopup();
        });

        this.$('#btn-update-credit').click(() => {
            window.top.popupManager.open(this.$('#update-credit-link').val(), `Update Credit - ${this.username}`, 500, 300);
            this.resizePopup();
        });
    };
}

const showQRCode = new ShowQRCode($, window.top);

$(() => {
    showQRCode.init();
});
