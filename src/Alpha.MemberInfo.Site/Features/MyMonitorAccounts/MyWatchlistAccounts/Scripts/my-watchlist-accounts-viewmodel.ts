"use strict";

class MyWatchlistAccountsViewModel {
    SearchAccountId: number;
    MemberInfoUrl: string;
    WatchlistAccountData: any;

    constructor(
        searchAccountId: number,
        memberInfoUrl: string,
        watchlistAccountData: any
    ) {
        this.SearchAccountId = searchAccountId;
        this.MemberInfoUrl = memberInfoUrl;
        this.WatchlistAccountData = watchlistAccountData;
    }
}