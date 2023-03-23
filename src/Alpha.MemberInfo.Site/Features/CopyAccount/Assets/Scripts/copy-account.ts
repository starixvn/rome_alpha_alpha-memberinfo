declare var Cleave: any;
declare var numeral: any;
declare var Clipboard: any;

class CopyAccount {
    $: JQueryStatic;
    encryptor: Encryptor;
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
    $showPass: JQuery;

    $creditInput: JQuery;
    credit: number = 0;
    creditMin: number = 0;
    creditMax: number = 0;
    memberMaxCredit: number;

    $memberMaxCreditInput: JQuery;

    $messageContainer: JQuery;
    $messageContent: JQuery;

    topWindow: any;
    cleave: any;
    memberMaxCreditCleave: any;

    isCreateAgent: boolean;
    inValidSourceAccount: boolean;
    invalidCredit: boolean;
    invalidMemberMaxCredit: boolean = false;
    invalidUsername: boolean;
    usernameIsInvalid: string;

    givenCreditMustBeBetweenMinMaxMessage: string;

    maxAccountsCreatePerRequest: number;

    $btnSwitchVersion: JQuery;
    oldVersionUrl: string;
    numberFormat = '0,0.##';

    userNotExistMessage: string;
    clipboardUsernames: any;
    hashKey: string;

    constructor(
        $: JQueryStatic,
        encryptor: Encryptor,
        topWindow: any,
        clipboardUsernames: any) {
        this.$ = $;
        this.topWindow = topWindow;
        this.clipboardUsernames = clipboardUsernames;
        this.encryptor = encryptor;
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

        this.userNotExistMessage = this.$("#UserNotExistMessage").val();

        this.$showPass = this.$(".show-pass");
        this.hashKey = this.$("#HashKey").val();
    }

    showPass = () => {
        this.changeEyeIcon(this.$showPass);
        if (this.$password.hasClass("input-pass")) {
            this.$password.removeClass("input-pass")
        }
        else {
            this.$password.addClass("input-pass")
        }
    }

    changeEyeIcon = (eyeElm) => {
        if (eyeElm.hasClass("icon-eye-close")) {
            eyeElm.removeClass("icon-eye-close");
            eyeElm.addClass("icon-eye");
        }
        else {
            eyeElm.removeClass("icon-eye");
            eyeElm.addClass("icon-eye-close");
        }
    }

    bindingEvent = () => {
        this.bidingEasyAutoComplete();
        this.$form.submit(event => this.submitEvent(event));

        this.$firstSuffix.change(() => this.checkExistedUsernameByChangeSuffix());
        this.$secondSuffix.change(() => this.checkExistedUsernameByChangeSuffix());
        this.$thirdSuffix.change(() => this.checkExistedUsernameByChangeSuffix());
        this.$password.keyup(() => this.validatePassword());
        this.$showPass.click(this.showPass);

        this.$creditInput.keyup(() => {
            this.validateCredit();
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
                var memberMaxCredit = parseFloat(this.memberMaxCreditCleave.getRawValue());
                this.invalidMemberMaxCredit = this.isInValidCredit(memberMaxCredit, this.credit);
                if (!this.invalidMemberMaxCredit) {
                    this.memberMaxCredit = memberMaxCredit;
                }
                const errorClass = this.invalidMemberMaxCredit ? 'icon-close' : 'icon-check';
                this.$('#icon-message-member-max-credit').removeClass().addClass(errorClass);
            });
        }



