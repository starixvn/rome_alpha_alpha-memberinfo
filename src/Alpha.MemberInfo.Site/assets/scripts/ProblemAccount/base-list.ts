/// <reference path="../shared/top-windows-interface.ts" />

class BaseList {
    $: JQueryStatic;
    topWindow: Window;
    reviewStatusComponent: SingleSelectionDropdownComponent;
    problemAccountPopup: ProblemAccountPopup;
    dateRangePickerComponent: DateRangePickerComponent;

    $fromDate: JQuery;
    $toDate: JQuery;
    $updateButton: JQuery;

    $dropdownStatus: JQuery;
    $username: JQuery;
    $uplineName: JQuery;
    $tblReport: JQuery;
    getProblemAccountUrl: string;
    $usernameAutoSuggestionPath: JQuery;
    $viewedNameDirectUpline: JQuery;

    $downlineObject: BaseList;
    reportParam: ReportParam;

    indexManager: ProblemAccount;

    levelName: string;
    levelNameLowerCase: string;
    levelId: number;
    isIndirectDownlineOfActor: boolean;
    directDownlineRoleId: number;

    customerListHelper: CustomerListHelper;
    displayNoneClass = "display-none";

    constructor(
        $: JQueryStatic,
        topWindow: Window,
        levelId: number,
        levelName: string,
        dateRangePickerComponent: DateRangePickerComponent,
        reviewStatusComponent: SingleSelectionDropdownComponent = null,
        problemAccountPopup: ProblemAccountPopup = null
    ) {
        this.$ = $;
        this.topWindow = topWindow;
        this.reviewStatusComponent = reviewStatusComponent === null ? new SingleSelectionDropdownComponent($) : reviewStatusComponent;
        this.levelName = levelName;
        this.levelNameLowerCase = levelName.toLowerCase();
        this.levelId = levelId;
        this.dateRangePickerComponent = dateRangePickerComponent;
        this.problemAccountPopup = problemAccountPopup;
        this.topWindow.needReloadMainFrameAfterUpdating = true;
    }

    public init(indexManager: ProblemAccount, downlineObject: BaseList) {
        this.indexManager = indexManager;

        this.isIndirectDownlineOfActor = this.levelId < this.indexManager.ActorLevel - 1;
        this.directDownlineRoleId = this.levelId - 1;
        this.cacheDOMElements();
        this.problemAccountPopup && this.problemAccountPopup.init(this.$tblReport, indexManager);

        this.customerListHelper = new CustomerListHelper(this.$, indexManager.viewResources);
        this.$downlineObject = downlineObject;
        this.reviewStatusComponent.render(
            new SingleSelectionDropdownOption(`dropdown-reviewstatus-${this.levelNameLowerCase}`, `UserSelectedReviewedStatusId-${this.levelName}`));
        this.dateRangePickerComponent.render(
            new DateRangePickerOption(`daterange-picker-${this.levelNameLowerCase}`, ""));

        this.$tblReport.stickyTableHeaders();

        this.$(`#btn-submit-${this.levelNameLowerCase}`).off("click").on("click", () => {
            this.viewList();
        });

        this.$(`#icon-excel-${this.levelNameLowerCase}`).off("click").on("click", () => {
            this.export();
        });

        this.$username.keypress((e) => {
            if (e.which == 13) {
                this.viewList();
                e.preventDefault();
            }
        });

        this.$viewedNameDirectUpline.keypress((e) => {
            if (e.which == 13) {
                this.viewList();
                e.preventDefault();
            }
        });
    }

