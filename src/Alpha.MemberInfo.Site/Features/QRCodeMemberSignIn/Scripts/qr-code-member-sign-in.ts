"use strict";

declare var clipboard: any;

class QRCodeMemberSignIn {
    $: JQueryStatic;
    topWindow: any;
    username: string;
    credit: number;
    memberLink: string;

    constructor(
        $: JQueryStatic,
        topWindow: any) {
        this.$ = $;
        this.topWindow = topWindow;
    }

    getMemberCredit = () => {
        this.$.blockUI();

        var chooseUsername = this.$('#Username').val();
        this.username = chooseUsername;

        var param = {
            SuggestionQuery: chooseUsername
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

                this.credit = parseFloat(result.Credit);
            }
        });
    }

    init = () => {
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

        this.$('#Username').easyAutocomplete(options);

        const memberName = this.$('#Username').val();
        if (memberName !== '' && memberName !== undefined) {
            this.getMemberCredit();
        }

        this.$('#btn-copy-link').click(() => {
            clipboard.writeText(this.memberLink);
        });
    };
}

const qrCodeMemberSignIn = new QRCodeMemberSignIn($, window.top);

$(() => {
    qrCodeMemberSignIn.init();
});
