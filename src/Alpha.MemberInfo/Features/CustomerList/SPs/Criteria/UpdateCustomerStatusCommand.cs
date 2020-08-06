namespace Alpha.MemberInfo.Features.CustomerList.Criteria
{
    using Alpha.MemberInfo.Features.Shared.Models;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using System.Collections;
    using System.Collections.Generic;

    public class UpdateCustomerStatusCommand : NonQueryCommand
    {
        private const byte AlphaSystemId = 2;

        private static readonly IDictionary<string, string> CustomerStatusSettingKeys =
         new Dictionary<string, string>
         {
                { StatusNames.DisableAutoMasterPT, "Acc_CustomerUpdMaster_DisableAutoMPT" },
                { StatusNames.Closed, "Acc_CustomerUpdClosed" },
                { StatusNames.DoubleComm, "Acc_CustomerUpdDoubleCommLosing" },
                { StatusNames.DisableOutright, "Acc_CustomerUpdDisableOutright" },
                { StatusNames.Suspended, "Acc_CustomerUpdSuspended" },
         };

        public UpdateCustomerStatusCommand(
            int searchAccountId,
            int actorId,
            int subAccountId,
            string language,
            string ip,
            string statusName,
            bool statusValue)
        {
            SearchAccountId = searchAccountId;
            ActorId = actorId;
            SubAccountId = subAccountId;
            Language = language;
            IP = ip;
            StatusName = statusName;
            StatusValue = statusValue;
        }

        [SpParam(SqlParamName = "CustId")]
        public int SearchAccountId { get; }

        [SpParam(SqlParamName = "UserId")]
        public int ActorId { get; }

        [SpParam(SqlParamName = "SubAccId")]
        public int SubAccountId { get; }

        public string Language { get; }

        public string IP { get; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        [SpParam(Ignored = true)]
        public string StatusName { get; }

        [SpParam(Ignored = true)]
        public bool StatusValue { get; }

        public override string GetSettingKey()
            => CustomerStatusSettingKeys[StatusName];

        public override Hashtable GetSpParams()
        {
            var spParams = base.GetSpParams();

            spParams.Add(StatusName, StatusValue);

            if(StatusName.Equals(StatusNames.Closed))
            {
                spParams.Add("SystemID", AlphaSystemId);
            }

            return spParams;
        }

        public override bool IsValid()
            => SearchAccountId > 0
            && ActorId > 0
            && !string.IsNullOrWhiteSpace(StatusName);
    }
}