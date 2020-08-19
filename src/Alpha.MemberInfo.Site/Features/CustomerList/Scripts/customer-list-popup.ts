class CustomerListPopup {
	private $: JQueryStatic;
	private $bootstrapContainer: JQuery;
	private $bootstrapModalDialog: JQuery;

	constructor($: JQueryStatic) {
		this.$ = $;
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

		const width = this.$(event.currentTarget).data("width"),
			height = this.$(event.currentTarget).data("height");

		//this.$('#myIframe').css({ width: `${width}`, height: `${height}` });

		this.$('#bootstrap-modal-product .modal-title').text(this.$(event.currentTarget).data("title"));
		this.$('#bootstrap-modal-product')
			.css({ height: height, width: width })
			.modal({ show: true, backdrop: false });
	}

	public bindingBootstrapModalEvent = () => {
		this.$bootstrapContainer.on('mouseleave', () => this.$bootstrapContainer.modal('hide'));

		this.$("#bootstrap-modal-product").on("hidden.bs.modal",  () => {
			this.$('#myIframe').attr('src', 'about:blank');
		});

		this.$('#bootstrap-modal-product').on('shown.bs.modal', () => {
			const headerClassesOrId = '.setting-header-agency,#header_main,#race-book-positiontaking-header,#title_header,#button_hr';
			this.$('#myIframe').contents().find(headerClassesOrId).css('display', 'none');

			const height01 = this.$('#myIframe').contents().find('body').outerHeight();
			const height = this.$('#bootstrap-modal-product').find('.modal-dialog').height();
			this.$('#bootstrap-modal-product').css({ height01 });
			this.$.unblockUI();
		});
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