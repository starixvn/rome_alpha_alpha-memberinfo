interface Window {
	$: any
}

class EditMultipleSetting {
	$: JQueryStatic;
	topWindow: any;
	customerListPopup: CustomerListPopup;
	customerListServices: CustomerListServices;
	editSingleSetting: EditSingleSetting;

	constructor(
		$: JQueryStatic,
		topWindow: any,
		customerListPopup: CustomerListPopup,
		customerListServices: CustomerListServices,
		editSingleSetting: EditSingleSetting) {
		this.$ = $;
		this.topWindow = topWindow;
		this.customerListPopup = customerListPopup;
		this.customerListServices = customerListServices;
		this.editSingleSetting = editSingleSetting;
	}

	bindingEditMultipleSettingOnMainPage = (event) => {
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

		if (custids.length > 0) {
			this.$('.edit-multiple').data("custid", custids[0]);
			this.$('.edit-multiple').data("url", this.$(`#setting-${custids[0]}`).data('url'));

			custids.length === 1 ?
				this.editSingleSetting.bindingEditSingleOnMainPage(event) : 
				this.customerListServices.getSettings(custids[0], `${memberInfoUrl}CustomerList/MultipleEditPopup?${this.$.param(params)}`, true, data => {
				this.customerListPopup.openBootstrapPopupModal(
					event,
					data,
					this.buildEditMultipleSettingPopupHtml,
					this.bindingEditMultipleSettingOnPopup);
			});
		}
	}

	private bindingEditMultipleSettingOnPopup = () => {
		this.$('.edit-multiple-settings').click(event => this.customerListPopup.openPopup(this.$(event.currentTarget).data("url"), 800, 600));
	}

	private buildEditMultipleSettingPopupHtml = (settings) => {
		let html = ['<ul>'];
		this.$.each(settings, (key, item) => {
			if (item.Visible) {
				html.push(`<li class="edit-multiple-settings" data-url="${item.Url}">${item.Text}</li>`);
			}
		});

		this.$('.modal-body').empty().append(html.join(''));
	}
}