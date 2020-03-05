"use strict";
var QRCodeMemberSignIn = /** @class */ (function () {
    function QRCodeMemberSignIn($, topWindow) {
        var _this = this;
        this.init = function () {
            _this.$("#btn-submit").click(function () {
                _this.$.blockUI();
                var param = {};
                _this.$.ajax({
                    method: "POST",
                    data: param,
                    url: "QRCodeMemberSignIn/CreateAccount",
                    cache: false,
                    success: function (result) {
                        _this.$.unblockUI();
                        if (result.ErrCode === 0) {
                            _this.$("#create-form").addClass("hidden");
                            _this.$("#create-account-successfully").removeClass("hidden");
                            var obj = JSON.parse(result.ErrMsg);
                            _this.$("#qr-code-image").attr("src", obj.QRCode);
                        }
                        else {
                            // Show error message
                        }
                    }
                });
            });
        };
        this.$ = $;
        this.topWindow = topWindow;
    }
    return QRCodeMemberSignIn;
}());
var qrCodeMemberSignIn = new QRCodeMemberSignIn($, window.top);
$(function () {
    qrCodeMemberSignIn.init();
});
//# sourceMappingURL=qr-code-member-sign-in.js.map