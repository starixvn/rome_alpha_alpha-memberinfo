/// <reference path="../components/single-selection-dropdown.ts" />
"use strict";

class MemberRequests {
    $: JQueryStatic;
    topWindow: any;
    singleSelectionDropdownComponent: SingleSelectionDropdownComponent;

    $dropdownProduct: JQuery;
    $msgWarning: JQuery;
    disabledProductWarningMessage: string;
    noSettingWarning: string;

    memberRequestUrl: string;
    $boxContent: JQuery;

    $accountInfoHeader: JQuery;
    $sideBar: JQuery;
    $accountInfoContent: JQuery;
    viewerId: number;
    editSettingUrl: string;

    constructor(
        $: JQueryStatic,
        topWindow: any,
        singleSelectionDropdownComponent: SingleSelectionDropdownComponent) {
        this.$ = $;
        this.topWindow = topWindow;
        this.singleSelectionDropdownComponent = singleSelectionDropdownComponent;
    }

    init = () => {
        this.cacheDOMElements();
        this.singleSelectionDropdownComponent.render(
            new SingleSelectionDropdownOption("dropdown-product", "UserSelectedProductId", this.changeProduct));

        this.registerEvents();
        this.changeProduct();
    };
    registerEvents = () => {
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
        this.$sideBar = this.topWindow.$(".sidebar");
        this.viewerId = parseInt(this.$("#viewerId").val(), 0);
        this.editSettingUrl = this.$("#editSettingUrl").val();
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
        let self = this;

        let $infoLinks = this.$(".view-member-info-link");
        $infoLinks.on("click", function (event: any) {
            self.viewMemberInfo(self.$(this));

            event.stopPropagation();
        });

        let $editSettingLink = this.$(".edit-setting:not([disabled])");
        $editSettingLink.on("click", function () {
            let $this = self.$(this);
            self.editSetting($this.data("custid"), $this.data("username"));
        });
    };
    viewMemberInfo = (element: JQuery) => {
        let self = this;

        let username = element.text();

        if (this.$sideBar.hasClass("collapsed") ||
            this.$sideBar.hasClass("hidden-mobile")) {
            this.topWindow.menuBar.collapseLeftPanel();
        }

        this.$accountInfoHeader.click();
        setTimeout(function () {
            self.$accountInfoContent = self.topWindow.$("#accountinfo-content").contents();
            self.$accountInfoContent.find("#txtUsername").val(username);
            self.$accountInfoContent.find(".icon-search").click();
        }, 300);
    };
    editSetting = (custId: number, username: string) => {
        var url = this.editSettingUrl + "/" + custId + "/" + this.viewerId + "?username=" + username;
        this.topWindow.popupManager.openByRelativeUrl(url, "", 1160);
        this.topWindow.clearPopupSubtitle();
    };
}

const memberRequests = new MemberRequests($, window.top, new SingleSelectionDropdownComponent($));

$(() => {
    memberRequests.init();
});