using CN88.TotalBets.Models;

namespace CN88.TotalBets.Site.Requests
{
    public class BaseQuery
    {
        private readonly IUser viewer;

        protected BaseQuery(IUser viewer)
        {
            this.viewer = viewer;
        }

        public int CustId { get; set; }

        public string CustName { get; set; }

        public byte CustLevelId { get; set; }

        public bool IsOwner { get; set; }

        public virtual UserLevel CustLevel => Enumeration.FromValue<UserLevel>(CustLevelId);

        public int ViewerId => viewer.Id;

        public string ViewerName => viewer.Name;

        public string NickName => viewer.NickName;

        public byte ViewerLevelId => viewer.LevelId;

        public virtual UserLevel ActorLevel => Enumeration.FromValue<UserLevel>(ViewerLevelId);

        public int SubAccountId => viewer.SubAccountId;

        public string IP => viewer.IP;

        public string Language => viewer.Language;

        public IUser GetViewer()
            => viewer;

        public virtual bool IsValid()
            => ViewerId > 0
            && !string.IsNullOrWhiteSpace(ViewerName)
            && !string.IsNullOrWhiteSpace(Language);
    }
}