"use strict";
declare var bootbox: any;

class MemberRequests {
    $: JQueryStatic;
    $dropdownContent: JQuery;
    $messageContent: JQuery;
    $enableButton: JQuery;
    $enableProductUrl: string;
    $memberRequestRootUrl: string;
    $tableContent: JQuery;
    $settingLink: JQuery;
    $oldMainRootPath: string;

    $selectedAll: JQuery

    $successfullyMessage: string;
    $failMessage: string;
    $waringMessage: string;
    $confirmMessage: string;

    $successfullyMessageClass: string;
    $failMessageClass: string;
    $waringMessageClass: string;
    topWindow: any;

    constructor($: JQueryStatic, topWindow: any) {
        this.$ = $;
        this.topWindow = topWindow;
    }

    init = () => {
        this.registerEventsForFilter();
        this.isDisabledProduct() ? this.showWarningMessage() : this.$messageContent.hide();
        this.renderData();
    };

    cacheFilterDOMElements = () => {
        this.$dropdownContent = this.$('#dropdown-product');
        this.$messageContent = this.$('#status-message-content');
        this.$enableButton = this.$('#enable-product');
        this.$enableProductUrl = this.$("#enableProductUrl").val();
        this.$memberRequestRootUrl = this.$("#memberRequestRootUrl").val()
        this.$tableContent = this.$("#member-requests");

        this.$successfullyMessage = this.$("#successfullyMessage").val();
        this.$failMessage = this.$("#failyMessage").val();
        this.$waringMessage = this.$("#warningMessage").val();
        this.$confirmMessage = this.$("#enableProductConfirmMessage").val();

        this.$successfullyMessageClass = "msg-success";
        this.$failMessageClass = "msg-error";
        this.$waringMessageClass = "msg-warning";

        this.$oldMainRootPath = this.$("#OldMainRootPath").val()
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
        this.$settingLink = this.$('.link');
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
            self.toggleSelectAllCheckbox();
        });

        this.$settingLink.on("click", function () {
            let username = self.$(this).parent().text();
            self.topWindow.$("#account-info-header").click();
            self.topWindow.$("#accountinfo-content").contents().find("#txtUsername").val(username);
            self.topWindow.$("#accountinfo-content").contents().find(".icon-search").click();
        });
    };

    toggleSelectAllCheckbox = () => {
        var isSelectAll = this.$('input.checkbox').not(":checked").not(":disabled").length > 0 ? false : true;
        this.$selectedAll.prop('checked', isSelectAll);
    };

    toggleEnabledButton = () => this.setEnabledButtonStatus(this.$('input.checkbox:checked').length > 0 ? false : true);

    setEnabledButtonStatus = (isDisabled) => this.$enableButton.prop("disabled", isDisabled);

    getProductId = () => this.$('select#dropdown-product option:selected').val();

    getProductName = () => this.$('select#dropdown-product option:selected').attr('data-productname');

    isDisabledProduct = () => this.$('select#dropdown-product option:selected').attr('data-disabled') == "True";

    createMessageContainer = (message: string) => "<div>" + message + "</div>";

    refreshPage = (productId: number) => window.location.href = this.$memberRequestRootUrl + "?UserSelectedProductId=" + productId;

    renderData = () => {
        this.$.blockUI();
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
                    this.$.unblockUI();
                }
            };

            this.$.ajax(ajaxSetting);
        }
    }

    confirmMessage = () => {
        let self = this;
        var productNameRep = /getProductName/gi;
        var memberNamesRep = /getMemberNames/gi;
        let confirmMessage = this.$confirmMessage.replace(productNameRep, this.getProductName());
            
        bootbox.confirm(confirmMessage.replace(memberNamesRep, this.getMemberNames()) , function (result) {
            if (result) {
                self.enableProduct();
            }
        });
    }

    enableProduct = () => {
        this.$.blockUI();
        let self = this;
        let ajaxSetting: JQueryAjaxSettings = {
            contentType: 'application/json; charset=utf-8',
            method: 'POST',
            data: JSON.stringify({ Members: self.getMemberInfoParam(), UserSelectedProductId: self.getProductId() }),
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
                        setTimeout(self.refreshPage, 5000);
                    } else {
                        self.showFailMessage(message);
                    }
                } else {
                    self.showFailMessage(self.$failMessage);
                }
                this.$.unblockUI();
            }
        };

        this.$.ajax(ajaxSetting);
    }

    getMemberInfoParam = () => {
        let self = this;
        let membersInfoArray = [];

        this.$('input.checkbox:checked').each(function () {
            membersInfoArray.push({
                custId: self.$(this).attr("data-custId"),
                username: self.$(this).attr("data-username")
            });
        });

        return membersInfoArray;
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

const memberRequests = new MemberRequests($, window.top);

$(function () {
    memberRequests.init();
});