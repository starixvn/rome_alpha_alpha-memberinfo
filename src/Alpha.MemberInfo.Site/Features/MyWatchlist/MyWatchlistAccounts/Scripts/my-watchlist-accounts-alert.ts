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
        const divName = this.$('<div class="name"></div>').text(name);
        const divMsg = this.$('<div class="msg msg-success"></div>').text(message);
        const divContainer = this.$('<div></div>').append(divName, divMsg);
        this.divAlert.append(divContainer);
    }

    public showMultipleError = (name, message) => {
        this.divAlert.addClass("show-alert-message");
        const divName = this.$('<div class="name"></div>').text(name);
        const divMsg = this.$('<div class="msg msg-error"></div>').text(message);
        const divContainer = this.$('<div></div>').append(divName, divMsg);
        this.divAlert.append(divContainer);
    }

    public hide = () => {
        this.divAlert.removeClass("show-alert-message").html("");
    }
}