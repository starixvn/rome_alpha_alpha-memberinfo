"use strict";

interface Window {
    reachlistAccountData: any;
}

class ReachedThreshold {
    private $: JQueryStatic;
    private dataTable: ReachedThresholdDataTable;
    private viewModel: ReachedThresholdViewModel;
    private dateRangePickerComponent: DateRangePickerComponent;
    private statusDropdown: SingleSelectionDropdownComponent;
    private limitTypeDropdown: SingleSelectionDropdownComponent;

    constructor(
        $: JQueryStatic,
        dataTable: ReachedThresholdDataTable,
        viewModel: ReachedThresholdViewModel,
        dateRangePickerComponent: DateRangePickerComponent,
    ) {
        this.$ = $;
        this.dataTable = dataTable;
        this.viewModel = viewModel;
        this.dateRangePickerComponent = dateRangePickerComponent;
        this.statusDropdown = new SingleSelectionDropdownComponent($);
        this.limitTypeDropdown = new SingleSelectionDropdownComponent($);
    }

    public init = () => {
        this.bindingTxtUsername();

        this.dateRangePickerComponent.render(
            new DateRangePickerOption());

        this.statusDropdown.render(
            new SingleSelectionDropdownOption("status-dropdown", "", () => { }, true, true));

        this.limitTypeDropdown.render(
            new SingleSelectionDropdownOption("limit-type-dropdown", "", () => { }, true, true));

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
    const viewModel = new ReachedThresholdViewModel(
        parseInt($('#search-account-id').val()),
        $('#member-info-url').val(),
        JSON.parse($('#is-mobile').val()),
        JSON.parse($('#is-show-last-login').val()),
        window.reachlistAccountData
    );
    const service = new ReachedThresholdService($);
    const dateRangePickerComponent = new DateRangePickerComponent($);
    const iframePopup = new IframePopup($, window.top);
    const dataTable = new ReachedThresholdDataTable($, service, viewModel, iframePopup);

    const reachedThreshold = new ReachedThreshold($, dataTable, viewModel, dateRangePickerComponent);

    reachedThreshold.init();
})