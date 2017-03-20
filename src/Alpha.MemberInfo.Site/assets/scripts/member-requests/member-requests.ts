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
            let disableProduct = self.$('option:selected', this).attr('data-disabled') == "True";
            self.renderData();
        })

    };

    renderData = () => {
        let productId = this.$('select#dropdown-product option:selected').val();
        let disableProduct = this.$('select#dropdown-product option:selected').attr('data-disabled') == "True";
        this.loadInfo("#member-requests", productId, disableProduct, this.registerEvents);
    }

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

    toggleEnabledButton = () => {
        let disabled = this.$('input.checkbox:checked').length > 0 ? false : true;
        this.$('.btn-default').prop("disabled", disabled);
    };

    private loadInfo = (id: string, productId: string, disableProduct: boolean, handleOnSuccessLoading: any) => {
        let $component = this.$(id),
            url = $component.attr('action-url');

        if (url !== '' && typeof url !== 'undefined') {
            const ajaxSettings = this.createAjaxSettings(url + "?UserSelectedProductId=" + productId + "&DisabledUserSelectedProduct=" + disableProduct, 'GET', $component, handleOnSuccessLoading);

            this.$.ajax(ajaxSettings);
        }
    }

    private createAjaxSettings = (url: string, method: string, component: any, handleOnSuccessLoading: any): JQueryAjaxSettings => {
        const ajaxSetting: JQueryAjaxSettings = {
            method: method,
            url: url,
            cache: false,
            success: (responseHtml) => {
                component.html(responseHtml);
                handleOnSuccessLoading();
            }
        };

        return ajaxSetting;
    }
}


const memberRequests = new MemberRequests(
    $,
    new SortComponent($));

$(function () {
    memberRequests.init();
});