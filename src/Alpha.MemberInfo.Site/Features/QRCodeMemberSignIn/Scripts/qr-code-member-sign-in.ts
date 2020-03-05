"use strict";

class QRCodeMemberSignIn {
    $: JQueryStatic;
    topWindow: any;

    constructor(
        $: JQueryStatic,
        topWindow: any) {
        this.$ = $;
        this.topWindow = topWindow;
    }

    init = () => {
        this.$("#btn-submit").click(() => {
            this.$.blockUI();

            var param = {
            };

            this.$.ajax({
                method: "POST",
                data: param,
                url: "QRCodeMemberSignIn/CreateAccount",
                cache: false,
                success: (result) => {

                }
            });
        });
    };
}

const qrCodeMemberSignIn = new QRCodeMemberSignIn($, window.top);

$(() => {
    qrCodeMemberSignIn.init();
});
