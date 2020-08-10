interface Window {
	$: any
}

class EditSingleSetting {
	$: JQueryStatic;
	topWindow: any;
	customerListPopup: CustomerListPopup;
	customerListServices: CustomerListServices;

	constructor(
		$: JQueryStatic,
		topWindow: any,
		customerListPopup: CustomerListPopup,
		customerListServices: CustomerListServices) {
		this.$ = $;
		this.topWindow = topWindow;
		this.customerListPopup = customerListPopup;
		this.customerListServices = customerListServices;
	}

	bindingEditSingleOnMainPage = (event) => {
		const custId = this.$(event.currentTarget).data("custid");
		const settings = this.customerListServices.statusSettingArray.filter(x => x.custId === custId);

		if (settings.length > 0) {
			this.buildEditSingleSettingPopupHtml(settings[0].data);
			this.customerListPopup.openBootstrapPopupModal(event, settings[0].data.Settings, this.buildEditSingleSettingPopupHtml, this.bindingEditSinglePopup);
		}
		else {
			this.customerListServices.getSettings(custId, this.$(event.currentTarget).data("url"), true, data => {
				this.customerListPopup.openBootstrapPopupModal(
					event,
					data.Settings,
					this.buildEditSingleSettingPopupHtml,
					this.bindingEditSinglePopup);
			});
		}
	}

	private buildEditSingleSettingPopupHtml = settings => {
		let html = ['<ul>'];
		this.$.each(settings, (key, item) => {
			if (item.Visible) {
				html.push(`<li class="edit-popup" data-url="${item.Url}">${item.Text}</li>`);
			}
		});

		this.$('.modal-body').empty().append(html.join(''));
	}

	private  bindingEditSinglePopup = () => {
		this.$('.edit-popup').click(event => {
			const url = this.$(event.currentTarget).data("url");
			this.customerListPopup.openPopup(url, 800, 600);
		});
	}
}