"use strict";

class MyWatchlistAccountsAlert {
    $: JQueryStatic;
    divAlert: JQuery;

    constructor($: JQueryStatic) {
        this.$ = $;
        this.cacheDOMElements();
    }

    private cacheDOMElements = () => {
        this.divAlert = this.$(".alert-message");
    }

    public showSuccess = (message) => {
        this.divAlert.removeClass("show-alert-message").html(`<div class="msg msg-success">${message}</div>`)
    }

    public showError = (message) => {
        this.divAlert.removeClass("show-alert-message").html(`<div class="msg msg-error">${message}</div>`);
    }

    public showMultipleSuccess = (name, message) => {
        this.divAlert.addClass("show-alert-message");
        this.divAlert.append(`
            <div>
                <div class="name">${name}</div>
                <div class="msg msg-success">${message}</div>
            </div>`);
    }

    public showMultipleError = (name, message) => {
        this.divAlert.addClass("show-alert-message");
        this.divAlert.append(`
            <div>
                <div class="name">${name}</div>
                <div class="msg msg-error">${message}</div>
            </div>`);
    }

    public hide = () => {
        this.divAlert.removeClass("show-alert-message").html("");
    }

    public hideAfter3Seconds = () => setTimeout(() => this.hide(), 3000);
}