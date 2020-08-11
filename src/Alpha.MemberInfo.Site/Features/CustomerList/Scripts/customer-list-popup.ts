class CustomerListPopup {
	private $: JQueryStatic;
	private topWindow: any;
	private $bootstrapContainer: JQuery;
	private $bootstrapModalDialog: JQuery;
	private $bootstrapModalContent: JQuery;

	constructor($: JQueryStatic, topWindow: any) {
		this.$ = $;
		this.topWindow = topWindow;
		this.cacheDom();
	}

	private cacheDom = () => {
		this.$bootstrapContainer = this.$('#bootstrap-modal');
		this.$bootstrapModalDialog = this.$('.modal-dialog');
		this.$bootstrapModalContent = this.$('.modal-content');
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

	public bindingBootstrapModalEvent = () => {
		this.$bootstrapModalContent.on('mouseleave', () => this.$bootstrapContainer.modal('hide'));

		this.$bootstrapContainer.on('shown.bs.modal', () => {
			const bodyHeight = this.$('body').height();
			const height = this.$bootstrapModalDialog.height();
			const width = this.$bootstrapModalDialog.width();

			this.$bootstrapContainer.height(height);

			if (bodyHeight - this.$bootstrapContainer.position().top < height) {
				this.$bootstrapContainer.css({
					top: bodyHeight - height,
					height: height === 0 ? 'auto' : height,
					width: width === 0 ? 'auto' : width
				});
			}
		});
	}

	public openBootstrapModal = (event, data, buldPopupHtml, bindingSettingEvent) => {
		buldPopupHtml(data, this.$(event.currentTarget).data("custid"));

		const element = this.$(event.currentTarget),
			left = this.$(element).offset().left + this.$(element).width() + 5,
			height = this.$bootstrapModalDialog.outerHeight(),
			width = this.$bootstrapModalDialog.outerWidth(),
			bodyHeight = this.$('body').height();

		let top = this.$(element).offset().top;

		if (bodyHeight - top < height) {
			top = bodyHeight - height;
		}

		this.$bootstrapContainer.css({
			top: top,
			left: left,
			height: height === 0 ? 'auto' : height,
			width: width === 0 ? 'auto' : width
		}).modal({
			show: true,
			backdrop: false
		});

		bindingSettingEvent(event);
	}
}