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