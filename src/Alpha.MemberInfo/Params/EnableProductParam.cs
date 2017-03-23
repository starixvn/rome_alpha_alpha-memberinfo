using System.Collections.Generic;
using Alpha.MemberInfo.Models.MemberRequests;

namespace Alpha.MemberInfo.Params
{
    public class EnableProductParam
    {
        public EnableProductParam(
         int productId,
         IEnumerable<MemberRequestModel> members,
         int actorId,
         string actorName,
         int subAccId,
         string loginIP,
         string language)
        {
            ProductId = productId;
            Members = members;
            ActorId = actorId;
            ActorName = actorName;
            SubAccId = subAccId;
            LoginIP = loginIP;
            Language = language;
        }

        public int ProductId { get; }

        public IEnumerable<MemberRequestModel> Members { get; }

        public int ActorId { get; }

        public string ActorName { get; }

        public int SubAccId { get; }

        public string LoginIP { get; }

        public string Language { get; }
    }
}