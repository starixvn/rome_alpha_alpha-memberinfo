namespace Alpha.MemberInfo.Site.Tests.Fixtures
{
    using MemberInfo.Models;
    using NSubstitute;
    using System;

    public class UserFixture
    {
        public UserFixture()
        {
            User = Substitute.For<IUser>();
            User.LevelId.Returns((byte)2);
            User.Id.Returns(10);
            User.Name.Returns("Name");
            User.Language.Returns("en-US");
            User.SubAccId.Returns(0);
            User.IP.Returns("192.168.1.1");
            User.GetHideProduct(Arg.Any<int>()).Returns(true);
            User.Language.Returns("En");
            User.IsInternal.Returns(false);
            User.AllowedTotalBalanceTransfer.Returns(true);
            User.GetDisabledProduct(Arg.Any<int>()).Returns(true);
            User.GetHideProduct(Arg.Any<int>()).Returns(true);
            User.GetSyncedProduct(Arg.Any<int>()).Returns(true);
            User.CanUpdateAccount.Returns(true);            
            User.IsDisabledESports.Returns(true);
            User.LimitedDateToShowInfo.Returns(new DateTime(2020, 11, 19));
        }

        public IUser User { get; private set; }
    }
}