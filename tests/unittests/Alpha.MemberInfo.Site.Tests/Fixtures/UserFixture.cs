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
        }

        public IUser User { get; private set; }
    }
}