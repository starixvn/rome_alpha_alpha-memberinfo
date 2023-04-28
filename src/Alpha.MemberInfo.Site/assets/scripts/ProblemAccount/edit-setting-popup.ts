"use strict";

class ProblemAccountPopup {
    private $: JQueryStatic;
    private topWindow: any;

    private $ckbSelectAll: JQuery;
    private $editPopupContainer: JQuery;
    private editText: string;
    private editMultipleText: string;
    private memberInfoUrl: string;
    private accountInfoUrl: string;
    private roleId: string;

    constructor($: JQueryStatic, topWindow: any) {
        this.$ = $;
        this.topWindow = topWindow;
    }

    public init(tableReport: JQuery) {
        this.$ckbSelectAll = this.$("#select-all");
        this.$editPopupContainer = this.$('#edit-popup');
        this.editText = this.$('#editSettingText').val();
        this.editMultipleText = this.$('#editMultipleSettingText').val();
        this.memberInfoUrl = this.$("#memberInfoUrl").val();
        this.accountInfoUrl = this.$("#accountInfoUrl").val();
        this.roleId = this.$("#RoleId").val();

        this.$ckbSelectAll.click(_ => tableReport.find(".checkbox").prop("checked", this.$ckbSelectAll.is(":checked")));
        this.$("#edit-multiple").click(event => this.openMultiplePopup(event.currentTarget));
        this.$editPopupContainer.on("mouseleave", _ => this.$editPopupContainer.modal("hide"));
    }

    public bindEvents(tableReport: JQuery) {
        this.$ckbSelectAll.prop("checked", false);
        tableReport.find(".checkbox").off().on("click", _ =>
            this.$ckbSelectAll.prop("checked", tableReport.find(".checkbox").length === tableReport.find(".checkbox:checked").length));

        this.$(".edit").off().on("click", event => this.openSinglePopup(this.$(event.currentTarget)));
        this.$(".open-status").off().on("click", event => this.changeStatusToSuspend(this.$(event.currentTarget)));
    }

    private changeStatusToSuspend(element) {
        const ids = this.$(element).data("custid");
        const roleid = this.roleId;

        const message = this.$("#changeStatusToSuspendMessage").val();
        $(`#status-${ids}`).toggleClass('suspend');
        $(`#status-${ids}:first-child`).html($(`#suspendText`).val());

        this.topWindow.bootboxConfirm(
            message,
            null,
            () => {

                AJAXHelper.postForm(
                    `${this.memberInfoUrl}ProblemAccount/UpdateCommand`,
                    { ids, roleid },
                    result => {
                        this.setOpenStatus(ids);

                        if (result && result.ErrCode === 1) {

                            this.topWindow.bootboxAlert(result.ErrMsg);
                            return;
                        }

                        if (result && result.length > 0) {
                            this.topWindow.reloadPromblemAccountListDataTable();
                        }

                    });
            },
            () => {
                this.setOpenStatus(ids);
            }

        );
    }

    private setOpenStatus(ids) {
        $(`#status-${ids}:first-child`).html($(`#openText`).val());
        $(`#status-${ids}`).toggleClass('suspend');
    }

    private openSinglePopup(element) {
        console.log("single popup item is clicked");

        var content =
            `<ul>
                <li class="edit-popup icon-edit" id="mark-as-reviewed-selected">${this.$("#markAsReviewedText").val()}</li>
                <li class="edit-popup icon-edit" id="add-to-my-watchlist-selected">${this.$("#addToMyWatchlistText").val()}</li>
             </ul>`;
        this.openEditSettingPopup(
            element,
            content,
            this.editText,
            {
                usernames: [this.$(element).data("username")],
                ids: [this.$(element).data("custid")],
            }
        );
    }

    private openMultiplePopup(element) {
        console.log("multiple popup item is open");

        var content =
            `<ul>
                <li class="edit-popup icon-edit" id="mark-multiple-as-reviewed-selected">${this.$("#markMultipleAsReviewedText").val()}</li>
                <li class="edit-popup icon-edit" id="add-multiple-to-watchlist-selected">${this.$("#addMultipleToWatchlistText").val()}</li>
                <li class="edit-popup icon-edit" id="suspend-multiple-accounts-selected">${this.$("#suspendMultipleAccountsText").val()}</li>
            </ul>`;

        const checkboxs = this.$('input.checkbox:checkbox:checked');
        const ids = [];
        const usernames = [];
        checkboxs.each((_, checkbox) => {
            usernames.push(this.$(checkbox).data("username"));
            ids.push(this.$(checkbox).data("custid"));
        });

        if (ids.length > 0 && usernames.length > 0) {
            this.openEditSettingPopup(
                element,
                content,
                this.editMultipleText,
                {
                    ids,
                    usernames
                }
            );
        }
    }

