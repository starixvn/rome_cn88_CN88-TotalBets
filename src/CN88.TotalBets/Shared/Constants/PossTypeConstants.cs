namespace CN88.TotalBets.Shared.Constants
{
    using System.Collections.Generic;
    using Models;

    public static class PossTypeConstants
    {
        public const byte Full_Percentage = 0;
        public const byte Company_Percentage = 1;
        public const byte SMA_Percentage = 2;
        public const byte MA_Percentage = 3;
        public const byte Agent_Percentage = 4;
        public const byte SMA_MA_Agent_Percentage = 5;
        public const byte SMA_MA_Percentage = 6;
        public const byte MA_Agent_Percentage = 7;

        private static IDictionary<byte, string> superPossTypes = new Dictionary<byte, string>
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

        private static IDictionary<byte, string> masterPossTypes = new Dictionary<byte, string>
        {
            [Full_Percentage] = nameof(Full_Percentage),
            [MA_Percentage] = nameof(MA_Percentage),
            [Agent_Percentage] = nameof(Agent_Percentage),
            [MA_Agent_Percentage] = nameof(MA_Agent_Percentage)
        };

        private static IDictionary<byte, string> agentPossTypes = new Dictionary<byte, string>
        {
            [Full_Percentage] = nameof(Full_Percentage),
            [Agent_Percentage] = nameof(Agent_Percentage)
        };

        public static IDictionary<UserLevel, IDictionary<byte, string>> PossTypeByLevel { get; } = new Dictionary<UserLevel, IDictionary<byte, string>>
        {
            [UserLevel.Super] = superPossTypes,
            [UserLevel.Master] = masterPossTypes,
            [UserLevel.Agent] = agentPossTypes
        };
    }
}