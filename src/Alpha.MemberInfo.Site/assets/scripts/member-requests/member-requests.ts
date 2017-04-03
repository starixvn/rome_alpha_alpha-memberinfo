"use strict";

class MemberRequests {
    $: JQueryStatic;
    topWindow: any;

    $dropdownProduct: JQuery;
    $msgWarning: JQuery;
    disabledProductWarningMessage: string;
    noSettingWarning: string;

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
    };

    cacheDOMElements = () => {
        this.$dropdownProduct = this.$("#dropdown-product");

        this.$msgWarning = this.$("#msg-warning");
        this.disabledProductWarningMessage = this.$("#disabledProductWarningMessage").val();
        this.noSettingWarning = this.$("#noSettingWarning").val();

    };
    changeProduct = () => {
        this.updateWarningMessage();
        this.renderData();
    };

    convertAttributeToBool = (attrValue: string) => !!attrValue && attrValue.toString().toLowerCase() === "true";
    updateWarningMessage = () => {
        var selectedProduct = this.$("#dropdown-product option:selected");
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
    renderData = () => {
        //this.$.blockUI();
        //let self = this;
        //let memberRequestUrl = self.$tableContent.attr('action-url');

        //if (memberRequestUrl !== '' && typeof memberRequestUrl !== 'undefined') {
        //        let ajaxSetting: JQueryAjaxSettings = {
        //            method: 'GET',
        //            data: { UserSelectedProductId: self.getProductId(), DisabledUserSelectedProduct: self.isDisabledProduct() },
        //            url: memberRequestUrl,
        //            cache: false,
        //            success: (responseHtml) => {
        //                self.$tableContent.html(responseHtml);
        //                self.registerEventsForTable();
        //                this.$.unblockUI();
        //            }
        //        };

        //        this.$.ajax(ajaxSetting);
        //    }
    }
}

const memberRequests = new MemberRequests($, window.top);

$(() => {
    memberRequests.init();
});