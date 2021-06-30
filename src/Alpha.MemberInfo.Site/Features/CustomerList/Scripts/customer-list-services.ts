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
        const xhr = this.callAjaxWithoutBlocking(url, type, params, onSuccess);
        xhr.error = this.$.unblockUI;
    }

    public callAjaxWithoutBlocking = (url, type, params, onSuccess): JQueryXHR => {
        return this.$.ajax({
            url: url,
            dataType: "json",
            type: type,
            headers: {
                __RequestVerificationToken: this.$("input[name='__RequestVerificationToken']").val(),
                'X-Requested-With': 'XMLHttpRequest'
            },
            data: params,
            success: onSuccess
        });
    }

    public getCutomerList = (params, callback) => {
        this.callAjax(
            `${this.viewModel.MemberInfoUrl}CustomerList/GetCustomerListData`,
            'POST',
            params,
            data => {
                this.$.unblockUI();
                this.$('#select-all').prop("checked", false);
                callback({
                    data: data.DownlinesList,
                    recordsTotal: data.TotalDownlines,
                    recordsFiltered: data.TotalDownlines
                });
            });
    }

    public updateCustomerStatus = (currentTarget, parentTarget, updateCustomerStatusSuccess) => {
        const $target = this.$(currentTarget),
            action = $target.data("url"),
            params = $target.data("params"),
            reloadcustomerlist = JSON.parse($target.data("reloadcustomerlist"));

        this.callAjax(
            this.viewModel.MemberInfoUrl + 'CustomerList/' + action,
            'POST',
            params,
            data => {
                this.$.unblockUI();
                updateCustomerStatusSuccess(currentTarget, parentTarget, data, reloadcustomerlist);
            })
    }

    public getSettings = (url, callback, type = "GET", params = {}) => this.callAjaxWithoutBlocking(
        url,
        type,
        params,
        data => typeof callback === 'function' && callback.apply(this, [data])
    )
}