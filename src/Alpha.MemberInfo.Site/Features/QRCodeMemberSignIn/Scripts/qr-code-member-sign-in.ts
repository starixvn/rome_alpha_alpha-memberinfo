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
    credit: number;
    creditMin: number;
    creditMax: number;

    $messageContainer: JQuery;
    $messageContent: JQuery;

    memberAccountInvalidMessage: string;
    usernameIsInvalid: string;
    iconMessageUsernameCopyFrom: string;

    topWindow: any;
    username: string;
    memberLink: string;
    cleave: any;
    givenCreditMustBeBetweenMinMaxMessage: string;

    inValidCustomerCopyFrom: boolean;
    inValidUsername: boolean;
    invalidCredit: boolean;

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
            this.blurEventCustomerCopyFrom();
        }
    };

    bindingEvent = () => {
        this.bidingEasyAutoComplete();
        this.$('#btn-submit').click(event => this.submitEvent(event));

        this.$('#FirstSuffix').change(() => this.checkExistedUsernameByChangeSuffix());
        this.$('#SecondSuffix').change(() => this.checkExistedUsernameByChangeSuffix());
        this.$('#ThirdSuffix').change(() => this.checkExistedUsernameByChangeSuffix());

        this.$('#Credit').keyup(() => this.validateCredit());
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

        this.$messageContainer = this.$('#message');
        this.$messageContent = this.$('#message-content');
        this.memberAccountInvalidMessage = this.$('#MemberAccountIsInvalid').val();
        this.usernameIsInvalid = this.$('#UsernameIsInvalid').val();
        this.iconMessageUsernameCopyFrom = 'icon-message-UsernameCopyFrom';
    }

    getMemberCredit = () => {
        this.$.blockUI();

        this.$.ajax({
            method: "POST",
            headers: this.getHeadersForPostRequest(),
            data: { SearchAccountName: this.$usernameCopyFrom.val() },
            dataType: 'json',
            url: "QRCodeMemberSignIn/GetMemberCredit",
            success: result => {
                this.$.unblockUI();

                const numberFormat = '0,0.##';
                this.creditMin = result.CreditMin;
                this.creditMax = result.CreditMax;
                this.givenCreditMustBeBetweenMinMaxMessage = result.GivenCreditMustBeBetweenMinMaxMessage;

                this.$creditRange.removeClass('hidden');
                this.$creditMinLabel.html(numeral(result.CreditMin).format(numberFormat));
                this.$creditMaxLabel.html(numeral(result.CreditMax).format(numberFormat));
                this.$creditInput.val(numeral(result.Credit).format(numberFormat));

                this.validateCredit();
            }
        });
    }

    showErrorMessage = (message, event) => {
        this.$messageContainer.removeClass('hidden').addClass('msg msg-error');
        this.$messageContent.html(message);
        event.preventDefault();
    }

    checkingLocalStorage = () => {
        this.usernameCopyFromInQueryRequest === '' && JSON.parse(localStorage.getItem('IsOldActor')) ?
            this.$usernameCopyFrom.val(localStorage.getItem('memberNameCopyFrom')) :
            localStorage.removeItem('memberNameCopyFrom');
    }

    getHeadersForPostRequest = () => {
        return {
            __RequestVerificationToken: this.$("input[name='__RequestVerificationToken']").val(),
            'X-Requested-With': 'XMLHttpRequest'
        };
    }

    validateCredit = () => {
        this.credit = parseFloat(this.cleave.getRawValue());
        this.invalidCredit = isNaN(this.credit)  || this.credit < this.creditMin || this.credit > this.creditMax;
        const errorClass = this.invalidCredit ?'icon-close' : 'icon-check';
        this.$('#icon-message-credit').removeClass().addClass(errorClass);
    }

    submitEvent = (event) => {
        if (this.inValidCustomerCopyFrom) {
            this.showErrorMessage(this.memberAccountInvalidMessage, event);
            return;
        }

        if (this.invalidCredit) {
            this.showErrorMessage(this.givenCreditMustBeBetweenMinMaxMessage, event);
            return;
        }

        if (this.inValidUsername) {
            this.showErrorMessage(this.usernameIsInvalid, event);
            return;
        }

        this.$.blockUI();

        const data = {
            UsernameCopyFrom: this.$usernameCopyFrom.val(),
            FirstName: this.$('#FirstName').val(),
            FirstSuffix: this.$('#FirstSuffix').val(),
            SecondSuffix: this.$('#SecondSuffix').val(),
            ThirdSuffix: this.$('#ThirdSuffix').val(),
            Credit: this.credit,
            CreditMin: this.creditMin,
            CreditMax: this.creditMax,
            ShowPageTitle: this.$('#ShowPageTitle').val(),
            c_v: this.$("input[name=c_v]").val(),
            c_f: this.$("input[name=c_f]").val()
        };

        this.$.ajax({
            method: "POST",
            headers: this.getHeadersForPostRequest(),
            data: data,
            dataType: 'json',
            url: "QRCodeMemberSignIn/CreateAccount",
            success: (result) => {
                if (result.ErrCode === 0) {
                    localStorage.setItem("memberNameCopyFrom", data.UsernameCopyFrom);
                    this.reloadFrame(result);
                }
                else {
                    this.showErrorMessage(result.ErrMsg, event);
                    this.$('.captcha-icon-refresh').click();
                }
            }
        });

        this.$.unblockUI();
    }

    reloadFrame = (result) => {
        const frms = window.top.parent.frames;
        const showQRCodeLink = `${this.$('#ShowQRCodeLink').val()}?${this.$.param(result)}`;
        if (frms && frms["main"]) {
            frms["main"].frames['frmAddNewMember'] ?
                frms["main"].frames['frmAddNewMember'].location = showQRCodeLink :
                frms["main"].location = showQRCodeLink;
        }
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
        this.$.ajax({
            method: "POST",
            headers: this.getHeadersForPostRequest(),
            data: { username: username },
            dataType: 'json',
            url: "QRCodeMemberSignIn/CheckExistedCustomer",
            success: (result) => {
                this.showMessageIcon(iconId, result, checkCustomerCopyFrom);
            }
        });
    }

    blurEventCustomerCopyFrom = () => {
        this.checkExistedCustomer(this.$usernameCopyFrom.val(), this.iconMessageUsernameCopyFrom);
        this.getMemberCredit();
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
                method: (value, item) => {
                    const nickname = item.UserName2 === null || item.UserName2 === '' ? '' : `(${item.UserName2})`;
                    return `<div class='suggestion'>${value} ${nickname}<div>${item.FirstName} ${item.LastName}</div></div>`;
                }
            },
            list: {
                maxNumberOfElements: 10
            }
        };

        this.$usernameCopyFrom.easyAutocomplete(options);
        this.$usernameCopyFrom.blur(() => {
            setTimeout(() => {
                    this.blurEventCustomerCopyFrom();
                }, 300);
        });
    }
}

const qrCodeMemberSignIn = new QRCodeMemberSignIn($, window.top);

$(() => {
    qrCodeMemberSignIn.init();
});
