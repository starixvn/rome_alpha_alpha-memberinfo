class CustomerListPopup {
	private $: JQueryStatic;
	private topWindow: any;
	private $bootstrapContainer: JQuery;
	private $bootstrapModalDialog: JQuery;

	constructor($: JQueryStatic, topWindow: any) {
		this.$ = $;
		this.topWindow = topWindow;
		this.cacheDom();
	}

	private cacheDom = () => {
		this.$bootstrapContainer = this.$('#bootstrap-modal');
		this.$bootstrapModalDialog = this.$('.modal-dialog');
	}

	//private editPopupOnCustomerListOnclosing() {
	//	//this.topWindow.editPopupOnCustomerList.close();
	//}

	//public openPopupPreUrl = (url, width = 800, height = 600) => {
	//	this.topWindow.popupManager.open('', '', 880, null, null, null, null, null, null, {
	//		preUrl: url
	//	});
	//}
	//public openPopup = (url, width = 800, height = 600) => {
	//	if (this.topWindow.editPopupOnCustomerList === null || this.topWindow.editPopupOnCustomerList === undefined) {
	//		this.topWindow.editPopupOnCustomerList = this.topWindow.ageWnd.createInstance();
	//	}

	//	this.topWindow.editPopupOnCustomerList.onClosing = this.editPopupOnCustomerListOnclosing;
	//	this.topWindow.popupManager.openWithHanlder(this.topWindow.editPopupOnCustomerList, url, '', width, height);
	//	this.topWindow.clearPopupSubtitle(true);
	//}

	public openProductPopup = (event, method = 'get') => {
		this.$('#myIframeForm').attr('action', this.$(event.currentTarget).data("url"));
		this.$('#myIframeForm').attr('method', method);

		const params = this.$(event.currentTarget).data("params");

		this.$('#myIframeForm').empty();

		if (params !== undefined && params !== null) {
			const attrs = this.$.map(params, (key, value) => {
				return `<input type="hidden"  name='${value}' id='${value}' value='${key}'/>`;
			}).join(' ');

			this.$('#myIframeForm').append(attrs);
		}

		this.$('#myIframeForm').submit();

		this.$('#myIframe').css({ width: '700px', height: '700px' });

		this.$('#bootstrap-modal-product')
			.css({ height: '750px', width: '710px', top: 10 })
			.modal({ show: true, backdrop: false });
	}

	public bindingBootstrapModalEvent = () => this.$bootstrapContainer.on('mouseleave', () => this.$bootstrapContainer.modal('hide'));

	public openBootstrapModal = (event, data, buildPopupHtml, bindingPopupEvent) => {
		buildPopupHtml(data, this.$(event.currentTarget).data("custid"));
		bindingPopupEvent(event);

		this.$bootstrapContainer
			.css({ height: 'auto', width: 'auto', top: 0 })
			.modal({ show: true, backdrop: false });

		this.fixBootstrapDimension(event);
	}

	private fixBootstrapDimension = event => {
		this.$bootstrapModalDialog.css({ width: 'inherit' });
		const element = this.$(event.currentTarget),
			left = this.$(element).offset().left + this.$(element).width(),
			height = this.$bootstrapModalDialog.outerHeight(),
			width = this.$bootstrapModalDialog.outerWidth(),
			bodyHeight = this.$('body').height();

		let top = this.$(element).offset().top - this.$(document).scrollTop();

		if (bodyHeight - top < height) {
			top = bodyHeight - height;
		}

		this.$bootstrapContainer.css({ top, left, height, width });
	}
}