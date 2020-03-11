"use strict";
var QRCodeMemberSignIn = /** @class */ (function () {
    function QRCodeMemberSignIn($, topWindow) {
        var _this = this;
        this.getMemberCredit = function () {
            _this.$.blockUI();
            var chooseUsername = _this.$('#Username').val();
            _this.username = chooseUsername;
            var param = {
                SearchAccountName: chooseUsername
            };
            _this.$.ajax({
                method: "POST",
                data: param,
                url: "QRCodeMemberSignIn/GetMemberCredit",
                cache: false,
                success: function (result) {
                    _this.$.unblockUI();
                    _this.$('#credit-range').removeClass('hidden');
                    _this.$("#credit-min-label").html(result.CreditMin);
                    _this.$("#credit-max-label").html(result.CreditMax);
                    _this.credit = parseFloat(result.Credit);
                }
            });
        };
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
                    onChooseEvent: function () { return _this.getMemberCredit(); }
                }
            };
            _this.$('#Username').easyAutocomplete(options);
            var memberName = _this.$('#Username').val();
            if (memberName !== '' && memberName !== undefined) {
                _this.getMemberCredit();
            }
            _this.$('#btn-copy-link').click(function () {
                clipboard.writeText(_this.memberLink);
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