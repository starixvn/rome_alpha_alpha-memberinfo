"use strict";
var ShowQRCode = /** @class */ (function () {
    function ShowQRCode($, topWindow) {
        var _this = this;
        this.init = function () {
            _this.$('#btn-copy-link').click(function () {
                clipboard.writeText(_this.$('#qrcode-link').val());
            });
            _this.$('#btn-update-information').click(function () {
                window.top.popupManager.open(_this.$('#update-infor-link').val(), 'Update Infomation', 800, 500);
            });
            _this.$('#btn-update-credit').click(function () {
                window.top.popupManager.open(_this.$('#update-credit-link').val(), 'Update Credit Username', 800, 500);
            });
        };
        this.$ = $;
        this.topWindow = topWindow;
    }
    return ShowQRCode;
}());
var showQRCode = new ShowQRCode($, window.top);
$(function () {
    showQRCode.init();
});
//# sourceMappingURL=show-qr-code.js.map