    public cacheDOMElements() {
        this.$updateButton = this.$(`.update-${this.levelNameLowerCase}`);
        this.$usernameAutoSuggestionPath = this.$("#usernameAutoSuggestionPath");
        this.$username = this.$(`#txt-username-${this.levelNameLowerCase}`);
        this.$uplineName = this.$(`#txt-uplinename-${this.levelNameLowerCase}`);
        this.$dropdownStatus = this.$(`#dropdown-reviewstatus-${this.levelNameLowerCase}`);
        this.getProblemAccountUrl = this.$("#getProblemAccountUrl").val();
        this.$tblReport = this.$(`#tbl-${this.levelNameLowerCase}`);
        this.$fromDate = this.$(`#from-date-${this.levelNameLowerCase}`);
        this.$toDate = this.$(`#to-date-${this.levelNameLowerCase}`);
        this.$viewedNameDirectUpline = this.$(`#txt-uplinename-${this.levelId === siteInfrastructure.Role.Agent.Id ? 'master' : 'agent'}`);
        if (this.isIndirectDownlineOfActor) {
            this.reportParam = new ReportParam();
        }
        else {
            this.reportParam = {
                UserName: this.$username.val(),
                SearchRoleId: this.levelId,
                UplineName: "",
                UserSelectedReviewedStatusId: parseInt(this.$("#UserSelectedReviewedStatusId").val()),
                FromDate: new Date(),
                ToDate: new Date()
            };
        }

        this.$updateButton.on("click", (event) => {
            this.update();
        });

        this.$(`#chkAll-${this.levelNameLowerCase}`).on('click touchend', () => {
            this.clickCheckboxAll(true);
        });
        this.$(`#chkNone-${this.levelNameLowerCase}`).on('click touchend', () => {
            this.clickCheckboxAll(false);
        });
    }

    export = () => {
        const downlineUsername = this.isIndirectDownlineOfActor ? this.$viewedNameDirectUpline.val() : null;

        this.$("#FromDate").val(this.$fromDate.val());
        this.$("#ToDate").val(this.$toDate.val());
        this.$("#UserSelectedReviewedStatusId").val(parseInt(this.$dropdownStatus.find("option:selected").val()));
        this.$("#UplineName").val(downlineUsername);
        this.$("#SearchRoleId").val(this.levelId);
        this.$("#UserName").val(this.$username.val());

        this.$("#export-excel-form").submit();
    }

    clickCheckboxAll = (ischeckAll: boolean) => {
        const chkids = this.$tblReport.find(".chk");
        if (ischeckAll) {
            for (var i = 0; i < chkids.length; i++) {
                this.$(chkids[i]).removeClass("icon-uncheck");
                this.$(chkids[i]).addClass("icon-checkbox");
            }
            return;
        }

        for (var i = 0; i < chkids.length; i++) {
            this.$(chkids[i]).removeClass("icon-checkbox");
            this.$(chkids[i]).addClass("icon-uncheck");
        }
    };

    loadData = () => {
        const downlineUsername = this.isIndirectDownlineOfActor ? this.$viewedNameDirectUpline.val() : null;

        this.reportParam.UplineName = downlineUsername;
        this.reportParam.SearchRoleId = this.levelId;
        this.reportParam.UserSelectedReviewedStatusId = parseInt(this.$dropdownStatus.find("option:selected").val());
        this.reportParam.UserName = this.$username.val();
        this.reportParam.FromDate = this.$fromDate.val();
        this.reportParam.ToDate = this.$toDate.val();

        this.$.ajax({
            method: "GET",
            data: this.reportParam,
            url: this.getProblemAccountUrl,
            cache: false,
            success: (rs) => {
                this.renderCustomerList(rs);
            },
            complete: this.$.unblockUI
        });
    }

    viewDownlines(event) {
        const $this = this.$(event.currentTarget),
            username = $this.attr('username');

        this.$(`#tab${this.directDownlineRoleId}`).click();
        this.$downlineObject.$fromDate.val(this.$fromDate.val());
        this.$downlineObject.$toDate.val(this.$toDate.val());
        this.$downlineObject.dateRangePickerComponent.render();
        this.$downlineObject.$viewedNameDirectUpline.val(username);
        this.$downlineObject.$username.val('');

        this.$downlineObject.reviewStatusComponent.changeSelection(this.reportParam.UserSelectedReviewedStatusId);
        this.$downlineObject.viewList();
    }

