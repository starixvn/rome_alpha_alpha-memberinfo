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

            custids.length === 1 ?
                this.editSingleSetting.openPopup(this.$(`#setting-${custids[0]}`)) :
                this.customerListServices.getSettings(
                    `${this.viewModel.MemberInfoUrl}CustomerList/MultipleEditPopup`,
                    data => this.customerListPopup.openStatusSettingPopup(
                        element,
                        this.buildPopupHtml(data),
                        this.$('#editMultipleSettingText').val(),
                        this.bindingPopupEvent),
                    'POST',
                    params);
        }
    }

    private buildPopupHtml = settings => {
        let html = ['<ul>'];
        this.$.each(settings, (key, item) => {
            //Synced Casino Array
            if (item.ProductId === 6) {
                this.$('#arrayStatusRnCasino').val(item.QueryStringParamaters.syncedCasinoList);
            }

            const attrs = {
                'class': `edit-multiple-settings ${item.Icon}`,
                'data-title': item.Title || item.Text,
                'data-method': item.Method,
                'data-params': JSON.stringify(item.QueryStringParamaters),
                'data-url': item.Url,
                'data-show-guide-line': item.ShowGuideLine,
                'data-guide-line-url': item.GuideLineUrl
            };

            html.push(`<li ${buildAttributes(attrs)}>${item.Text}</li>`);
        });

        return html.join('');
    }

    private bindingPopupEvent = () => this.$('.edit-multiple-settings').click(event => this.customerListPopup.openProductPopup(event.currentTarget, this.$(event.currentTarget).data("method")))
}