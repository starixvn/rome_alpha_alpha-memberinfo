declare var memberInfoUrl;
declare var accountInfoUrl;
declare var actorId;
declare var actorName;
declare var actorRoleId;
declare var searchAccountRoleId;

class CustomerList {
	$: JQueryStatic;
	customerListPopup: CustomerListPopup;
	editMultipleSetting: EditMultipleSetting;
	customerListDataTable: CustomerListDataTable;

	constructor(
		$: JQueryStatic,
		editMultipleSetting: EditMultipleSetting,
		customerListPopup: CustomerListPopup,
		customerListDataTable: CustomerListDataTable) {
		this.$ = $;
		this.customerListDataTable = customerListDataTable;
		this.customerListPopup = customerListPopup;
		this.editMultipleSetting = editMultipleSetting;
	}

	init = () => {
		this.bidingEasyAutoComplete();

		this.customerListDataTable.initDataTable();

		this.editMultipleSetting.bindingEditMultipleEvent();

		this.customerListPopup.bindingBootstrapModalEvent();
	};

	bidingEasyAutoComplete = () => this.$('#txt-username').easyAutocomplete({
		url: phrase => `${memberInfoUrl}UserNameSuggestion/GetUsernamesSuggestion?SuggestionQuery=${phrase}&DirectDownlines=true`,
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
	const customerListPopup = new CustomerListPopup($, window.top),
		customerListServices = new CustomerListServices($, customerListPopup),
		editSingleSetting = new EditSingleSetting($, customerListPopup, customerListServices),
		editMultipleSetting = new EditMultipleSetting($, customerListPopup, customerListServices, editSingleSetting),
		editStatus = new EditStatus($, window.top, customerListServices, customerListPopup),
		customerListDataTable = new CustomerListDataTable($, customerListPopup, customerListServices, editSingleSetting, editStatus),
		customerList = new CustomerList($, editMultipleSetting, customerListPopup, customerListDataTable);

	customerList.init();
});