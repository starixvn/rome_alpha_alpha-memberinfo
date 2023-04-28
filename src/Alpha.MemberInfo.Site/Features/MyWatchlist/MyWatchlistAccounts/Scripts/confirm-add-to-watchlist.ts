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
            //set url
            const newlyAddedAccounts = this.$('#NewlyAddedAccounts').val();
            const baseUrl = this.$('#member-info-url').val();
            const watchlistPage = this.$('#member-info-url').val() + `MyWatchlistAccounts`;
            

            const mainFrame = this.topWindow.$('#main');

            mainFrame.attr('src', watchlistPage);

            this.topWindow.popupManager.open(
                `${baseUrl}MyWatchlistAccounts/ManageWatchlistAccounts?Usernames=${newlyAddedAccounts}&isEdite=true`,
                undefined,
                820,
                550,
                "modal-mobile-friendly");


            //open tab my watch list

            //call open add member to watch list
        });
    }


}

$(function () {
    new ConfirmAddToWatchlist($, window.top).init();
});