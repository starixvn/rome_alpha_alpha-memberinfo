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
                self.$('#status-message-content').show().removeClass("msg-success").removeClass("msg-error").addClass("msg-warning").empty().html(self.$("#warningMessage").val());
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

    toggleEnabledButton = () => {
        let disabled = this.$('input.checkbox:checked').length > 0 ? false : true;
        this.$('.btn-default').prop("disabled", disabled);
    };

    enableProduct = () => {
        let self = this;
        let $component = this.$("#status-message-content");

        let userInfo = "";
        this.$('input.checkbox:checked').each(function () {
            userInfo += self.$(this).attr("data-userInfo") + ",";
        });

        let ajaxSetting: JQueryAjaxSettings = {
            method: 'POST',
            data: { CustIds: userInfo, UserSelectedProductId: 5 },
            dataType: 'json',
            url: self.$("#enableProductUrl").val(),
            cache: false,
            success: (results) => {
                $component.empty().show().removeClass('msg-warning');

                let html = "";
                if (typeof results != 'undefined') {
                    let length = results.length;
                    let isError = false;
                    for (let i = 0; i < length; i++) {
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
        $messageContainer.removeClass('msg-error').addClass('msg-success').empty().html(successfullyMessage);
    };

    showFailMessage = ($messageContainer: any, failMessage: string) => {
        $messageContainer.removeClass('msg-success').addClass('msg-error').empty().html(failMessage);
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
}

const memberRequests = new MemberRequests(
    $,
    new SortComponent($));

$(function () {
    memberRequests.init();
});