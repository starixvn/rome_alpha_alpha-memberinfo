"use strict";

class WatchlistHelper {
    static formatThreshold = threshold => threshold === null ? "Unlimited" : threshold;

    static reloadGrid = () => {
        $("#btn-submit").click();
    }

    static reloadPage = () => window.location = window.location;

    static reloadPageWithTimeout = callback => setTimeout(() => {
        callback();
        WatchlistHelper.reloadPage();
    }, 3000);

    static ToMoneyFormat(value) {
        return value
            .replace(WatchlistRegex.NonDigitalCharsRegex, WatchlistConstants.EmptyChar)
            .replace(WatchlistRegex.LeadingZerosRegex, WatchlistConstants.EmptyChar)
            .replace(WatchlistRegex.ThousandsSeparatorRegex, WatchlistConstants.SeparatorChar);
    }

    static ToNumber(value) {
        return Number(value.replace(WatchlistRegex.AllowedCharsRegex, WatchlistConstants.EmptyChar));
    }
}

class WatchlistRegex {
    static AllowedCharsRegex: RegExp = /\,/g;
    static LeadingZerosRegex: RegExp = /^0+(?!$)/;
    static NonDigitalCharsRegex: RegExp = /\D/g;
    static ThousandsSeparatorRegex: RegExp = /\B(?=(\d{3})+(?!\d))/g;
}

class WatchlistConstants {
    static EmptyChar: string = "";
    static SeparatorChar: string = ",";
}