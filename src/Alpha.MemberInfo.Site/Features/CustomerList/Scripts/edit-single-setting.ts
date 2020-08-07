interface Window {
	$: any
}

class EditSingleSetting {
	$: JQueryStatic;
	topWindow: any;
	customerListPopup: CustomerListPopup;

	constructor($: JQueryStatic, topWindow: any, customerListPopup: CustomerListPopup) {
		this.$ = $;
		this.topWindow = topWindow;
		this.customerListPopup = customerListPopup;
	}

	buildEditSingleSettingPopupHtml = data => {
		let html = ['<ul>'];
		this.$.each(data.Settings, (key, item) => {
			if (item.Visible) {
				html.push(`<li class="edit-popup" data-url="${item.Url}">${item.Text}</li>`);
			}
		});

		this.$('.modal-body').empty().append(html.join(''));
	}

	bindingEditSinglePopup = () => {
		this.$('.edit-popup').click(event => {
			const url = this.$(event.currentTarget).data("url");
			this.customerListPopup.openPopup(url, 800, 600);
		});
	}
}