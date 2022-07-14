class ReachedThresholdViewModel {
    SearchAccountId: number;
    MemberInfoUrl: string;
    IsMobile: boolean;
    ReachlistAccountData: any;
    URL: string;

    constructor(
        searchAccountId: number,
        memberInfoUrl: string,
        isMobile: boolean,
        reachlistAccountData: any
    ) {
        this.SearchAccountId = searchAccountId;
        this.MemberInfoUrl = memberInfoUrl;
        this.IsMobile = isMobile;
        this.ReachlistAccountData = reachlistAccountData;
        this.URL = this.MemberInfoUrl + "ReachedThreshold";
    }
}