/// <reference path="../components/single-selection-dropdown.ts" />
/// <reference path="../components/daterangepicker.ts" />
declare var initAutoComplete: Function;
"use strict";

class ProblemAccount {
    $: JQueryStatic;
    ViewerId: number;
    ActorLevel: number;
    topWindow: any;
    $username: JQuery;
    $usernameAutoSuggestionPath: JQuery;

    masterList: BaseList;
    agentList: BaseList;
    memberList: BaseList;
    viewResources: ViewResources;

    constructor(
        $: JQueryStatic,
        topWindow: any,
        viewResources: ViewResources = null) {
        this.$ = $;
        this.viewResources = viewResources === null ? new ViewResources($) : viewResources;
        this.topWindow = topWindow;
    }

    init = () => {
        this.cacheDOMElements();
        this.registerEvents();
        this.render();
    };

    render = () => {
        this.initDownlineLists();

        const searchAccountRoleId = parseInt(this.$("#SearchRoleId").val());
        if (searchAccountRoleId > 0) {
            this.$(`#tab${searchAccountRoleId}`).click();
            switch (searchAccountRoleId) {
                case siteInfrastructure.Role.Master.Id:
                    this.masterList.reloadPage();
                    break;
                case siteInfrastructure.Role.Agent.Id:
                    this.masterList && this.masterList.reloadPage();
                    this.agentList.reloadPage();
                    break;
                case siteInfrastructure.Role.Member.Id:
                    this.masterList && this.masterList.reloadPage();
                    this.agentList && this.agentList.reloadPage();
                    this.memberList.reloadPage();
                    break;
            }
        }
    };

    registerEvents = () => {
        this.$("#tab1").on("click", () => {
            const queryUsernameURL = `${this.$usernameAutoSuggestionPath.val()}?${this.$.param({ isDir: 1, issub: 0, ismember: 0 })}`;
            initAutoComplete(this.$("#txt-username-member")[0], queryUsernameURL);
        });
        this.$("#tab2").on("click", () => {
            const queryUsernameURL = `${this.$usernameAutoSuggestionPath.val()}?${this.$.param({ isDir: 1, issub: 0, ismember: 0 })}`;
            initAutoComplete(this.$("#txt-username-agent")[0], queryUsernameURL);
        });
        this.$("#tab3").on("click", () => {
            const queryUsernameURL = `${this.$usernameAutoSuggestionPath.val()}?${this.$.param({ isDir: 1, issub: 0, ismember: 0 })}`;
            initAutoComplete(this.$("#txt-username-master")[0], queryUsernameURL);
        });
    };
   
    cacheDOMElements = () => {
        this.$usernameAutoSuggestionPath = this.$("#usernameAutoSuggestionPath");
    };   

    initDownlineLists(): void {
        this.memberList = new BaseList(this.$, this.topWindow, siteInfrastructure.Role.Member.Id, siteInfrastructure.Role.Member.Name, new DateRangePickerComponent($));
        this.memberList.init(this, null);

        this.agentList = new BaseList(this.$, this.topWindow, siteInfrastructure.Role.Agent.Id, siteInfrastructure.Role.Agent.Name, new DateRangePickerComponent($));
        this.agentList.init(this, this.memberList);

        this.masterList = new BaseList(this.$, this.topWindow, siteInfrastructure.Role.Master.Id, siteInfrastructure.Role.Master.Name, new DateRangePickerComponent($));
        this.masterList.init(this, this.agentList);
    }
}

const problemAccount = new ProblemAccount($, window.top);

$(() => {
    problemAccount.init();
});
