declare var clipboard: any;
declare var Cleave: any;
declare var numeral: any;

class QRCodeMemberSignIn {
    $: JQueryStatic;
    $form: JQuery;
    $usernameCopyFrom: JQuery;
    usernameCopyFromInQueryRequest: string;
    $creditRange: JQuery;
    $creditMinLabel: JQuery;
    $creditMaxLabel: JQuery;

    $creditInput: JQuery;
    $creditMinInput: JQuery;
    $creditMaxInput: JQuery;

    $messageContainer: JQuery;
    $messageContent: JQuery;

    memberAccountInvalidMessage: string;
    iconMessageUsernameCopyFrom: string;

    topWindow: any;
    username: string;
    memberLink: string;
    cleave: any;
    givenCreditMustBeBetweenMinMaxMessage: string;

    inValidCustomerCopyFrom: boolean;
    inValidUsername: boolean;

    constructor(
        $: JQueryStatic,
        topWindow: any) {
        this.$ = $;
        this.topWindow = topWindow;
    }

    init = () => {
        this.cacheDOMElement();
        this.checkingLocalStorage();
        this.bindingEvent();

        if (this.$usernameCopyFrom.val() !== '') {
            this.getMemberCredit();
        }
    };

    bindingEvent = () => {
        this.bidingEasyAutoComplete();
        this.$('#btn-submit').click(event => this.submitEvent(event));

        this.$('#FirstSuffix').change(() => this.checkExistedUsernameByChangeSuffix());
        this.$('#SecondSuffix').change(() => this.checkExistedUsernameByChangeSuffix());
        this.$('#ThirdSuffix').change(() => this.checkExistedUsernameByChangeSuffix());

        this.cleave = new Cleave('#Credit', {
            numeral: true,
            numeralDecimalMark: '.',
            delimiter: ','
        });
    }

    checkExistedUsernameByChangeSuffix = () => {
        const username = `${this.$('#actor-name').text()}${this.$('#FirstSuffix').val()}${this.$('#SecondSuffix').val()}${this.$('#ThirdSuffix').val()}`;
        this.checkExistedCustomer(username, 'icon-message-username', false);
    }

    cacheDOMElement = () => {
        this.$form = this.$('#qrCodeMemberSignInForm');
        this.$usernameCopyFrom = this.$('#UsernameCopyFrom');
        this.usernameCopyFromInQueryRequest = this.$('#MemberNameCopyFrom').val();

        this.$creditRange = this.$('#credit-range');
        this.$creditMinLabel = this.$("#credit-min-label");
        this.$creditMaxLabel = this.$("#credit-max-label");

        this.$creditInput = this.$("#Credit");
        this.$creditMinInput = this.$("#CreditMin");
        this.$creditMaxInput = this.$("#CreditMax");

        this.$messageContainer = this.$('#message');
        this.$messageContent = this.$('#message-content');
        this.memberAccountInvalidMessage = this.$('#MemberAccountIsInvalid').val();

        this.iconMessageUsernameCopyFrom = 'icon-message-UsernameCopyFrom';
    }

    bindingCreditContainer = (result) => {
        this.$.unblockUI();

        const numberFormat = '0,0.##';
        const creditMin = numeral(result.CreditMin);
        const creditMax = numeral(result.CreditMax);
        this.givenCreditMustBeBetweenMinMaxMessage = result.GivenCreditMustBeBetweenMinMaxMessage;

        this.$creditRange.removeClass('hidden');
        this.$creditMinLabel.html(creditMin.format(numberFormat));
        this.$creditMaxLabel.html(creditMax.format(numberFormat));

        const credit = numeral(result.Credit);

        this.$creditInput.val(credit.format(numberFormat));
        this.$creditMinInput.val(result.CreditMin);
        this.$creditMaxInput.val(result.CreditMax);
    }

    getMemberCredit = () => {
        this.$.blockUI();

        const headers = {
            __RequestVerificationToken: this.$("input[name='__RequestVerificationToken']").val(),
            'X-Requested-With': 'XMLHttpRequest'
        };

        this.$.ajax({
            method: "POST",
            headers: headers,
            data: { SearchAccountName: this.$usernameCopyFrom.val()},
            dataType: 'json',
            url: "QRCodeMemberSignIn/GetMemberCredit",
            success: result => this.bindingCreditContainer(result)
        });
    }

    showErrorMessage = (message, event) => {
        this.$messageContainer.removeClass('hidden').addClass('msg msg-error');
        this.$messageContent.html(message);
        event.preventDefault();
    }

    checkingLocalStorage = () => {
        if (this.usernameCopyFromInQueryRequest === '' && JSON.parse(localStorage.getItem('IsOldActor'))) {
            this.$usernameCopyFrom.val(localStorage.getItem('memberNameCopyFrom'));
        }
        else {
            localStorage.removeItem('memberNameCopyFrom');
        }
    }

