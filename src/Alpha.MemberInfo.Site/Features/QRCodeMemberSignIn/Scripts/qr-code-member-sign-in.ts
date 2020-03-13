"use strict";

declare var clipboard: any;

class QRCodeMemberSignIn {
    $: JQueryStatic;
    topWindow: any;
    username: string;
    memberLink: string;

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

                this.$("#Credit").val(result.Credit);

                this.$('#credit-range').removeClass('hidden');
                this.$("#credit-min-label").html(result.CreditMin);
                this.$("#credit-max-label").html(result.CreditMax);

                this.$("#CreditMin").val(result.CreditMin);
                this.$("#CreditMax").val(result.CreditMax);
            }
        });
    }

    init = () => {
        this.$('#qrCodeMemberSignInForm').submit(() => {
            this.$.blockUI();
        });

        const options = {
            url: function (phrase) {
                return 'QRCodeMemberSignIn/GetUsernamesSuggestion?SuggestionQuery=' + phrase;
            },
            getValue: 'UserName',
            requestDelay: 300,
            minCharNumber: 2,
            categories: [{
                maxNumberOfElements: 10
            }],
            list: {
                onChooseEvent: () => this.getMemberCredit()
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
