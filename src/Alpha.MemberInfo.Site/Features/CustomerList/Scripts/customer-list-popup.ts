class CustomerListPopup {
	$: JQueryStatic;
	topWindow: any;

	constructor($: JQueryStatic, topWindow: any) {
		this.$ = $;
		this.topWindow = topWindow;
	}

	editPopupOnCustomerListOnclosing() {
		//this.topWindow.editPopupOnCustomerList.close();
	}

	openPopup = (url, width = 800, height = 600) => {
		if (this.topWindow.editPopupOnCustomerList === null || this.topWindow.editPopupOnCustomerList === undefined) {
			this.topWindow.editPopupOnCustomerList = this.topWindow.ageWnd.createInstance();
		}

		this.topWindow.editPopupOnCustomerList.onClosing = this.editPopupOnCustomerListOnclosing;
		this.topWindow.popupManager.openWithHanlder(this.topWindow.editPopupOnCustomerList, url, '', width, height);
	}

	bindingBootstrapModalEvent = () => {
		this.$('.modal-content').on('mouseleave', () => {
			this.$('#exampleModal').modal('hide');
		});

		this.$('#exampleModal').on('shown.bs.modal', () => {
			const height = this.$('.modal-dialog').height();
			const width = this.$('.modal-dialog').width();
			const bodyHeight = this.$('body').height();

			this.$('#exampleModal').height(height);

			if (bodyHeight - this.$('#exampleModal').position().top < height) {
				this.$('#exampleModal').css({
					top: bodyHeight - height,
					height: height === 0 ? 'auto' : height,
					width: width === 0 ? 'auto' : width
				});
			}
		});
	}

	openBootstrapModal = (event, data, buldPopupHtml, bindingSettingEvent) => {
		buldPopupHtml(data, this.$(event.currentTarget).data("custid"));

		const element = this.$(event.currentTarget),
			left = this.$(element).offset().left + this.$(element).width() + 5,
			height = this.$('.modal-dialog').outerHeight(),
			width = this.$('.modal-dialog').outerWidth(),
			bodyHeight = this.$('body').height();

		let top = this.$(element).offset().top;

		if (bodyHeight - top < height) {
			top = bodyHeight - height;
		}

		this.$('#exampleModal').css({
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