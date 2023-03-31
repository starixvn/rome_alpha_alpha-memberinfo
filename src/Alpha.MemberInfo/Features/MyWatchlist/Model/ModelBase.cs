namespace Alpha.MemberInfo.Features.MyWatchlist.Model
{
    public class ModelBase
    {
        public string GetFirstLastName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName))
            {
                return string.Empty;
            }

            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
            {
                return $"{firstName}/ {lastName}";
            }

            return $"{firstName}{lastName}";
        }
    }
}