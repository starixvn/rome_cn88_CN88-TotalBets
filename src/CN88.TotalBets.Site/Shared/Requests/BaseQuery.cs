namespace CN88.TotalBets.Site.Shared.Requests
{
    using TotalBets.Shared.Models;

    public class BaseQuery
    {
        internal BaseQuery()
        {
        }

        protected BaseQuery(IUser viewer)
        {
            ViewerId = viewer.Id;
            ViewerLevelId = viewer.LevelId;
            ViewerName = viewer.Name;
            SubAccountId = viewer.SubAccountId;
            IsAllDownline = viewer.IsAllDownline;
            Language = viewer.Language;
        }

        public int CustId { get; set; }

        public string CustName { get; set; }

        public byte CustLevelId { get; set; }

        public virtual UserLevel CustLevel => Enumeration.FromValue<UserLevel>(CustLevelId);

        public int ViewerId { get; }

        public byte ViewerLevelId { get; }

        public string ViewerName { get; }

        public int SubAccountId { get; }

        public bool IsAllDownline { get; }

        public virtual UserLevel ActorLevel => Enumeration.FromValue<UserLevel>(ViewerLevelId);

        public string Language { get; }

        public virtual bool IsValid()
            => ViewerId > 0
            && !string.IsNullOrWhiteSpace(Language);
    }
}