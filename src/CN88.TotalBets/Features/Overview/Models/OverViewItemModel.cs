namespace CN88.TotalBets.Features.Overview.Models
{
    public class OverViewItemModel
    {
        public OverViewItemModel()
        {
        }

        public OverViewItemModel(int sportTypeGroup, int wagers, decimal stake)
        {
            Wagers = wagers;
            Stake = stake;
            SportTypeGroup = sportTypeGroup;            
        }

        public int SportTypeGroup { get; private set; }

        public int Wagers { get; private set; }

        public decimal Stake { get; private set; }

        public static OverViewItemModel operator +(OverViewItemModel first, OverViewItemModel second)
        {
            var temp = new OverViewItemModel();
            temp.Wagers = first.Wagers + second.Wagers;
            temp.Stake = first.Stake + second.Stake;

            return temp;
        }
    }
}