/// <reference path="../components/pager.ts" />
"use strict";

interface JQuery {
    stickyTableHeaders(): void;
}

class CreditBalance {
    $: JQueryStatic;
    topWindow: any;
    creditBalanceForm: JQuery;
    pager: PagerComponent;
    columnsControl: ColumnsControlComponent;
    statusComponent: SingleSelectionDropdownComponent

    constructor(
        $: JQueryStatic,
        topWindow: any,
        pager: PagerComponent,
        columnsControl: ColumnsControlComponent,
        statusComponent: SingleSelectionDropdownComponent) {
        this.$ = $;
        this.topWindow = topWindow;
        this.pager = pager;
        this.columnsControl = columnsControl;
        this.statusComponent = statusComponent;
    }

    init = () => {
        this.cacheDOMElements();

        this.registerEvents();

        this.registerAutoSuggestion();

        this.statusComponent.render(
            new SingleSelectionDropdownOption("StatusId", "HiddenStatusId"));

        this.pager.render(new PagerOption(
            () => {
                this.$.blockUI();
                this.creditBalanceForm.submit();
            },
            "report-pager",
            "creditBalanceForm",
            "PageIndex",
            "PageSize",
            [10, 50, 100, 500]));

        this.columnsControl.render(this.$("#CookiePrefix").val());

        this.$("#tbl-report")
            .removeClass("hide")
            .stickyTableHeaders();
    };

    cacheDOMElements = () => {
        this.creditBalanceForm = this.$("#creditBalanceForm");
    };

    registerEvents = () => {
        const self: any = this;

        this.$("#btn-submit").click(function () {
            self.$.blockUI();
            self.$("#PageIndex").val("1");
        });

        this.$(".creditlink").click(function () {
            const $this: JQuery = self.$(this);
            const url: string = "_MemberInfo/CreditBalance/CreditTransfer.aspx"
                + "?custid=" + $this.data("custid")
                + "&amt=" + $this.text()
                + "&username=" + $this.data("username")
                + "&roleId=" + $this.data("levelid");
            self.topWindow.popupManager.openByRelativeUrl(url, "", 400, 300);
        });

        this.$(".balancelink, .yesterdaybalancelink").click(function () {
            const url: string = "_Transfer/Agent/MemberTransfer.aspx?custid=" + self.$(this).data("custid");
            const popH: number = navigator.userAgent.match(/iPad/i) != null ? 430 : 534;
            self.topWindow.popupManager.openByRelativeUrl(url, "", 520, popH);
        });

        this.$(".iplink").click(function () {
            self.topWindow.popupManager.openByRelativeUrl("_IPInfo/IpInfo.aspx?ip=" + this.text, "", 400, 300);
        });
    };

    registerAutoSuggestion = () => {
        const autoSuggestionUrl: string = this.$("#OldMainRootPath").val() + "_GlobalResources/Handlers/QueryUserName.ashx";
        const actorId: number = this.$("#ActorId").val();
        const $userNameInput: JQuery = this.$(".typeahead");

        const getSuggestions: any = (query: string, process: any) => {
            return this.$.get(
                autoSuggestionUrl,
                { custid: actorId, isdir: 1, query: query },
                (data: any) => {
                    const suggestions: any = eval("(" + data + ")").suggestions;
                    return !suggestions || !suggestions.length
                        ? process(["Not found"])
                        : process(suggestions);
                });
        };

        const typeaheadOptions: TypeaheadOptions = {
            minLength: 2,
            items: 10,
            name: "UserName",
            autoSelect: false,
            delay: 200,
            source: getSuggestions,
            displayText: (item: any) => item[0] + "#" + item[1] + " " + item[2],
            highlighter: (displayText: string) => {
                const parts: string[] = displayText.split("#");
                return parts[0] + "<br/>" + parts[1];
            },
            afterSelect: (item: any) => $userNameInput.val(item[0])
        };

        $userNameInput.typeahead(typeaheadOptions);
    };
}

$(() => {
    const creditBalance: any = new CreditBalance(
        $, window.top,
        new PagerComponent($),
        new ColumnsControlComponent($, window.document),
        new SingleSelectionDropdownComponent($));
    creditBalance.init();
});
