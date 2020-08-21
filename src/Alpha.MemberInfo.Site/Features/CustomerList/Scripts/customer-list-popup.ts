class CustomerListPopup {
	private $: JQueryStatic;
	private $bootstrapContainer: JQuery;
	private $bootstrapModalDialog: JQuery;
	private topWindow: any;
	private isMobile: boolean;

	constructor($: JQueryStatic, topWindow: any, isMobile: boolean) {
		this.$ = $;
		this.isMobile = isMobile;
		this.topWindow = topWindow;
		this.cacheDom();
	}

	private cacheDom = () => {
		this.$bootstrapContainer = this.$('#bootstrap-modal');
		this.$bootstrapModalDialog = this.$('.modal-dialog');
	}

	public openProductPopup = (event, method = 'get') => {
		this.$.blockUI();

		this.$('#myIframe').attr('src', '');
		this.$('#myIframeForm').attr('action', this.$(event.currentTarget).data("url"));
		this.$('#myIframeForm').attr('method', method);

		const params = this.$(event.currentTarget).data("params");

		this.$('#myIframeForm').empty();

		if (params !== undefined && params !== null) {
			const attrs = this.$.map(params, (value, key) => `<input type="hidden"  name='${key}' id='${key}' value='${value}'/>`).join(' ');

			this.$('#myIframeForm').append(attrs);
		}

		this.$('#myIframeForm').submit();

		//const width = this.$(event.currentTarget).data("width"),
		//	height = this.$(event.currentTarget).data("height");

		//this.$('#myIframe').css({ width: `${width}`, height: `${height}` });


		const bodyWidth = this.$(window).width() - this.topWindow.$('#side-bar').width();

		let width = '100%';
		if (!this.isMobile) {
			width = bodyWidth < 1160 ? `100%` : '1160px';
		}

		this.$('#bootstrap-modal-product .modal-title').text(this.$(event.currentTarget).data("title"));
		this.$('#bootstrap-modal-product')
			.css({ height: '100%', width })
			.modal({ show: true, backdrop: false });

		this.$.unblockUI();
	}

	private initFrame = () => {
		setTimeout(() => {
			const headerClassesOrId = '.setting-header-agency,#header_main,#race-book-positiontaking-header,#title_header,#button_hr';
			this.$('#myIframe').contents().find(headerClassesOrId).css('display', 'none');
		}, 100);

		

		//setTimeout(() => {
		//	const height01 = this.$('#myIframe').contents().find('body').outerHeight();
		//	const height02 = this.topWindow.$('#main').contents().find('#bootstrap-modal-product').find('.modal-header').outerHeight();
		//	const paddingBottom = 20;

		//	const mainBodyHeight = this.topWindow.$('#main').contents().find('body').height() - height02;

		//	const height = height01 > mainBodyHeight ? '100%' : height01 + height02 + paddingBottom;

		//	//const width = this.$('#myIframe').contents().find('body').outerWidth();

		//	//const height = this.$('#bootstrap-modal-product').find('.modal-dialog').height();
		//	this.topWindow.$('#main').contents().find('#bootstrap-modal-product').css({ height });
		//}, 1500);
		
	}

	public bindingBootstrapModalEvent = () => {
		this.$bootstrapContainer.on('mouseleave ', () => this.$bootstrapContainer.modal('hide'));

		//this.$("#bootstrap-modal").on("hide.bs.modal", () => {
		//	this.topWindow.$('#content').css({ padding: '' });
		//});

		this.$('#close-edit-popup').click(() => {
			this.topWindow.$('#content').css({ padding: '' });
			this.closeStatusPopup();
		});

		this.$('#close-product-popup').click(() => {
			this.topWindow.$('#content').css({ padding: '' });
			this.$('#myIframe').attr('src', 'about:blank');
			this.$("#bootstrap-modal-product").modal('hide');

		});

		//this.$("#bootstrap-modal-product").on("hide.bs.modal-product", () => {
		//	this.topWindow.$('#content').css({ padding: '' });
		//	this.$('#myIframe').attr('src', 'about:blank');
		//});

		//this.$("#bootstrap-modal-product").on("hidden.bs.modal", () => {
		//});


		this.$('#bootstrap-modal-product').on('shown.bs.modal', () => {
			this.topWindow.$('#content').css({ padding: '0px' });
			this.initFrame.apply(this.$('#myIframe').contents());

			//const height01 = this.$('#myIframe').contents().find('body').outerHeight();
			//const height = this.$('#bootstrap-modal-product').find('.modal-dialog').height();
			//this.$('#bootstrap-modal-product').css({ height01 });
		});
	}

	public closeStatusPopup = () => this.$bootstrapContainer.modal('hide');

	public openBootstrapModal = (event, data, buildPopupHtml, bindingPopupEvent) => {
		buildPopupHtml(data, this.$(event.currentTarget).data("custid"));
		bindingPopupEvent(event);

		if (this.isMobile) {
			this.topWindow.$('#content').css({ padding: '0px' });

			this.$bootstrapContainer.find('.modal-dialog').css({ height: '100%', width: '100%' })
			this.$bootstrapContainer.css({ top: 0, left: 0, height: '100%', width: '100%', paddingRight: '0px' }).modal({ show: true, backdrop: false });

		}
		else {
			this.$bootstrapContainer
				.css({ height: 'auto', width: 'auto', top: 0 })
				.modal({ show: true, backdrop: false });

			this.fixBootstrapDimension(event);
		}

		//if (this.isMobile) {
		//	setTimeout(() => this.$bootstrapContainer.modal('hide'), 5000);
		//}
	}

	private fixBootstrapDimension = event => {
		if (this.isMobile) {
			this.$bootstrapContainer.css({ top : 0, left: 0, height: '100%', width: '100%' });
		}
		else {
			this.$bootstrapModalDialog.css({ width: 'inherit' });
			const element = this.$(event.currentTarget),
				height = this.$bootstrapModalDialog.outerHeight(),
				width = this.$bootstrapModalDialog.outerWidth(),
				bodyHeight = this.$('body').height();

			let top = this.$(element).offset().top - this.$(document).scrollTop(),
				left = this.$(element).offset().left + this.$(element).width();

			if (bodyHeight - top < height) {
				top = bodyHeight - height;
			}

			if (this.$(window).width() - left < width) {
				left = left - width;
			}

			this.$bootstrapContainer.css({ top, left, height, width });
		}
	}
}