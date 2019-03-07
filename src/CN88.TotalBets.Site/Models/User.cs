namespace CN88.TotalBets.Site.Models
{
    using Sunplus.Agent.Model.Entities;
    using TotalBets.Models;

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
    }
}