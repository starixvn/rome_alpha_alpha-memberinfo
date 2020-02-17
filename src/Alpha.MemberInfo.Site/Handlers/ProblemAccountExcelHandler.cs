namespace Alpha.MemberInfo.Site.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.MemberInfo.AppResources;
    using Alpha.MemberInfo.Constants;
    using Alpha.MemberInfo.Extensions;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Models.ProblemAccount;
    using Alpha.MemberInfo.Params.ProblemAccount;
    using Alpha.MemberInfo.Services;
    using Alpha.MemberInfo.Site.Requests.ProblemAccount;
    using Alpha.MemberInfo.Site.ViewModels;
    using MediatR;
    using ONEbook.SpreadSheets;
    using static ONEbook.SpreadSheets.Column;

    public class ProblemAccountExcelHandler : ExcelBaseHandler, IRequestHandler<ProblemAccountExcelQuery, ExcelFileViewModel>
    {
        private int currentRowIndex = 1;
        private readonly IProblemAccountService problemAccountService;
        private readonly ProblemAccountWorkbook workbook;

        public ProblemAccountExcelHandler(
            IProblemAccountService problemAccountService,
            ProblemAccountWorkbook workbook)
        {
            this.problemAccountService = problemAccountService;
            this.workbook = workbook;
        }

        public ExcelFileViewModel Handle(ProblemAccountExcelQuery message)
        {
            var param = new GetProblemAccountParam
            {
                FromDate = message.FromDate,
                ToDate = message.ToDate,
                ReviewStatusId = message.UserSelectedReviewedStatusId,
                CustId = message.CustId,
                SubAccId = message.CustId,
                UplineUserName = message.UplineName,
                SearchRoleId = message.SearchRoleId,
                SearchUserName = message.UserName
            };

            var problemAccounts = problemAccountService.Get(param);           

            var fileContent = workbook
                .SetData(problemAccounts)
                .SetRowMappers(GetBodyRowMapper(message))
                .UseTemplate(GetTemplateInfo(message))
                .Sheet("Sheet 1")
                .Title(GetReportTitle(message))
                .Header(Resources.ResourceManager.GetString)
                .ExportToBytes();

            return new ExcelFileViewModel(GetExcelFileName(message), fileContent);
        }

        protected override string GetExcelTemplateDir() => "~/App_Data/ExcelTemplates/ProblemAccount";

        internal Mapper<ProblemAccountModel> GetBodyRowMapper(ProblemAccountExcelQuery message)
        {
            if (message.SearchRole.IsMasterLevel())
            {
                return GetBodyRowMapperForMaster();
            }
            else if (message.SearchRole.IsAgentLevel())
            {
                return GetBodyRowMapperForAgent();
            }
            else
            {
                return GetBodyRowMapperForMember();
            }
        }

        private Mapper<ProblemAccountModel> GetBodyRowMapperForMaster()
        {
            var mapper = new Mapper<ProblemAccountModel>()
                .Map(A, _ => currentRowIndex++)
                .Map(B, data => data.UserName)
                .Map(C, data => data.NickName)
                .Map(D, data => data.FirstName)
                .Map(E, data => data.LastName)
                .Map(F, data => data.StatusName)
                .Map(G, data => data.CreatedDateStr)
                .Map(H, data => data.Count)
                .Map(I, data => data.ReviewStatus);

            return mapper;
        }

        private Mapper<ProblemAccountModel> GetBodyRowMapperForAgent()
        {
            var mapper = new Mapper<ProblemAccountModel>()
                .Map(A, _ => currentRowIndex++)
                .Map(B, data => data.UserName)
                .Map(C, data => data.FirstName)
                .Map(D, data => data.LastName)
                .Map(E, data => data.StatusName)
                .Map(F, data => data.CreatedDateStr)
                .Map(G, data => data.Count)
                .Map(H, data => data.ReviewStatus);

            return mapper;
        }

        private Mapper<ProblemAccountModel> GetBodyRowMapperForMember()
        {
            var mapper = new Mapper<ProblemAccountModel>()
                .Map(A, _ => currentRowIndex++)
                .Map(B, data => data.UserName)
                .Map(C, data => data.FirstName)
                .Map(D, data => data.LastName)
                .Map(E, data => data.StatusName)
                .Map(F, data => data.CreatedDateStr)
                .Map(G, data => data.ReviewStatus);

            return mapper;
        }

        private static string GetExcelFileName(ProblemAccountExcelQuery message)
            => $"Problem Account {message.FromDate.ToShortDateWithHyphen()} -- {message.ToDate.ToShortDateWithHyphen()} - {message.SearchRole.DisplayName}";

        private TemplateInfo GetTemplateInfo(ProblemAccountExcelQuery message)
        {
            var templateName = message.SearchRole.DisplayName;

            return new TemplateInfo(
                   GetExcelTemplatePath(templateName),
                   noInfomationText: Resources.NoInformationIsAvailable,
                   titleStartIndex: 1,
                   headerStartIndex: 3,
                   columnStartName: A,
                   sheetIndex: 1,
                   bodyStartIndex: 4,
                   bodyEndIndex: 7);
        }

        private static string GetReportTitle(ProblemAccountExcelQuery message)
        {
            var reviewStatus = ReviewStatusConstant.GetReviewStatus(message.UserSelectedReviewedStatusId);
            var title = string.Empty;
            if (message.SearchRole.IsNotMasterLevel())
            {
                title = $"{GetSourceText(message.SearchRole.GetDirectUplineLevel().DisplayName)}: {message.UplineName} \n";
            }

            title += $"{Resources.DateNotified}: {message.FromDate.ToShortDateWithSlash()} \n";
            title += $"{Resources.ReviewStatus}: {GetSourceText(reviewStatus)} \n";
            title += $"{Resources.Username}: {message.UserName} \n";
            title += $"{Resources.ExportDate}: {DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")} ";

            return title;
        }

        private static string GetSourceText(string key)
            => Resources.ResourceManager.GetString(key);
    }
}