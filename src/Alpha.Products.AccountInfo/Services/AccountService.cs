namespace Alpha.Products.AccountInfo.Services
{
    using System.Text;
    using Alpha.Products.AccountInfo.AppResources;
    using Alpha.Products.AccountInfo.Criteria;
    using Fanex.Data.Repository;

    public interface AccountService
    {
        string GetTransferCondition(int custId);
    }

    public class AccountServiceImpl : AccountService
    {
        private readonly IDynamicRepository repository;
        private const int DailyIndex = 3;
        private const int WeekyIndex = 7;

        public AccountServiceImpl(IDynamicRepository dynamicRepository)
        {
            repository = dynamicRepository;
        }

        public string GetTransferCondition(int custId)
        {
            string transferOption = GetTransferOption(custId);

            string transferTime = string.Empty;
            char[] charArray = transferOption.ToCharArray();

            if (charArray.Length > 0)
            {
                StringBuilder dayBuilder = new StringBuilder();
                string[] dayArray =
                {
                    Lang.Sunday,
                    Lang.Monday,
                    Lang.Tuesday,
                    Lang.Wednesday,
                    Lang.Thursday,
                    Lang.Friday,
                    Lang.Saturday
                };

                for (int i = 1, max = charArray.Length; i < max - 1; i++)
                {
                    if (charArray[i] == '1')
                    {
                        dayBuilder.Append(" - ").Append(dayArray[i]);
                    }
                }

                if (charArray[0] == '1')
                {
                    dayBuilder.Append(" - ").Append(dayArray[0]);
                }

                string day = dayBuilder.ToString();

                if (day.Length > 0)
                {
                    day = day.Substring(DailyIndex);
                }

                transferTime += charArray[WeekyIndex] == '1' ? day : Lang.Daily;
            }

            return transferTime;
        }

        private string GetTransferOption(int custId)
        {
            var criteria = new GetTransferOptionCriteria(custId);

            repository.Execute(criteria);

            return criteria.TransferOption;
        }
    }
}
