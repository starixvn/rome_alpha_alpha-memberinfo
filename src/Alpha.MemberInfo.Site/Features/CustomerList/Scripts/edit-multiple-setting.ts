"use strict";

class EditMultipleSetting {
    private $: JQueryStatic;
    private customerListPopup: CustomerListPopup;
    private customerListServices: CustomerListServices;
    private editSingleSetting: EditSingleSetting;
    private viewModel: CustomerListViewModel;

    constructor(
        $: JQueryStatic,
        viewModel: CustomerListViewModel,
        customerListPopup: CustomerListPopup,
        customerListServices: CustomerListServices,
        editSingleSetting: EditSingleSetting) {
        this.$ = $;
        this.viewModel = viewModel;
        this.customerListPopup = customerListPopup;
        this.customerListServices = customerListServices;
        this.editSingleSetting = editSingleSetting;
    }

    public bindingEvent = () => {
        this.$('.edit-multiple').unbind().bind("click", event => this.openPopup(event.currentTarget));
        this.$('#select-all').unbind().bind("click", () => this.$('tbody .checkbox').prop('checked', this.$('#select-all').is(':checked')));
    }

    private openPopup = element => {
        let usernames = [], custids = [];
        this.$('input.checkbox:checkbox:checked').each((index, element) => {
            usernames.push(this.$(element).data("username"));
            custids.push(this.$(element).data("custid"));
        });

        const params = {
            UplineId: this.viewModel.SearchAccountId,
            UplineName: this.viewModel.SearchAccountRoleId,
            UplineRoleId: this.viewModel.SearchAccountRoleId,
            SearchAccountRoleId: this.viewModel.SearchAccountRoleId - 1,
            custids: custids.join(','),
            usernames: usernames.join(',')
        };

        if (custids.length > 0) {
            this.$('.edit-multiple').data("custid", custids[0]);
            this.$('.edit-multiple').data("url", this.$(`#setting-${custids[0]}`).data('url'));

            this.$('#arrayCustID').val(custids.join('^'));
            this.$('#arrayUserName').val(usernames.join('^'));

            //Synced Casino Array
            //this.$('#arrayStatusRnCasino').val(custids.join('^'));

            custids.length === 1 ?
                this.editSingleSetting.openPopup(this.$(`#setting-${custids[0]}`)) :
                this.customerListServices.getSettings(
                    custids[0],
                    `${this.viewModel.MemberInfoUrl}CustomerList/MultipleEditPopup`,
                    data => this.customerListPopup.openBootstrapModal(
                        element,
                        data,
                        this.buildPopupHtml,
                        this.bindingPopupEvent),
                    'POST',
                    params);
        }
    }

    private buildPopupHtml = settings => {
        let html = ['<ul>'];
        this.$.each(settings, (key, item) => {
            const methodByProductArray = [1, 6, 8];
            const method = this.$.inArray(item.ProductId, methodByProductArray) !== -1 ? 'GET' : 'POST';
            html.push(`<li data-title='${item.Text}' data-method='${method}' data-params='${JSON.stringify(item.QueryStringParamaters)}' class="edit-multiple-settings ${item.Icon}" data-url="${item.Url}">${item.Text}</li>`);
        });

        this.$('#bootstrap-modal').find('.modal-body').empty().append(html.join(''));

        if (this.viewModel.IsMobileView) {
            this.$('#bootstrap-modal').find('.modal-title').empty().append('Edit Multiple Setting');
        }
    }

    private bindingPopupEvent = () => this.$('.edit-multiple-settings').click(event => this.customerListPopup.openProductPopup(event.currentTarget, this.$(event.currentTarget).data("method")))
}