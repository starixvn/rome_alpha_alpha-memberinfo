class EditMultipleSetting {
	private $: JQueryStatic;
	private customerListPopup: CustomerListPopup;
	private customerListServices: CustomerListServices;
	private editSingleSetting: EditSingleSetting;
	private viewModel: CustomerListViewModel;

	constructor(
		$: JQueryStatic,
		viewModel: CustomerListViewModel,
		customerListPopup: CustomerListPopup,
		customerListServices: CustomerListServices,
		editSingleSetting: EditSingleSetting) {
		this.$ = $;
		this.viewModel = viewModel;
		this.customerListPopup = customerListPopup;
		this.customerListServices = customerListServices;
		this.editSingleSetting = editSingleSetting;
	}

	public bindingEvent = () => {
		this.$("div.top").prepend('<div class="select-all">Select <a class="check-all">All</a>|<a class="uncheck-all">None</a></div>');

		this.$('.edit-multiple').click(event => this.openPopup(event));

		this.$('.check-all').click(() => {
			this.$('tbody .checkbox').prop('checked', true);
		});

		this.$('.uncheck-all').click(() => {
			this.$('tbody .checkbox').prop('checked', false);
		});
	}

	private openPopup = event => {
		let usernames = [], custids = [];
		this.$('input.checkbox:checkbox:checked').each((index, element) => {
			usernames.push(this.$(element).data("username"));
			custids.push(this.$(element).data("custid"));
		});

		const params = {
			UplineId: this.viewModel.ActorId,
			UplineName: this.viewModel.ActorName,
			UplineRoleId: this.viewModel.ActorRoleId,
			SearchAccountRoleId: this.viewModel.SearchAccountRoleId - 1,
			custids: custids.join(','),
			usernames: usernames.join(',')
		};

		if (custids.length > 0) {
			this.$('.edit-multiple').data("custid", custids[0]);
			this.$('.edit-multiple').data("url", this.$(`#setting-${custids[0]}`).data('url'));

			this.$('#arrayCustID').val(custids.join('^'));
			this.$('#arrayUserName').val(usernames.join('^'));

			//Synced Casino Array
			//this.$('#arrayStatusRnCasino').val(custids.join('^'));

			custids.length === 1 ?
				this.editSingleSetting.openPopup(event) :
				this.customerListServices.getSettings(
					custids[0],
					`${this.viewModel.MemberInfoUrl}CustomerList/MultipleEditPopup`,
					data => this.customerListPopup.openBootstrapModal(
						event,
						data,
						this.buildPopupHtml,
						this.bindingEventOnPopup),
					'POST',
					params,
					false);
		}
	}

	private buildPopupHtml = settings => {
		let html = ['<ul>'];
		this.$.each(settings, (key, item) => {
			html.push(`<li data-title='${item.Text}' data-params='${JSON.stringify(item.QueryStringParamaters)}' class="edit-multiple-settings ${item.Icon}" data-url="${item.Url}">${item.Text}</li>`);
		});

		this.$('.modal-body').empty().append(html.join(''));
	}

	private bindingEventOnPopup = () => this.$('.edit-multiple-settings').click(event => this.customerListPopup.openProductPopup(event, 'post'))
}