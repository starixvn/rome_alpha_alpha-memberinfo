interface Window {
	$: any
}

class EditStatus {
	$: JQueryStatic;
	topWindow: any;
	customerListServices: CustomerListServices;
	customerListPopup: CustomerListPopup;
	statusArray: any;

	constructor(
		$: JQueryStatic,
		topWindow: any,
		customerListServices: CustomerListServices,
		customerListPopup: CustomerListPopup) {
		this.$ = $;
		this.topWindow = topWindow;
		this.customerListServices = customerListServices;
		this.customerListPopup = customerListPopup;
	}

	bindingEditStatusOnMainPage = event => {
		this.customerListServices.getSettings(this.$(event.currentTarget).data("custid"), this.$(event.currentTarget).data("url"), true, data => {
			this.customerListPopup.openBootstrapPopupModal(
				event,
				data.Status,
				this.buildEditStatusPopupHtml,
				this.bindingEditStatusPopup);
		});
	}

	private buildEditStatusPopupHtml = (statusArray, custId) => {
		let html = ['<ul>'];
		this.$.each(statusArray, (key, item) => {
			if (item.Visible && item.ShowInCustomerList) {
				html.push(`<li class="edit-status-popup" data-custid='${custId}' data-displayorder='${item.DisplayOrder}' data-url="${item.Url}" data-param='${item.JsonParams}' data-message='${item.ConfirmMessage}'><input type="checkbox" ${item.Checked ? 'checked' : ''}/> ${item.Text}</li>`);
			}
		});

		this.$('.modal-body').empty().append(html.join(''));
	}

	private  bindingEditStatusPopup = parentEvent => {
		this.$('.edit-status-popup').click(event => {
			const $target = this.$(event.currentTarget),
				url = $target.data("url"),
				params = $target.data("param"),
				displayOrder = $target.data("displayorder"),
				message = $target.data("message");

			this.topWindow.bootboxConfirm(
				message,
				parentEvent.target,
				() => this.customerListServices.updateCustomerStatus(parentEvent, url, params, displayOrder, this.updateCustomerStatusSuccess)
			);
		});
	}

	private updateCustomerStatusSuccess = (parentEvent, data, displayOrder) => {
		if (data.IsSuccessful) {
			// Closed or Suspended
			if (displayOrder === 1 || displayOrder === 2) {
				this.$('.btn-submit').click();
			}
		}
		else {
			this.topWindow.bootboxAlert(data.Message, parentEvent.target);
		}
	}
}