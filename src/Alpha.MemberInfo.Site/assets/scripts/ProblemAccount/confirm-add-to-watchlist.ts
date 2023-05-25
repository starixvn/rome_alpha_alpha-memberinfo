interface Window {
    watchlistAccountData: any;
    reloadMyWatchlistDataTable: Function;
}

class ConfirmAddToWatchlist {
    private $: JQueryStatic;
    private topWindow: any;

    constructor(
        $: JQueryStatic,
        topWindow: any) {
        this.$ = $;
        this.topWindow = topWindow;

    }

    public init() {
        $("#btnCancel").on("click touchend", () => {
            this.topWindow["popupManager"].close();
        });

        $("#btnSubmit").on("click touchend", () => {
            const newlyAddedAccounts = this.$('#NewlyAddedAccounts').val();
            const watchlistPage = this.$('#member-info-url').val() + `MyWatchlistAccounts`;
            const params = `?IsOpenFromProblemAccount=true&custIds=${newlyAddedAccounts}`;

            const mainFrame = this.topWindow.$('#main');
            mainFrame.attr('src', watchlistPage + params);
        });
    }
}

$(function () {
    new ConfirmAddToWatchlist($, window.top).init();
});