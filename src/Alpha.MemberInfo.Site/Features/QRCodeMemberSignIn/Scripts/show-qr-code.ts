"use strict";

declare var clipboard: any;

class ShowQRCode {
    $: JQueryStatic;

    constructor($: JQueryStatic) {
        this.$ = $;
    }

    init = () => {

        this.$('#btn-copy-link').click(() => {
            clipboard.writeText(this.$('#qrcode-link').val());
        });
    };
}

const showQRCode = new ShowQRCode($);

$(() => {
    showQRCode.init();
});
