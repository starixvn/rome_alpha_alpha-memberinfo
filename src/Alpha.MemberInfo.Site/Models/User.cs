namespace Alpha.MemberInfo.Site.Models
{
    using System;
    using MemberInfo.Models;
    using Sunplus.Agent.Model.Entities;

    public class User : IUser
    {
        private readonly IUserSession userSession;

        public User(IUserSession userSession)
        {
            this.userSession = userSession;
        }

        public int Id => userSession.CustId;

        public string Name => userSession.UserName;

        public string Language => userSession.Language;

        public byte LevelId => userSession.RoleId;

        public int SubAccId => userSession.SubAccId;

        public string IP => userSession.UserLoginIp;

        public bool GetHideProduct(int productId) => userSession.GetHideProduct(productId);
    }
}