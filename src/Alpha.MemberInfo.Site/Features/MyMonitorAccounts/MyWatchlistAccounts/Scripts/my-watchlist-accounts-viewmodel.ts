"use strict";

class MyWatchlistAccountsViewModel {
    SearchAccountId: number;
    MemberInfoUrl: string;
    IsMobile: boolean;
    CanUpdateAccount: boolean;
    LimitedDateToShowInfo: boolean
    WatchlistAccountData: any;
    URL: string;

    constructor(
        searchAccountId: number,
        memberInfoUrl: string,
        isMobile: boolean,
        canUpdateAccount: boolean,
        limitedDateToShowInfo: boolean,
        watchlistAccountData: any
    ) {
        this.SearchAccountId = searchAccountId;
        this.MemberInfoUrl = memberInfoUrl;
        this.IsMobile = isMobile;
        this.CanUpdateAccount = canUpdateAccount;
        this.LimitedDateToShowInfo = limitedDateToShowInfo;
        this.WatchlistAccountData = watchlistAccountData;
        this.URL = this.MemberInfoUrl + "MyWatchlistAccounts";
    }
}

const myWatchlistAccountsClosedReason = {
    ByAdmin: 1,
    ByUpline: 2,
    ByArchive: 4
};

const myWatchlistAccountsLockedAccess = {
    FalseOTPSC: 1,
    FalsePassword: 2
};

const myWatchlistAccountsStatusClasses = {
    Closed: [
        {
            Id: myWatchlistAccountsClosedReason.ByAdmin,
            Css: 'closed-byAdmin'
        },
        {
            Id: myWatchlistAccountsClosedReason.ByUpline,
            Css: 'closed-byUpline'
        },
        {
            Id: myWatchlistAccountsClosedReason.ByArchive,
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
    },
    Locked: [
        {
            Id: myWatchlistAccountsLockedAccess.FalseOTPSC,
            Css: 'locked-falseOTPSC'
        },
        {
            Id: myWatchlistAccountsLockedAccess.FalsePassword,
            Css: 'locked-falsePassword'
        }
    ]
};