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
							if (idx.column === 9) {
								return commissionTable + "<tr><td>Commison</td></tr><tr><td><table>";
							}
							if (idx.column >= 9 && idx.column <= 14) {
								return commissionTable +
									`<tr data-dtr-index="${idx.column}">` +
									`<td class="dtr-title">${header.text()}</td> ` +
									`<td class="dtr-data">${cellData}</td></tr>`;
							}

							if (idx.column === 14) {
								return commissionTable + "</table></td></tr>";
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
