interface Window {
    watchlistAccountData: any;
    reloadDataTable: Function;
}

class MyWatchlistAccounts {
    private $: JQueryStatic;
    private topWindow: any;
    private viewModel: MyWatchlistAccountsViewModel;
    private dataTable: MyWatchlistAccountsDataTable;
    private iframePopup: IframePopup;

    constructor(
        $: JQueryStatic,
        topWindow: any,
        viewModel: MyWatchlistAccountsViewModel,
        dataTable: MyWatchlistAccountsDataTable,
        iframePopup: IframePopup
    ) {
        this.$ = $;
        this.topWindow = topWindow,
        this.viewModel = viewModel;
        this.dataTable = dataTable;
        this.iframePopup = iframePopup;
    }

    public init = () => {
        this.bindingTxtUsername();
        this.$("#btn-global-threshold").off().click(_ =>
            this.iframePopup.open(
                "700px",
                "330px",
                this.$("#global-threshold-popup-title").val(),
                this.viewModel.URL + "/ManageGlobalThreshold")
        );
        this.$("#btn-manage-preset").off().click(_ =>
            this.iframePopup.open(
                "795px",
                "700px",
                this.$("#manage-preset-popup-title").val(),
                this.viewModel.URL + "/ManagePersonalThresholdPreset")
        );
        this.$("#btn-add-members").off().click(_ => 
            this.iframePopup.open(
                "820px",
                "520px",
                this.$("#add-new-account-popup-title").val(),
                this.viewModel.URL + "/ManageWatchlistAccounts")
        );
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
        JSON.parse($('#is-mobile').val()),
        JSON.parse($('#limited-date').val()),
        window.watchlistAccountData
    );
    const service = new MyWatchlistAccountService($);
    const iframePopup = new IframePopup($, window.top);
    const editSettingPopup = new EditSettingPopup($, window.top, viewModel, service, iframePopup);
    const dataTable = new MyWatchlistAccountsDataTable($, service, viewModel, editSettingPopup);

    const myWatchlistAccounts = new MyWatchlistAccounts(
        $,
        window.top,
        viewModel,
        dataTable,
        iframePopup
    );

    window.top.reloadDataTable = () => {
        dataTable.reloadData();
    }

    myWatchlistAccounts.init();
});