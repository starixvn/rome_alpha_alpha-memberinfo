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
            dataType: "json",
            type: type,
            data: params,
            success: onSuccess
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
}