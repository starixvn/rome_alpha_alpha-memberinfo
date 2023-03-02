declare var Cleave: any;
declare var numeral: any;

class CopyAccount {
    $: JQueryStatic;
    $form: JQuery;
    $sourceUsername: JQuery;
    $creditRange: JQuery;
    $creditMinLabel: JQuery;
    $creditMaxLabel: JQuery;
    $submitBtn: JQuery;

    $creditInput: JQuery;
    credit: number;
    creditMin: number;
    creditMax: number;

    $messageContainer: JQuery;
    $messageContent: JQuery;

    topWindow: any;
    cleave: any;

    srouceUsernameInQueryRequest: string;
    iconMessageSourceUsername = "icon-message-SourceUsername";

    inValidSourceAccount: boolean;
    inValidUsername: boolean;
    invalidCredit: boolean;
    memberAccountInvalidMessage: string;
    usernameIsInvalid: string;
    iconMessageUsernameCopyFrom: string;

    showCreateFromUnopenedMemberMessage: boolean;
    cannotCreateClosedMemberQRCodeMessage: string;
    givenCreditMustBeBetweenMinMaxMessage: string;

    constructor(
        $: JQueryStatic,
        topWindow: any) {
        this.$ = $;
        this.topWindow = topWindow;
    }

    init = () => {
        this.cacheDOMElement();
        this.bindingEvent();

        if (this.$sourceUsername.val() !== '') {
            this.blurEventSourceAccount();
        }
    };

    cacheDOMElement = () => {
        this.$form = this.$('#copyAccountForm');
        this.$sourceUsername = this.$('#SourceUsername');
        this.srouceUsernameInQueryRequest = this.$('#SourceUsernameHidden').val();
        this.$submitBtn = this.$('#btn-submit');

        this.$creditRange = this.$('#credit-range');
        this.$creditMinLabel = this.$("#credit-min-label");
        this.$creditMaxLabel = this.$("#credit-max-label");
        this.$creditInput = this.$("#Credit");

        this.$messageContainer = this.$('#message');
        this.$messageContent = this.$('#message-content');
    }

    bindingEvent = () => {
        this.bidingEasyAutoComplete();
        this.$('#qrCodeMemberSignInForm').submit(event => this.submitEvent(event));

        this.$('#FirstSuffix').change(() => this.checkExistedUsernameByChangeSuffix());
        this.$('#SecondSuffix').change(() => this.checkExistedUsernameByChangeSuffix());
        this.$('#ThirdSuffix').change(() => this.checkExistedUsernameByChangeSuffix());

        this.$('#Credit').keyup(() => {
            this.validateCredit();
            this.checkErrorAndShowMessage();
        });
        this.cleave = new Cleave('#Credit', {
            numeral: true,
            numeralDecimalMark: '.',
            delimiter: ','
        });
    }

