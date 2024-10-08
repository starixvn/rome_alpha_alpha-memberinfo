namespace Alpha.MemberInfo.Features.MemberBettingBehavior.Models
{
    public class CustomerProfile
    {
        public CustomerProfile() {}

        public int CategoryID { get; set; }

        public int? CustomerClass { get; set; }

        public bool? IsMonitoredMember { get; set; }

        public bool IsProblemAccount { get; set; }

        public string Status { get; set; }

        public string Level { get; set; }

        public bool RelatedToOtherAccount { get; set; }

        public bool CanUpdateAccount { get; set; }
    }
}
