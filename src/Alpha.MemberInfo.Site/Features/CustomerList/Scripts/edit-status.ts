"use strict";
class EditStatus {
    private $: JQueryStatic;
    private topWindow: any;
    private customerListServices: CustomerListServices;
    private customerListPopup: CustomerListPopup;
    private isMobile: boolean;

    constructor(
        $: JQueryStatic,
        topWindow: any,
        isMobile: boolean,
        customerListServices: CustomerListServices,
        customerListPopup: CustomerListPopup) {
        this.$ = $;
        this.topWindow = topWindow;
        this.isMobile = isMobile;
        this.customerListServices = customerListServices;
        this.customerListPopup = customerListPopup;
    }

    public bindingEvent = () => this.$('.status-popup').unbind().bind('click', event => this.openPopup(this.$(event.currentTarget)));

    private openPopup = element => this.customerListServices.getSettings(
        this.$(element).data("url"),
        data => {
            const username = this.$(element).data("username"),
                custId = this.$(element).data("custid"),
                content = this.buildPopupHtml(data.Status, username, custId),
                title = `${this.$('#editStatusText').val()} - ${username}`;

            this.customerListPopup.openStatusSettingPopup(element, content, title, this.bindingPopupEvent)
        }
    );

    private buildPopupHtml = (statusArray, username, custId) => {
        let html = ['<ul>'];
        this.$.each(statusArray, (key, item) => {
            if (item.ShowInCustomerList) {
                const attrs = {
                    'class': `edit-status-popup ${item.Disabled ? 'grey-color' : ''}`,
                    'data-disabled': item.Disabled,
                    'data-custid': custId,
                    'data-username': username,
                    'data-reloadcustomerlist': item.ReloadCustomerList,
                    'data-url': item.Url,
                    'data-ispopup': item.IsPopup,
                    'data-popupheadertext': item.PopupHeaderText,
                    'data-params': item.JsonParams
                };

                html.push(
                    `<li data-message="${item.ConfirmMessage}" ${buildAttributes(attrs)}><input ${item.Disabled ? 'disabled' : ''} type="checkbox" ${item.Checked ? 'checked' : ''}/> ${item.Text}</li>`);
            }
        });

        return html.join('');
    }

    private bindingPopupEvent = parentElement => {
        this.$('.edit-status-popup').click(event => {
            const isOpenPopup = JSON.parse(this.$(event.currentTarget).data("ispopup"));
            if (isOpenPopup) {
                const popupHeaderText = this.$(event.currentTarget).data("popupheadertext");
                const username = this.$(event.currentTarget).data("username");
                this.$(event.currentTarget).data('title', popupHeaderText.replace('<username>', username));
                this.customerListPopup.openProductPopup(event.currentTarget)
            }
            else {
                if (!JSON.parse(this.$(event.currentTarget).data("disabled"))) {
                    this.topWindow.bootboxConfirm(
                        this.$(event.currentTarget).data("message"),
                        event.currentTarget,
                        () => this.customerListServices.updateCustomerStatus(
                            event.currentTarget,
                            this.$(parentElement).context,
                            this.updateCustomerStatusSuccess));
                }
            }
        })
    }

    private updateCustomerStatusSuccess = (currentTarget, parentTarget, data, reloadcustomerlist) => {
        if (data.IsSuccessful) {
            if (reloadcustomerlist && this.topWindow.customerListNew) {
                this.topWindow.successfulUpdatingCallbackAndReloadMainFrame(true);
            }
            else {
                this.$.unblockUI();
            }

            this.customerListPopup.closeStatusPopup();
        }
        else {
            this.$.unblockUI();
            this.topWindow.bootboxAlert(data.Message, this.isMobile ? currentTarget : parentTarget);
        }
    }
}