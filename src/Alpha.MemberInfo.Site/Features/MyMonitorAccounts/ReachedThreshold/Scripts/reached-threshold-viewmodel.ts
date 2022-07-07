class ReachedThresholdViewModel {
    SearchAccountId: number;
    MemberInfoUrl: string;
    ReachlistAccountData: any;

    constructor(
        searchAccountId: number,
        memberInfoUrl: string,
        reachlistAccountData: any
    ) {
        this.SearchAccountId = searchAccountId;
        this.MemberInfoUrl = memberInfoUrl;
        this.ReachlistAccountData = reachlistAccountData;
    }
}