"use strict";

class EditSettingPopup {
    private $: JQueryStatic;
    private topWindow: any;
    private viewModel: MyWatchlistAccountsViewModel;
    private service: MyWatchlistAccountService;
    private iframePopup: IframePopup;
    private $editPopupContainer: JQuery;

    constructor(
        $: JQueryStatic,
        topWindow: any,
        viewModel: MyWatchlistAccountsViewModel,
        service: MyWatchlistAccountService,
        iframePopup: IframePopup
    ) {
        this.$ = $;
        this.topWindow = topWindow;
        this.viewModel = viewModel;
        this.service = service;
        this.iframePopup = iframePopup;
        this.cacheDom();
    }

    private cacheDom = () => {
        this.$editPopupContainer = this.$('#edit-popup');
    }

    public bindingModalEvent = () => this.$editPopupContainer.off().on('mouseleave ', () => this.$editPopupContainer.modal('hide'));
    
    public bindingEditEvent = () => this.$('.edit').off().click(event => this.openSinglePopup(this.$(event.currentTarget)));

    private openSinglePopup = element => {
        var content =
            `<ul>
                <li class="edit-popup icon-edit">${this.$("#edit-personal-threshold").val()}</li>
                <li class="remove-popup icon-bin">${this.$("#remove-accounts").val()}</li>
             </ul>`;
        this.openEditSettingPopup(
            element,
            content,
            "Edit",
            {
                usernames: [this.$(element).data("username")],
                custIds: [this.$(element).data("custid")],
                winningThreshold: this.$(element).data("winningthreshold"),
                losingThreshold: this.$(element).data("losingthreshold"),
                isEdit: true
            }
        );
    }

    public bindingEditMultipleEvent = () => {
        var self = this;
        const tblRpt = this.$(".tblRpt");
        tblRpt.undelegate();
        tblRpt.delegate(".edit-multiple", "click", event => this.openMultiplePopup(event.currentTarget));
        tblRpt.delegate(".select-all", "click", function () {
            const checked = this.checked;
            self.$('tbody .checkbox').prop('checked', checked);
            self.$(".select-all").prop('checked', checked);
        });
    }

    private openMultiplePopup = element => {
        var content =
                    `<ul>
                        <li class="edit-popup icon-edit">${this.$("#edit-multiple-personal-threshold").val()}</li>
                        <li class="remove-popup icon-bin">${this.$("#remove-multiple-accounts").val()}</li>
                    </ul>`;

        const checkboxs = this.$('input.checkbox:checkbox:checked');
        const custIds = [];
        const usernames = [];
        checkboxs.each((_, checkbox) => {
            usernames.push(this.$(checkbox).data("username"));
            custIds.push(this.$(checkbox).data("custid"));
        });

        if (custIds.length > 0 && usernames.length > 0) {
            this.openEditSettingPopup(
                element,
                content,
                "Edit Multiple",
                {
                    custIds,
                    usernames,
                    winningThreshold: checkboxs.first().data("winningthreshold"),
                    losingThreshold: checkboxs.first().data("losingthreshold"),
                    isEdit: true
                }
            );
        }
    }

    private openEditSettingPopup = (element, content, title, data) => {
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
                .css({ height: '70px', width: '250px' })
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

        this.$('.edit-popup').click(event => {
            const title = data.usernames.length === 1
                ? data.usernames + " - " + this.$("#edit-account-popup-title").val()
                : this.$("#edit-account-popup-title").val();

            data.usernames = data.usernames.join(", ");
            data.custIds = data.custIds.join(",");

            const url = this.viewModel.URL + "/ManageWatchlistAccounts";
            this.iframePopup.open("820px", "520px", title, url, data);
        });
        this.$('.remove-popup').click(() => this.openRemoveWatchlistAccountPopup(data));
    }

    private openRemoveWatchlistAccountPopup = data => {
        const usernames = data.usernames.join(", ") + " <br />";

        const users = [];
        for (let i = 0; i < data.custIds.length; i++) {
            users.push({
                custId: data.custIds[i],
                username: data.usernames[i].trim()
            })
        }

        this.topWindow.bootboxConfirm(
            usernames + this.$("#are-you-sure-to-remove-accounts").val(),
            null,
            () => {
                this.service.deleteMultiplePersonalThreshold({ users }, result => {
                    if (result && result.length > 0) {
                        WatchlistHelper.reloadPage();
                    }
                });
            });
    }
}