    submitEvent = (event) => {
        this.$.blockUI();

        if (this.inValidCustomerCopyFrom || this.inValidUsername) {
            this.showErrorMessage(this.memberAccountInvalidMessage, event);
        }

        const credit = parseFloat(this.cleave.getRawValue());
        const creditMin = parseFloat(this.$creditMinInput.val());
        const creditMax = parseFloat(this.$creditMaxInput.val());

        if (credit < creditMin || credit > creditMax) {
            this.showErrorMessage(this.givenCreditMustBeBetweenMinMaxMessage, event);
        }
        else {
            this.$creditInput.val(this.cleave.getRawValue());
        }

        const headers = {
            __RequestVerificationToken: this.$("input[name='__RequestVerificationToken']").val(),
            'X-Requested-With': 'XMLHttpRequest'
        };

        const data = {
            UsernameCopyFrom: this.$usernameCopyFrom.val(),
            FirstName: this.$('#FirstName').val(),
            FirstSuffix: this.$('#FirstSuffix').val(),
            SecondSuffix: this.$('#SecondSuffix').val(),
            ThirdSuffix: this.$('#ThirdSuffix').val(),
            Credit: credit,
            CreditMin: creditMin,
            CreditMax: creditMax,
            v: this.$('.captcha-input').val()
        };

        this.$.ajax({
            method: "POST",
            headers: headers,
            data: data,
            dataType: 'json',
            url: "QRCodeMemberSignIn/CreateAccount",
            success: (result) => {
                if (result.Success) {
                    const frms = window.top.parent.frames;
                    const showQRCodeLink = `${this.$('#ShowQRCodeLink').val()}?${this.$.param(result)})`;
                    if (frms && frms["main"]) {
                       frms["main"].frames['frmAddNewMember'] ?
                            frms["main"].frames['frmAddNewMember'].location = showQRCodeLink :
                            frms["main"].location = showQRCodeLink;
                    }
                }
                else {
                    this.showErrorMessage(result.ErrMsg, event);

                    const ErrorCodeIncaseInvalidCaptcha = 998;
                    if (result.ErrCode === ErrorCodeIncaseInvalidCaptcha) {
                        this.$('.captcha-icon-refresh').click();
                    }
                }
            }
        });

        this.$.unblockUI();
    }

    showMessageIcon = (iconId: string, existedCustomer: boolean, checkCustomerCopyFrom: boolean = true) => {
        this.$(`#${iconId}`).removeClass();

        let iconClass = ''; 
        if (checkCustomerCopyFrom) {
            this.inValidCustomerCopyFrom = !existedCustomer;
            iconClass = existedCustomer ? 'icon-check' : 'icon-close';
        }
        else {
            this.inValidUsername = existedCustomer;
            iconClass = existedCustomer ? 'icon-close' : 'icon-check';
        }

        this.$(`#${iconId}`).addClass(iconClass);
    }

    checkExistedCustomer = (username, iconId: string, checkCustomerCopyFrom: boolean = true) => {
        const headers = {
            __RequestVerificationToken: this.$("input[name='__RequestVerificationToken']").val(),
            'X-Requested-With': 'XMLHttpRequest'
        };

        this.$.ajax({
            method: "POST",
            headers: headers,
            data: { username: username },
            dataType: 'json',
            url: "QRCodeMemberSignIn/CheckExistedCustomer",
            success: (result) => {
                this.showMessageIcon(iconId, result, checkCustomerCopyFrom);
            }
        });
    }

    bidingEasyAutoComplete = () => {
        const options = {
            url: function (phrase) {
                return 'QRCodeMemberSignIn/GetUsernamesSuggestion?SuggestionQuery=' + phrase;
            },
            getValue: 'UserName',
            requestDelay: 200,
            minCharNumber: 2,
            template: {
                type: "custom",
                method: function (value, item) {
                    const nickname = item.UserName2 === null || item.UserName2 === '' ? '' : `(${item.UserName2})`;
                    return `<div class='suggestion'>${value} ${nickname}<div>${item.FirstName} ${item.LastName}</div></div>`;
                }
            },
            list: {
                onChooseEvent: () => {
                    this.showMessageIcon(this.iconMessageUsernameCopyFrom, true);
                    this.getMemberCredit();
                },
                maxNumberOfElements: 10
            }
        };

        this.$usernameCopyFrom.easyAutocomplete(options);
        this.$usernameCopyFrom.blur(() => this.checkExistedCustomer(this.$usernameCopyFrom.val(), this.iconMessageUsernameCopyFrom));
    }
}

const qrCodeMemberSignIn = new QRCodeMemberSignIn($, window.top);

$(() => {
    qrCodeMemberSignIn.init();
});
