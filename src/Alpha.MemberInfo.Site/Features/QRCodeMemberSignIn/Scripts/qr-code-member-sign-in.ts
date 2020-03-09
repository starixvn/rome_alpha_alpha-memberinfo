"use strict";

declare var clipboard: any;

class QRCodeMemberSignIn {
    $: JQueryStatic;
    topWindow: any;
    username: string;
    firstname: string;
    credit: number;
    memberLink: string;

    constructor(
        $: JQueryStatic,
        topWindow: any) {
        this.$ = $;
        this.topWindow = topWindow;
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
                onChooseEvent: () => {
                    this.$.blockUI();

                    var chooseUsername = this.$('#txt-username').val();
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
                            this.$("#txt-member-credit").val(result.Credit);
                            this.$("#credit-min-label").html(result.CreditMin);
                            this.$("#credit-max-label").html(result.CreditMax);

                            this.credit = parseFloat(result.Credit);
                        }
                    });
                }
            }
        };

        this.$('#txt-username').easyAutocomplete(options);

        this.$('#btn-copy-link').click(() => {
            clipboard.writeText(this.memberLink);
        });

        this.$("#btn-submit").click(() => {
            this.$.blockUI();

            var param = {
            };

            this.$.ajax({
                method: "POST",
                data: param,
                url: "QRCodeMemberSignIn/CreateAccount",
                cache: false,
                success: (result) => {
                    this.$.unblockUI();
                    if (result.ErrCode === 0) {
                        this.$("#create-form").addClass("hidden");
                        this.$("#create-account-successfully").removeClass("hidden");

                        const obj = JSON.parse(result.ErrMsg);
                        this.$("#qr-code-image").attr("src", obj.QRCode);
                        this.memberLink = obj.MemberSiteLink;

                        this.firstname = this.$('#txt-first-name').val();
                        this.$("#first-name-label").html(this.firstname);
                        this.$("#credit-label").html(`${this.credit}`);
                        this.$("#username-label").html(this.username);
                    }
                    else {
                        // Show error message
                    }
                }
            });
        });
    };
}

const qrCodeMemberSignIn = new QRCodeMemberSignIn($, window.top);

$(() => {
    qrCodeMemberSignIn.init();
});
