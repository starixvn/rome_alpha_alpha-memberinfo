/// <reference path="../shared/top-windows-interface.ts" />
class UpdateMultipleSuspend {
    $: JQueryStatic;
    topWindow: Window;

    $messageContainer: JQuery;
    $formUpdateMultipleSuspend: JQuery;
    $btnCancel: JQuery;

    successMessage: string;

    constructor($: JQueryStatic, topWindow: Window) {
        this.$ = $;
        this.topWindow = topWindow;
    }

    init(): void {
        this.$.blockUI();
        this.cacheDOMElements();
        this.bindEvents();
        this.$.unblockUI();
    }

    cacheDOMElements(): void {
        this.$messageContainer = this.$("#message-container");
        this.$formUpdateMultipleSuspend = this.$("#formUpdateMultipleSuspend");
        this.$btnCancel = this.$("#btnCancel");
        this.successMessage = this.$("#SuccessMessage").val();
    }

    bindEvents(): void {
        this.$btnCancel.on("click touchend", () => {
            this.topWindow["popupManager"].close();
        });

        this.$formUpdateMultipleSuspend.submit(this.onSubmit);

        this.$(".more-link").click(function () {
            $(this).parent().prev().toggle();
            $(this).prev().toggle();
            $(this).remove();

            return false;
        });
    }

    onSubmit = (e: JQueryEventObject) => {
        e.preventDefault();
        this.$.blockUI();
        var token = this.$("input[name=\"__RequestVerificationToken\"").val();
        this.$.ajax({
            type: "POST",
            beforeSend: xhr => xhr.setRequestHeader("__RequestVerificationToken", token),
            url: this.$formUpdateMultipleSuspend.attr("action"),
            data: this.getParams(),
            dataType: "json",
            success: this.submitSuccess,
            complete: this.$.unblockUI
        });
    };

    submitSuccess = (data) => {
        if (data.Results) {
            const result = data.Results[0];

            if (result.IsSuccess) {
                this.$messageContainer.empty();

                this.$messageContainer.html("<div class='msg msg-success'>" + result.Message + "</div>");

                setTimeout(() => {
                    this.topWindow["popupManager"].closeAllPopup();
                    this.topWindow["indexManager"].reloadProblemAccount();
                }, 3000);
            } else {
                this.$messageContainer.html("<div class='msg msg-error'>" + result.Message + "</div>");

            }
        } else {
            this.$messageContainer.html("<div class='msg msg-error'>" + data.ErrMsg + "</div>");
        }
    };

    getParams() {
        return {
            RoleId: this.$("#RoleId").val(),
            UnsuspendList: this.getUnsuspendList()
        };
    }
    getUnsuspendList() {
        const ids: string = this.$("#Ids").val();
        const idArray = ids.split(",");
        const userNames: string = this.$("#UserNames").val();
        const userArray = userNames.split(",");

        return userArray.map((value, index) => {
            return {
                SearchAccountId: idArray[index],
                Username: value,
                Suspended: true
            }
        });

    }
}

$(document).ready(() => {
    const updateMultipleSuspend = new UpdateMultipleSuspend($, window.top);
    updateMultipleSuspend.init();
});