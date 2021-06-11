"use strict";

class AlertService {
    private $: JQueryStatic;

    private $divMessageContainer: JQuery;
    private $divMessageText: JQuery;
    private $divPmpContainer: JQuery;
    private $divHint: JQuery;
    private $divHintText: JQuery;
    private $divHintPointer: JQuery;

    private hideHintTimeout = 3000;

    constructor($: JQueryStatic) {
        this.$ = $;
    }

    public Init(): void {
        this.cacheDomElements();
    }

    private cacheDomElements(): void {
        this.$divMessageContainer = this.$("#message-container");
        this.$divMessageText = this.$("#message-text");
        this.$divPmpContainer = this.$("#pmp-container");
        this.$divHint = this.$("#hint");
        this.$divHintText = this.$("#hint-text");
        this.$divHintPointer = this.$("#hint-pointer");
    }

    public showHint(el: HTMLElement, message: string): void {
        this.$divHintText.text(message);
        this.$divHint.css("display", "block");

        this.adjustHintPosition(el);
        this.setHideHint();
    }

    private adjustHintPosition(el: HTMLElement): void {
        const hintHeight = 30,
            hintPointerHeight = 12,
            inputWidth = 120,
            hintWidth = this.$divHint.width(),
            elOffset = this.$(el).offset(),
            containerOffset = this.$divPmpContainer.offset(),
            containerWidth = this.$divPmpContainer.width(),
            top = elOffset.top - containerOffset.top - hintHeight - hintPointerHeight;

        if (elOffset.left + hintWidth > containerWidth) {
            const right = containerOffset.left + containerWidth - elOffset.left - inputWidth;
            this.$divHint.css({ top, right, left: "unset" });
            this.$divHintPointer.css({ "margin-left": "auto", "margin-right": "unset" });
        } else {
            this.$divHint.css({ top, right: "unset", left: elOffset.left });
            this.$divHintPointer.css({ "margin-left": "unset", "margin-right": "auto" });
        }
    }

    private setHideHint(): void {
        setTimeout(() => {
            this.$divHint.css("display", "none");
        }, this.hideHintTimeout);
    }

    public showMessage(message: string, isError: boolean = false): void {
        this.$divMessageText.text(message);
        this.$divMessageText.addClass(isError ? "message-error" : "message-success");
        this.$divMessageContainer.css("display", "block");
    }

    public showMessages(messages: string) {
        this.$divMessageContainer.empty();
        this.$divMessageContainer.html(messages);
        this.$divMessageContainer.css("display", "block");
    }
}