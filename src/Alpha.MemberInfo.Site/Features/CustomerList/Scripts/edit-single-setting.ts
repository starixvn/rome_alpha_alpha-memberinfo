"use strict";

class EditSingleSetting {
    private $: JQueryStatic;
    private customerListPopup: CustomerListPopup;
    private customerListServices: CustomerListServices;

    constructor(
        $: JQueryStatic,
        customerListPopup: CustomerListPopup,
        customerListServices: CustomerListServices) {
        this.$ = $;
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
            data => {
                const username = this.$(element).data("username"),
                    content = this.buildPopupHtml(data.Settings, username),
                    title = `${this.$('#editSettingText').val()} - ${username}`;

                this.customerListPopup.openStatusSettingPopup(element, content, title, this.bindingPopupEvent)
            });
    }

    private buildPopupHtml = (settings, username) => {
        let html = ['<ul>'];
        this.$.each(settings, (key, item) => {
            const title = `${item.AddEditTextToPopupTitle ? this.$('#editText').val() : ''} ${item.Text} - ${username}`;
            html.push(`<li data-title="${title}" data-method='${item.Method}' data-params='${JSON.stringify(item.QueryStringParamaters)}' class="edit-popup ${item.Icon}" data-url="${item.Url}">${item.Text}</li>`);
        });

        return html.join('');
    }

    private bindingPopupEvent = () => this.$('.edit-popup').click(event => this.customerListPopup.openProductPopup(event.currentTarget, this.$(event.currentTarget).data("method")))
}