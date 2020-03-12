"use strict";
var ShowQRCode = /** @class */ (function () {
    function ShowQRCode($) {
        var _this = this;
        this.init = function () {
            _this.$('#btn-copy-link').click(function () {
                clipboard.writeText(_this.$('#qrcode-link').val());
            });
        };
        this.$ = $;
    }
    return ShowQRCode;
}());
var showQRCode = new ShowQRCode($);
$(function () {
    showQRCode.init();
});
//# sourceMappingURL=show-qr-code.js.map