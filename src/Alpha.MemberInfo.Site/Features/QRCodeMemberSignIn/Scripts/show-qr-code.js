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
        this.givenPopupOnclosing = function (username, updateSuccessfully, credit) {
            if (username === void 0) { username = null; }
            if (updateSuccessfully === void 0) { updateSuccessfully = false; }
            if (credit === void 0) { credit = 0; }
            if (updateSuccessfully === true) {
                var creditFormat = numeral(credit);
                _this.$('#credit-label').text(creditFormat.format('0,0.##'));
                setTimeout(function () {
                    window.top.updateGivenCreditPopup.close();
                }, 1000);
            }
            else {
                window.top.updateGivenCreditPopup.close();
            }
        };
        this.informationPopupOnclosing = function (updateSuccessfully, firstName) {
            if (updateSuccessfully === void 0) { updateSuccessfully = false; }
            if (firstName === void 0) { firstName = ''; }
            if (updateSuccessfully === true) {
                _this.$('#first-name-label').text(firstName);
                setTimeout(function () {
                    window.top.updateInformationPopup.close();
                }, 1000);
            }
            else {
                window.top.updateInformationPopup.close();
            }
        };
        this.init = function () {
            _this.username = _this.$('.username-label').html();
            _this.$('#btn-copy-link').click(function () {
                clipboard.writeText(_this.$('#qrcode-link').val());
            });
            _this.$('#domains').change(function () {
                _this.$.blockUI();
                var chooseUsername = _this.$('#UsernameCopyFrom').val();
                _this.username = chooseUsername;
                var domain = _this.$('#domains').val();
                var param = {
                    SearchAccountName: _this.$('.username-label').text(),
                    Domain: domain
                };
                _this.$.ajax({
                    method: "POST",
                    data: param,
                    url: "GenerateQrCode",
                    cache: false,
                    success: function (result) {
                        _this.$('#qr-code-image').attr('src', result);
                        _this.$('#qrcode-link').val(domain);
                        _this.$.unblockUI();
                    }
                });
            });
            _this.$('#btn-update-information').click(function () {
                if (window.top.updateInformationPopup === null || window.top.updateInformationPopup === undefined) {
                    window.top.updateInformationPopup = window.top.ageWnd.createInstance();
                }
                window.top.updateInformationPopup.onClosing = _this.informationPopupOnclosing;
                window.top.popupManager.openWithHanlder(top.updateInformationPopup, _this.$('#update-infor-link').val(), "Update Infomation - " + _this.username, 700, 400);
                _this.resizePopup();
            });
            _this.$('#btn-update-credit').click(function () {
                if (window.top.updateGivenCreditPopup === null || window.top.updateGivenCreditPopup === undefined) {
                    window.top.updateGivenCreditPopup = window.top.ageWnd.createInstance();
                }
                window.top.updateGivenCreditPopup.onClosing = _this.givenPopupOnclosing;
                window.top.popupManager.openWithHanlder(top.updateGivenCreditPopup, _this.$('#update-credit-link').val(), "Update Credit - " + _this.username, 500, 300);
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