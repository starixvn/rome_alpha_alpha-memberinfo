"use strict";

declare var clipboard: any;
interface Window {
    popupManager: {
        open: Function
    }
}

class ShowQRCode {
    $: JQueryStatic;
    topWindow: any;

    constructor(
        $: JQueryStatic,
        topWindow: any) {
        this.$ = $;
        this.topWindow = topWindow;
    }

    init = () => {
        this.$('#btn-copy-link').click(() => {
            clipboard.writeText(this.$('#qrcode-link').val());
        });

        this.$('#btn-update-information').click(() => {
            window.top.popupManager.open(this.$('#update-infor-link').val(), 'Update Infomation', 800, 500);
        });

        this.$('#btn-update-credit').click(() => {
            window.top.popupManager.open(this.$('#update-credit-link').val(), 'Update Credit Username', 800, 500);
        });
    };
}

const showQRCode = new ShowQRCode($, window.top);

$(() => {
    showQRCode.init();
});
