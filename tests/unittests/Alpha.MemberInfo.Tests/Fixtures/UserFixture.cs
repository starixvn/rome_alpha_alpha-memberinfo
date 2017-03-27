namespace Alpha.MemberInfo.Tests.Fixtures
{
    using Models;
    using NSubstitute;

    public class UserFixture
    {
        public UserFixture()
        {
            User = Substitute.For<IUser>();
            User.LevelId.Returns((byte)4);
            User.Id.Returns(10);
            User.Name.Returns("Name");
            User.Language.Returns("en-US");
            User.SubAccId.Returns(0);
            User.IP.Returns("192.168.1.1");
        }

        public IUser User { get; private set; }
    }
}