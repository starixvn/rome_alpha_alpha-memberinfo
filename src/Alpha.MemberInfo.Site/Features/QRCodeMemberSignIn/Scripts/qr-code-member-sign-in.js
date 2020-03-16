"use strict";
var QRCodeMemberSignIn = /** @class */ (function () {
    function QRCodeMemberSignIn($, topWindow) {
        var _this = this;
        this.getMemberCredit = function () {
            _this.$.blockUI();
            var chooseUsername = _this.$('#UsernameCopyFrom').val();
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
                    _this.$("#Credit").val(result.Credit);
                    _this.$("#CreditMin").val(result.CreditMin);
                    _this.$("#CreditMax").val(result.CreditMax);
                }
            });
        };
        this.init = function () {
            _this.cleave = new Cleave('#Credit', {
                numeral: true,
                numeralDecimalMark: ',',
                delimiter: '.'
            });
            _this.$('#qrCodeMemberSignInForm').submit(function () {
                _this.$.blockUI();
                var credit = parseFloat(_this.cleave.getRawValue());
                var creditMin = parseFloat(_this.$("#CreditMin").val());
                var creditMax = parseFloat(_this.$("#CreditMax").val());
                if (credit < creditMin || credit > creditMax) {
                    event.preventDefault();
                    _this.$('#message').removeClass('hidden');
                    _this.$('#message-content').html('Invalid Credit');
                    _this.$.unblockUI();
                }
                else {
                    _this.$("#Credit").val(_this.cleave.getRawValue());
                }
            });
            var options = {
                url: function (phrase) {
                    return 'QRCodeMemberSignIn/GetUsernamesSuggestion?SuggestionQuery=' + phrase;
                },
                placeholder: "Username or First/Last Name",
                getValue: 'UserName',
                requestDelay: 300,
                minCharNumber: 2,
                template: {
                    type: "custom",
                    method: function (value, item) {
                        var nickname = item.UserName2 === null || item.UserName2 === '' ? '' : "(" + item.UserName2 + ")";
                        return "<div>" + value + " " + nickname + "<div>" + item.FirstName + " " + item.LastName + "</div></div>";
                    }
                },
                list: {
                    onChooseEvent: function () { return _this.getMemberCredit(); },
                    maxNumberOfElements: 10
                }
            };
            _this.$('#UsernameCopyFrom').easyAutocomplete(options);
            var memberName = _this.$('#UsernameCopyFrom').val();
            if (memberName !== '' && memberName !== undefined) {
                _this.getMemberCredit();
            }
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