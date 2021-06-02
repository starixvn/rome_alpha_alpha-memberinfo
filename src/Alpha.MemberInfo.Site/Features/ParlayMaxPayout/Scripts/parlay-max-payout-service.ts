"use strict";

class ParlayMaxPayoutService {
    private $: JQueryStatic;

    constructor($: JQueryStatic) {
        this.$ = $;
    }

    public UpdateParlayMaxPayout(params, callback) {
        this.callAjax(
            this.$("#tb-updateurl").val(),
            "POST",
            params,
            data => {
                callback(data);
            }
        );
    }

    private callAjax(url, type, params, onSuccess): void {
        this.$.ajax({
            url: url,
            beforeSend: this.$.blockUI,
            contentType: 'application/json; charset=utf-8',
            dataType: "json",
            type: type,
            data: params,
            success: onSuccess,
            complete: this.$.unblockUI
        });
    }
}