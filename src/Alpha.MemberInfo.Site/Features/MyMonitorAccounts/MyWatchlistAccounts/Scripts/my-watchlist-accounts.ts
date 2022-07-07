interface Window {
    watchlistAccountData: any;
}

class MyWatchlistAccounts {
    private $: JQueryStatic;
    private topWindow: any;
    private viewModel: MyWatchlistAccountsViewModel;
    private dataTable: MyWatchlistAccountsDataTable;

    constructor(
        $: JQueryStatic,
        topWindow: any,
        viewModel: MyWatchlistAccountsViewModel,
        dataTable: MyWatchlistAccountsDataTable,
    ) {
        this.$ = $;
        this.topWindow = topWindow,
        this.viewModel = viewModel;
        this.dataTable = dataTable;
    }

    public init = () => {
        this.bindingTxtUsername();
        this.$("#btn-global-threshold").off().click(event => {
            const url = this.viewModel.MemberInfoUrl + "MyWatchlistAccounts/ManageGlobalThreshold";
            this.topWindow.popupManager.open(url, undefined, 630, this.topWindow.alpha.isMobile ? 430 : 350);
        });
        this.$("#btn-manage-preset").off().click(event => {
            const url = this.viewModel.MemberInfoUrl + "MyWatchlistAccounts/ManagePersonalThresholdPreset";
            this.topWindow.popupManager.open(url, undefined, 630, this.topWindow.alpha.isMobile ? 430 : 350);
        });
        this.$("#btn-add-members").off().click(event => {
            const url = this.viewModel.MemberInfoUrl + "MyWatchlistAccounts/ManageWatchlistAccounts";
            this.topWindow.popupManager.open(url, undefined, 630, this.topWindow.alpha.isMobile ? 430 : 350);
        })
        this.dataTable.bindingEvent();
    }

    private bindingTxtUsername = () => {
        const options = {
            url: phrase => `${this.viewModel.MemberInfoUrl}UserNameSuggestion/GetUsernamesSuggestion?SuggestionQuery=${phrase}&CustId=${this.viewModel.SearchAccountId}&DirectDownlines=true&IsAccInfo=false`,
            getValue: 'UserName',
            requestDelay: 200,
            minCharNumber: 2,
            template: {
                type: "custom",
                method: (value, item) => {
                    const nickname = item.UserName2 === null || item.UserName2 === '' ? '' : `(${item.UserName2})`;
                    return `<div class='suggestion'>${value} ${nickname}<div>${item.FirstName} ${item.LastName}</div></div>`;
                }
            },
            list: {
                maxNumberOfElements: 10
            }
        };

        const $txtUsername = this.$('#txt-username');
        $txtUsername.easyAutocomplete(options);

        $txtUsername.bind('keypress', event => {
            if (event.which == 13) {
                this.$('#btn-submit').click();
            }
        });
    }
}

$(function () {
    const viewModel = new MyWatchlistAccountsViewModel(
        parseInt($('#search-account-id').val()),
        $('#member-info-url').val(),
        window.watchlistAccountData
    );
    const service = new MyWatchlistAccountService($);
    const editSettingPopup = new EditSettingPopup($, window.top, viewModel, service);
    const dataTable = new MyWatchlistAccountsDataTable($, service, viewModel, editSettingPopup);

    const myWatchlistAccounts = new MyWatchlistAccounts(
        $,
        window.top,
        viewModel,
        dataTable
    );

    myWatchlistAccounts.init();
});