class MyWatchlistAccountService {
    private $: JQueryStatic;
    private url: string;

    constructor($: JQueryStatic) {
        this.$ = $;
        this.url = this.$("#member-info-url").val() + "MyWatchlistAccounts";
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

    public getWatchlistAccounts = (params, callback) => {
        this.callAjax(
            this.url + "/GetWatchlistAccounts",
            "POST",
            params,
            data => {
                this.$.unblockUI();
                callback({
                    data: data.WatchlistAccounts,
                    recordsTotal: data.TotalItems,
                    recordsFiltered: data.TotalItems
                });
            }
        )
    }

    public getPersonalPresets = (params, callback) => {
        this.callAjax(
            this.url + "/GetPersonalPresets",
            "POST",
            params,
            data => {
                this.$.unblockUI();
                callback(data);
            });
    }

    public addPersonalThreshold = (params, callback) => {
        this.callAjaxWithoutBlocking(
            this.url + "/AddPersonalThreshold",
            "POST",
            params,
            data => callback(data)
        );
    }

    public updateMultiplePersonalThreshold = (params, callback) => {
        this.callAjaxWithoutBlocking(
            this.url + "/UpdateMultiplePersonalThreshold",
            "POST",
            params,
            data => callback(data)
        );
    }

    public deleteMultiplePersonalThreshold = (params, callback) => {
        this.callAjaxWithoutBlocking(
            this.url + "/DeleteMultiplePersonalThreshold",
            "POST",
            params,
            data => callback(data)
        );
    }

    public getGlobalThreshold = (params, callback) => {
        this.callAjax(
            this.url + "/GetGlobalThreshold",
            "POST",
            params,
            data => {
                this.$.unblockUI();
                callback(data);
            });
    }

    public updateGlobalThreshold = (params, callback) => {
        this.callAjaxWithoutBlocking(
            this.url + "/UpdateGlobalThreshold",
            "POST",
            params,
            data => callback(data)
        );
    }

    public addMultiplePersonalPreset = (params, callback) => {
        this.callAjax(
            this.url + "/AddMultiplePersonalPreset",
            "POST",
            params,
            data => {
                this.$.unblockUI();
                callback(data);
            }
        )
    }

    public updatePersonalPreset = (params, callback) => {
        this.callAjax(
            this.url + "/UpdatePersonalPreset",
            "POST",
            params,
            data => {
                this.$.unblockUI();
                callback(data);
            }
        )
    }

    public deletePersonalPreset = (params, callback) => {
        this.callAjax(
            this.url + "/DeletePersonalPreset",
            "POST",
            params,
            data => {
                this.$.unblockUI();
                callback(data);
            }
        )
    }
}