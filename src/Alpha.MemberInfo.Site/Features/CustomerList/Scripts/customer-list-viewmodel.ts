class CustomerListViewModel {
	ActorId: number;
	ActorName: string;
	ActorRoleId: number;
	SearchAccountId: number;
	SearchAccountName: string;
	SearchAccountRoleId: number;
	DownlineLevelName: string;
	ShowEditMultiple: boolean;
	IsMobileView: boolean;
	BreadcrumbLevelJson: string;
	OldMainUrl: string;
	ReportsUrl: string;
	MemberInfoUrl: string;
	AccountInfoUrl: string;

	constructor(
		actorId: number,
		actorName: string,
		actorRoleId: number,
		searchAccountId: number,
		searchAccountName: string,
		searchAccountRoleId: number,
		downlineLevelName: string,
		showEditMultiple: boolean,
		isMobileView: boolean,
		breadcrumbLevelJson: string,
		oldMainUrl: string,
		reportsUrl: string,
		memberInfoUrl: string,
		accountInfoUrl: string) {
		this.ActorId = actorId;
		this.ActorName = actorName;
		this.ActorRoleId = actorRoleId;
		this.SearchAccountId = searchAccountId;
		this.SearchAccountRoleId = searchAccountRoleId;
		this.SearchAccountName = searchAccountName;
		this.DownlineLevelName = downlineLevelName;
		this.ShowEditMultiple = showEditMultiple;
		this.IsMobileView = isMobileView;
		this.BreadcrumbLevelJson = breadcrumbLevelJson;
		this.OldMainUrl = oldMainUrl;
		this.ReportsUrl = reportsUrl;
		this.MemberInfoUrl = memberInfoUrl;
		this.AccountInfoUrl = accountInfoUrl;
	}
}

enum CustomerLevel {
	Member = 1,
	Agent,
	Master,
	Super
}