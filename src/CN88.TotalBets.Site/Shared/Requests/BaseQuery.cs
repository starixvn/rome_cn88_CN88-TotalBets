namespace CN88.TotalBets.Site.Shared.Requests
{
    using TotalBets.Shared.Models;

    public class BaseQuery
    {
        private readonly IUser viewer;

        protected BaseQuery(IUser viewer)
        {
            this.viewer = viewer;
            ViewerId = viewer.Id;
            ViewerLevelId = viewer.LevelId;
            Language = viewer.Language;
        }

        public int CustId { get; set; }

        public string CustName { get; set; }

        public byte CustLevelId { get; set; }

        public virtual UserLevel CustLevel => Enumeration.FromValue<UserLevel>(CustLevelId);

        public int ViewerId { get; }

        public byte ViewerLevelId { get; }

        public virtual UserLevel ActorLevel => Enumeration.FromValue<UserLevel>(ViewerLevelId);

        public string Language { get; }

        public virtual bool IsValid()
            => ViewerId > 0
            && !string.IsNullOrWhiteSpace(Language);
    }
}