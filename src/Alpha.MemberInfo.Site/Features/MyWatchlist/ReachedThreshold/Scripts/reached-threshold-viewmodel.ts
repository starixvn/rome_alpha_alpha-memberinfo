class ReachedThresholdViewModel {
    SearchAccountId: number;
    MemberInfoUrl: string;
    IsMobile: boolean;
    ReachlistAccountData: any;
    IsShowLastLoginColumn: boolean;
    URL: string;

    constructor(
        searchAccountId: number,
        memberInfoUrl: string,
        isMobile: boolean,
        isShowLastLoginColumn: boolean,
        reachlistAccountData: any
    ) {
        this.SearchAccountId = searchAccountId;
        this.MemberInfoUrl = memberInfoUrl;
        this.IsMobile = isMobile;
        this.IsShowLastLoginColumn = isShowLastLoginColumn;
        this.ReachlistAccountData = reachlistAccountData;
        this.URL = this.MemberInfoUrl + "ReachedThreshold";
    }
}

const reachedThresholdClosedReason = {
    ByAdmin: 1,
    ByUpline: 2,
    ByArchive: 4
};

const reachedThresholdLockedAccess = {
    FalseOTPSC: 1,
    FalsePassword: 2
};

const reachedThresholdStatusClasses = {
    Closed: [
        {
            Id: reachedThresholdClosedReason.ByAdmin,
            Css: 'closed-byAdmin'
        },
        {
            Id: reachedThresholdClosedReason.ByUpline,
            Css: 'closed-byUpline'
        },
        {
            Id: reachedThresholdClosedReason.ByArchive,
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
            Id: reachedThresholdLockedAccess.FalseOTPSC,
            Css: 'locked-falseOTPSC'
        },
        {
            Id: reachedThresholdLockedAccess.FalsePassword,
            Css: 'locked-falsePassword'
        }
    ]
};