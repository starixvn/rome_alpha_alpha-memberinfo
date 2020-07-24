"use strict";

//declare var viewModel;
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

interface Window {
	$: any
}

class MasterList {
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

	init = () => {
		this.initDataTable();

		this.$('.login-ip').click((event) => {
			const url = this.$(event.currentTarget).data("url");
			this.openPopup(event, url, false);
		});

		this.$('.edit').click((event) => {
			const url = this.$(event.currentTarget).data("url");
			this.openPopup(event, url);
		});

		this.$('.edit-multiple').click((event) => {
			let usernames=[], custids = [];
			this.$('input.checkbox:checkbox:checked').each((index, element) => {
				usernames.push(this.$(element).data("username"));
				custids.push(this.$(element).data("custid"));
			});

			const url = `${this.$(event.currentTarget).data("url")}&custids=${custids.join(',')}&usernames=${usernames.join(',')}`;

			this.openPopup(event, url);
		});

		this.$('.btn-submit').click(() => {
			this.$table.ajax.reload();
		});
	};

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

		if (specificPositon) {
			this.topWindow.popupManager.openWithHanlder(this.topWindow.editPopupOnCustomerList, url, '', 200, 400, '', '', '', sidebar.width() + left, sidebar.height() + top);
		}
		else {
			this.topWindow.popupManager.openWithHanlder(this.topWindow.editPopupOnCustomerList, url, '', 400, 400);
		}
	}

	getLevelName = (roleId) => {
		switch (roleId) {
			case 3: return 'Master';
			case 2: return 'Agent';
			default: return 'Member';
		}
	}
	initDataTable = () => {
		const self = this;

		const hideEditMultiple = JSON.parse(localStorage.getItem('HideEditMultiple'));

		let columns = [
			{
				"className": "",
				"data": "RowNumber"
			},
			
			{
				"className": "",
				"data": null,
				"render": function (item) {
					const content = `${item.Username} <br /> ${item.UserName2 === '' || item.UserName2 === null ? "--" : item.UserName2}`;
					const queryString = {
						SearchAccountRoleId: item.RoleId,
						SearchAccountId: item.CustId,
						SearchAccountName: item.Username
					};

					const viewDownlineLink = `${memberInfoUrl}CustomerList/${self.getLevelName(item.RoleId - 1)}?${self.$.param(queryString)}`;
					return item.RoleId === 1 ? content : `<a href="${viewDownlineLink}">${content}</a>`;
				}
			},
			{
				"className": "",
				"data": "Status"
			},
			{
				"className": "",
				"data": null,
				"render": function (item) {
					const editPopupLink = `${accountInfoUrl}SearchAccount/EditPopup?` +
						`SearchAccountId=${item.CustId}&SearchAccountName=${item.Username}&SearchAccountRoleId=${item.RoleId}&` +
						`UplineId=${actorId}&UplineName=${actorName}&UplineRoleId=${actorRoleId}`;
					return `<div href=${editPopupLink}'>Edit</div>`;
				}
			},
			{
				"className": "",
				"data": null,
				"render": function (item) {
					const actionName = item.RoleId === 1 ? "MemberStatement" : "SMAStatement";
					const viewStatementLink = `${reportsUrl}Statement/${actionName}?CustID=${item.CustId}&CustName=${item.Username}&CustLevelId=${item.RoleId}`;
					return `<a href='${viewStatementLink}'>View</a>`;
				}
			},
			{
				"className": "",
				"data": null,
				"render": function (item) {
					if (item.RoleId == 1) {
						return `<div>${item.DoubleCommLosing ? "ON" : "OFF"}</div>`;
					}

					return `<div>${item.DoubleCommLosing ? "Allowed" : "Disallowed"}</div>`;
				}
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
				"render": function (item) {
					return item.RoleId == 1 ? item.Discount : item.GroupA;
				}
			},
			{
				"className": "",
				"data": null,
				"render": function (item) {
					return item.RoleId == 1 ? item.Discount1x2 : item.GroupB;
				}
			},
			{
				"className": "",
				"data": null,
				"render": function(item) {
					return item.RoleId == 1 ? item.DiscountCS : item.GroupC;
				}
			},
			{
				"className": "",
				"data": null,
				"render": function (item) {
					return item.RoleId == 1 ? item.PlayerDiscount : item.GroupD;
				}
			},
			{
				"className": "",
				"data": null,
				"render": function (item) {
					return item.RoleId == 1 ? item.PlayerDiscount1x2 : item.Discount1x2;
				}
			},
			{
				"className": "",
				"data": null,
				"render": function (item) {
					return item.RoleId == 1 ? item.PlayerDiscountCS : item.DiscountCS;
				}
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
					return `<div href='${viewIP}'>${item.LastLoginIP}</div>`;
				}
			},
			{
				"className": "",
				"data": null,
				"render": function (row) {
					return '';
				}
			}
		];

		const editMultipleColumn = {
			"className": "",
			"data": null,
			"render": function (item) {
				return `<input type="checkbox" class="checkbox" data-custid="${item.CustId}" data-username="${item.Username}"/>`;
			}
		};

		if (showEditMultiple) {
			columns.splice(1, 0, editMultipleColumn);
		}

		this.$table = this.$('#master-list').DataTable({
			serverSide: true,
			ajax: function (data, callback, settings) {
				self.$.ajax({
					url: `${memberInfoUrl}CustomerList/GetCustomerListData`,
					dataType: "json",
					type: 'post',
					data: {
						SearchAccountName: searchAccountName,
						SearchAccountId: searchAccountId,
						SearchAccountRoleId: searchAccountRoleId,
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
			},
			columns: columns,
			responsive: {
				details: {
					type: 'column',
					target: -1,
					auto: false,
					renderer: function (api, rowIdx) {
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

							const start = hideEditMultiple ? 8 : 9;
							const end = hideEditMultiple ? 12 : 13;

							if (idx.column >= start && idx.column <= end) {
								if (addTableCommissionTag) {
									addTableCommissionTag = false;
									commissionTable = commissionTable + "<tr><td><table><tr><td>Commison</td></tr>";
								}

								commissionTable =  commissionTable + `<tr data-dtr-index="${idx.column}">` +
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
			//deferRender: true,
			//scrollY: 200,
			//scrollCollapse: true,
			//scroller: true,
			language: {
				oPaginate: {
					sFirst: '<<',
					sPrevious: '<',
					sNext: '>',
					sLast: '>>'
				}
			},
			drawCallback: () => {
				this.$('#master-list').removeClass('hidden');
			}
		});
	}
}

$(function () {
	const masterList = new MasterList($, window.top);
	masterList.init();
});
