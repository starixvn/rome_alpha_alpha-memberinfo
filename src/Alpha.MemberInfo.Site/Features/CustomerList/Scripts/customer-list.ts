/// <reference path="customer-list-datatable.ts" />

declare var memberInfoUrl;
declare var accountInfoUrl;
declare var actorId;
declare var actorName;
declare var actorRoleId;
declare var searchAccountRoleId;

interface Window {
	$: any
}

class CustomerList extends CustomerListDataTable {
	$: JQueryStatic;
	$table: JQuery;
	topWindow: any;
	customerListPopup: CustomerListPopup;
	customerListServices: CustomerListServices;
	editSingleSetting: EditSingleSetting;
	editStatus: EditStatus;

	constructor(
		$: JQueryStatic,
		topWindow: any,
		customerListPopup: CustomerListPopup,
		customerListServices: CustomerListServices,
		editSingleSetting: EditSingleSetting,
		editStatus: EditStatus) {
		super($, topWindow, customerListPopup, customerListServices, editSingleSetting, editStatus);
	}

	init = () => {
		this.bidingEasyAutoComplete();

		this.initDataTable();

		this.$('.edit-multiple').click(event => {
			let usernames = [], custids = [];
			this.$('input.checkbox:checkbox:checked').each((index, element) => {
				usernames.push(this.$(element).data("username"));
				custids.push(this.$(element).data("custid"));
			});

			const params = {
				UplineId: actorId,
				UplineName: actorName,
				UplineRoleId: actorRoleId,
				SearchAccountRoleId: searchAccountRoleId - 1,
				custids: custids.join(','),
				usernames: usernames.join(',')
			};

			this.buildPopupHtml(
				event,
				`${memberInfoUrl}CustomerList/MultipleEditPopup?${this.$.param(params)}`,
				this.buildEditMultipleSettingPopupHtml,
				this.bindingEditMultipleSettingPopup,
				false);
		});

		this.$('.btn-submit').click(() => this.$table.ajax.reload());

		this.$('.modal-content').on('mouseleave', () => {
			this.$('#exampleModal').modal('hide');
		});

		this.$('#exampleModal').on('shown.bs.modal', () => {
			const height = this.$('.modal-dialog').height();
			const width = this.$('.modal-dialog').width();
			const bodyHeight = this.$('body').height();

			this.$('#exampleModal').height(height);

			if (bodyHeight - this.$('#exampleModal').position().top < height) {
				this.$('#exampleModal').css({
					top: bodyHeight - height,
					height: height === 0 ? 'auto' : height,
					width: width === 0 ? 'auto' : width
				});
			}
		});
	};

	bindingEditMultipleSettingPopup = () => {
		this.$('.edit-multiple-settings').click(event => this.customerListPopup.openPopup(this.$(event.currentTarget).data("url"), 800, 600));
	}

	buildEditMultipleSettingPopupHtml = (settings) => {
		let html = ['<ul>'];
		this.$.each(settings, (key, item) => {
			if (item.Visible) {
				html.push(`<li class="edit-multiple-settings" data-url="${item.Url}">${item.Text}</li>`);
			}
		});

		this.$('.modal-body').empty().append(html.join(''));
	}

	bidingEasyAutoComplete = () => {
		const options = {
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
		};

		this.$('#txt-username').easyAutocomplete(options);
	}
}

$(function () {
	const customerListPopup = new CustomerListPopup($, window.top),
		customerListServices = new CustomerListServices($, window.top),
		editSingleSetting = new EditSingleSetting($, window.top, customerListPopup),
		editStatus = new EditStatus($, window.top, customerListServices),
		customerList = new CustomerList($, window.top, customerListPopup, customerListServices, editSingleSetting, editStatus);

	customerList.init();
});
