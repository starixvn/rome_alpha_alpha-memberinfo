"use strict";

class MyWatchlistAccountsViewModel {
    SearchAccountId: number;
    MemberInfoUrl: string;
    IsMobile: boolean;
    WatchlistAccountData: any;
    URL: string;

    constructor(
        searchAccountId: number,
        memberInfoUrl: string,
        isMobile: boolean,
        watchlistAccountData: any
    ) {
        this.SearchAccountId = searchAccountId;
        this.MemberInfoUrl = memberInfoUrl;
        this.IsMobile = isMobile;
        this.WatchlistAccountData = watchlistAccountData;
        this.URL = this.MemberInfoUrl + "MyWatchlistAccounts";
    }
}