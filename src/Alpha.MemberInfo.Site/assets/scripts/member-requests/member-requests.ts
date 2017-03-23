"use strict";
declare var bootbox: any;
class MemberRequests {
    $: JQueryStatic;
    $dropdownContent: JQuery;
    $messageContent: JQuery;
    $enableButton: JQuery;
    $enableProductUrl: string;
    $tableContent: JQuery;

    $selectedAll: JQuery

    $successfullyMessage: string;
    $failMessage: string;
    $waringMessage: string;

    $successfullyMessageClass: string;
    $failMessageClass: string;
    $waringMessageClass: string;

    constructor($: JQueryStatic) {
        this.$ = $;
    }

    init = () => {
        this.registerEventsForFilter();
        this.renderData();
    };

    cacheFilterDOMElements = () => {
        this.$dropdownContent = this.$('#dropdown-product');
        this.$messageContent = this.$('#status-message-content');
        this.$enableButton = this.$('#enable-product');
        this.$enableProductUrl = this.$("#enableProductUrl").val();
        this.$tableContent = this.$("#member-requests");

        this.$successfullyMessage = this.$("#successfullyMessage").val();
        this.$failMessage = this.$("#failyMessage").val();
        this.$waringMessage = this.$("#warningMessage").val();

        this.$successfullyMessageClass = "msg-success";
        this.$failMessageClass = "msg-error";
        this.$waringMessageClass = "msg-warning";
    };

    registerEventsForFilter = () => {
        let self = this;
        this.cacheFilterDOMElements();
        this.$dropdownContent.change(function () {
            self.setEnabledButtonStatus(true);
            self.isDisabledProduct() ? self.showWarningMessage() : self.$messageContent.hide();
            self.renderData();
        })

        this.$enableButton.on("click", this.confirmMessage);
    };

    cacheTableDOMElements = () => {
        this.$selectedAll = this.$('#select-all');
    };

    registerEventsForTable = () => {
        let self = this;
        this.cacheTableDOMElements();

        this.$selectedAll.change(function () {
            let status = this.checked;
            self.$('.checkbox').each(function () {
                if (!this.disabled) {
                    this.checked = status;
                }
            });

            self.toggleEnabledButton();
        });

        this.$('.checkbox').change(function () {
            self.toggleEnabledButton();
        });
    };

    toggleEnabledButton = () => this.setEnabledButtonStatus(this.$('input.checkbox:checked').length > 0 ? false : true);

    setEnabledButtonStatus = (isDisabled) => this.$enableButton.prop("disabled", isDisabled);

    getProductId = () => this.$('select#dropdown-product option:selected').val();

    getProductName = () => this.$('select#dropdown-product option:selected').attr('data-productname');

    isDisabledProduct = () => this.$('select#dropdown-product option:selected').attr('data-disabled') == "True";

    createMessageContainer = (message: string) => "<div>" + message + "</div>";

    renderData = () => {
        let self = this;
        let memberRequestUrl = self.$tableContent.attr('action-url');

        if (memberRequestUrl !== '' && typeof memberRequestUrl !== 'undefined') {
            let ajaxSetting: JQueryAjaxSettings = {
                method: 'GET',
                data: { UserSelectedProductId: self.getProductId(), DisabledUserSelectedProduct: self.isDisabledProduct() },
                url: memberRequestUrl,
                cache: false,
                success: (responseHtml) => {
                    self.$tableContent.html(responseHtml);
                    self.registerEventsForTable();
                }
            };

            this.$.ajax(ajaxSetting);
        }
    }

    confirmMessage = () => {
        let self = this;
        bootbox.confirm("Are you sure to enable Product " + this.getProductName() + " for user " + this.getMemberNames() + " ?", function (result) {
            if (result) {
                self.enableProduct();
            }
        });
    }

    enableProduct = () => {
        let self = this;
        let ajaxSetting: JQueryAjaxSettings = {
            method: 'POST',
            data: { MembersInfo: self.getMemberInfoParam(), UserSelectedProductId: self.getProductId() },
            dataType: 'json',
            url: self.$enableProductUrl,
            cache: false,
            success: (results) => {
                let message = "";
                if (typeof results != 'undefined') {
                    let length = results.length;
                    let isError = false;
                    for (let i = 0; i < length; i++) {
                        if (!results[i].IsSuccessful) {
                            isError = true;
                            message += self.createMessageContainer(results[i].Message);
                        }
                    }

                    if (!isError) {
                        self.showSuccessfullyMessage();
                    } else {
                        self.showFailMessage(message);
                    }
                } else {
                    self.showFailMessage(self.$failMessage);
                }
            }
        };

        this.$.ajax(ajaxSetting);
    }

    getMemberInfoParam = () => {
        let self = this;
        let membersInfoArray = [];

        this.$('input.checkbox:checked').each(function () {
            membersInfoArray.push(self.$(this).attr("data-userInfo"));
        });

        return membersInfoArray.toString();
    };

    getMemberNames = () => {
        let self = this;
        let memberArray = [];

        this.$('input.checkbox:checked').each(function () {
            memberArray.push(self.$(this).attr("data-username"));
        });

        return memberArray.join(', ');
    };

    showSuccessfullyMessage = () => {
        this.clearMessageContent();
        this.$messageContent.show().addClass(this.$successfullyMessageClass).html(this.$successfullyMessage);
    };

    showFailMessage = (failMessage: string) => {
        this.clearMessageContent();
        this.$messageContent.show().addClass(this.$failMessageClass).html(failMessage);
    };

    showWarningMessage = () => {
        this.clearMessageContent();
        this.$messageContent.show().addClass(this.$waringMessageClass).html(this.$waringMessage);
    };

    clearMessageContent = () => this.$messageContent.removeClass(this.$successfullyMessageClass).removeClass(this.$failMessageClass).removeClass(this.$waringMessageClass).empty();
}

const memberRequests = new MemberRequests($);

$(function () {
    memberRequests.init();
});