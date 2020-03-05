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
                    this.$.unblockUI();
                    if (result.ErrCode === 0) {
                        this.$("#create-form").addClass("hidden");
                        this.$("#create-account-successfully").removeClass("hidden");

                        const obj = JSON.parse(result.ErrMsg);
                        this.$("#qr-code-image").attr("src", obj.QRCode);
                    }
                    else {
                        // Show error message
                    }
                }
            });
        });
    };
}

const qrCodeMemberSignIn = new QRCodeMemberSignIn($, window.top);

$(() => {
    qrCodeMemberSignIn.init();
});
