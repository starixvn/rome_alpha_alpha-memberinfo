interface Window {
	$: any
}

class EditStatus {
	$: JQueryStatic;
	topWindow: any;
	customerListServices: CustomerListServices;

	constructor($: JQueryStatic, topWindow: any, customerListServices: CustomerListServices) {
		this.$ = $;
		this.topWindow = topWindow;
		this.customerListServices = customerListServices;
	}

	buildEditStatusPopupHtml = (data, custId) => {
		let html = ['<ul>'];
		this.$.each(data.Status, (key, item) => {
			if (item.Visible && item.ShowInCustomerList) {
				html.push(`<li class="edit-status-popup" data-custid='${custId}' data-displayorder='${item.DisplayOrder}' data-url="${item.Url}" data-param='${item.JsonParams}' data-message='${item.ConfirmMessage}'><input type="checkbox" ${item.Checked ? 'checked' : ''}/> ${item.Text}</li>`);
			}
		});

		this.$('.modal-body').empty().append(html.join(''));
	}

	bindingEditStatusPopup = parentEvent => {
		this.$('.edit-status-popup').click(event => {
			const $target = this.$(event.currentTarget),
				url = $target.data("url"),
				params = $target.data("param"),
				displayOrder = $target.data("displayorder"),
				message = $target.data("message");

			this.topWindow.bootboxConfirm(
				message,
				parentEvent.target,
				() => this.customerListServices.updateCustomerStatus(parentEvent, url, params, displayOrder)
			);
		});
	}
}