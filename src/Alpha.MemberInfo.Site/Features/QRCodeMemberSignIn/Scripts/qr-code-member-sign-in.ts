"use strict";

declare var clipboard: any;

declare var Cleave: any;

class QRCodeMemberSignIn {
    $: JQueryStatic;
    topWindow: any;
    username: string;
    memberLink: string;
    cleave: any;

    constructor(
        $: JQueryStatic,
        topWindow: any) {
        this.$ = $;
        this.topWindow = topWindow;
    }

    getMemberCredit = () => {
        this.$.blockUI();

        var chooseUsername = this.$('#UsernameCopyFrom').val();
        this.username = chooseUsername;

        var param = {
            SearchAccountName: chooseUsername
        };

        this.$.ajax({
            method: "POST",
            data: param,
            url: "QRCodeMemberSignIn/GetMemberCredit",
            cache: false,
            success: (result) => {
                this.$.unblockUI();

                this.$('#credit-range').removeClass('hidden');
                this.$("#credit-min-label").html(result.CreditMin);
                this.$("#credit-max-label").html(result.CreditMax);

                this.$("#Credit").val(result.Credit);
                this.$("#CreditMin").val(result.CreditMin);
                this.$("#CreditMax").val(result.CreditMax);
            }
        });
    }

    init = () => {
        this.cleave = new Cleave('#Credit', {
            numeral: true,
            numeralDecimalMark: ',',
            delimiter: '.'
        });

        this.$('#qrCodeMemberSignInForm').submit(() => {
            this.$.blockUI();
            const credit = parseFloat(this.cleave.getRawValue());
            const creditMin = parseFloat(this.$("#CreditMin").val());
            const creditMax = parseFloat(this.$("#CreditMax").val());

            if (credit < creditMin || credit > creditMax) {
                event.preventDefault();

                this.$('#message').removeClass('hidden');
                this.$('#message-content').html('Invalid Credit');
                this.$.unblockUI();
            }
            else {
                this.$("#Credit").val(this.cleave.getRawValue());
            }
        });

        const options = {
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
                    const nickname = item.UserName2 === null || item.UserName2 === '' ? '' : `(${item.UserName2})`;
                    return `<div>${value} ${nickname}<div>${item.FirstName} ${item.LastName}</div></div>`;
                }
            },
            list: {
                onChooseEvent: () => this.getMemberCredit(),
                maxNumberOfElements: 10
            }
        };

        this.$('#UsernameCopyFrom').easyAutocomplete(options);

        const memberName = this.$('#UsernameCopyFrom').val();
        if (memberName !== '' && memberName !== undefined) {
            this.getMemberCredit();
        }
    };
}

const qrCodeMemberSignIn = new QRCodeMemberSignIn($, window.top);

$(() => {
    qrCodeMemberSignIn.init();
});
