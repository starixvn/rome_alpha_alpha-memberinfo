declare var oldMainUrl;
declare var reportsUrl;
declare var memberInfoUrl;
declare var accountInfoUrl;
declare var actorId;
declare var actorName;
declare var actorRoleId;
declare var showEditMultiple;
declare var searchAccountId;
declare var searchAccountName;
declare var searchAccountRoleId;
declare var breadcrumbLevelJson;
declare var downlineLevelName;

class CustomerListDataTable {
	$: JQueryStatic;
	$table: JQuery;
	customerListPopup: CustomerListPopup;
	customerListServices: CustomerListServices;
	editSingleSetting: EditSingleSetting;
	editStatus: EditStatus;

	constructor(
		$: JQueryStatic,
		customerListPopup: CustomerListPopup,
		customerListServices: CustomerListServices,
		editSingleSetting: EditSingleSetting,
		editStatus: EditStatus) {
		this.$ = $;
		this.customerListPopup = customerListPopup;
		this.customerListServices = customerListServices;
		this.editSingleSetting = editSingleSetting;
		this.editStatus = editStatus;
	}

	renderUsernameColumn = item => {
		const content = `${item.Username} <br /> ${item.UserName2 === '' || item.UserName2 === null ? "--" : item.UserName2}`;
		const queryString = {
			CustLevelId: item.RoleId,
			CustId: item.CustId,
			CustName: item.Username,
			Status: this.$('#statusFilter').val(),
			DoubleComm: this.$('#dcommFilter').val(),
			UnconfirmSetting: this.$('#unconfirmSettingFilter').val()
		};

		const viewDownlineLink = `${memberInfoUrl}CustomerList/${downlineLevelName}?${this.$.param(queryString)}&BreadcrumbLevelJson=${breadcrumbLevelJson}`;
		return item.RoleId === 1 ? content : `<a href="${viewDownlineLink}">${content}</a>`;
	}

	renderEditColumn = item => {
		const editPopupLink = `${memberInfoUrl}CustomerList/GetCustomerAllStatus?` +
			`SearchAccountId=${item.CustId}&SearchAccountName=${item.Username}&SearchAccountRoleId=${item.RoleId}` +
			`&UplineId=${actorId}&UplineName=${actorName}&UplineRoleId=${actorRoleId}`;
		return `<div class='edit' id='setting-${item.CustId}' data-custid='${item.CustId}' data-url='${editPopupLink}'>Edit</div>`;
	}

	renderStatusColumn = item => {
		const statusPopupLink = `${memberInfoUrl}CustomerList/GetCustomerAllStatus?` +
			`SearchAccountId=${item.CustId}&SearchAccountName=${item.Username}&SearchAccountRoleId=${item.RoleId}` +
			`&UplineId=${actorId}&UplineName=${actorName}&UplineRoleId=${actorRoleId}`;
		return `<div id='status-${item.CustId}' class='status-popup' data-custid='${item.CustId}' data-url='${statusPopupLink}'>${item.Status}</div>`;
	}

	renderStatementColumn = item => {
		const actionName = item.RoleId === 1 ? "MemberStatement" : "SMAStatement";
		const viewStatementLink = `${reportsUrl}Statement/${actionName}?CustID=${item.CustId}&CustName=${item.Username}&CustLevelId=${item.RoleId}`;
		return `<a href='${viewStatementLink}'>View</a>`;
	}

	buildDataTableColumns = () => {
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
				"render": item => item.RoleId == 1 ? item.Discount : item.GroupA
			},
			{
				"className": "",
				"data": null,
				"render": item => item.RoleId == 1 ? item.Discount1x2 : item.GroupB
			},
			{
				"className": "",
				"data": null,
				"render": item => item.RoleId == 1 ? item.DiscountCS : item.GroupC
			},
			{
				"className": "",
				"data": null,
				"render": item => item.RoleId == 1 ? item.PlayerDiscount : item.GroupD
			},
			{
				"className": "",
				"data": null,
				"render": item => item.RoleId == 1 ? item.PlayerDiscount1x2 : item.Discount1x2
			},
			{
				"className": "",
				"data": null,
				"render": item => item.RoleId == 1 ? item.PlayerDiscountCS : item.DiscountCS
			},
			{
				"className": "",
				"data": "CreateDate"
			},
			{
				"className": "",
				"data": null,
				"render": function (item) {
					const viewIP = `${oldMainUrl}_IPInfo/IPInfo.aspx?ip=${item.LastLoginIP}`;
					return `<div class='login-ip' data-url='${viewIP}'>${item.LastLoginIP}</div>`;
				}
			},
			{
				"className": "",
				"data": null,
				"render": () => ''
			}
		];

		if (showEditMultiple) {
			const editMultipleColumn = {
				"className": "",
				"data": null,
				"render": function (item) {
					return `<input type="checkbox" class="checkbox" data-custid="${item.CustId}" data-username="${item.Username}"/>`;
				}
			};

			columns.splice(1, 0, editMultipleColumn);
		}

		return columns;
	}

	renderDetailColumn = (api, rowIdx) => {
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

			let commissionTable = '';

			const start = showEditMultiple ? 8 : 7;
			const end = showEditMultiple ? 12 : 11;

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

	drawCallback = () => {
		this.$('#customer-list').removeClass('hidden');

		this.$('.btn-submit').click(() => this.$table.ajax.reload());

		this.$('.login-ip').click(event => {
			const url = this.$(event.currentTarget).data("url");
			this.customerListPopup.openPopup(url, 400, 600);
		});

		this.editSingleSetting.bindingEvent();

		this.editStatus.bindingEvent();
	}

	initDataTable = () => {
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
				searching: false,
				bInfo: false,
				fixedHeader: true,
				processing: true,
				language: {
					processing: '',
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
}