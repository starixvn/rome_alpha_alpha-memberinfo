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

    topWindow: any;
    username: string;
    memberLink: string;
    cleave: any;
    givenCreditMustBeBetweenMinMaxMessage: string;

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
        this.$form.submit(event => this.submitEvent(event));

        this.cleave = new Cleave('#Credit', {
            numeral: true,
            numeralDecimalMark: '.',
            delimiter: ','
        });
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

        const username = this.$usernameCopyFrom.val();
        if (username === '') {
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

        this.$.unblockUI();
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
                onChooseEvent: () => this.getMemberCredit(),
                maxNumberOfElements: 10
            }
        };

        this.$usernameCopyFrom.easyAutocomplete(options);
    }
}

const qrCodeMemberSignIn = new QRCodeMemberSignIn($, window.top);

$(() => {
    qrCodeMemberSignIn.init();
});
