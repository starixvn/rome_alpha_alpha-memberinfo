"use strict";

class ManagePresets {
    private $: JQueryStatic;
    private topWindow: any;
    private alert: MyWatchlistAccountsAlert;
    private service: MyWatchlistAccountService;

    private $tableBody: JQuery;
    private reloadTimeout = 1000;
    private removeReloadTimeout = 500;

    constructor(
        $: JQueryStatic,
        topWindow: any,
        service: MyWatchlistAccountService,
    ) {
        this.$ = $;
        this.topWindow = topWindow;
        this.service = service;
        this.cacheDom();
    }

    private cacheDom = () => {
        this.$tableBody = this.$("#table-body");
        this.alert = new MyWatchlistAccountsAlert(this.$);
    }

    public init = () => {
        this.$(".icon-edit").off().click(this.handleOpenEdit);
        this.$(".icon-check").off().click(this.handleSaveEdit);
        this.$(".icon-close").off().click(this.handleCloseEdit);
        this.$(".remove").off().click(this.handleRemove);
        this.$("#btn-add").off().click(this.handleAddRow);
        this.$("#btn-save").off().click(this.handleSaveNewRows);
        this.$tableBody.off().on("input", ".threshold-input", this.handleInputThresholdChange);
    }

    private escapeHtml = (str) => str.replace(/&/g, '&amp;')
        .replace(/>/g, '&gt;')
        .replace(/</g, '&lt;')
        .replace(/\//g, '&#x2F')
        .replace(/"/g, '&quot;')
        .replace(/'/g, '&apos;');

    private handleOpenEdit = event => {
        const $presetHtml = this.$(event.currentTarget).parents(".preset");

        $presetHtml.find(".preset-title").html(`<input type="text" name="presetTitle" value="${this.escapeHtml($presetHtml.data("title"))}" />`);
        $presetHtml.find(".winning-threshold").html(
            `<input type="text" class="threshold-input" name="winningThreshold" value="${$presetHtml.data("winningthresholdstring")}" maxlength="18" />`);
        $presetHtml.find(".losing-threshold").html(
            `<input type="text" class="threshold-input" name="losingThreshold" value="${$presetHtml.data("losingthresholdstring")}" maxlength="18" />`);

        $presetHtml.find(".edit-group").addClass("hidden");
        $presetHtml.find(".confirm-group").removeClass("hidden");
    }

    private handleInputThresholdChange = event => {
        const el = event.currentTarget;
        el.value = WatchlistHelper.ToMoneyFormat(el.value);
    }

    private handleSaveEdit = event => {
        const $presetHtml = this.$(event.currentTarget).parent().parent().parent();

        const presetTitle = $presetHtml.find('input[name="presetTitle"]').val();
        const winningThreshold = $presetHtml.find('input[name="winningThreshold"]').val();
        const losingThreshold = $presetHtml.find('input[name="losingThreshold"]').val();

        if (!this.validateThresholdInputs(presetTitle, winningThreshold, losingThreshold)) return false;

        const params = {
            id: parseInt($presetHtml.data("id")),
            presetTitle,
            winningThreshold: WatchlistHelper.ToNumber(winningThreshold),
            losingThreshold: WatchlistHelper.ToNumber(losingThreshold)
        }

        this.service.updatePersonalPreset(params, result => {
            if (result.IsSuccessful) {
                this.alert.showSuccess(this.$("#update-successfully").val());
                this.reloadAfterSuccess(this.reloadTimeout);
            } else if (result.Code === 103) {
                this.alert.showError(this.$("#preset-is-duplicated").val());
            }
        });
    }

    private handleCloseEdit = event => {
        const $presetHtml = this.$(event.currentTarget).parents(".preset");

        $presetHtml.find(".preset-title").html(this.escapeHtml($presetHtml.data("title")));
        $presetHtml.find(".winning-threshold").html($presetHtml.data("winningthresholdstring"));
        $presetHtml.find(".losing-threshold").html($presetHtml.data("losingthresholdstring"));

        $presetHtml.find(".edit-group").removeClass("hidden");
        $presetHtml.find(".confirm-group").addClass("hidden");
    }

    private handleRemove = event => {
        this.topWindow.bootboxConfirm(
            this.$("#are-you-sure-to-remove-preset").val(),
            null,
            () => {
                const $presetHtml = this.$(event.currentTarget).parents(".preset");

                const params = {
                    id: parseInt($presetHtml.data("id"))
                }

                this.service.deletePersonalPreset(params, result => {
                    if (result.IsSuccessful) {
                        this.alert.showSuccess(this.$("#remove-successfully").val());
                        this.reloadAfterSuccess(this.removeReloadTimeout);
                    } else {
                        this.alert.showError(result.ErrMsg);
                    }
                });
            }
        );
    }

    private handleAddRow = () => {
        const dataIndex = parseInt(this.$tableBody.children().last().attr("data-index"));
        var index = isNaN(dataIndex) ? 0 : dataIndex + 1;
        this.$tableBody.append(
            `<tr id="preset-${index}"
                             class="preset newRow"
                             data-index="${index}">
                                <td class="text-center">${index + 1}</td>
                                <td class="preset-title"><input type="text" name="presetTitle" value="" /></td>
                                <td class="winning-threshold"><input type="text" class="threshold-input" name="winningThreshold" value="" maxlength="18"/></td>
                                <td class="losing-threshold"><input type="text" class="threshold-input" name="losingThreshold" value="" maxlength="18" /></td>
                                <td class="action">
                                    <span class="icon-bin remove-new-row"></span>
                                </td>
                         </tr>`);

        this.$(".remove-new-row").on("click", event => this.$(event.currentTarget).parents(".preset").remove());
    }

    private handleSaveNewRows = () => {
        this.alert.hide();
        const params = { presets: [] };
        const newRows = this.$tableBody.find(".newRow");
        for (var row of newRows.toArray()) {
            const presetTitle = this.$(row).find('input[name="presetTitle"]').val();
            const winningThreshold = this.$(row).find('input[name="winningThreshold"]').val();
            const losingThreshold = this.$(row).find('input[name="losingThreshold"]').val();

            if (!this.validateThresholdInputs(presetTitle, winningThreshold, losingThreshold)) return;

            if (presetTitle !== "" && winningThreshold !== "" && losingThreshold !== "") {
                params.presets.push({
                    presetTitle,
                    winningThreshold: WatchlistHelper.ToNumber(winningThreshold),
                    losingThreshold: WatchlistHelper.ToNumber(losingThreshold)
                });
            }
        }

        if (params.presets.length === 0) {
            this.alert.showError(this.$("#no-change-to-update-error").val());
            return;
        }

        this.service.addMultiplePersonalPreset(params, result => {
            if (result && result.length > 0) {
                let isError = false;
                result.forEach(res => {
                    switch (res.ErrCode) {
                        case 0:
                            this.alert.showMultipleSuccess(res.Name, this.$("#add-successfully").val());
                            break;
                        case 103:
                            this.alert.showMultipleError(res.Name, this.$("#preset-is-duplicated").val());
                            isError = true;
                            break;
                        default:
                            break;
                    }
                });

                if (isError)  return;
               
                this.reloadAfterSuccess(3000);
            }
        });
    }

    private validateThresholdInputs = (presetTitle, winningThreshold, losingThreshold) => {
        if (presetTitle === "" && (winningThreshold !== "" || losingThreshold !== "")) {
            this.alert.showError("Must enter preset name");
            return false;
        }

        if (presetTitle !== "" && (winningThreshold === "" || losingThreshold === "")) {
            this.alert.showError("Must enter threshold");
            return false;
        }

        return true;
    }

    private reloadAfterSuccess = reloadTimeout => {
        setTimeout(function () {
            window.location.reload();
        }, reloadTimeout);
    }
}

$(function () {
    const service = new MyWatchlistAccountService($);
    const managePersonalThresholdPreset = new ManagePresets($, window.top, service);
    managePersonalThresholdPreset.init();
})