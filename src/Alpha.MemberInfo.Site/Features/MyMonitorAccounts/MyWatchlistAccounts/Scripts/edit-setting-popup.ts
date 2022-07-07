"use strict";

class EditSettingPopup {
    private $: JQueryStatic;
    private topWindow: any;
    private viewModel: MyWatchlistAccountsViewModel;
    private service: MyWatchlistAccountService;
    private $editPopupContainer: JQuery;

    constructor(
        $: JQueryStatic,
        topWindow: any,
        viewModel: MyWatchlistAccountsViewModel,
        service: MyWatchlistAccountService
    ) {
        this.$ = $;
        this.topWindow = topWindow;
        this.viewModel = viewModel;
        this.service = service;
        this.cacheDom();
    }

    private cacheDom = () => {
        this.$editPopupContainer = this.$('#edit-popup');
    }

    public bindingModalEvent = () => this.$editPopupContainer.off().on('mouseleave ', () => this.$editPopupContainer.modal('hide'));
    
    public bindingEditEvent = () => this.$('.edit').off().click(event => this.openSinglePopup(this.$(event.currentTarget)));

    private openSinglePopup = element => {
        // Show popup with edit and remove button
        //this.$('.edit-multiple').data("custid", '');
        //this.$('.edit-multiple').data("url", '');
        //this.$('#arrayCustID').val('');
        //this.$('#arrayUserName').val('');

        var content =
            `<ul>
                <li class="edit-popup icon-edit">Edit Personal Threshold</li>
                <li class="remove-popup icon-edit">Remove Accounts</li> 
             </ul>`;
        this.openEditSettingPopup(
            element,
            content,
            "",
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
                        <li class="edit-popup icon-edit">Edit Multiple Personal Threshold</li>
                        <li class="remove-popup icon-edit">Remove Multiple Accounts</li> 
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
                "",
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

        //if (this.isMobile) {
        //    this.$editPopupContainer.find('.modal-title').empty().append(title);
        //}

        const $modalDialog = this.$editPopupContainer.find('.modal-dialog');

        //if (this.isMobile) {
        //    $bootstrapModalDialog.css({ height: '100%', width: '100%' });

        //    this.$editPopupContainer
        //        .css({ top: 0, left: 0, height: '100%', width: '100%', paddingRight: '0px' })
        //        .modal({ show: true, backdrop: false });
        //}
        //else {
            this.$editPopupContainer
                .css({ height: 'auto', width: 'auto' })
                .modal({ show: true, backdrop: false });

            $modalDialog.css({ width: 'inherit' });
            const width = $modalDialog.outerWidth();

            let top = this.$(element).offset().top - this.$(document).scrollTop();
            let left = this.$(element).offset().left + this.$(element).width();

            if (this.$(window).width() - left < width) {
                left = left - width;
            }

            this.$editPopupContainer.css({ top, left });
        //}

        // Bind edit, remove button
        //this.$('.edit-popup').click(event => this.manageWatchlistAccountsPopup.openEditPopup(data));
        this.$('.edit-popup').click(event => {
            data.usernames = data.usernames.join(", ");
            data.custIds = data.custIds.join(",");
            const url = this.viewModel.MemberInfoUrl + `MyWatchlistAccounts/ManageWatchlistAccounts?${this.$.param(data)}`;
            this.topWindow.popupManager.open(url, undefined, 630, this.topWindow.alpha.isMobile ? 430 : 350);
        });
        this.$('.remove-popup').click(() => this.openRemoveWatchlistAccountPopup(data));
    }

    private openRemoveWatchlistAccountPopup = data => {
        const usernames = data.usernames.length > 1 ? data.usernames + " <br />" : "";

        const users = [];
        for (let i = 0; i < data.custIds.length; i++) {
            users.push({
                custId: data.custIds[i],
                username: data.usernames[i].trim()
            })
        }

        this.topWindow.bootboxConfirm(
            usernames + "Are you sure you want to remove customer(s) from the list ?",
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