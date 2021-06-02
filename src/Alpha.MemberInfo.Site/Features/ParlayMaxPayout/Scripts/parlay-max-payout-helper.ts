"use strict";

class Constants {
    public static Unlimited: string = "Unlimited";
}

class Helper {
    public static ToNumber(value: string): number {
        return Number(value.replace(/\,/gi, ""));
    }

    public static ToMoneyFormat(value: string): string {
        return value
            .replace(/\,/gi, "")
            .replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    }

    public static ToNumberFormat(value: string): number {
        return value === Constants.Unlimited
            ? 0
            : this.ToNumber(value);
    }
}