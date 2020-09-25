"use strict";

class EditSingleSetting {
	private $: JQueryStatic;
	private customerListPopup: CustomerListPopup;
	private customerListServices: CustomerListServices;
	private isMobile: boolean;

	constructor(
		$: JQueryStatic,
		isMobile: boolean,
		customerListPopup: CustomerListPopup,
		customerListServices: CustomerListServices) {
		this.$ = $;
		this.isMobile = isMobile;
		this.customerListPopup = customerListPopup;
		this.customerListServices = customerListServices;
	}

	public bindingEvent = () => this.$('.edit').unbind().bind('click', event => this.openPopup(this.$(event.currentTarget)));

	public openPopup = element => {
		const custId = this.$(element).data("custid");
		const settings = this.customerListServices.statusSettingArray.filter(x => x.custId === custId);

		settings.length > 0 ?
			this.customerListPopup.openBootstrapModal(
				element,
				settings[0].data.Settings,
				this.buildPopupHtml,
				this.bindingPopupEvent) :
			this.customerListServices.getSettings(
				custId,
				this.$(element).data("url"),
				data => this.customerListPopup.openBootstrapModal(
					element,
					data.Settings,
					this.buildPopupHtml,
					this.bindingPopupEvent));
	}

	private buildPopupHtml = settings => {
		let html = ['<ul>'];
		this.$.each(settings, (key, item) => {
			html.push(`<li data-title='${item.Text}' data-params='${JSON.stringify(item.QueryStringParamaters)}' class="edit-popup ${item.Icon}" data-url="${item.Url}">${item.Text}</li>`);
		});

		this.$('#bootstrap-modal').find('.modal-body').empty().append(html.join(''));

		if (this.isMobile) {
			this.$('#bootstrap-modal').find('.modal-title').empty().append('Edit Setting');
		}
	}

	private bindingPopupEvent = () => this.$('.edit-popup').click(event => this.customerListPopup.openProductPopup(event.currentTarget))
}