"use strict";

class EditSingleSetting {
    private $: JQueryStatic;
    private customerListPopup: CustomerListPopup;
    private customerListServices: CustomerListServices;
    private isMobile: boolean;

    constructor(
        $: JQueryStatic,
        isMobile: boolean,
        customerListPopup: CustomerListPopup,
        customerListServices: CustomerListServices) {
        this.$ = $;
        this.isMobile = isMobile;
        this.customerListPopup = customerListPopup;
        this.customerListServices = customerListServices;
    }

    public bindingEvent = () => this.$('.edit').unbind().bind('click', event => this.openPopup(this.$(event.currentTarget)));

    public openPopup = element => {
        this.$('.edit-multiple').data("custid", '');
        this.$('.edit-multiple').data("url", '');
        this.$('#arrayCustID').val('');
        this.$('#arrayUserName').val('');

        this.customerListServices.getSettings(
            this.$(element).data("url"),
            data => this.customerListPopup.openBootstrapModal(
                element,
                data.Settings,
                this.buildPopupHtml,
                this.bindingPopupEvent));
    }

    private buildPopupHtml = (settings, username) => {
        let html = ['<ul>'];
        this.$.each(settings, (key, item) => {
            const title = `${item.AddEditTextToPopupTitle ? this.$('#editText').val() : ''} ${item.Text} - ${username}`;
            html.push(`<li data-title="${title}" data-method='${item.Method}' data-params='${JSON.stringify(item.QueryStringParamaters)}' class="edit-popup ${item.Icon}" data-url="${item.Url}">${item.Text}</li>`);
        });

        this.$('#bootstrap-modal').find('.modal-body').empty().append(html.join(''));

        if (this.isMobile) {
            this.$('#bootstrap-modal').find('.modal-title').empty().append(`${this.$('#editSettingText').val()} - ${username}`);
        }
    }

    private bindingPopupEvent = () => this.$('.edit-popup').click(event => this.customerListPopup.openProductPopup(event.currentTarget, this.$(event.currentTarget).data("method")))
}