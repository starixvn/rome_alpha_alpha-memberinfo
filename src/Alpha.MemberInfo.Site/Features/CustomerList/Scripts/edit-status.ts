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

	bindingEvent = () => this.$('.status-popup').click(event => this.openPopup(event));

	private openPopup = event => this.customerListServices.getSettings(
		this.$(event.currentTarget).data("custid"),
		this.$(event.currentTarget).data("url"),
		data => this.customerListPopup.openBootstrapModal(
			event,
			data.Status,
			this.buildPopupHtml,
			this.bindingEventOnPopup)
	)

	private buildPopupHtml = (statusArray, custId) => {
		let html = ['<ul>'];
		this.$.each(statusArray, (key, item) => {
			if (item.Visible && item.ShowInCustomerList) {
				html.push(`<li class="edit-status-popup" data-custid='${custId}' data-displayorder='${item.DisplayOrder}' data-url="${item.Url}" data-param='${item.JsonParams}' data-message='${item.ConfirmMessage}'><input type="checkbox" ${item.Checked ? 'checked' : ''}/> ${item.Text}</li>`);
			}
		});

		this.$('.modal-body').empty().append(html.join(''));
	}

	private bindingEventOnPopup = parentEvent => {
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

			setTimeout(() => {
				this.$.unblockUI();
			}, 1500);
		}
		else {
			this.$.unblockUI();
			this.topWindow.bootboxAlert(data.Message, parentEvent.target);
		}
	}
}