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
        data => this.customerListPopup.openBootstrapModal(element, data.Status, this.buildPopupHtml, this.bindingPopupEvent)
    );

    private buildPopupHtml = (statusArray, custId) => {
        let html = ['<ul>'];
        this.$.each(statusArray, (key, item) => {
            if (item.ShowInCustomerList) {
                html.push(
                    `<li class="edit-status-popup" data-custid='${custId}' data-displayorder='${item.DisplayOrder}'` +
                    `data-url="${item.Url}" data-param='${item.JsonParams}' data-message='${item.ConfirmMessage}'>` +
                    `<input ${item.Disabled ? 'disabled' : ''} type="checkbox" ${item.Checked ? 'checked' : ''}/> ${item.Text}</li>`);
            }
        });

        this.$('#bootstrap-modal').find('.modal-body').empty().append(html.join(''));

        if (this.isMobile) {
            this.$('#bootstrap-modal').find('.modal-title').empty().append('Edit Status');
        }
    }

    private bindingPopupEvent = parentElement => this.$('.edit-status-popup').click(event =>
        this.topWindow.bootboxConfirm(
            this.$(event.currentTarget).data("message"),
            this.isMobile ? event.target : this.$(parentElement).context,
            () => this.customerListServices.updateCustomerStatus(event.currentTarget, this.$(parentElement).context, this.updateCustomerStatusSuccess)));

    private updateCustomerStatusSuccess = (currentTarget, parentTarget, data, displayOrder) => {
        if (data.IsSuccessful) {
            // Closed or Suspended
            if (displayOrder === 1 || displayOrder === 2) {
                this.$('.btn-submit').click();
            }

            this.customerListPopup.closeStatusPopup();

            setTimeout(() => this.$.unblockUI(), 1500);
        }
        else {
            this.$.unblockUI();
            this.topWindow.bootboxAlert(data.Message, this.isMobile ? currentTarget : parentTarget);
        }
    }
}