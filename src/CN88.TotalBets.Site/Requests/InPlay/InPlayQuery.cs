namespace CN88.TotalBets.Site.Requests.InPlay
{
    using MediatR;
    using Shared.Requests;
    using TotalBets.Shared.Models;
    using ViewModels.InPlay;

    public class InPlayQuery : BaseQuery, IRequest<InPlayViewModel>
    {
        public InPlayQuery(IUser viewer)
            : base(viewer)
        {
        }

        public byte Type { get; set; }

        public int SportTypeGroup { get; set; }

        public int LeagueId { get; set; }

        public byte StakeCondition { get; set; }

        public decimal Stake { get; set; }

        public byte PossType { get; set; }

        public byte IsLive { get; set; }
    }
}
