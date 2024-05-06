"use strict"

class MemberBettingBehavior {
    private $: JQueryStatic;

    constructor(
        $: JQueryStatic
    ) {
        this.$ = $
    }

    public init() {
        const $txtUsername = this.$('#txt-username');
        $txtUsername.text("Test");
    }
}

$(function () {
    const memberBettingBehavior = new MemberBettingBehavior($);
    memberBettingBehavior.init();
})