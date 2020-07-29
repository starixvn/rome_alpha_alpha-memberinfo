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

	constructor($: JQueryStatic, topWindow: any) {
		super($, topWindow);
		this.topWindow = topWindow;
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

			const url = `${memberInfoUrl}CustomerList/MultipleEditPopup?${this.$.param(params)}`;

			this.openPopup(event, url);
		});

		this.$('.btn-submit').click(() => this.$table.ajax.reload());
	};

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
	const customerList = new CustomerList($, window.top);
	customerList.init();
});
