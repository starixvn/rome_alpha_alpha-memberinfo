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

interface Window {
	$: any
}

class CustomerListDataTable {
	$: JQueryStatic;
	$table: JQuery;
	topWindow: any;

	constructor($: JQueryStatic, topWindow: any) {
		this.$ = $;
		this.topWindow = topWindow;
	}

	editPopupOnCustomerListOnclosing() {
		//this.topWindow.editPopupOnCustomerList.close();
	}

	openPopup = (event, url, specificPositon = true) => {
		const left = this.$(event.currentTarget).offset().left + this.$(event.currentTarget).width() + 15;
		const top = this.$(event.currentTarget).offset().top;

		if (this.topWindow.editPopupOnCustomerList === null || this.topWindow.editPopupOnCustomerList === undefined) {
			this.topWindow.editPopupOnCustomerList = this.topWindow.ageWnd.createInstance();
		}

		const mainFrame = window.top.$('#main');
		const topBody = mainFrame.parent().parent().parent();

		const sidebar = this.$(topBody).find('#profile');

		this.topWindow.editPopupOnCustomerList.onClosing = this.editPopupOnCustomerListOnclosing;

		specificPositon ?
			this.topWindow.popupManager.openWithHanlder(this.topWindow.editPopupOnCustomerList, url, '', 200, 450, '', '', '', sidebar.width() + left, sidebar.height() + top) :
			this.topWindow.popupManager.openWithHanlder(this.topWindow.editPopupOnCustomerList, url, '', 400, 400);
	}

	renderUsernameColumn = (item) => {
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

	renderEditColumn = (item) => {
		const editPopupLink = `${memberInfoUrl}CustomerList/EditPopup?` +
			`SearchAccountId=${item.CustId}&SearchAccountName=${item.Username}&SearchAccountRoleId=${item.RoleId}` +
			`&UplineId=${actorId}&UplineName=${actorName}&UplineRoleId=${actorRoleId}`;
		return `<div class='edit' data-url='${editPopupLink}'>Edit</div>`;
	}

	renderStatementColumn = (item) => {
		const actionName = item.RoleId === 1 ? "MemberStatement" : "SMAStatement";
		const viewStatementLink = `${reportsUrl}Statement/${actionName}?CustID=${item.CustId}&CustName=${item.Username}&CustLevelId=${item.RoleId}`;
		return `<a href='${viewStatementLink}'>View</a>`;
	}

	renderDoubleCommColumn = (item) => {
		if (item.RoleId == 1) {
			return `<div>${item.DoubleCommLosing ? "ON" : "OFF"}</div>`;
		}

		return `<div>${item.DoubleCommLosing ? "Allowed" : "Disallowed"}</div>`;
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
				"data": "Status"
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
				"data": null,
				"render": item => this.renderDoubleCommColumn(item)
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

	callAjax = (data, callback) => {
		const self = this;
		this.$.ajax({
			url: `${memberInfoUrl}CustomerList/GetCustomerListData`,
			dataType: "json",
			type: 'post',
			data: {
				CustName: searchAccountName,
				CustId: searchAccountId,
				CustLevelId: searchAccountRoleId,
				PageIndex: (data.start / data.length) + 1,
				PageSize: data.length,
				Username: self.$('#txt-username').val(),
				Status: self.$('#statusFilter').val(),
				DoubleComm: self.$('#dcommFilter').val(),
				UnconfirmSetting: self.$('#unconfirmSettingFilter').val()
			},
			success: function (data, textStatus, jQxhr) {
				callback({
					// draw: data.draw,
					data: data.DownlinesList,
					recordsTotal: data.TotalDownlines,
					recordsFiltered: data.TotalDownlines
				});
			},
			error: function (jqXhr, textStatus, errorThrown) {
			}
		});
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

			const start = showEditMultiple ? 9 : 8;
			const end = showEditMultiple ? 13 : 12;

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
		this.$('.login-ip').click(event => {
			const url = this.$(event.currentTarget).data("url");
			this.openPopup(event, url, false);
		});

		this.$('.edit').click(event => {
			const url = this.$(event.currentTarget).data("url");
			this.openPopup(event, url);
		});
	}

	initDataTable = () => {
		this.$table = this.$('#customer-list').on('processing.dt',
			(e, settings, processing) => processing ? this.$.blockUI() : this.$.unblockUI())
			.DataTable({
				serverSide: true,
				ajax: (data, callback) => this.callAjax(data, callback),
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
				drawCallback: () => this.drawCallback()
		});
	}
}