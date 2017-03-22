/// <reference path="../components/sort.ts" />

"use strict";

class MemberRequests {
    $: JQueryStatic;
    $selectAll: JQuery;

    sortComponent: SortComponent;

    constructor(
        $: JQueryStatic,
        sortComponent: SortComponent) {
        this.$ = $;
        this.sortComponent = sortComponent;
    }

    init = () => {
        let self = this;

        this.renderData();

        this.$("#dropdown-product").change(function () {
            let disableProduct = self.$('select#dropdown-product option:selected').attr('data-disabled') == "True";
            self.$('.btn-default').prop("disabled", disableProduct);

            if (disableProduct) {
                self.$('#status-message-content').removeClass("msg-success").removeClass("msg-error").addClass("msg-warning").empty().html(self.$("#warningMessage").val()).show();
            }
            else {
                self.$('#status-message-content').hide();
            }

            self.renderData();
        })

        this.$("#enable-product").on("click", this.enableProduct);
    };

    cacheDOMElements = () => {
        this.$selectAll = this.$('#select-all');
    };

    registerEvents = () => {
        let self = this;

        this.cacheDOMElements();

        this.$selectAll.change(function () {
            var status = this.checked;
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

    enableProduct = () => {
        let self = this;
        let $component = this.$("#status-message-content");
        let ajaxSetting: JQueryAjaxSettings = {
            method: 'POST',
            data: { CustIds: "125,1456,15678,1234", UserSelectedProductId: 5 },
            dataType: 'json',
            url: self.$("#enableProductUrl").val(),
            cache: false,
            success: (results) => {
                $component.empty().show().removeClass('msg-warning');

                let html = "";
                if (typeof results != 'undefined') {
                    var length = results.length;
                    var isError = false;
                    for (var i = 0; i < length; i++) {
                        if (!results[i].IsSuccessful) {
                            isError = true;
                            html += '<div>' + results[i].Message + '</div>';
                        }
                    }

                    if (!isError) {
                        self.showSuccessfullyMessage($component, self.$("#successfullyMessage").val());
                    } else {
                        self.showFailMessage($component, html);
                    }
                } else {
                    self.showSuccessfullyMessage($component, self.$("#failyMessage").val());
                }
            }
        };

        this.$.ajax(ajaxSetting);
    }

    showSuccessfullyMessage = ($messageContainer: any, successfullyMessage: string) => {
        $messageContainer.removeClass('msg-error');
        $messageContainer.addClass('msg-success').empty().html(successfullyMessage);
    };

    showFailMessage = ($messageContainer: any, failMessage: string) => {
        $messageContainer.removeClass('msg-success');
        $messageContainer.addClass('msg-error').empty().html(failMessage);
    };

    renderData = () => {
        let self = this;
        let productId = this.$('select#dropdown-product option:selected').val();
        let disableProduct = this.$('select#dropdown-product option:selected').attr('data-disabled') == "True";
       
        let $component = this.$("#member-requests");
        let url = $component.attr('action-url');

        if (url !== '' && typeof url !== 'undefined') {
            let ajaxSetting: JQueryAjaxSettings = {
                method: 'GET',
                data: { UserSelectedProductId: productId, DisabledUserSelectedProduct: disableProduct },
                url: $component.attr('action-url'),
                cache: false,
                success: (responseHtml) => {
                    $component.html(responseHtml);
                    self.registerEvents();
                }
            };

            this.$.ajax(ajaxSetting);
        }

    }

    toggleEnabledButton = () => {
        let disabled = this.$('input.checkbox:checked').length > 0 ? false : true;
        this.$('.btn-default').prop("disabled", disabled);
    };

    private loadInfo = (id: string, productId: string, disableProduct: boolean, handleOnSuccessLoading: any) => {
        let $component = this.$(id),
            url = $component.attr('action-url');

        if (url !== '' && typeof url !== 'undefined') {

            //const ajaxSettings = this.createAjaxSettings(url + "?UserSelectedProductId=" + productId + "&DisabledUserSelectedProduct=" + disableProduct, 'GET', $component, handleOnSuccessLoading);

           let ajaxSetting: JQueryAjaxSettings = {
               method: 'GET',
               data: { UserSelectedProductId: productId, DisabledUserSelectedProduct: disableProduct },
                url: url,
                cache: false,
                success: (responseHtml) => {
                    $component.html(responseHtml);
                    handleOnSuccessLoading();
                }
            };

           this.$.ajax(ajaxSetting);
        }
    }
}


const memberRequests = new MemberRequests(
    $,
    new SortComponent($));

$(function () {
    memberRequests.init();
});