    bidingEasyAutoComplete = () => {
        const options = {
            url: phrase => {
                return `${this.$('#member-info-url').val()}UserNameSuggestion/GetUsernamesSuggestion?SuggestionQuery=${phrase}`;
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

        this.$sourceUsername.easyAutocomplete(options);
        this.$sourceUsername.blur(() => {
            setTimeout(() => {
                this.blurEventSourceAccount();
            }, 300);
        });

        const validateInputs = () => {
            const username = this.$.trim(this.$sourceUsername.val());
            this.$submitBtn.prop('disabled', !username);
        }

        this.$sourceUsername.on('input', validateInputs);
    }

    submitEvent = (event) => {
        event.preventDefault();

        if (this.checkErrorAndShowMessage()) {
            return;
        }

        this.$.blockUI();

        const data = {
            UsernameCopyFrom: this.$sourceUsername.val(),
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

        //this.$.ajax({
        //    method: "POST",
        //    headers: this.getHeadersForPostRequest(),
        //    data: data,
        //    dataType: 'json',
        //    url: "QRCodeMemberSignIn/CreateAccount",
        //    success: (result, status, xqr) => {
        //        if (result.ErrCode === 0 || status === "302" || xqr.status === 302) {
        //            localStorage.setItem("memberNameCopyFrom", data.UsernameCopyFrom);
        //            setTimeout(() => {
        //                this.reloadFrame(result);
        //            }, 1000);
        //        }
        //        else {
        //            this.$('.captcha-icon-refresh').click();
        //            this.showErrorMessage(result.ErrMsg);
        //            this.$.unblockUI();
        //        }
        //    },
        //    error: () => { this.$.unblockUI(); }
        //});
    }

    checkExistedUsernameByChangeSuffix = () => {
        const username = `${this.$('#actor-name').text()}${this.$('#FirstSuffix').val()}${this.$('#SecondSuffix').val()}${this.$('#ThirdSuffix').val()}`;
        this.checkExistedCustomer(username, 'icon-message-username', false);
    }

    blurEventSourceAccount = () => {
        this.checkExistedCustomer(this.$sourceUsername.val(), this.iconMessageSourceUsername);
        this.getCustomerCredit();
    }

    getCustomerCredit = () => {
        this.$.blockUI();

        this.$.ajax({
            method: "POST",
            headers: this.getHeadersForPostRequest(),
            data: { SearchAccountName: this.$sourceUsername.val() },
            dataType: 'json',
            url: "QRCodeMemberSignIn/GetMemberCreditAndStatus",
            success: result => {
                this.$.unblockUI();
                this.showCreateFromUnopenedMemberMessage = result.Status !== null && result.Status.toUpperCase() !== 'OPEN';

                this.showCreateFromUnopenedMemberMessage ?
                    this.$(`#${this.iconMessageSourceUsername}`).removeClass().addClass('icon-close') :
                    this.$messageContainer.removeClass('msg msg-error').addClass('hidden');

                const numberFormat = '0,0.##';
                this.creditMin = result.CreditMin;
                this.creditMax = result.CreditMax;
                this.givenCreditMustBeBetweenMinMaxMessage = result.GivenCreditMustBeBetweenMinMaxMessage;

                this.$creditRange.removeClass('hidden');
                this.$creditMinLabel.html(numeral(result.CreditMin).format(numberFormat));
                this.$creditMaxLabel.html(numeral(result.CreditMax).format(numberFormat));
                this.$creditInput.val(numeral(result.Credit).format(numberFormat));

                this.validateCredit();
                this.checkErrorAndShowMessage();
                this.$.unblockUI();
            },
            error: () => { this.$.unblockUI(); }
        });
    }

    validateCredit = () => {
        this.credit = parseFloat(this.cleave.getRawValue());
        this.invalidCredit = isNaN(this.credit) || this.credit < this.creditMin || this.credit > this.creditMax;
        const errorClass = this.invalidCredit ? 'icon-close' : 'icon-check';
        this.$('#icon-message-credit').removeClass().addClass(errorClass);
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

    getHeadersForPostRequest = () => {
        return {
            __RequestVerificationToken: this.$("input[name='__RequestVerificationToken']").val(),
            'X-Requested-With': 'XMLHttpRequest'
        };
    }

    showErrorMessage = (message) => {
        this.$messageContainer.removeClass('hidden').addClass('msg msg-error');
        this.$messageContent.html(message);
    }

    showMessageIcon = (iconId: string, existedCustomer: boolean, checkCustomerCopyFrom: boolean = true) => {
        this.$(`#${iconId}`).removeClass();

        let iconClass = '';
        if (checkCustomerCopyFrom) {
            this.inValidSourceAccount = !existedCustomer;
            iconClass = existedCustomer ? 'icon-check' : 'icon-close';
        }
        else {
            this.inValidUsername = existedCustomer;
            iconClass = existedCustomer ? 'icon-close' : 'icon-check';
        }

        this.$(`#${iconId}`).addClass(iconClass);
        this.checkErrorAndShowMessage();
    }

    checkErrorAndShowMessage = () => {
        if (this.showCreateFromUnopenedMemberMessage) {
            this.showErrorMessage(this.cannotCreateClosedMemberQRCodeMessage);
            return true;
        }

        if (this.inValidSourceAccount) {
            this.showErrorMessage(this.memberAccountInvalidMessage);
            return true;
        }

        if (this.invalidCredit) {
            this.showErrorMessage(this.givenCreditMustBeBetweenMinMaxMessage);
            return true;
        }

        if (this.inValidUsername) {
            this.showErrorMessage(this.usernameIsInvalid);
            return true;
        }

        this.$messageContainer.addClass('hidden');
        this.$messageContent.html('');
        return false;
    }
}

const copyAccount = new CopyAccount($, window.top);

$(() => {
    copyAccount.init();
});