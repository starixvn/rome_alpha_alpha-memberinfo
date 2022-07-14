"use strict";

class IframePopup {
    private $: JQueryStatic;
    private topWindow: any;
    private $iframePopupContainer: JQuery;
    private $iframePopup: JQuery;
    private $iframePopupForm: JQuery;

    constructor(
        $,
        topWindow) {
        this.$ = $;
        this.topWindow = topWindow;
        this.cacheDom();
    }

    private cacheDom = () => {
        this.$iframePopupContainer = this.$("#iframe-popup-container");
        this.$iframePopup = this.$("#iframe-popup");
        this.$iframePopupForm = this.$("#iframe-popup-form");
    }

    public open = (width, height, title, url, params = {}, method = 'GET') => {
        this.$.blockUI();

        this.$iframePopup.attr('src', '');
        this.$iframePopupForm.attr('action', url);
        this.$iframePopupForm.attr('method', method);

        this.$iframePopupForm.empty();
        
        if (params !== undefined && params !== null) {
            const attrs = this.$.map(params, (value, key) => `<input type="hidden"  name='${key}' id='${key}' value='${value ? value : ""}'/>`).join(' ');
            this.$iframePopupForm.append(attrs);
        }

        this.$iframePopupForm.submit();

        let popupWidth = "100%";
        let popupHeight = "100%";
        if (!this.topWindow.alpha.isMobile) {
            popupWidth = width;
            popupHeight = height;
        }

        this.$iframePopupContainer.find('.modal-title').html(`<div>${title}</div>`);
        this.$iframePopupContainer
            .css({ height: popupHeight, width: popupWidth })
            .modal({ show: true, backdrop: false });

        this.$iframePopupContainer.draggable({ containment: "window" });
        this.$.unblockUI();
    }
}