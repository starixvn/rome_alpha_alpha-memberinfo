declare var Cleave: any;
declare var numeral: any;

class CopyAccount {
    $: JQueryStatic;
    $form: JQuery;
    $result: JQuery;
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
    memberMaxCredit: number;

    $memberMaxCreditInput: JQuery;

    $messageContainer: JQuery;
    $messageContent: JQuery;

    topWindow: any;
    cleave: any;
    memberMaxCreditCleave: any;

    isCreateAgent: boolean;
    inValidSourceAccount: boolean;
    inValidUsername: boolean;
    invalidCredit: boolean;
    memberAccountInvalidMessage: string;
    usernameIsInvalid: string;

    showCreateFromUnopenedMemberMessage: boolean;
    givenCreditMustBeBetweenMinMaxMessage: string;

    maxAccountsCreatePerRequest: number;

    $btnSwitchVersion: JQuery;
    oldVersionUrl: string;
    numberFormat = '0,0.##';

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
        this.$result = this.$("#result");
        this.$sourceUsername = this.$('#SourceUsername');
        this.$submitBtn = this.$('#btn-submit');

        this.$creditRange = this.$('#credit-range');
        this.$creditMinLabel = this.$("#credit-min-label");
        this.$creditMaxLabel = this.$("#credit-max-label");
        this.$creditInput = this.$("#Credit");
        this.$memberMaxCreditInput = this.$("#MemberMaxCredit");

        this.$messageContainer = this.$('#message');
        this.$messageContent = this.$('#message-content');
        this.$firstSuffix = this.$("#FirstSuffix");
        this.$secondSuffix = this.$("#SecondSuffix");
        this.$thirdSuffix = this.$("#ThirdSuffix");
        this.$noOfAccounts = this.$("#NoOfAccounts");
        this.$password = this.$("#Password");

        this.maxAccountsCreatePerRequest = parseInt(this.$("#MaxAccountsCreatePerRequest").val());
        this.isCreateAgent = this.$("#IsCreateAgent").val() == "True";
        this.$btnSwitchVersion = this.$(".btn-switch-version");
        this.oldVersionUrl = this.$("#old-version-url").val();
    }

    bindingEvent = () => {
        this.bidingEasyAutoComplete();
        this.$form.submit(event => this.submitEvent(event));

        this.$firstSuffix.change(() => this.checkExistedUsernameByChangeSuffix());
        this.$secondSuffix.change(() => this.checkExistedUsernameByChangeSuffix());
        this.$thirdSuffix.change(() => this.checkExistedUsernameByChangeSuffix());
        this.$password.keyup(() => this.validatePassword());
        this.$noOfAccounts.keyup(() => this.validateNoOfAccount());

        this.$creditInput.keyup(() => {
            this.validateCredit();
            this.checkErrorAndShowMessage();
        });
        this.cleave = new Cleave('#Credit', {
            numeral: true,
            numeralDecimalMark: '.',
            delimiter: ','
        });

        this.$btnSwitchVersion.click(() => {
            this.topWindow.indexManager.changeMenu(this.oldVersionUrl);
        })

        if (this.isCreateAgent) {
            this.memberMaxCreditCleave = new Cleave("#MemberMaxCredit", {
                numeral: true,
                numeralDecimalMark: '.',
                delimiter: ','
            });

            this.$memberMaxCreditInput.keyup(() => {
                this.memberMaxCredit = parseFloat(this.memberMaxCreditCleave.getRawValue());
                this.invalidCredit = isNaN(this.memberMaxCredit) || this.memberMaxCredit < this.creditMin || this.memberMaxCredit > this.credit;
                const errorClass = this.invalidCredit ? 'icon-close' : 'icon-check';
                this.$('#icon-message-member-max-credit').removeClass().addClass(errorClass);
            });
        }
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
                return `${this.$('#member-info-url').val()}UserNameSuggestion/GetUsernamesSuggestion?IsAccInfo=false&DirectDownlines=true&SuggestionQuery=${phrase}`;
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

        if (this.isCreateAgent) {
            data["MemberMaxCredit"] = this.$memberMaxCreditInput.val();
        }

        this.$.ajax({
            method: "POST",
            headers: this.getHeadersForPostRequest(),
            data: data,
            dataType: 'json',
            url: "CopyAccount/Index",
            success: (result) => {
                if (result.Code > 0) {
                    this.showErrorMessage(result.Message);
                } else {
                    this.$("#usernames").html(result.Usernames.join(", "));
                    this.$("#txtClonedCredit").html(numeral(this.credit).format(this.numberFormat));
                    this.$result.removeClass("hidden");
                    this.$form.addClass("hidden");
                }
                this.$.unblockUI();
            },
            error: () => { this.$.unblockUI(); }
        });
    }

    checkExistedUsernameByChangeSuffix = () => {
        const username = `${this.$('#actor-name').text()}${this.$('#FirstSuffix').val()}${this.$('#SecondSuffix').val()}${this.$('#ThirdSuffix').val()}`;
        const onSuccess = (result: number) => {
            const iconClass = result > 0 ? 'icon-close' : 'icon-check';
            this.$("#icon-message-username").removeClass().addClass(iconClass);
        };

        this.checkExistedCustomer(username, onSuccess);
    }

    blurEventSourceAccount = () => {
        const onSucess = (result: number) => {
            const iconClass = result > 0 ? 'icon-check' : 'icon-close';
            this.$("#icon-message-SourceUsername").removeClass().addClass(iconClass);
            if (result > 0) {
                this.getCustomerCredit(this.$sourceUsername.val());
            }
        };

        this.checkExistedCustomer(this.$sourceUsername.val(), onSucess);
    }

    getCustomerCredit = (username: string) => {
        this.$.blockUI();

        this.$.ajax({
            method: "POST",
            headers: this.getHeadersForPostRequest(),
            data: { custName: username },
            dataType: 'json',
            url: "CopyAccount/GetCredit",
            success: result => {
                this.$.unblockUI();


                this.creditMin = 0;
                this.creditMax = result.MaxCredit;
                this.givenCreditMustBeBetweenMinMaxMessage = result.GivenCreditMustBeBetweenMinMaxMessage;

                this.$creditRange.removeClass('hidden');
                this.$creditMinLabel.html(numeral(this.creditMin).format(this.numberFormat));
                this.$creditMaxLabel.html(numeral(this.creditMax).format(this.numberFormat));

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

    checkExistedCustomer = (username: string, onSuccess: (result: number) => void) => {
        this.$.ajax({
            method: "POST",
            headers: this.getHeadersForPostRequest(),
            data: { username: username },
            dataType: 'json',
            url: "CopyAccount/CheckUsername",
            success: (result) => {
                onSuccess(result);
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

    checkErrorAndShowMessage = () => {
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