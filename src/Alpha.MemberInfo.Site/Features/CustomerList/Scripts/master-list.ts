"use strict";
declare var accountInfoUrl;

class MasterList {
	$: JQueryStatic;
	$table: JQuery;
	topWindow: any;

	constructor($: JQueryStatic, topWindow: any) {
		this.$ = $;
		this.topWindow = topWindow;
	}

	init = () => {
		this.initDataTable();
		const self = this;

		this.$('.edit').click(() => {
			const url = `${accountInfoUrl}SearchAccount/EditPopup` +
				`?SearchAccountId=23003231&SearchAccountName=SupertestTE0101` +
				`&SearchAccountRoleId=3&UplineId=23003140&UplineName=SupertestTE01&UplineRoleId=4`;
			self.topWindow.popupManager.open(url, "", 800, 600);
		});
	};
	initDataTable = () => {
		const self = this;

		this.$table = this.$('#master-list').DataTable({
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

							if (idx.column >= 8 && idx.column <= 12) {
								if (addTableCommissionTag) {
									addTableCommissionTag = false;
									commissionTable = commissionTable + "<tr><td><table><tr><td>Commison</td></tr>";
								}

								commissionTable =  commissionTable + `<tr data-dtr-index="${idx.column}">` +
									`<td class="dtr-title">${header.text()}</td> ` +
									`<td class="dtr-data">${cellData}</td></tr>`;

								if (idx.column === 12 && !addTableCommissionTag) {
									commissionTable = commissionTable + '</table></td></tr>';
								}

								return commissionTable;
							}

							if (idx.column < 8 || idx.column > 12) {
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
			paging: false,
			searching: false,
			bInfo: false,
			fixedHeader: true,
			processing: true,
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
