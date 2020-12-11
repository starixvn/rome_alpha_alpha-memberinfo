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