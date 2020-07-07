"use strict";

class MasterList {
	$: JQueryStatic;
	$table: JQuery;

	constructor($: JQueryStatic) {
		this.$ = $;
	}

	init = () => {
		this.initDataTable();
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

							if (idx.column >= 9 && idx.column <= 14) {
								if (addTableCommissionTag) {
									addTableCommissionTag = false;
									commissionTable = commissionTable + "<tr><td>Commison</td></tr><tr><td><table>";
								}

								commissionTable =  commissionTable + `<tr data-dtr-index="${idx.column}">` +
									`<td class="dtr-title">${header.text()}</td> ` +
									`<td class="dtr-data">${cellData}</td></tr>`;

								if (idx.column === 14 && !addTableCommissionTag) {
									commissionTable = commissionTable + '</table></td></tr>';
								}

								return commissionTable;
							}

							if (idx.column < 9 || idx.column > 14) {
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
			ordering: true,
			order: [[0, 'desc']],
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
	const masterList = new MasterList($);

	masterList.init();
});
