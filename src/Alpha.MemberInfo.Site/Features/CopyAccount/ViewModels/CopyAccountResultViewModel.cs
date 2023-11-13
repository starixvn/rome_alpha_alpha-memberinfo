namespace Alpha.MemberInfo.Site.Features.CopyAccount.ViewModels
{
    using Fanex.Customer.Client.Models;
    public class CopyAccountResultViewModel
    {
        public CopyMultipleCustomersResult Result { get; set; }
        
        public decimal Credit { get; set; }
        
        public int NumberOfAccounts { get; set; }
        
        public int NoOfFailedAccount { get; set; }
    }
}