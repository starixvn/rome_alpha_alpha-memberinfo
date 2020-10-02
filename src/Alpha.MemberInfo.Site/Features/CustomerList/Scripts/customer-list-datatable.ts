"use strict";

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

	private getRowClass = (data) => {
		if (!data.Enabled) {
			return statusClasses.Disabled.Css;
		}
		else if (data.Closed) {
			return statusClasses.Closed.filter(closedReason => data.ClosedReason === closedReason.Id)[0].Css;
		}
		else if (data.Suspended) {
			return statusClasses.Suspended.Css;
		}

		return data.Enabled.Css;
	}

	public initDataTable = () => {
		const details = {
			type: 'column',
			target: -1,
			auto: false,
			renderer: (api, rowIdx) => this.renderDetailColumn(api, rowIdx),
			bindingCallback: this.drawCallback
		},
			lengthMenu = `${this.viewModel.CustomerListResources.PageSize} <select>` +
				'<option value="10">10</option>' +
				'<option value="50">50</option>' +
				'<option value="100">100</option>' +
				'<option value="500">500</option>' +
				'</select>',
			oPaginate = {
				sFirst: '<<',
				sPrevious: '<',
				sNext: '>',
				sLast: '>>',
				info: `${this.viewModel.CustomerListResources.Page} _INPUT_ ${this.viewModel.CustomerListResources.Of} _TOTAL_`
			};

		this.$table = this.$('#tbl-customer-list')
			.on('processing.dt', (e, settings, processing) => processing ? this.$.blockUI() : this.$.unblockUI())
			.DataTable({
				serverSide: true,
				ajax: (data, callback) => this.customerListServices.getCutomerList(data, callback),
				columns: this.buildDataTableColumns(),
				createdRow: (row, data) => this.$(row).addClass(this.getRowClass(data)),
				responsive: { details },
				columnDefs: [{
					className: 'control',
					orderable: false,
					targets: -1
				}],
				stripeClasses: [],
				ordering: false,
				paging: true,
				pagingType: "input",
				pageLength: this.viewModel.IsMobileView ? 100 : 500,
				searching: false,
				bInfo: false,
				fixedHeader: true,
				processing: true,
				dom: '<"top"iflp<"clear">>rt<"bottom"ifp<"clear">>',
				language: {
					processing: '',
					lengthMenu,
					oPaginate,
					emptyTable: this.viewModel.CustomerListResources.ThereIsNoData
				},
				drawCallback: this.drawCallback
			});

		this.$("#tbl-customer-list").find('tbody').on('click', 'td.control', event => this.doAdjustHeader(event));
	}

	private doAdjustHeader = (event) => {
		this.$table.fixedHeader.adjust();

		if (this.$(event.currentTarget).closest("tr").is(":last-child")) {
			setTimeout(() => {
				const scrollTo = this.$(document).height();
				this.$(window).scrollTop(scrollTo + 15);
			}, 100);
		}
	};

	private buildDataTableColumns = () => {
		let columns = [
			{
				"className": 'center',
				"data": null,
				"render": item => this.renderEditColumn(item)
			},
			{
				"className": 'left',
				"data": null,
				"render": item => this.renderUsernameColumn(item)
			},
			{
				"className": 'center',
				"data": item => this.renderStatusColumn(item)
			},
			{
				"className": 'center',
				"data": null,
				"render": item => this.renderStatementColumn(item)
			},
			{
				"className": 'left',
				"data": "FirstName"
			},
			{
				"className": 'left',
				"data": "LastName"
			},
			{
				"className": 'center',
				"data": null,
				"render": item => item.RoleId == CustomerLevel.Member ? item.Discount : item.GroupA
			},
			{
				"className": 'center',
				"data": null,
				"render": item => item.RoleId == CustomerLevel.Member ? item.Discount1x2 : item.GroupB
			},
			{
				"className": 'center',
				"data": null,
				"render": item => item.RoleId == CustomerLevel.Member ? item.DiscountCS : item.GroupC
			},
			{
				"className": 'center',
				"data": null,
				"render": item => item.RoleId == CustomerLevel.Member ? item.PlayerDiscount : item.GroupD
			},
			{
				"className": 'center',
				"data": null,
				"render": item => item.RoleId == CustomerLevel.Member ? item.PlayerDiscount1x2 : item.Discount1x2
			},
			{
				"className": 'center',
				"data": null,
				"render": item => item.RoleId == CustomerLevel.Member ? item.PlayerDiscountCS : item.DiscountCS
			},
			{
				"className": 'center',
				"data": null,
				"render": item => this.renderLoginIP(item)
			},
			{
				"data": null,
				"render": () => ''
			}
		];

		if (!this.viewModel.IsMobileView) {
			const rowNumberColumn = {
				"className": 'center',
				"data": "RowNumber"
			};

			columns.splice(0, 0, rowNumberColumn);
		}

		if (this.viewModel.ShowEditMultiple) {
			const editMultipleColumn = {
				"className": 'center',
				"data": null,
				"render": item => `<input type="checkbox" class="checkbox" data-custid="${item.CustId}" data-username="${item.Username}"/>`
			};

			columns.splice(this.viewModel.IsMobileView ? 0 : 1, 0, editMultipleColumn);
		}

		if (!this.viewModel.LimitedDateToShowInfo) {
			const createDateColumn = {
				"className": 'center',
				"data": "CreateDate"
			};

			columns.splice(columns.length - 2, 0, createDateColumn);
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

	private renderEditColumn = item => {
		const attrs = {
			'class': 'edit icon-table-edit',
			'id': `setting-${item.CustId}`,
			'data-custid': item.CustId,
			'data-url': this.buildGetAllStatusLink(item)
		};

		return `<div ${this.buildAttributes(attrs)}/>`;
	}

	private renderStatusColumn = item => {
		const attrs = {
			'class': 'status-popup',
			'id': `status-${item.CustId}`,
			'data-custid': item.CustId,
			'data-url': this.buildGetAllStatusLink(item)
		};

		return `<a ${this.buildAttributes(attrs)}><span>${item.Status}</span><span class='icon-menu-down'/></a>`;
	}

	private renderStatementColumn = item => {
		const queryString = {
			CustId: item.CustId,
			CustName: item.Username,
			CustLevelId: item.RoleId
		}, actionName = item.RoleId === CustomerLevel.Member ? 'MemberStatement' : 'SMAStatement';

		const viewStatementLink = `${this.viewModel.ReportsUrl}Statement/${actionName}?${this.$.param(queryString)}`;
		return `<a href='${viewStatementLink}'>${this.viewModel.CustomerListResources.View}</a>`;
	}

	private renderLoginIP = item => {
		if (item.LastLoginIP === '' || item.LastLoginIP === null) {
			return '---';
		}

		const attrs = {
			'class': 'login-ip',
			'data-url': `${this.viewModel.OldMainUrl}_IPInfo/IPInfo.aspx?ip=${item.LastLoginIP}`,
			'data-title': 'Login IP',
			'data-width': '500px',
			'data-height': '400px'
		};

		return `<a ${this.buildAttributes(attrs)}>${item.LastLoginIP}</a>`
	}

	private renderDetailColumn = (api, rowIdx) => {
		const self = this;
		let addTableCommissionTag = true;
		let commissionColumns = [];
		let otherColumns = [];
		const statusCss = self.getRowClass(api.data()[rowIdx]);

		api.cells(rowIdx, ':hidden').eq(0).each(function (cell) {
			const header = self.$(api.column(cell.column).header());
			const idx = api.cell(cell).index();

			if (header.hasClass('control') || header.hasClass('never')) {
				return '';
			}

			const dtPrivate = api.settings()[0];
			const cellData = dtPrivate.oApi._fnGetCellData(
				dtPrivate, idx.row, idx.column, 'display'
			);

			let start = 6, end = 11;
			if (self.viewModel.ShowEditMultiple) {
				start += 1;
				end += 1
			}

			if (idx.column >= start && idx.column <= end) {
				if (addTableCommissionTag) {
					addTableCommissionTag = false;
					commissionColumns.push(`<tr class="${statusCss}"><td class='commission-wrapper' colspan='2'><table class='commission-table'><tr class="${statusCss}"><td class='commission-header'>${self.$('#commissionText').val()}</td></tr>`);
				}

				commissionColumns.push(`<tr class="${statusCss}" data-dtr-index="${idx.column}">` +
					`<td class="dtr-title">${header.text()}</td> ` +
					`<td class="dtr-data">${cellData}</td></tr>`);

				if (idx.column === end && !addTableCommissionTag) {
					commissionColumns.push('</table></td></tr>');
				}
			}

			if (idx.column < start || idx.column > end) {
				otherColumns.push(`<tr class="${statusCss}" data-dtr-index="${idx.column}">` +
					`<td class="dtr-title">${header.text()}</td> ` +
					`<td class="dtr-data">${cellData}</td></tr>`)
			}
		});

		const detailsColumn = otherColumns.concat(commissionColumns);
		return detailsColumn ?
			self.$(`<table class="detail-table" data-dtr-index="${rowIdx}"/>`).append(detailsColumn.join('')) :
			false;
	}

	private drawCallback = () => {

		this.$('#tbl-customer-list').removeClass('hidden');

		this.$('.btn-submit').unbind().bind('click', () => this.$table.ajax.reload());

		this.$('.login-ip').unbind().bind('click', event => this.customerListPopup.openProductPopup(event.currentTarget));

		this.editSingleSetting.bindingEvent();

		this.editStatus.bindingEvent();
	}

	private buildGetAllStatusLink = item => {
		const queryString = {
			SearchAccountId: item.CustId,
			SearchAccountName: item.Username,
			SearchAccountRoleId: item.RoleId,
			UplineId: this.viewModel.SearchAccountId,
			UplineName: this.viewModel.SearchAccountName,
			UplineRoleId: this.viewModel.SearchAccountRoleId
		};

		return `${this.viewModel.MemberInfoUrl}CustomerList/GetCustomerAllStatus?${this.$.param(queryString)}`;
	}

	private buildAttributes = attrs => this.$.map(attrs, (value, key) => `${key}='${value}'`).join(' ');
}