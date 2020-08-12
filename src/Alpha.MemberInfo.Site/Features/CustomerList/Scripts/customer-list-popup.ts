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

	public editPopupOnCustomerListOnclosing() {
		//this.topWindow.editPopupOnCustomerList.close();
	}

	public openPopup = (url, width = 800, height = 600) => {
		if (this.topWindow.editPopupOnCustomerList === null || this.topWindow.editPopupOnCustomerList === undefined) {
			this.topWindow.editPopupOnCustomerList = this.topWindow.ageWnd.createInstance();
		}

		this.topWindow.editPopupOnCustomerList.onClosing = this.editPopupOnCustomerListOnclosing;
		this.topWindow.popupManager.openWithHanlder(this.topWindow.editPopupOnCustomerList, url, '', width, height);
		this.topWindow.clearPopupSubtitle(true);
	}

	public openBootstrapModal = (event, data, buildPopupHtml, bindingPopupEvent) => {
		buildPopupHtml(data, this.$(event.currentTarget).data("custid"));
		bindingPopupEvent(event);

		this.$bootstrapContainer
			.css({ height: 'auto', width: 'auto', top: 0 })
			.modal({ show: true, backdrop: false });

		this.fixBootstrapDimension(event);
	}

	private fixBootstrapDimension = event => {
		const element = this.$(event.currentTarget),
			left = this.$(element).offset().left + this.$(element).outerWidth(),
			height = this.$bootstrapModalDialog.outerHeight(),
			width = this.$bootstrapModalDialog.outerWidth(),
			bodyHeight = this.$('body').height();

		let top = this.$(element).offset().top;

		if (bodyHeight - top < height) {
			top = bodyHeight - height;
		}

		this.$bootstrapContainer.css({top, left, height, width});
	}
}