    bindReportEvents() {
        this.$tblReport.find(".view-downline").click(event => this.viewDownlines(event));
        this.$tblReport.find(".chk").click(event => this.checkbox(event));
    }

    checkbox(event) {
        const $this = this.$(event.currentTarget);
        var uncheck = $this.hasClass('icon-uncheck');
        if (uncheck) {
            $this.removeClass('icon-uncheck');
            $this.addClass('icon-checkbox');
        } else {
            $this.removeClass('icon-checkbox');
            $this.addClass('icon-uncheck');
        }
    }

    update() {
        var chk = this.$tblReport.find(".icon-checkbox");
        var ids = [], usernames = [], roleId = "";

        for (var i = 0; i < chk.length; i++) {
            ids.push(this.$(chk[i]).attr("cust-id"));
            usernames.push(this.$(chk[i]).attr("username"));
            roleId = this.$(chk[i]).attr("cust-role");
        }

        if (usernames.length === 0 || ids.length === 0 || roleId === "") return;

        this.$("#Ids").val(ids);
        this.$("#UserNames").val(usernames);
        this.$("#RoleId").val(roleId);
        var popupTitle = this.indexManager.viewResources.UpdatePopupTitle;

        const popupWidth = 500, popupHeight = parseInt(roleId) == siteInfrastructure.Role.Member.Id ? 250 : 300;
        this.openPopup(popupTitle, '', popupWidth, popupHeight);
    }

    openPopup(title: string, popupCss: string, width = 480, height = 300) {
        var action = this.$("target").attr("action");
        this.topWindow["indexManager"].reloadProblemAccount = () => {
            this.viewList();
        };
        this.topWindow["popupManager"].open('', title, width, height, popupCss, null, null, null, null, {
            preUrl: action
        });
    }

    renderCustomerList(rs) {

        if (!rs) {
            return;
        }

        this.$tblReport.removeClass(this.displayNoneClass);

        this.$tblReport.find("> tbody").empty().html(
            this.customerListHelper.buildBody(rs, this.levelId, this.indexManager.ActorLevel, this.reportParam)
        );
        this.$(window).trigger('resize.stickyTableHeaders');

        this.bindReportEvents();
        this.problemAccountPopup && this.problemAccountPopup.bindEvents(this.$tblReport);
    }

    reloadPage() {
        const { UserName } = this.reportParam;
        this.$username.val(UserName);
        if (this.isIndirectDownlineOfActor) {
            this.$viewedNameDirectUpline.val(UserName);
        }

        this.viewList();
    }

    viewList() {
        this.$.blockUI();

        this.loadData();
    }
}

class ViewResources {
    $: JQueryStatic;
    public NoInformationIsAvailable: string;
    public UpdatePopupTitle: string;
    public TotalColumnMaster: number;
    public TotalColumnAgent: number;
    public TotalColumnMember: number;

    constructor($: JQueryStatic) {
        this.$ = $;

        this.NoInformationIsAvailable = this.$("#NoInformationIsAvailable").val();
        this.UpdatePopupTitle = this.$("#markAsReviewedText").val();
        this.TotalColumnMaster = parseInt(this.$("#TotalColumnMaster").val());
        this.TotalColumnAgent = parseInt(this.$("#TotalColumnAgent").val());
        this.TotalColumnMember = parseInt(this.$("#TotalColumnMember").val());
    }
}

class ReportParam {
    public UserName: string = '';
    public SearchRoleId: number = 0;
    public UplineName: string = '';
    public UserSelectedReviewedStatusId: number = 0;
    public FromDate: Date;
    public ToDate: Date;
}

class SiteInfrastructure {
    Role = {
        Super: { Id: 4, Name: "Super" },
        Master: { Id: 3, Name: "Master" },
        Agent: { Id: 2, Name: "Agent" },
        Member: { Id: 1, Name: "Member" },
    };
}
const siteInfrastructure = new SiteInfrastructure();