        const self = this;
        if (window.document.queryCommandSupported
            && window.document.queryCommandSupported("copy")) {
            this.clipboardUsernames
                .on("success", function (e) {
                    self.$(e.trigger).tooltip("show");
                })
                .on("error", function (e) {
                    self.$(e.trigger).remove();
                    window.console.error("Action:", e.action);
                    window.console.error("Trigger:", e.trigger);
                });
        }
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
            const noOfAccount = parseInt(this.$noOfAccounts.val());
            const password = this.$.trim(this.$password.val());
            this.$submitBtn.prop('disabled', this.inValidSourceAccount || this.invalidCredit || this.invalidMemberMaxCredit || !this.isValidNoOfAccount(noOfAccount) || !this.isValidPassword(password));
        }

        this.$sourceUsername.on('input', validateInputs);
        this.$noOfAccounts.on('input', validateInputs);
        this.$password.on('input', validateInputs);
        this.$creditInput.on('input', validateInputs);
        this.$memberMaxCreditInput.on('input', validateInputs);
    }

    submitEvent = (event) => {
        event.preventDefault();

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
            Password: this.encryptor.encrypt(this.$password.val(), this.hashKey)
        };

        if (this.isCreateAgent) {
            data["MemberMaxCredit"] = this.$memberMaxCreditInput.val();
        }

        this.$.ajax({
            method: "POST",
            headers: this.getHeadersForPostRequest(),
            data: data,
            dataType: 'text',
            url: "CopyAccount/Index",
            success: (data) => {
                var result = this.safelyParseJSON(data);
                if (result && result.IsNotOk) {
                    this.showErrorMessage(result.Message);
                } else {
                    this.$result.append(data);
                    this.$result.removeClass("hidden");
                    this.$form.addClass("hidden");
                    this.$("#btnCreateMore").click(() => {
                        setTimeout(() => window.location.reload(), 1000)
                    });
                }
                this.$.unblockUI();
            },
            error: () => { this.$.unblockUI(); }
        });
    }

    safelyParseJSON = (json) => {
        var parsed;
        try {
            parsed = JSON.parse(json);
        } catch (e) { }

        return parsed;
    }

    checkExistedUsernameByChangeSuffix = () => {
        let username = `${this.$('#actor-name').text()}${this.$firstSuffix.val()}${this.$secondSuffix.val()}`;
        if (this.$thirdSuffix.val()) {
            username = `${username}${this.$thirdSuffix.val()}`;
        }

        this.$.ajax({
            method: "POST",
            headers: this.getHeadersForPostRequest(),
            data: { custName: username },
            dataType: 'json',
            url: "CopyAccount/CheckExistedCustomer",
            success: (result) => {
                this.invalidUsername = result;
                const iconClass = result ? 'icon-close' : 'icon-check';
                this.$("#icon-message-username").removeClass().addClass(iconClass);
            }
        });
    }

    blurEventSourceAccount = () => {
        var sourceUsername: string = this.$sourceUsername.val();
        if (sourceUsername && sourceUsername.length) {
            this.$.ajax({
                method: "POST",
                headers: this.getHeadersForPostRequest(),
                data: { custName: sourceUsername },
                dataType: 'json',
                url: "CopyAccount/CheckUsername",
                success: (result) => {
                    const iconClass = result ? 'icon-check' : 'icon-close';
                    this.$("#icon-message-SourceUsername").removeClass().addClass(iconClass);
                    this.inValidSourceAccount = !result;
                    if (result) {
                        this.getCustomerCredit(this.$sourceUsername.val());
                    } else {
                        this.showErrorMessage(this.userNotExistMessage);
                        this.$creditRange.addClass("hidden");
                    }
                }
            });
        }
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

                this.$.unblockUI();
            },
            error: () => { this.$.unblockUI(); }
        });
    }

    validateCredit = () => {
        var credit = parseFloat(this.cleave.getRawValue());
        this.invalidCredit = this.isInValidCredit(credit, this.creditMax);
        if (this.invalidCredit) {
            this.givenCreditMustBeBetweenMinMaxMessage && this.showErrorMessage(this.givenCreditMustBeBetweenMinMaxMessage);
        } else {
            this.credit = credit;
        }

        const errorClass = this.invalidCredit ? 'icon-close' : 'icon-check';
        this.$('#icon-message-credit').removeClass().addClass(errorClass);
    }

    isInValidCredit = (credit: number, max: number) => {
        return isNaN(credit) || credit < this.creditMin || credit > max;
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

const copyAccount = new CopyAccount($, new Encryptor(), window.top, new Clipboard(".icon-content-copy"));

$(() => {
    copyAccount.init();
});