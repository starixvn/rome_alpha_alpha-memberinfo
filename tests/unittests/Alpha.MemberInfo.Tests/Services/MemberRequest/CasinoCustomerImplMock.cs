namespace Alpha.MemberInfo.Tests.Services.MemberRequest
{
    using System.Collections.Generic;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Services.MemberRequest;

    public class CasinoCustomerImplMock : CasinoCustomerImpl
    {
        private IEnumerable<CasinoGameKindModel> gameKinds;

        public void SetGameKinds(IEnumerable<CasinoGameKindModel> gameKinds)
        {
            this.gameKinds = gameKinds;
        }

        public override IEnumerable<CasinoGameKindModel> GameKinds(int custId)
        {
            return gameKinds;
        }
    }
}
