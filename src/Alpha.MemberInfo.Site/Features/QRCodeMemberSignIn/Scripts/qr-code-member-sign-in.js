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
            _this.$('#btn-submit').click(function (event) { return _this.submitEvent(event); });
            _this.$('#FirstSuffix').change(function () { return _this.checkExistedUsernameByChangeSuffix(); });
            _this.$('#SecondSuffix').change(function () { return _this.checkExistedUsernameByChangeSuffix(); });
            _this.$('#ThirdSuffix').change(function () { return _this.checkExistedUsernameByChangeSuffix(); });
            _this.$('#Credit').keyup(function () { return _this.validateCredit(); });
            _this.cleave = new Cleave('#Credit', {
                numeral: true,
                numeralDecimalMark: '.',
                delimiter: ','
            });
        };
        this.checkExistedUsernameByChangeSuffix = function () {
            var username = "" + _this.$('#actor-name').text() + _this.$('#FirstSuffix').val() + _this.$('#SecondSuffix').val() + _this.$('#ThirdSuffix').val();
            _this.checkExistedCustomer(username, 'icon-message-username', false);
        };
        this.cacheDOMElement = function () {
            _this.$form = _this.$('#qrCodeMemberSignInForm');
            _this.$usernameCopyFrom = _this.$('#UsernameCopyFrom');
            _this.usernameCopyFromInQueryRequest = _this.$('#MemberNameCopyFrom').val();
            _this.$creditRange = _this.$('#credit-range');
            _this.$creditMinLabel = _this.$("#credit-min-label");
            _this.$creditMaxLabel = _this.$("#credit-max-label");
            _this.$creditInput = _this.$("#Credit");
            _this.$messageContainer = _this.$('#message');
            _this.$messageContent = _this.$('#message-content');
            _this.memberAccountInvalidMessage = _this.$('#MemberAccountIsInvalid').val();
            _this.usernameIsInvalid = _this.$('#UsernameIsInvalid').val();
            _this.iconMessageUsernameCopyFrom = 'icon-message-UsernameCopyFrom';
        };
        this.getMemberCredit = function () {
            _this.$.blockUI();
            _this.$.ajax({
                method: "POST",
                headers: _this.getHeadersForPostRequest(),
                data: { SearchAccountName: _this.$usernameCopyFrom.val() },
                dataType: 'json',
                url: "QRCodeMemberSignIn/GetMemberCredit",
                success: function (result) {
                    _this.$.unblockUI();
                    var numberFormat = '0,0.##';
                    _this.creditMin = result.CreditMin;
                    _this.creditMax = result.CreditMax;
                    _this.givenCreditMustBeBetweenMinMaxMessage = result.GivenCreditMustBeBetweenMinMaxMessage;
                    _this.$creditRange.removeClass('hidden');
                    _this.$creditMinLabel.html(numeral(result.CreditMin).format(numberFormat));
                    _this.$creditMaxLabel.html(numeral(result.CreditMax).format(numberFormat));
                    _this.$creditInput.val(numeral(result.Credit).format(numberFormat));
                    _this.validateCredit();
                }
            });
        };
        this.showErrorMessage = function (message, event) {
            _this.$messageContainer.removeClass('hidden').addClass('msg msg-error');
            _this.$messageContent.html(message);
            event.preventDefault();
        };
        this.checkingLocalStorage = function () {
            _this.usernameCopyFromInQueryRequest === '' && JSON.parse(localStorage.getItem('IsOldActor')) ?
                _this.$usernameCopyFrom.val(localStorage.getItem('memberNameCopyFrom')) :
                localStorage.removeItem('memberNameCopyFrom');
        };
        this.getHeadersForPostRequest = function () {
            return {
                __RequestVerificationToken: _this.$("input[name='__RequestVerificationToken']").val(),
                'X-Requested-With': 'XMLHttpRequest'
            };
        };
        this.validateCredit = function () {
            _this.credit = parseFloat(_this.cleave.getRawValue());
            _this.invalidCredit = isNaN(_this.credit) || _this.credit < _this.creditMin || _this.credit > _this.creditMax;
            var errorClass = _this.invalidCredit ? 'icon-close' : 'icon-check';
            _this.$('#icon-message-credit').removeClass().addClass(errorClass);
        };
        this.submitEvent = function (event) {
            if (_this.inValidCustomerCopyFrom) {
                _this.showErrorMessage(_this.memberAccountInvalidMessage, event);
                return;
            }
            if (_this.invalidCredit) {
                _this.showErrorMessage(_this.givenCreditMustBeBetweenMinMaxMessage, event);
                return;
            }
            if (_this.inValidUsername) {
                _this.showErrorMessage(_this.usernameIsInvalid, event);
                return;
            }
            _this.$.blockUI();
            var data = {
                UsernameCopyFrom: _this.$usernameCopyFrom.val(),
                FirstName: _this.$('#FirstName').val(),
                FirstSuffix: _this.$('#FirstSuffix').val(),
                SecondSuffix: _this.$('#SecondSuffix').val(),
                ThirdSuffix: _this.$('#ThirdSuffix').val(),
                Credit: _this.credit,
                CreditMin: _this.creditMin,
                CreditMax: _this.creditMax,
                ShowPageTitle: _this.$('#ShowPageTitle').val(),
                c_v: _this.$("input[name=c_v]").val(),
                c_f: _this.$("input[name=c_f]").val()
            };
            _this.$.ajax({
                method: "POST",
                headers: _this.getHeadersForPostRequest(),
                data: data,
                dataType: 'json',
                url: "QRCodeMemberSignIn/CreateAccount",
                success: function (result) {
                    if (result.ErrCode === 0) {
                        localStorage.setItem("memberNameCopyFrom", data.UsernameCopyFrom);
                        _this.reloadFrame(result);
                    }
                    else {
                        _this.showErrorMessage(result.ErrMsg, event);
                        _this.$('.captcha-icon-refresh').click();
                    }
                }
            });
            _this.$.unblockUI();
        };
        this.reloadFrame = function (result) {
            var frms = window.top.parent.frames;
            var showQRCodeLink = _this.$('#ShowQRCodeLink').val() + "?" + _this.$.param(result);
            if (frms && frms["main"]) {
                frms["main"].frames['frmAddNewMember'] ?
                    frms["main"].frames['frmAddNewMember'].location = showQRCodeLink :
                    frms["main"].location = showQRCodeLink;
            }
        };
        this.showMessageIcon = function (iconId, existedCustomer, checkCustomerCopyFrom) {
            if (checkCustomerCopyFrom === void 0) { checkCustomerCopyFrom = true; }
            _this.$("#" + iconId).removeClass();
            var iconClass = '';
            if (checkCustomerCopyFrom) {
                _this.inValidCustomerCopyFrom = !existedCustomer;
                iconClass = existedCustomer ? 'icon-check' : 'icon-close';
            }
            else {
                _this.inValidUsername = existedCustomer;
                iconClass = existedCustomer ? 'icon-close' : 'icon-check';
            }
            _this.$("#" + iconId).addClass(iconClass);
        };
        this.checkExistedCustomer = function (username, iconId, checkCustomerCopyFrom) {
            if (checkCustomerCopyFrom === void 0) { checkCustomerCopyFrom = true; }
            _this.$.ajax({
                method: "POST",
                headers: _this.getHeadersForPostRequest(),
                data: { username: username },
                dataType: 'json',
                url: "QRCodeMemberSignIn/CheckExistedCustomer",
                success: function (result) {
                    _this.showMessageIcon(iconId, result, checkCustomerCopyFrom);
                }
            });
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
                    onLoadEvent: function () {
                        var existedUsername = _this.$(_this.$usernameCopyFrom).getItemData(0) !== -1;
                        _this.showMessageIcon(_this.iconMessageUsernameCopyFrom, existedUsername);
                    },
                    maxNumberOfElements: 10
                }
            };
            _this.$usernameCopyFrom.easyAutocomplete(options);
            _this.$usernameCopyFrom.blur(function () {
                setTimeout(function () {
                    _this.getMemberCredit();
                }, 300);
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