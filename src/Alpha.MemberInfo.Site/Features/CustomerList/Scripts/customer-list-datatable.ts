class CustomerListDataTable {
	private $: JQueryStatic;
	private $table: JQuery;
	private customerListPopup: CustomerListPopup;
	private customerListServices: CustomerListServices;
	private editSingleSetting: EditSingleSetting;
	private editStatus: EditStatus;
	private viewModel: CustomerListViewModel;

	constructor(
		$: JQueryStatic,
		viewModel: CustomerListViewModel,
		customerListPopup: CustomerListPopup,
		customerListServices: CustomerListServices,
		editSingleSetting: EditSingleSetting,
		editStatus: EditStatus) {
		this.$ = $;
		this.viewModel = viewModel;
		this.customerListPopup = customerListPopup;
		this.customerListServices = customerListServices;
		this.editSingleSetting = editSingleSetting;
		this.editStatus = editStatus;
	}

	public initDataTable = () => {
		this.$table = this.$('#customer-list').on('processing.dt',
			(e, settings, processing) => processing ? this.$.blockUI() : this.$.unblockUI())
			.DataTable({
				serverSide: true,
				ajax: (data, callback) => this.customerListServices.getCutomerList(data, callback),
				columns: this.buildDataTableColumns(),
				responsive: {
					details: {
						type: 'column',
						target: -1,
						auto: false,
						renderer: (api, rowIdx) => this.renderDetailColumn(api, rowIdx)
					}
				},
				columnDefs: [{
					className: 'control',
					orderable: false,
					targets: -1
				}],
				ordering: false,
				paging: true,
				pagingType: "input",
				pageLength: this.viewModel.IsMobileView ? 50 : 500,
				searching: false,
				bInfo: false,
				fixedHeader: true,
				processing: true,
				dom: '<"top"iflp<"clear">>rt<"bottom"ifp<"clear">>',
				language: {
					processing: '',
					lengthMenu: 'Page Size <select>' +
						'<option value="10">10</option>' +
						'<option value="50">50</option>' +
						'<option value="100">100</option>' +
						'<option value="500">500</option>' +
						'</select>',
					oPaginate: {
						sFirst: '<<',
						sPrevious: '<',
						sNext: '>',
						sLast: '>>'
					}
				},
				drawCallback: this.drawCallback
			});
	}

	private buildDataTableColumns = () => {
		let columns = [
			{
				"className": "",
				"data": "RowNumber"
			},
			{
				"className": "",
				"data": null,
				"render": item => this.renderUsernameColumn(item)
			},
			{
				"className": "",
				"data": item => this.renderStatusColumn(item)
			},
			{
				"className": "",
				"data": null,
				"render": item => this.renderEditColumn(item)
			},
			{
				"className": "",
				"data": null,
				"render": item => this.renderStatementColumn(item)
			},
			{
				"className": "",
				"data": "FirstName"
			},
			{
				"className": "",
				"data": "LastName"
			},
			{
				"className": "",
				"data": null,
				"render": item => item.RoleId == CustomerLevel.Member ? item.Discount : item.GroupA
			},
			{
				"className": "",
				"data": null,
				"render": item => item.RoleId == CustomerLevel.Member ? item.Discount1x2 : item.GroupB
			},
			{
				"className": "",
				"data": null,
				"render": item => item.RoleId == CustomerLevel.Member ? item.DiscountCS : item.GroupC
			},
			{
				"className": "",
				"data": null,
				"render": item => item.RoleId == CustomerLevel.Member ? item.PlayerDiscount : item.GroupD
			},
			{
				"className": "",
				"data": null,
				"render": item => item.RoleId == CustomerLevel.Member ? item.PlayerDiscount1x2 : item.Discount1x2
			},
			{
				"className": "",
				"data": null,
				"render": item => item.RoleId == CustomerLevel.Member ? item.PlayerDiscountCS : item.DiscountCS
			},
			{
				"className": "",
				"data": "CreateDate"
			},
			{
				"className": "",
				"data": null,
				"render": item => `<div class='login-ip' data-url='${`${this.viewModel.OldMainUrl}_IPInfo/IPInfo.aspx?ip=${item.LastLoginIP}`}'>${item.LastLoginIP}</div>`
			},
			{
				"className": "",
				"data": null,
				"render": () => ''
			}
		];

		if (this.viewModel.ShowEditMultiple) {
			const editMultipleColumn = {
				"className": "",
				"data": null,
				"render": item => `<input type="checkbox" class="checkbox" data-custid="${item.CustId}" data-username="${item.Username}"/>`
			};

			columns.splice(1, 0, editMultipleColumn);
		}

		return columns;
	}

	private renderUsernameColumn = item => {
		const content = `${item.Username} <br /> ${item.UserName2 === '' || item.UserName2 === null ? "--" : item.UserName2}`;
		const queryString = {
			CustLevelId: item.RoleId,
			CustId: item.CustId,
			CustName: item.Username,
			Status: this.$('#statusFilter').val(),
			DoubleComm: this.$('#dcommFilter').val(),
			UnconfirmSetting: this.$('#unconfirmSettingFilter').val()
		};

		const viewDownlineLink = `${this.viewModel.MemberInfoUrl}CustomerList/${this.viewModel.DownlineLevelName}?${this.$.param(queryString)}&BreadcrumbLevelJson=${this.viewModel.BreadcrumbLevelJson}`;
		return item.RoleId === CustomerLevel.Member ? content : `<a href="${viewDownlineLink}">${content}</a>`;
	}

	private renderEditColumn = item => `<div class='edit' id='setting-${item.CustId}' data-custid='${item.CustId}' data-url='${this.buildGetAllStatusLink(item)}'>Edit</div>`;

	private renderStatusColumn = item => `<div id='status-${item.CustId}' class='status-popup' data-custid='${item.CustId}' data-url='${this.buildGetAllStatusLink(item)}'>${item.Status}</div>`;

	private buildGetAllStatusLink = item => {
		const queryString = {
			SearchAccountId: item.CustId,
			SearchAccountName: item.Username,
			SearchAccountRoleId: item.RoleId,
			UplineId: this.viewModel.ActorId,
			UplineName: this.viewModel.ActorName,
			UplineRoleId: this.viewModel.ActorRoleId
		};

		return `${this.viewModel.MemberInfoUrl}CustomerList/GetCustomerAllStatus?${this.$.param(queryString)}`;
	}

	private renderStatementColumn = item => {
		const queryString = {
			CustId: item.CustId,
			CustName: item.Username,
			CustLevelId: item.RoleId
		}, actionName = item.RoleId === CustomerLevel.Member ? 'MemberStatement' : 'SMAStatement';

		const viewStatementLink = `${this.viewModel.ReportsUrl}Statement/${actionName}?${this.$.param(queryString)}`;
		return `<a href='${viewStatementLink}'>View</a>`;
	}

	private renderDetailColumn = (api, rowIdx) => {
		const self = this;
		let addTableCommissionTag = true;
		const data = api.cells(rowIdx, ':hidden').eq(0).map(function (cell) {
			const header = self.$(api.column(cell.column).header());
			const idx = api.cell(cell).index();

			if (header.hasClass('control') || header.hasClass('never')) {
				return '';
			}

			const dtPrivate = api.settings()[0];
			const cellData = dtPrivate.oApi._fnGetCellData(
				dtPrivate, idx.row, idx.column, 'display'
			);

			let commissionTable = '', start = 7, end = 11;
			if (self.viewModel.ShowEditMultiple) {
				start += 1;
				end += 1
			}

			if (idx.column >= start && idx.column <= end) {
				if (addTableCommissionTag) {
					addTableCommissionTag = false;
					commissionTable = commissionTable + "<tr><td><table><tr><td>Commison</td></tr>";
				}

				commissionTable = commissionTable + `<tr data-dtr-index="${idx.column}">` +
					`<td class="dtr-title">${header.text()}</td> ` +
					`<td class="dtr-data">${cellData}</td></tr>`;

				if (idx.column === end && !addTableCommissionTag) {
					commissionTable = commissionTable + '</table></td></tr>';
				}

				return commissionTable;
			}

			if (idx.column < start || idx.column > end) {
				return `<tr data-dtr-index="${idx.column}">` +
					`<td class="dtr-title">${header.text()}</td> ` +
					`<td class="dtr-data">${cellData}</td></tr>`;
			}

		}).toArray().join('');

		return data ?
			self.$(`<table data-dtr-index="${rowIdx}"/>`).append(data) : 
			false;
	}

	private drawCallback = () => {
		this.$('#customer-list').removeClass('hidden');

		this.$('.btn-submit').click(() => this.$table.ajax.reload());

		this.$('.login-ip').click(event => this.customerListPopup.openPopup(this.$(event.currentTarget).data("url"), 400, 600));

		this.editSingleSetting.bindingEvent();

		this.editStatus.bindingEvent();
	}
}