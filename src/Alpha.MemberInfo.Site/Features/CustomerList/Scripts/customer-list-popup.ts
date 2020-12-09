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
        this.$bootstrapModalDialog = this.$('#bootstrap-modal .modal-dialog');
    }

    public openProductPopup = (currentTarget, method = 'GET') => {
        this.$.blockUI();

        this.$('#productIframe').attr('src', '');
        this.$('#productIframeForm').attr('action', this.$(currentTarget).data("url"));
        this.$('#productIframeForm').attr('method', method);

        const params = this.$(currentTarget).data("params");

        this.$('#productIframeForm').empty();

        if (params !== undefined && params !== null) {
            const attrs = this.$.map(params, (value, key) => `<input type="hidden"  name='${key}' id='${key}' value='${value}'/>`).join(' ');

            this.$('#productIframeForm').append(attrs);
        }

        this.$('#productIframeForm').submit();

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
            this.$('#productIframe').contents().find(headerClassesOrId).css('display', 'none');

            if (this.$('#productIframe').contents().find('#txtSecCode')) {
                this.$('#productIframe').contents().find('#txtSecCode').focus();
            }
        }, 100);
    }

    public bindingBootstrapModalEvent = () => {
        //this.$bootstrapContainer.unbind().bind('mouseleave ', () => this.$bootstrapContainer.modal('hide'));

        this.$('#bootstrap-modal-product').on('hidden.bs.modal', () => {
            this.$('#productIframe').attr('src', 'about:blank');
        });

        this.$('#bootstrap-modal-product').on('shown.bs.modal', () => {
            this.initFrame.apply(this.$('#productIframe').contents());
        });
    }

    public closeStatusPopup = () => this.$bootstrapContainer.modal('hide');

    public closeProductPopup = () => this.$('#bootstrap-modal-product').modal('hide');

    public successfulUpdatingCallback = () => {
        this.$('#productIframeForm').submit();
    }

    public successfulUpdatingCallbackSportbook = () => {
        this.$('#productIframeForm').submit();
        this.$('.btn-submit').click();
    }

    public openBootstrapModal = (element, data, buildPopupHtml, bindingPopupEvent) => {
        buildPopupHtml(data, this.$(element).data("username"), this.$(element).data("custid"));
        bindingPopupEvent(element);

        if (this.isMobile) {
            this.$bootstrapModalDialog.css({ height: '100%', width: '100%' });

            this.$bootstrapContainer
                .css({ top: 0, left: 0, height: '100%', width: '100%', paddingRight: '0px' })
                .modal({ show: true, backdrop: false });
        }
        else {
            this.$bootstrapContainer
                .css({ height: 'auto', width: 'auto', top: 0 })
                .modal({ show: true, backdrop: false });

            this.$bootstrapModalDialog.css({ width: 'inherit' });
            const height = this.$bootstrapModalDialog.outerHeight(),
                width = this.$bootstrapModalDialog.outerWidth(),
                bodyHeight = this.$(this.topWindow.parent.frames["main"]).height();

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