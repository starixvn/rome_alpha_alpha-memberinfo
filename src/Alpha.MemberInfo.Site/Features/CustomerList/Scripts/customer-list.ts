/// <reference path="../../../assets/scripts/components/single-selection-dropdown.ts" />
"use strict";

interface Window {
    successfulUpdatingCallback: Function,
    reloadPopUpSetting: Function,
    actionAfterUpdatedLiveCasinoSettingMultiple: Function,
    actionAfterUpdatedLiveCasinoSettingSingle: Function,
    successfulUpdatingCallbackSportbook: Function,
    customerListPopup: any;
}

class CustomerList {
    private $: JQueryStatic;
    private customerListPopup: CustomerListPopup;
    private editMultipleSetting: EditMultipleSetting;
    private customerListDataTable: CustomerListDataTable;
    private viewModel: CustomerListViewModel;

    private statusDropdown: SingleSelectionDropdownComponent;
    private doubleCommDropdown: SingleSelectionDropdownComponent;
    private unconfirmSettingDropdown: SingleSelectionDropdownComponent;

    constructor(
        $: JQueryStatic,
        viewModel: CustomerListViewModel,
        editMultipleSetting: EditMultipleSetting,
        customerListPopup: CustomerListPopup,
        customerListDataTable: CustomerListDataTable,
        statusDropdown: SingleSelectionDropdownComponent,
        doubleCommDropdown: SingleSelectionDropdownComponent,
        unconfirmSettingDropdown: SingleSelectionDropdownComponent) {
        this.$ = $;
        this.viewModel = viewModel;
        this.customerListDataTable = customerListDataTable;
        this.customerListPopup = customerListPopup;
        this.editMultipleSetting = editMultipleSetting;

        this.statusDropdown = statusDropdown;
        this.doubleCommDropdown = doubleCommDropdown;
        this.unconfirmSettingDropdown = unconfirmSettingDropdown;
    }

    public init = () => {
        this.statusDropdown.render(
            new SingleSelectionDropdownOption("statusFilter", "StatusId", () => { }, true, true));

        this.doubleCommDropdown.render(
            new SingleSelectionDropdownOption("dcommFilter", "DoubleCommId", () => { }, true, true));

        this.unconfirmSettingDropdown.render(
            new SingleSelectionDropdownOption("unconfirmSettingFilter", "UnconfirmSettingId", () => { }, true, true));

        this.bidingEasyAutoComplete();
        this.customerListDataTable.initDataTable();
        this.editMultipleSetting.bindingEvent();
        this.customerListPopup.bindingBootstrapModalEvent();
    };

    private bidingEasyAutoComplete = () => this.$('#txt-username').easyAutocomplete({
        url: phrase => `${this.viewModel.MemberInfoUrl}UserNameSuggestion/GetUsernamesSuggestion?SuggestionQuery=${phrase}&DirectDownlines=true`,
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
    })
}

$(function () {
    const viewModel = new CustomerListViewModel(
        parseInt($('#searchAccountId').val()),
        $('#searchAccountName').val(),
        parseInt($('#searchAccountRoleId').val()),
        $('#downlineLevelName').val(),
        JSON.parse($('#showEditMultiple').val()),
        JSON.parse($('#isMobileView').val()),
        $('#breadcrumbLevelJson').val(),
        $('#oldMainUrl').val(),
        $('#reportsUrl').val(),
        $('#memberInfoUrl').val(),
        $('#accountInfoUrl').val(),
        JSON.parse($('#limitedDateToShowInfo').val()),
        $.parseJSON($('#customerListResources').val())
    );

    const customerListPopup = new CustomerListPopup($, window.top, viewModel.IsMobileView),
        customerListServices = new CustomerListServices($, viewModel),
        editSingleSetting = new EditSingleSetting($, viewModel.IsMobileView, customerListPopup, customerListServices),
        editMultipleSetting = new EditMultipleSetting($, viewModel, customerListPopup, customerListServices, editSingleSetting),
        editStatus = new EditStatus($, window.top, viewModel.IsMobileView, customerListServices, customerListPopup),
        customerListDataTable = new CustomerListDataTable($, window.top, viewModel, customerListPopup, customerListServices, editSingleSetting, editStatus),
        customerList = new CustomerList(
            $,
            viewModel,
            editMultipleSetting,
            customerListPopup,
            customerListDataTable,
            new SingleSelectionDropdownComponent($),
            new SingleSelectionDropdownComponent($),
            new SingleSelectionDropdownComponent($));

    window.top.customerListPopup = customerListPopup;

    window.successfulUpdatingCallback = (isPartialUpdate, isMultipleUpdate) => customerListPopup.successfulUpdatingCallback();

    window.actionAfterUpdatedLiveCasinoSettingMultiple = isUpdatePartial => customerListPopup.successfulUpdatingCallback();
    window.actionAfterUpdatedLiveCasinoSettingSingle = isUpdatePartial => customerListPopup.successfulUpdatingCallback();

    window.successfulUpdatingCallbackSportbook = () => customerListPopup.successfulUpdatingCallbackSportbook();
    customerList.init();
});
