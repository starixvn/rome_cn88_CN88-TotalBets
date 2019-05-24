namespace CN88.TotalBets.Features.Overview.Models
{
    public class OverViewItemModel
    {
        public OverViewItemModel()
        {
        }

        public OverViewItemModel(int sportTypeGroup, int wager, decimal stake)
        {
            Wager = wager;
            Stake = stake;
            SportTypeGroup = sportTypeGroup;
        }

        public int SportTypeGroup { get; private set; }

        public int Wager { get; private set; }

        public decimal Stake { get; private set; }

        public static OverViewItemModel operator +(OverViewItemModel first, OverViewItemModel second)
        {
            var temp = new OverViewItemModel();
            temp.Wager = first.Wager + second.Wager;
            temp.Stake = first.Stake + second.Stake;

            return temp;
        }
    }
}