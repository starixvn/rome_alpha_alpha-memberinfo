"use strict";

class MemberRequests {
    $: JQueryStatic;
    topWindow: any;

    $dropdownProduct: JQuery;
    $msgWarning: JQuery;
    disabledProductWarningMessage: string;
    noSettingWarning: string;

    memberRequestUrl: string;
    $boxContent: JQuery;

    $accountInfoHeader: JQuery;
    $accountInfoContent: JQuery;

    constructor($: JQueryStatic, topWindow: any) {
        this.$ = $;
        this.topWindow = topWindow;
    }

    init = () => {
        this.cacheDOMElements();
        this.registerEvents();
        this.changeProduct();
    };
    registerEvents = () => {
        this.$dropdownProduct.change(this.changeProduct);
        this.$("#icon-refresh").click(this.loadMemberRequests);
    };

    cacheDOMElements = () => {
        this.$dropdownProduct = this.$("#dropdown-product");

        this.$msgWarning = this.$("#msg-warning");
        this.disabledProductWarningMessage = this.$("#disabledProductWarningMessage").val();
        this.noSettingWarning = this.$("#noSettingWarning").val();

        this.memberRequestUrl = this.$("#memberRequestUrl").val();
        this.$boxContent = this.$("#box-content");

        this.$accountInfoHeader = this.topWindow.$("#account-info-header");
        this.$accountInfoContent = this.topWindow.$("#accountinfo-content").contents();
    };
    changeProduct = () => {
        this.updateWarningMessage();
        this.loadMemberRequests();
    };

    convertAttributeToBool = (attrValue: string) => !!attrValue && attrValue.toString().toLowerCase() === "true";
    updateWarningMessage = () => {
        var selectedProduct = this.getSelectedProduct();
        var isDisable = this.convertAttributeToBool(selectedProduct.data("disabled"));
        var hasSetting = this.convertAttributeToBool(selectedProduct.data("has-setting"));

        if (isDisable) {
            this.$msgWarning.html(this.disabledProductWarningMessage).removeClass("hide");
        } else if (!hasSetting) {
            this.$msgWarning.html(this.noSettingWarning).removeClass("hide");
        } else {
            this.$msgWarning.addClass("hide");
        }
    };
    loadMemberRequests = () => {
        this.$.blockUI();

        var selectedProduct = this.getSelectedProduct();
        var param = {
            UserSelectedProductId: selectedProduct.val(),
            DisabledUserSelectedProduct: this.convertAttributeToBool(selectedProduct.data("disabled"))
        };

        this.$.ajax({
            method: "GET",
            data: param,
            url: this.memberRequestUrl,
            cache: false,
            success: this.renderMemberRequests
        });
    };
    renderMemberRequests = (html: string) => {
        this.$boxContent.html(html);
        this.registerEventsForTable();
        this.$.unblockUI();
    };
    getSelectedProduct = () => this.$dropdownProduct.find("option:selected");
    registerEventsForTable = () => {
        let $infoLinks = this.$(".view-member-info-link");
        $infoLinks.on("click", this.viewMemberInfo);

        let $editSettingLink = this.$(".action-edit-setting");
        let self = this;
        $editSettingLink.on("click", function () {
            let $this = self.$(this);
            this.editSetting($this.data("custId"), $this.data("username"));
        });
    };
    viewMemberInfo = (event: any) => {
        let username = this.$(this).parent().text();
        this.topWindow.menuBar.collapseLeftPanel();
        this.$accountInfoHeader.click();
        setTimeout(function () {
            this.$accountInfoContent.find("#txtUsername").val(username);
            this.$accountInfoContent.find(".icon-search").click();
        }, 300);

        event.stopPropagation();
    };
    editSetting = (custId: number, username: string) => {

    };
}

const memberRequests = new MemberRequests($, window.top);

$(() => {
    memberRequests.init();
});