var QRCodeMemberSignIn = /** @class */ (function () {
    function QRCodeMemberSignIn($, topWindow) {
        var _this = this;
        this.init = function () {
            _this.cacheDOMElement();
            _this.checkingLocalStorage();
            _this.bindingEvent();
            if (_this.$usernameCopyFrom.val() !== '') {
                _this.getMemberCredit();
            }
        };
        this.bindingEvent = function () {
            _this.bidingEasyAutoComplete();
            _this.$form.submit(function (event) { return _this.submitEvent(event); });
            _this.cleave = new Cleave('#Credit', {
                numeral: true,
                numeralDecimalMark: '.',
                delimiter: ','
            });
        };
        this.cacheDOMElement = function () {
            _this.$form = _this.$('#qrCodeMemberSignInForm');
            _this.$usernameCopyFrom = _this.$('#UsernameCopyFrom');
            _this.usernameCopyFromInQueryRequest = _this.$('#MemberNameCopyFrom').val();
            _this.$creditRange = _this.$('#credit-range');
            _this.$creditMinLabel = _this.$("#credit-min-label");
            _this.$creditMaxLabel = _this.$("#credit-max-label");
            _this.$creditInput = _this.$("#Credit");
            _this.$creditMinInput = _this.$("#CreditMin");
            _this.$creditMaxInput = _this.$("#CreditMax");
            _this.$messageContainer = _this.$('#message');
            _this.$messageContent = _this.$('#message-content');
            _this.memberAccountInvalidMessage = _this.$('#MemberAccountIsInvalid').val();
        };
        this.bindingCreditContainer = function (result) {
            _this.$.unblockUI();
            var numberFormat = '0,0.##';
            var creditMin = numeral(result.CreditMin);
            var creditMax = numeral(result.CreditMax);
            _this.givenCreditMustBeBetweenMinMaxMessage = result.GivenCreditMustBeBetweenMinMaxMessage;
            _this.$creditRange.removeClass('hidden');
            _this.$creditMinLabel.html(creditMin.format(numberFormat));
            _this.$creditMaxLabel.html(creditMax.format(numberFormat));
            var credit = numeral(result.Credit);
            _this.$creditInput.val(credit.format(numberFormat));
            _this.$creditMinInput.val(result.CreditMin);
            _this.$creditMaxInput.val(result.CreditMax);
        };
        this.getMemberCredit = function () {
            _this.$.blockUI();
            var headers = {
                __RequestVerificationToken: _this.$("input[name='__RequestVerificationToken']").val(),
                'X-Requested-With': 'XMLHttpRequest'
            };
            _this.$.ajax({
                method: "POST",
                headers: headers,
                data: { SearchAccountName: _this.$usernameCopyFrom.val() },
                dataType: 'json',
                url: "QRCodeMemberSignIn/GetMemberCredit",
                success: function (result) { return _this.bindingCreditContainer(result); }
            });
        };
        this.showErrorMessage = function (message, event) {
            _this.$messageContainer.removeClass('hidden').addClass('msg msg-error');
            _this.$messageContent.html(message);
            event.preventDefault();
        };
        this.checkingLocalStorage = function () {
            if (_this.usernameCopyFromInQueryRequest === '' && JSON.parse(localStorage.getItem('IsOldActor'))) {
                _this.$usernameCopyFrom.val(localStorage.getItem('memberNameCopyFrom'));
            }
            else {
                localStorage.removeItem('memberNameCopyFrom');
            }
        };
        this.submitEvent = function (event) {
            _this.$.blockUI();
            var username = _this.$usernameCopyFrom.val();
            if (username === '') {
                _this.showErrorMessage(_this.memberAccountInvalidMessage, event);
            }
            var credit = parseFloat(_this.cleave.getRawValue());
            var creditMin = parseFloat(_this.$creditMinInput.val());
            var creditMax = parseFloat(_this.$creditMaxInput.val());
            if (credit < creditMin || credit > creditMax) {
                _this.showErrorMessage(_this.givenCreditMustBeBetweenMinMaxMessage, event);
            }
            else {
                _this.$creditInput.val(_this.cleave.getRawValue());
            }
            _this.$.unblockUI();
        };
        this.bidingEasyAutoComplete = function () {
            var options = {
                url: function (phrase) {
                    return 'QRCodeMemberSignIn/GetUsernamesSuggestion?SuggestionQuery=' + phrase;
                },
                getValue: 'UserName',
                requestDelay: 200,
                minCharNumber: 2,
                template: {
                    type: "custom",
                    method: function (value, item) {
                        var nickname = item.UserName2 === null || item.UserName2 === '' ? '' : "(" + item.UserName2 + ")";
                        return "<div class='suggestion'>" + value + " " + nickname + "<div>" + item.FirstName + " " + item.LastName + "</div></div>";
                    }
                },
                list: {
                    onChooseEvent: function () { return _this.getMemberCredit(); },
                    maxNumberOfElements: 10
                }
            };
            _this.$usernameCopyFrom.easyAutocomplete(options);
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