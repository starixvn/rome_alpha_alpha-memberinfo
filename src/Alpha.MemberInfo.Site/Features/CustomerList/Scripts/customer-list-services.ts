interface Window {
	$: any
}

class CustomerListServices {
	$: JQueryStatic;
	topWindow: any;

	constructor($: JQueryStatic, topWindow: any) {
		this.$ = $;
		this.topWindow = topWindow;
	}

	callAjax = (url, type, params, onSuccess) => {
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
			complete: this.$.unblockUI,
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
				callback({
					data: data.DownlinesList,
					recordsTotal: data.TotalDownlines,
					recordsFiltered: data.TotalDownlines
				});
			}
		);
	}

	updateCustomerStatus = (parentEvent, action, params, displayOrder) => {
		this.callAjax(
			memberInfoUrl + 'CustomerList/' + action,
			'post',
			params,
			data => {
				if (data.IsSuccessful) {
					// Closed or Suspended
					if (displayOrder === 1 || displayOrder === 2) {
						this.$('.btn-submit').click();
					}
				}
				else {
					this.topWindow.bootboxAlert(data.Message, parentEvent.target);
				}
			}
		);
	}
}