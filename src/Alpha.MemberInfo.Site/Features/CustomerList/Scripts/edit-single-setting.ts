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
            const attrs = {
                'data-title': `${item.AddEditTextToPopupTitle ? this.$('#editText').val() : ''} ${item.Text} - ${username}`,
                'class': `edit-popup ${item.Icon} ${item.Enabled ? '' : 'grey-color'}`,
                'data-method': item.Method,
                'data-params': JSON.stringify(item.QueryStringParamaters),
                'data-url': item.Url,
                'data-show-guide-line': item.ShowGuideLine,
            };

            html.push(`<li ${buildAttributes(attrs)}>${item.Text}</li>`);
        });

        return html.join('');
    }

    private bindingPopupEvent = () => this.$('.edit-popup').click(event => this.customerListPopup.openProductPopup(event.currentTarget, this.$(event.currentTarget).data("method")))
}