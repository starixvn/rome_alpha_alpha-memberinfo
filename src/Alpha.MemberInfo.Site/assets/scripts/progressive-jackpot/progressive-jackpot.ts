/// <reference path="../components/productmultiselection.ts" />
"use strict";

class ProgressiveJackpot {
    $: JQueryStatic;
    topWindow: any;
    productMultiSelectionComponent: ProductMultiSelectionComponent;
    $dropdownProduct: JQuery;
    $tableReport: JQuery;

    constructor(
        $: JQueryStatic,
        topWindow: any,
        productMultiSelectionComponent: ProductMultiSelectionComponent) {
        this.$ = $;
        this.topWindow = topWindow;
        this.productMultiSelectionComponent = productMultiSelectionComponent;
    }

    init = () => {
        this.cacheDOMElements();
        this.productMultiSelectionComponent.render();
    }

    cacheDOMElements = () => {
        this.$dropdownProduct = this.$("#dropdown-product");
    };
}

const progressiveJackpot = new ProgressiveJackpot($, window.top, new ProductMultiSelectionComponent($));

$(() => {
    progressiveJackpot.init();
});