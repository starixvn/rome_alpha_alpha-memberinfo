"use strict";

class ReachedThresholdService {
    private $: JQueryStatic;
    private url: string;

    constructor($: JQueryStatic) {
        this.$ = $;
        this.url = this.$("#member-info-url").val() + "ReachedThreshold";
    }

    public callAjax = (url, type, params, onSuccess) => {
        this.$.blockUI();
        const xhr = this.callAjaxWithoutBlocking(url, type, params, onSuccess);
        xhr.error = this.$.unblockUI;
    }

    public callAjaxWithoutBlocking = (url, type, params, onSuccess): JQueryXHR => {
        return this.$.ajax({
            url: url,
            headers: {
                __RequestVerificationToken: this.$("input[name='__RequestVerificationToken']").val(),
                'X-Requested-With': 'XMLHttpRequest'
            },
            dataType: "json",
            type: type,
            data: params,
            success: onSuccess,
            error: function (jqXHR) {
                if (jqXHR && jqXHR.status === 500 && jqXHR.responseText) {
                    const jsonData = JSON.parse(jqXHR.responseText);
                    if (jsonData.UMFrom && jsonData.UMTo) {
                        location.href = top["alpha"].urls.mainRootPath
                            + "PartialUM/Index?umfrom=" + jsonData.UMFrom
                            + "&umto=" + jsonData.UMTo;
                    }
                }
            }
        });
    }

    public getReachlistThreshold = (params, callback) => {
        this.callAjax(
            this.url + "/GetReachlistThreshold",
            "POST",
            params,
            data => {
                this.$.unblockUI();
                callback({
                    data: data.ReachlistAccounts,
                    recordsTotal: data.TotalItems,
                    recordsFiltered: data.TotalItems
                });
            }
        )
    }

    public getReachHistoryList = (params, callback) => {
        this.callAjax(
            this.url + "/GetReachHistoryList",
            "POST",
            params,
            data => {
                this.$.unblockUI();
                callback({
                    data,
                })
            }
        )
    }
}