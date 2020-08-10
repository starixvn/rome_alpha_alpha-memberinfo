interface Window {
	$: any
}

class CustomerListServices {
	$: JQueryStatic;
	topWindow: any;
	statusSettingArray: any;
	customerListPopup: CustomerListPopup;

	constructor($: JQueryStatic, topWindow: any, customerListPopup: CustomerListPopup) {
		this.$ = $;
		this.topWindow = topWindow;
		this.customerListPopup = customerListPopup;
		this.statusSettingArray = []
	}

	private callAjax = (url, type, params, onSuccess) => {
		this.$.blockUI();
		this.$.ajax({
			url: url,
			dataType: "json",
			type: type,
			headers: {
				__RequestVerificationToken: this.$("input[name='__RequestVerificationToken']").val(),
				'X-Requested-With': 'XMLHttpRequest'
			},
			data: params,
			success: onSuccess,
			//complete: this.$.unblockUI,
			error: this.$.unblockUI
		});
	}

	getCutomerList = (data, callback) => {
		const params = {
			CustName: searchAccountName,
			CustId: searchAccountId,
			CustLevelId: searchAccountRoleId,
			PageIndex: (data.start / data.length) + 1,
			PageSize: data.length,
			Username: this.$('#txt-username').val(),
			Status: this.$('#statusFilter').val(),
			DoubleComm: this.$('#dcommFilter').val(),
			UnconfirmSetting: this.$('#unconfirmSettingFilter').val()
		};

		this.callAjax(
			`${memberInfoUrl}CustomerList/GetCustomerListData`,
			'post',
			params,
			data => {
				this.$.unblockUI();
				callback({
					data: data.DownlinesList,
					recordsTotal: data.TotalDownlines,
					recordsFiltered: data.TotalDownlines
				});
			}
		);
	}

	updateCustomerStatus = (parentEvent, action, params, displayOrder, updateCustomerStatusSuccess) => this.callAjax(
		memberInfoUrl + 'CustomerList/' + action,
		'post',
		params,
		data => updateCustomerStatusSuccess(parentEvent, data, displayOrder)
	)

	getSettings = (custId, url, callback = undefined, isStoreData = true,) => {
		this.callAjax(
			url,
			'GET',
			{},
			data => {
				this.$.unblockUI();

				if (isStoreData) { // not store for update multiple
					this.statusSettingArray.push({ custId, data });
				}

				if (typeof callback === 'function') {
					callback.apply(this, [data]);

				}
			}
		);
	}
}