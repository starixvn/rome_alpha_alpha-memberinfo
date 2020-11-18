namespace Alpha.MemberInfo.Site.Tests.Fixtures
{
    using MemberInfo.Models;
    using NSubstitute;

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
            User.AllowedAllDayBalTransfer.Returns(false);
            User.GetDisabledProduct(Arg.Any<int>()).Returns(true);
            User.GetHideProduct(Arg.Any<int>()).Returns(true);
            User.GetSyncedProduct(Arg.Any<int>()).Returns(true);
            User.IsHideKeno.Returns(true);
            User.CanUpdateAccount.Returns(true);
            User.IsCasinoSync.Returns(true);
            User.IsCasinoDisabled.Returns(true);
            User.DisableGoldDeluxe.Returns(true);
            User.IsHideGoldDeluxe.Returns(true);
            User.IsDisabledESports.Returns(true);
            User.IsNumberGameDisabled.Returns(true);
        }

        public IUser User { get; private set; }
    }
}