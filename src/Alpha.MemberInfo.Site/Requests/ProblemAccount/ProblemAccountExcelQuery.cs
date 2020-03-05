namespace Alpha.MemberInfo.Site.Requests.ProblemAccount
{
    using Alpha.MemberInfo.Enumerations;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.ViewModels;
    using MediatR;

    public class ProblemAccountExcelQuery: ProblemAccountRequestBase, IRequest<ExcelFileViewModel>
    {
        public ProblemAccountExcelQuery(IUser viewer)
            : base(viewer)
        {
        }

        public UserLevel SearchRole => Enumeration.FromValue<UserLevel>(SearchRoleId);

        public bool IsViewDirectDownLine => CustLevel.GetDirectDownLineLevel().Is(SearchRole);
    }
}