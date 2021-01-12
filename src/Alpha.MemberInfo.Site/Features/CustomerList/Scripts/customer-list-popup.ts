"use strict";

class CustomerListPopup {
    private $: JQueryStatic;
    private $statusPopupContainer: JQuery;
    private $productPopupContainer: JQuery;
    private $productIframe: JQuery;
    private $productIframeForm: JQuery;
    private topWindow: any;
    private isMobile: boolean;

    constructor($: JQueryStatic, topWindow: any, isMobile: boolean) {
        this.$ = $;
        this.isMobile = isMobile;
        this.topWindow = topWindow;
        this.cacheDom();
    }

    private cacheDom = () => {
        this.$statusPopupContainer = this.$('#status-popup');
        this.$productPopupContainer = this.$('#product-popup');
        this.$productIframe = this.$('#productIframe');
        this.$productIframeForm = this.$('#productIframeForm');
    }

    public openProductPopup = (currentTarget, method = 'GET') => {
        this.$.blockUI();

        const url = this.$(currentTarget).data("url");
        this.$productIframe.attr('src', '');
        this.$productIframeForm.attr('action', url);
        this.$productIframeForm.attr('method', method);

        this.topWindow.productPopupUrl = url;
        this.topWindow[url] = () => {
            this.$productIframeForm.submit();
        };

        const params = this.$(currentTarget).data("params");

        this.$productIframeForm.empty();

        if (params !== undefined && params !== null) {
            const attrs = this.$.map(params, (value, key) => `<input type="hidden"  name='${key}' id='${key}' value='${value}'/>`).join(' ');
            this.$productIframeForm.append(attrs);
        }

        this.$productIframeForm.submit();

        const bodyWidth = this.$(window).width() - this.topWindow.$('#side-bar').width();

        let width = '100%';
        if (!this.isMobile) {
            width = bodyWidth < 1160 ? `${bodyWidth - 50}px` : '1160px';
        }

        const showGuideLineData = this.$(currentTarget).data("showGuideLine"),
            showGuideLine = showGuideLineData === undefined || showGuideLineData === '' ? false : JSON.parse(this.$(currentTarget).data("showGuideLine")),
            formartGuideLineUrl = showGuideLine ? `<a class='icon-information-outline icon-info' href='${this.$(currentTarget).data("guideLineUrl")}' target='_blank'/>` : '';

        this.$productPopupContainer.find('.modal-title').html(`<div>${this.$(currentTarget).data("title")}${formartGuideLineUrl}</div>`);
        this.$productPopupContainer
            .css({ height: '100%', width })
            .modal({ show: true, backdrop: false });


        this.$productPopupContainer.draggable({ containment: "window" });
        this.$.unblockUI();
    }

    private clearDuplicateHeader = () => {
        for (let i = 1; i <= 10; i++) {
            setTimeout(() => this.modifyContentPopup(i), 50 * i);
        }
    }

    private forcusInputElement = () => {
        var firstInput = this.$productIframe.contents().find('input[type=text],input[type=password],input[type=radio],input[type=checkbox],textarea,select').filter(':visible:first');
        if (firstInput != null) {
            firstInput.focus();
        }
    }

    private modifyContentPopup = i => {
        const headerClassesOrId = '.setting-header-agency,#header_main,#race-book-positiontaking-header,#title_header,#button_hr';
        this.$productIframe.contents().find(headerClassesOrId).css('display', 'none');
    }

    public bindingBootstrapModalEvent = () => {
        this.$statusPopupContainer.unbind().bind('mouseleave ', () => this.$statusPopupContainer.modal('hide'));

        this.$productPopupContainer.on('hidden.bs.modal', () => {
            this.$productIframe.attr('src', 'about:blank');
        });

        this.$productPopupContainer.on('show.bs.modal', () => {
            this.clearDuplicateHeader.apply(this.$productIframe.contents());
        });

        this.$productPopupContainer.on('shown.bs.modal', () => {
            this.forcusInputElement.apply(this.$productIframe.contents());
        });
    }

    public closeStatusPopup = () => this.$statusPopupContainer.modal('hide');

    public closeProductPopup = () => this.$productPopupContainer.modal('hide');

    public successfulUpdatingCallback = () => {
        setTimeout(() => {
            this.$productIframeForm.submit();
        }, 1500);
    }

    public successfulUpdatingCallbackAndReloadMainFrame = () => {
        setTimeout(() => {
            this.successfulUpdatingCallback();
            this.$('.btn-submit').click();
        }, 1500);
    }

    public openStatusSettingPopup = (element, content, title, bindingStatusSettingEvent) => {
        this.buildStatusSettingPopupHtml(content, title);

        bindingStatusSettingEvent(element);

        const $bootstrapModalDialog = this.$statusPopupContainer.find('.modal-dialog');

        if (this.isMobile) {
            $bootstrapModalDialog.css({ height: '100%', width: '100%' });

            this.$statusPopupContainer
                .css({ top: 0, left: 0, height: '100%', width: '100%', paddingRight: '0px' })
                .modal({ show: true, backdrop: false });
        }
        else {
            this.$statusPopupContainer
                .css({ height: 'auto', width: 'auto' })
                .modal({ show: true, backdrop: false });

            $bootstrapModalDialog.css({ width: 'inherit' });
            const height = $bootstrapModalDialog.outerHeight(),
                width = $bootstrapModalDialog.outerWidth(),
                bodyHeight = this.$(this.topWindow.parent.frames["main"]).height();

            let top = this.$(element).offset().top - this.$(document).scrollTop(),
                left = this.$(element).offset().left + this.$(element).width();

            if (bodyHeight - top < height) {
                top = bodyHeight - height;
            }

            if (this.$(window).width() - left < width) {
                left = left - width;
            }

            this.$statusPopupContainer.css({ top, left, height, width });
        }
    }

    public buildStatusSettingPopupHtml = (content, title) => {
        this.$statusPopupContainer.find('.modal-body').empty().append(content);

        if (this.isMobile) {
            this.$statusPopupContainer.find('.modal-title').empty().append(title);
        }
    }
}