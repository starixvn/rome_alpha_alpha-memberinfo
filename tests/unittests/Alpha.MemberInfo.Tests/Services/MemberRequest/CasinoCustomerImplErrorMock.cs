using System;
using System.Collections.Generic;
using Alpha.MemberInfo.Models;
using Alpha.MemberInfo.Services.MemberRequest;

namespace Alpha.MemberInfo.Tests.Services.MemberRequest
{
    public class CasinoCustomerImplErrorMock : CasinoCustomerImpl
    {
        public override IEnumerable<CasinoGameKindModel> GameKinds(int custId)
        {
            throw new Exception("Not casino member.");
        }
    }
}
