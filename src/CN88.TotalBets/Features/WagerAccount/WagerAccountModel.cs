namespace CN88.TotalBets.Features.WagerAccount
{
    public class WagerAccountModel
    {
        public WagerAccountModel()
        {
        }

        public WagerAccountModel(
            int custId,
            string userName,
            string firstName,
            int wager,
            decimal stake)
        {
            CustId = custId;
            UserName = userName;
            FirstName = firstName;
            Wager = wager;
            Stake = stake;
        }

        public int CustId { get; }

        public string UserName { get; }

        public string FirstName { get; }

        public int Wager { get; }

        public decimal Stake { get; }
    }
}