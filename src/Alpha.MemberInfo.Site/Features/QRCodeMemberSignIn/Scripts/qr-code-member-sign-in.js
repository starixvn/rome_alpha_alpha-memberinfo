"use strict";
var QRCodeMemberSignIn = /** @class */ (function () {
    function QRCodeMemberSignIn($, topWindow) {
        var _this = this;
        this.init = function () {
            var options = {
                url: function (phrase) {
                    return 'QRCodeMemberSignIn/GetUsernamesSuggestion?SuggestionQuery=' + phrase;
                },
                getValue: 'UserName',
                requestDelay: 300,
                minCharNumber: 2,
                categories: [{
                        maxNumberOfElements: 10
                    }],
                list: {
                    onChooseEvent: function () {
                        _this.$.blockUI();
                        var chooseUsername = _this.$('#txt-username').val();
                        _this.username = chooseUsername;
                        var param = {
                            SuggestionQuery: chooseUsername
                        };
                        _this.$.ajax({
                            method: "POST",
                            data: param,
                            url: "QRCodeMemberSignIn/GetMemberCredit",
                            cache: false,
                            success: function (result) {
                                _this.$.unblockUI();
                                _this.$("#txt-member-credit").val(result);
                                _this.credit = parseFloat(result);
                            }
                        });
                    }
                }
            };
            _this.$('#txt-username').easyAutocomplete(options);
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
                            _this.firstname = _this.$('#txt-first-name').val();
                            _this.$("#first-name-label").html(_this.firstname);
                            _this.$("#credit-label").html("" + _this.credit);
                            _this.$("#username-label").html(_this.username);
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