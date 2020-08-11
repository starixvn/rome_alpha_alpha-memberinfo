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

	public bindingEvent = () => this.$('.edit-multiple').click(event => this.openPopup(event));

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
					`${this.viewModel.MemberInfoUrl}CustomerList/MultipleEditPopup?${this.$.param(params)}`,
					data => this.customerListPopup.openBootstrapModal(
						event,
						data,
						this.buildPopupHtml,
						this.bindingEventOnPopup),
					false);
		}
	}

	private buildPopupHtml = settings => {
		let html = ['<ul>'];
		this.$.each(settings, (key, item) => {
			if (item.Visible) {
				html.push(`<li class="edit-multiple-settings" data-url="${item.Url}">${item.Text}</li>`);
			}
		});

		this.$('.modal-body').empty().append(html.join(''));
	}

	private bindingEventOnPopup = () => this.$('.edit-multiple-settings').click(event => this.customerListPopup.openPopup(this.$(event.currentTarget).data("url"), 800, 600));
}