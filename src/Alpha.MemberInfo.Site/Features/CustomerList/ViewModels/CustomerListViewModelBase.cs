namespace Alpha.MemberInfo.Site.Features.CustomerList.ViewModels
{
    public class CustomerListViewModelBase
    {
        public CustomerListViewModelBase()
        {

        }

        public int No { get; set; }

        public string Username { get; set; }

        public string Status { get; set; }

        public bool DoubleComm { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string LoginIP { get; set; }
    }
}