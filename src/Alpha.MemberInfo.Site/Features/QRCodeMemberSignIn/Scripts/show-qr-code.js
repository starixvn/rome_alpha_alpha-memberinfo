"use strict";
var ShowQRCode = /** @class */ (function () {
    function ShowQRCode($, topWindow) {
        var _this = this;
        this.resizePopup = function () {
            if (top.alpha.isScreenLower768) {
                var modalElement = window.top.document.querySelector('[id^="page-popup-"]');
                if (modalElement) {
                    modalElement.classList.add('modal-account-info');
                }
            }
        };
        this.init = function () {
            _this.username = _this.$('.username-label').html();
            _this.$('#btn-copy-link').click(function () {
                clipboard.writeText(_this.$('#qrcode-link').val());
            });
            _this.$('#btn-update-information').click(function () {
                window.top.popupManager.open(_this.$('#update-infor-link').val(), "Update Infomation - " + _this.username, 700, 400);
                _this.resizePopup();
            });
            _this.$('#btn-update-credit').click(function () {
                window.top.popupManager.open(_this.$('#update-credit-link').val(), "Update Credit - " + _this.username, 500, 300);
                _this.resizePopup();
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