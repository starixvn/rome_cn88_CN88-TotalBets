namespace CN88.TotalBets.Features.WagerAccount
{
    public class WagerAccountModel
    {
        public WagerAccountModel()
        {
        }

        public int CustId { get; }

        public string UserName { get; }

        public string FirstName { get; }

        public int Wager { get; }

        public decimal Stake { get; }
    }
}