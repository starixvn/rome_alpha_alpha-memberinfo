"use strict";

class Constants {
    public static Unlimited: string = "Unlimited";
}

class Helper {
    public static ToNumber(value: string): number {
        return value === Constants.Unlimited
            ? 0
            : Number(value.replace(/\,/gi, ""));
    }
}