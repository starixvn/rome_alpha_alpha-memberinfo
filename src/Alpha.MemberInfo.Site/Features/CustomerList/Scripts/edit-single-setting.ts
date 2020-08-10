class EditSingleSetting {
	$: JQueryStatic;
	customerListPopup: CustomerListPopup;
	customerListServices: CustomerListServices;

	constructor(
		$: JQueryStatic,
		customerListPopup: CustomerListPopup,
		customerListServices: CustomerListServices) {
		this.$ = $;
		this.customerListPopup = customerListPopup;
		this.customerListServices = customerListServices;
	}

	bindingEvent = () => this.$('.edit').click(event => this.openPopup(event));

	openPopup = event => {
		const custId = this.$(event.currentTarget).data("custid");
		const settings = this.customerListServices.statusSettingArray.filter(x => x.custId === custId);

		settings.length > 0 ?
			this.customerListPopup.openBootstrapModal(
				event,
				settings[0].data.Settings,
				this.buildPopupHtml,
				this.bindingEventOnPopup) :
			this.customerListServices.getSettings(
				custId,
				this.$(event.currentTarget).data("url"),
				data => this.customerListPopup.openBootstrapModal(
					event,
					data.Settings,
					this.buildPopupHtml,
					this.bindingEventOnPopup));
	}

	private buildPopupHtml = settings => {
		let html = ['<ul>'];
		this.$.each(settings, (key, item) => {
			if (item.Visible) {
				html.push(`<li class="edit-popup" data-url="${item.Url}">${item.Text}</li>`);
			}
		});

		this.$('.modal-body').empty().append(html.join(''));
	}

	private bindingEventOnPopup = () => this.$('.edit-popup').click(event => this.customerListPopup.openPopup(this.$(event.currentTarget).data("url"), 800, 600));
}