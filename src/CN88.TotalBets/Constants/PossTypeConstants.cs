namespace CN88.TotalBets.Constants
{
    using System.Collections.Generic;

    public static class PossTypeConstants
    {
        public const int Full_Percentage = 0;
        public const int Company_Percentage = 1;
        public const int SMA_Percentage = 2;
        public const int MA_Percentage = 3;
        public const int Agent_Percentage = 4;
        public const int SMA_MA_Agent_Percentage = 5;
        public const int SMA_MA_Percentage = 6;
        public const int MA_Agent_Percentage = 7;

        public static IDictionary<int, string> PossTypes { get; } = new Dictionary<int, string>
        {
            [Full_Percentage] = nameof(Full_Percentage),
            [Company_Percentage] = nameof(Company_Percentage),
            [SMA_Percentage] = nameof(SMA_Percentage),
            [MA_Percentage] = nameof(MA_Percentage),
            [Agent_Percentage] = nameof(Agent_Percentage),
            [SMA_MA_Agent_Percentage] = nameof(SMA_MA_Agent_Percentage),
            [SMA_MA_Percentage] = nameof(SMA_MA_Percentage),
            [MA_Agent_Percentage] = nameof(MA_Agent_Percentage)
        };
    }
}