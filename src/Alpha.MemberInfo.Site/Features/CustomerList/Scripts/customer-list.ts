class CustomerList {
	private $: JQueryStatic;
	private customerListPopup: CustomerListPopup;
	private editMultipleSetting: EditMultipleSetting;
	private customerListDataTable: CustomerListDataTable;
	private viewModel: CustomerListViewModel;

	constructor(
		$: JQueryStatic,
		viewModel: CustomerListViewModel,
		editMultipleSetting: EditMultipleSetting,
		customerListPopup: CustomerListPopup,
		customerListDataTable: CustomerListDataTable) {
		this.$ = $;
		this.viewModel = viewModel;
		this.customerListDataTable = customerListDataTable;
		this.customerListPopup = customerListPopup;
		this.editMultipleSetting = editMultipleSetting;
	}

	public init = () => {
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
		parseInt($('#actorId').val()),
		$('#actorName').val(),
		parseInt($('#actorRoleId').val()),
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
		$('#accountInfoUrl').val()
	);

	const customerListPopup = new CustomerListPopup($, window.top),
		customerListServices = new CustomerListServices($, viewModel),
		editSingleSetting = new EditSingleSetting($, customerListPopup, customerListServices),
		editMultipleSetting = new EditMultipleSetting($, viewModel, customerListPopup, customerListServices, editSingleSetting),
		editStatus = new EditStatus($, window.top, customerListServices, customerListPopup),
		customerListDataTable = new CustomerListDataTable($, viewModel, customerListPopup, customerListServices, editSingleSetting, editStatus),
		customerList = new CustomerList($, viewModel, editMultipleSetting, customerListPopup, customerListDataTable);

	customerList.init();
});