    private openEditSettingPopup(element, content, title, data) {
        console.log("popup item is clicked");

        this.$editPopupContainer.find('.modal-body').empty().append(content);

        const $modalDialog = this.$editPopupContainer.find('.modal-dialog');

        if (this.topWindow.alpha.isMobile) {
            this.$editPopupContainer.find('.modal-title').empty().append(title);
            $modalDialog.css({ height: '100%', width: '100%' });
            this.$editPopupContainer
                .css({ top: 0, left: 0, height: '100%', width: '100%', paddingRight: '0px' })
                .modal({ show: true, backdrop: false });
        }
        else {
            this.$editPopupContainer
                .css({ height: '70px', width: 'fit-content' })
                .modal({ show: true, backdrop: false });

            $modalDialog.css({ width: '100%' });
            const width = $modalDialog.outerWidth();

            let top = this.$(element).offset().top - this.$(document).scrollTop();
            let left = this.$(element).offset().left + this.$(element).width();

            if (this.$(window).width() - left < width) {
                left = left - width;
            }

            this.$editPopupContainer.css({ top, left });
        }

        this.$('#mark-as-reviewed-selected').click(() => this.openMarkReviewPopup(data));

        this.$('#mark-multiple-as-reviewed-selected').click(() => this.openMarkReviewPopup(data));

        this.$('#suspend-multiple-accounts-selected').click(() => this.openSuspendStatusPopup(data));

        this.$('#add-to-my-watchlist-selected').click(() => this.openAddToWatchlistPopup(data));

        this.$('#add-multiple-to-watchlist-selected').click(() => this.openAddToWatchlistPopup(data));
    }

    private openSuspendStatusPopup(data) {
        const title = this.$("#suspendMultipleAccountsText").val();

        const ids = data.ids.join(",");
        const params = `custIds=${ids}&roleId=${this.roleId}`;
        const url = `${this.accountInfoUrl}UnsuspendDownlines/EditMultiple?${params}`;
        this.topWindow.popupManager.open(
            url,
            title,
            820,
            520,
            "modal-mobile-friendly");
    }


    private openMarkReviewPopup(data) {

        const title = data.usernames.length === 1
            ? this.$("#markAsReviewedText").val()
            : this.$("#markMultipleAsReviewedText").val();

        const usernames = data.usernames.join(", ");
        const ids = data.ids.join(",");
        const params = `ids=${ids}&usernames=${usernames}&roleId=${this.roleId}`;
        const url = `${this.memberInfoUrl}ProblemAccount/Update?${params}`;
        this.topWindow.popupManager.open(
            url,
            title,
            820,
            520,
            "modal-mobile-friendly");
    }

    private IsSingleSelect = data => data.usernames.length === 1;


    private openAddToWatchlistPopup(data) {
        const isSingleSelect = this.IsSingleSelect(data);
        const title = isSingleSelect
            ? this.$("#addToMyWatchlistText").val()
            : this.$("#addMultipleToMyWatchlistText").val();
        const usernames = data.usernames.join(", ");
        const custIds = data.ids.join(",");
        
        if (isSingleSelect) {
            this.topWindow.popupManager.open(
                `${this.memberInfoUrl}MyWatchlistAccounts/ConfirmAddToWatchlist?Ids=${"18363561,18363711,1176092"}`,
                title,
                820,
                550,
                "modal-mobile-friendly");
            return;
        }


        const divUsernames = `<div style="max-height: 245px; overflow-y: auto;">${usernames}</div><br/>`;

        this.topWindow.bootboxConfirm(
            divUsernames + this.$("#are-you-sure-to-remove-accounts").val(),
            null,
            () => {
                AJAXHelper.postForm(
                    `${this.memberInfoUrl}MyWatchlistAccounts/DeleteMultiplePersonalThreshold`,
                    { usernames, custIds },
                    result => {
                        if (result && result.Code === 1) {
                            this.topWindow.bootboxAlert(result.Message);
                            return;
                        }

                        if (result && result.length > 0) {
                            this.topWindow.reloadMyWatchlistDataTable();
                        }
                    });
            });
    }
}