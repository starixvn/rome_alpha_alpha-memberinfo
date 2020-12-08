"use strict";

class CustomerListViewModel {
    UplineRoleId: number;
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
    LimitedDateToShowInfo: boolean;
    CustomerListResources: any;
    IsShowMyanmarOdds: any;

    constructor(
        uplineRoleId: number,
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
        accountInfoUrl: string,
        limitedDateToShowInfo: boolean,
        isShowMyanmarOdds: boolean,
        customerListResources: any) {
        this.UplineRoleId = uplineRoleId;
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
        this.LimitedDateToShowInfo = limitedDateToShowInfo;
        this.IsShowMyanmarOdds = isShowMyanmarOdds;
        this.CustomerListResources = customerListResources;
    }
}

enum CustomerLevel {
    Member = 1,
    Agent,
    Master,
    Super
}

const ClosedReason = {
    ByOtherReason: 0,
    ByAdmin: 1,
    ByUpline: 2,
    ByWrongOTPSC: 3,
    ByArchive: 4
};

const statusClasses = {
    Closed: [
        {
            Id: ClosedReason.ByOtherReason,
            Css: 'closed-wrongPwdOthers'
        },
        {
            Id: ClosedReason.ByAdmin,
            Css: 'closed-byAdmin'
        },
        {
            Id: ClosedReason.ByUpline,
            Css: 'closed-byUpline'
        },
        {
            Id: ClosedReason.ByWrongOTPSC,
            Css: 'closed-wrongOTPSC'
        },
        {
            Id: ClosedReason.ByArchive,
            Css: 'closed-byArchive'
        }
    ],
    Suspended: {
        Css: 'RowBgSuspended'
    },
    Disabled: {
        Css: 'RowBgDisabled'
    },
    Enabled: {
        Css: 'enabled'
    }
};

const ColumnIndexsViewOnMember = {
    ByDefault: {
        commisionIndexStart: 7,
        commisionIndexEnd: 12,
        ugroupIndex: 6,
        otherStartIndex: 6,
        otherEndIndex: 12
    },
    ByShowMyanmarOdd: {
        commisionIndexStart: 7,
        commisionIndexEnd: 14,
        ugroupIndex: 6,
        otherStartIndex: 6,
        otherEndIndex: 14
    }
};

const ColumnIndexsSuperMasterViewDirectDownline = {
    ByDefault: {
        commisionIndexStart: 6,
        commisionIndexEnd: 11,
        ugroupIndex: 0,
        otherStartIndex: 6,
        otherEndIndex: 11
    },
    ByShowMyanmarOdd: {
        commisionIndexStart: 6,
        commisionIndexEnd: 9,
        ugroupIndex: 0,
        otherStartIndex: 6,
        otherEndIndex: 9
    }
};

const ColumnIndexs = {
    Super: {
        ViewMaster: ColumnIndexsSuperMasterViewDirectDownline,
        ViewAgent: {
            ByDefault: {
                commisionIndexStart: 5,
                commisionIndexEnd: 10,
                ugroupIndex: 0,
                otherStartIndex: 5,
                otherEndIndex: 10
            },
            ByShowMyanmarOdd: {
                commisionIndexStart: 5,
                commisionIndexEnd: 8,
                ugroupIndex: false,
                otherStartIndex: 5,
                otherEndIndex: 8
            }
        },
        ViewMember: ColumnIndexsViewOnMember
    },
    Master: {
        ViewAgent: ColumnIndexsSuperMasterViewDirectDownline,
        ViewMember: ColumnIndexsViewOnMember
    },
    Agent: {
        ViewMember: ColumnIndexsViewOnMember
    }
};