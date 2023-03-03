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
    $firstSuffix: JQuery;
    $secondSuffix: JQuery;
    $thirdSuffix: JQuery;
    $noOfAccounts: JQuery;
    $password: JQuery;

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

    maxAccountsCreatePerRequest: number;

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
        this.$firstSuffix = this.$("#FirstSuffix");
        this.$secondSuffix = this.$("#SecondSuffix");
        this.$thirdSuffix = this.$("#ThirdSuffix");
        this.$noOfAccounts = this.$("#NoOfAccounts");
        this.$password = this.$("#Password");

        this.maxAccountsCreatePerRequest = parseInt(this.$("#MaxAccountsCreatePerRequest").val());
    }

    bindingEvent = () => {
        this.bidingEasyAutoComplete();
        this.$form.submit(event => this.submitEvent(event));

        this.$firstSuffix.change(() => this.checkExistedUsernameByChangeSuffix());
        this.$secondSuffix.change(() => this.checkExistedUsernameByChangeSuffix());
        this.$thirdSuffix.change(() => this.checkExistedUsernameByChangeSuffix());
        this.$password.keyup(() => this.validatePassword());
        this.$noOfAccounts.keyup(() => this.validateNoOfAccount());

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

    validateNoOfAccount = () => {
        var noOfAccount = parseInt(this.$noOfAccounts.val());
        var isValid = this.isValidNoOfAccount(noOfAccount);
        const iconClass = isValid ? 'icon-check' : 'icon-close';
        this.$('#icon-message-NoOfAccounts').removeClass().addClass(iconClass);
    }

    isValidNoOfAccount = (noOfAccount: number) => {
        return noOfAccount > 0 && noOfAccount <= this.maxAccountsCreatePerRequest;
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
            const noOfAccount = parseInt(this.$noOfAccounts.val());
            const password = this.$.trim(this.$password.val());
            this.$submitBtn.prop('disabled', !username || !this.isValidNoOfAccount(noOfAccount) || !this.isValidPassword(password));
        }

        this.$sourceUsername.on('input', validateInputs);
        this.$noOfAccounts.on('input', validateInputs);
        this.$password.on('input', validateInputs);
    }

    submitEvent = (event) => {
        event.preventDefault();

        if (this.checkErrorAndShowMessage()) {
            return;
        }

        this.$.blockUI();

        const data = {
            SourceUsername: this.$sourceUsername.val(),
            FirstSuffix: this.$firstSuffix.val(),
            SecondSuffix: this.$secondSuffix.val(),
            ThirdSuffix: this.$thirdSuffix.val(),
            Credit: this.credit,
            CreditMin: this.creditMin,
            CreditMax: this.creditMax,
            NumberOfAccounts: this.$noOfAccounts.val(),
            Password: this.$password.val()
        };

        this.$.ajax({
            method: "POST",
            headers: this.getHeadersForPostRequest(),
            data: data,
            dataType: 'json',
            url: "CopyAccount/Index",
            success: (result, status, xqr) => {
                //if (result.ErrCode === 0 || status === "302" || xqr.status === 302) {
                //    localStorage.setItem("memberNameCopyFrom", data.UsernameCopyFrom);
                //    setTimeout(() => {
                //        this.reloadFrame(result);
                //    }, 1000);
                //}
                //else {
                //    this.$('.captcha-icon-refresh').click();
                //    this.showErrorMessage(result.ErrMsg);
                //    this.$.unblockUI();
                //}
            },
            error: () => { this.$.unblockUI(); }
        });
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

    validatePassword = () => {
        var validPassword = this.isValidPassword(this.$password.val());
        const iconClass = validPassword ? 'icon-check' : 'icon-close';
        this.$('#icon-message-password').removeClass().addClass(iconClass);
    }

    isValidPassword = (password) => {
        var whiteSpace = /^\S*$/;
        var whiteSpaceCase = password.search(whiteSpace) != -1;

        var regex1 = /^(?=.{8,100}$).*\d/i;
        var regex2 = /^(?=.{8,100}$).*[a-z]/;
        var regex3 = /^(?=.{8,100}$).*[A-Z]/;
        var regex4 = /^(?=.{8,100}$).*[\[\]\^\$\.\|\?\*\+\(\)\\~`\!@#%&\-_+={}'""<>:;,]/i;

        var case1 = password.search(regex1) != -1 ? 1 : 0;
        var case2 = password.search(regex2) != -1 ? 1 : 0;
        var case3 = password.search(regex3) != -1 ? 1 : 0;
        var case4 = password.search(regex4) != -1 ? 1 : 0;

        return (case1 + case2 + case3 + case4 >= 3) && whiteSpaceCase;
    }
}

const copyAccount = new CopyAccount($, window.top);

$(() => {
    copyAccount.init();
});