"use strict";

class ManagePresets {
    private $: JQueryStatic;
    private topWindow: any;
    private alert: MyWatchlistAccountsAlert;
    private service: MyWatchlistAccountService;

    private $tableBody: JQuery;

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
        this.$tableBody.off().on("keyup", ".threshold-input", this.handleInputThresholdChange);
    }

    private handleOpenEdit = event => {
        const $presetHtml = this.$(event.currentTarget).parents(".preset");

        $presetHtml.find(".preset-title").html(`<input type="text" name="presetTitle" value="${$presetHtml.data("title")}" />`);
        $presetHtml.find(".winning-threshold").html(`<input type="text" class="threshold-input" name="winningThreshold" value="${$presetHtml.data("winningthresholdstring")}" />`);
        $presetHtml.find(".losing-threshold").html(`<input type="text" class="threshold-input" name="losingThreshold" value="${$presetHtml.data("losingthresholdstring")}" />`);

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
                this.alert.showSuccess("Update successfully");
                WatchlistHelper.reloadPage();
                //WatchlistHelper.reloadPageWithTimeout(() => this.alert.showSuccess("Update successfully"));
            } else if (result.Code === 103) {
                this.alert.showError("Preset name is duplicated");
            }
        });
    }

    private handleCloseEdit = event => {
        const $presetHtml = this.$(event.currentTarget).parents(".preset");

        $presetHtml.find(".preset-title").html($presetHtml.data("title"));
        $presetHtml.find(".winning-threshold").html($presetHtml.data("winningthresholdstring"));
        $presetHtml.find(".losing-threshold").html($presetHtml.data("losingthresholdstring"));

        $presetHtml.find(".edit-group").removeClass("hidden");
        $presetHtml.find(".confirm-group").addClass("hidden");
    }

    private handleRemove = event => {
        this.topWindow.bootboxConfirm(
            "Are you sure you want to remove the Personal Threshold preset?",
            null,
            () => {
                const $presetHtml = this.$(event.currentTarget).parents(".preset");

                const params = {
                    id: parseInt($presetHtml.data("id"))
                }

                this.service.deletePersonalPreset(params, result => {
                    if (result.IsSuccessful) {
                        this.alert.showSuccess("Remove successfully");
                        WatchlistHelper.reloadPage();
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
                                <td>${index + 1}</td>
                                <td class="preset-title"><input type="text" name="presetTitle" value="" /></td>
                                <td class="winning-threshold"><input type="text" class="threshold-input" name="winningThreshold" value="" /></td>
                                <td class="losing-threshold"><input type="text" class="threshold-input" name="losingThreshold" value="" /></td>
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
            this.alert.showError("There is no change to update.");
            return;
        }

        this.service.addMultiplePersonalPreset(params, result => {
            if (result && result.length > 0) {
                result.forEach(res => {
                    switch (res.ErrCode) {
                        case 0:
                            this.alert.showMultipleSuccess(res.Name, "Add successfully");
                            break;
                        case 103:
                            this.alert.showMultipleError(res.Name, "Preset name is duplicated");
                            break;
                        default:
                            break;
                    }
                });
            }

            //if (result.find(r => r.ErrCode === 0) && result.find(r => r.ErrCode !== 0)) {
            //    WatchlistHelper.reloadGrid();
            //}

            if (result.filter(r => r.ErrCode === 0).length === result.length) {
                this.topWindow.closePopupAndReloadMainFrameAfterUpdateSuccess(3000);
            }
            return;
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
}

$(function () {
    const service = new MyWatchlistAccountService($);
    const managePersonalThresholdPreset = new ManagePresets($, window.top, service);
    managePersonalThresholdPreset.init();
})


//public openPopup = currentTarget => {
//    this.service.getPersonalPresets({}, presets => {
//        if (presets) {
//            this.$tableBody.html("");
//            this.alert.hide();

//            presets.forEach((element, index) => {
//                this.$tableBody.append(
//                    ``);
//            });

//            this.$managePresetContainer.find(".icon-edit").off().on("click", event => {
//                const $presetHtml = this.$(event.currentTarget).parents(".preset");
//                const id = parseInt($presetHtml.attr("data-id"));
//                const presetData = presets.find(p => p.ID === id);

//                $presetHtml.find(".preset-title").html(`<input type="text" name="presetTitle" value="${presetData.PresetTitle}" />`);
//                $presetHtml.find(".winning-threshold").html(`<input type="text" name="winningThreshold" value="${presetData.WinningThreshold}" />`);
//                $presetHtml.find(".losing-threshold").html(`<input type="text" name="losingThreshold" value="${presetData.LosingThreshold}" />`);

//                $presetHtml.find(".edit-group").addClass("hidden");
//                $presetHtml.find(".confirm-group").removeClass("hidden");

//                console.log(presets);
//            });

//            this.$tableBody.off().on("click", ".remove", event => {
//                this.topWindow.bootboxConfirm(
//                    "Are you sure you want to remove the Personal Threshold preset?",
//                    null,
//                    () => {
//                        const $presetHtml = this.$(event.currentTarget).parents(".preset");

//                        const params = {
//                            id: parseInt($presetHtml.attr("data-id"))
//                        }

//                        this.service.deletePersonalPreset(params, result => {
//                            if (result.IsSuccessful) {
//                                this.alert.showSuccess("Remove successfully");

//                                const id = parseInt($presetHtml.attr("data-id"));
//                                presets = presets.filter(p => p.ID !== id);
//                                $presetHtml.remove();
//                            } else {
//                                this.alert.showError(result.ErrMsg);
//                            }
//                        });
//                    }
//                );
//            });

//            this.$managePresetContainer.find(".icon-check").off().on("click", event => {
//                const $presetHtml = this.$(event.currentTarget).parent().parent().parent();

//                const params = {
//                    ID: parseInt($presetHtml.attr("data-id")),
//                    PresetTitle: $presetHtml.find('input[name="presetTitle"]').val(),
//                    WinningThreshold: $presetHtml.find('input[name="winningThreshold"]').val(),
//                    LosingThreshold: $presetHtml.find('input[name="losingThreshold"]').val()
//                }

//                this.service.updatePersonalPreset(params, result => {
//                    if (result.IsSuccessful) {
//                        $presetHtml.find(".preset-title").html(`${params.PresetTitle}`);
//                        $presetHtml.find(".winning-threshold").html(`${params.WinningThreshold}`);
//                        $presetHtml.find(".losing-threshold").html(`${params.LosingThreshold}`);

//                        $presetHtml.find(".edit-group").removeClass("hidden");
//                        $presetHtml.find(".confirm-group").addClass("hidden");

//                        const index = parseInt($presetHtml.attr("data-index"));
//                        presets[index] = params;

//                        console.log(presets);

//                        this.alert.showSuccess("Update successfully");
//                    } else if (result.Code === 103) {
//                        this.alert.showError("Preset name is duplicated");
//                    }
//                });
//            });

//            this.$managePresetContainer.find(".icon-close").off().on("click", event => {
//                const $presetHtml = this.$(event.currentTarget).parents(".preset");
//                const id = parseInt($presetHtml.attr("data-id"));
//                const presetData = presets.find(p => p.ID === id);

//                $presetHtml.find(".preset-title").html(presetData.PresetTitle);
//                $presetHtml.find(".winning-threshold").html(`${presetData.WinningThreshold}`);
//                $presetHtml.find(".losing-threshold").html(`${presetData.LosingThreshold}`);

//                $presetHtml.find(".edit-group").removeClass("hidden");
//                $presetHtml.find(".confirm-group").addClass("hidden");
//            });

//            this.$managePresetContainer.find("#btn-add-row").off().on("click", event => {
//                const dataIndex = parseInt(this.$tableBody.children().last().attr("data-index"));
//                var index = isNaN(dataIndex) ? 0 : dataIndex + 1;
//                this.$tableBody.append(
//                    `<tr id="preset-${index}"
//                             class="preset newRow"
//                             data-index="${index}">
//                                <td>${index + 1}</td>
//                                <td class="preset-title"><input type="text" name="presetTitle" value="" /></td>
//                                <td class="winning-threshold"><input type="text" name="winningThreshold" value="" /></td>
//                                <td class="losing-threshold"><input type="text" name="losingThreshold" value="" /></td>
//                                <td class="action">
//                                    <span class="icon-bin remove-new-row"></span>
//                                </td>
//                         </tr>`);

//                this.$(".remove-new-row").on("click", event => this.$(event.currentTarget).parents(".preset").remove());
//            });

//            this.$managePresetContainer.find("#btn-save").off().on("click", event => {
//                this.alert.hide();
//                const params = { presets: [] };
//                const newRows = this.$tableBody.find(".newRow");
//                for (var row of newRows.toArray()) {
//                    const preset = {
//                        presetTitle: this.$(row).find('input[name="presetTitle"]').val(),
//                        winningThreshold: this.$(row).find('input[name="winningThreshold"]').val(),
//                        losingThreshold: this.$(row).find('input[name="losingThreshold"]').val()
//                    }

//                    if (preset.presetTitle === "" && (preset.winningThreshold !== "" || preset.losingThreshold !== "")) {
//                        this.alert.showError("Must enter preset name");
//                        return false;
//                    }

//                    if (preset.presetTitle !== "" && (preset.winningThreshold === "" || preset.losingThreshold === "")) {
//                        this.alert.showError("Must enter threshold");
//                        return false;
//                    }

//                    if (preset.presetTitle !== "" && preset.winningThreshold !== "" && preset.losingThreshold !== "") {
//                        params.presets.push(preset);
//                    }
//                }

//                if (params.presets.length === 0) {
//                    this.alert.showError("There is no change to update.");
//                    return;
//                }

//                this.service.addMultiplePersonalPreset(params, result => {
//                    if (result && result.length > 0) {
//                        result.forEach(res => {
//                            switch (res.ErrCode) {
//                                case 0:
//                                    this.alert.showMultipleSuccess(res.Name, "Add successfully");
//                                    break;
//                                case 103:
//                                    this.alert.showMultipleError(res.Name, "Preset name is duplicated");
//                                    break;
//                                default:
//                                    break;
//                            }
//                        });
//                    }

//                    if (result.find(r => r.ErrCode === 0) && result.find(r => r.ErrCode !== 0)) {
//                        WatchlistHelper.reloadGrid();
//                    }

//                    if (result.filter(r => r.ErrCode === 0).length === result.length) {
//                        WatchlistHelper.reloadPageWithTimeout(() => this.$managePresetContainer.modal("hide"));
//                    }
//                    return;
//                });
//            });

//            this.$managePresetContainer
//                .css({ height: '100%', width: '100%' })
//                .modal({ show: true, backdrop: false });

//            this.$managePresetContainer.draggable({ containment: "window" });
//        }
//    });
//}