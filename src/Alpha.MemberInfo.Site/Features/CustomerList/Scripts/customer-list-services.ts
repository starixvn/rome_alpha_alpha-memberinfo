"use strict";

class CustomerListServices {
    private $: JQueryStatic;
    public statusSettingArray: any;
    private viewModel: CustomerListViewModel;

    constructor($: JQueryStatic, viewModel: CustomerListViewModel) {
        this.$ = $;
        this.viewModel = viewModel;
    }

    public callAjax = (url, type, params, onSuccess) => {
        this.$.blockUI();
        this.$.ajax({
            url: url,
            dataType: "json",
            type: type,
            headers: {
                __RequestVerificationToken: this.$("input[name='__RequestVerificationToken']").val(),
                'X-Requested-With': 'XMLHttpRequest'
            },
            data: params,
            success: onSuccess,
            error: this.$.unblockUI
        });
    }

    public getCutomerList = (params, callback) => this.callAjax(
        `${this.viewModel.MemberInfoUrl}CustomerList/GetCustomerListData`,
        'POST',
        params,
        data => {
            this.$.unblockUI();
            callback({
                data: data.DownlinesList,
                recordsTotal: data.TotalDownlines,
                recordsFiltered: data.TotalDownlines
            });
        }
    );

    public updateCustomerStatus = (currentTarget, parentTarget, updateCustomerStatusSuccess) => {
        const $target = this.$(currentTarget),
            action = $target.data("url"),
            params = $target.data("params"),
            displayOrder = $target.data("displayorder");

        this.callAjax(
            this.viewModel.MemberInfoUrl + 'CustomerList/' + action,
            'POST',
            params,
            data => updateCustomerStatusSuccess(currentTarget, parentTarget, data, displayOrder))
    }

    public getSettings = (url, callback, type = "GET", params = {}) => this.callAjax(
        url,
        type,
        params,
        data => {
            this.$.unblockUI();

            if (typeof callback === 'function') {
                callback.apply(this, [data]);
            }
        }
    )
}