"use strict";

class CustomerListPopup {
    private $: JQueryStatic;
    private $bootstrapContainer: JQuery;
    private $bootstrapModalDialog: JQuery;
    private topWindow: any;
    private isMobile: boolean;

    constructor($: JQueryStatic, topWindow: any, isMobile: boolean) {
        this.$ = $;
        this.isMobile = isMobile;
        this.topWindow = topWindow;
        this.cacheDom();
    }

    private cacheDom = () => {
        this.$bootstrapContainer = this.$('#bootstrap-modal');
        this.$bootstrapModalDialog = this.$('.modal-dialog');
    }

    public openProductPopup = (currentTarget, method = 'get') => {
        this.$.blockUI();

        this.$('#myIframe').attr('src', '');
        this.$('#myIframeForm').attr('action', this.$(currentTarget).data("url"));
        this.$('#myIframeForm').attr('method', method);

        const params = this.$(currentTarget).data("params");

        this.$('#myIframeForm').empty();

        if (params !== undefined && params !== null) {
            const attrs = this.$.map(params, (value, key) => `<input type="hidden"  name='${key}' id='${key}' value='${value}'/>`).join(' ');

            this.$('#myIframeForm').append(attrs);
        }

        this.$('#myIframeForm').submit();

        const bodyWidth = this.$(window).width() - this.topWindow.$('#side-bar').width();

        let width = '100%';
        if (!this.isMobile) {
            width = bodyWidth < 1160 ? `${bodyWidth - 50}px` : '1160px';
        }

        this.$('#bootstrap-modal-product .modal-title').text(this.$(currentTarget).data("title"));
        this.$('#bootstrap-modal-product')
            .css({ height: '100%', width })
            .modal({ show: true, backdrop: false });

        this.$.unblockUI();
    }

    private initFrame = () => {
        setTimeout(() => {
            const headerClassesOrId = '.setting-header-agency,#header_main,#race-book-positiontaking-header,#title_header,#button_hr';
            this.$('#myIframe').contents().find(headerClassesOrId).css('display', 'none');
        }, 100);
    }

    public bindingBootstrapModalEvent = () => {
        this.$bootstrapContainer.unbind().bind('mouseleave ', () => this.$bootstrapContainer.modal('hide'));

        this.$('#bootstrap-modal-product').on('hidden.bs.modal', () => {
            this.$('#myIframe').attr('src', 'about:blank');
        });

        this.$('#bootstrap-modal-product').on('shown.bs.modal', () => {
            this.initFrame.apply(this.$('#myIframe').contents());
        });
    }

    public closeStatusPopup = () => this.$bootstrapContainer.modal('hide');

    public openBootstrapModal = (element, data, buildPopupHtml, bindingPopupEvent) => {
        buildPopupHtml(data, this.$(element).data("custid"));
        bindingPopupEvent(element);

        if (this.isMobile) {
            this.$bootstrapContainer
                .find('.modal-dialog')
                .css({ height: '100%', width: '100%' });

            this.$bootstrapContainer
                .css({ top: 0, left: 0, height: '100%', width: '100%', paddingRight: '0px' })
                .modal({ show: true, backdrop: false });
        }
        else {
            this.$bootstrapContainer
                .css({ height: 'auto', width: 'auto', top: 0 })
                .modal({ show: true, backdrop: false });

            this.fixBootstrapDimension(element);
        }
    }

    private fixBootstrapDimension = element => {
        if (this.isMobile) {
            this.$bootstrapContainer.css({ top: 0, left: 0, height: '100%', width: '100%' });
        }
        else {
            this.$bootstrapModalDialog.css({ width: 'inherit' });
            const height = this.$bootstrapModalDialog.outerHeight(),
                width = this.$bootstrapModalDialog.outerWidth(),
                bodyHeight = this.$('body').height();

            let top = this.$(element).offset().top - this.$(document).scrollTop(),
                left = this.$(element).offset().left + this.$(element).width();

            if (bodyHeight - top < height) {
                top = bodyHeight - height;
            }

            if (this.$(window).width() - left < width) {
                left = left - width;
            }

            this.$bootstrapContainer.css({ top, left, height, width });
        }
    }
}