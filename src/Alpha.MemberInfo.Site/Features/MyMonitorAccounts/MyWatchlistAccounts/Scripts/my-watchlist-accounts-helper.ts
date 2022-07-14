"use strict";

class WatchlistHelper {
    static reloadPage = () => window.location = window.location;

    static reloadMainFrameAfterSuccess = reloadTimeout => {
        setTimeout(function () {
            window.parent.location = window.parent.location;
        }, reloadTimeout);
    }

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