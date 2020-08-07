interface Window {
	$: any
}

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

	openBootstrapPopupModal = (event, bindingSettingEvent) => {
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

	openPopup = (url, width = 800, height = 600) => {
		if (this.topWindow.editPopupOnCustomerList === null || this.topWindow.editPopupOnCustomerList === undefined) {
			this.topWindow.editPopupOnCustomerList = this.topWindow.ageWnd.createInstance();
		}

		this.topWindow.editPopupOnCustomerList.onClosing = this.editPopupOnCustomerListOnclosing;
		this.topWindow.popupManager.openWithHanlder(this.topWindow.editPopupOnCustomerList, url, '', width, height);
	}
}