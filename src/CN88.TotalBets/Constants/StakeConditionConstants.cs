namespace CN88.TotalBets.Constants
{
    using System.Collections.Generic;

    public static class StakeConditionConstants
    {
        public const byte Equal = 0;
        public const byte GreaterThan = 1;
        public const byte LessThan = 2;

        public static IDictionary<int, string> Conditions { get; } = new Dictionary<int, string>
        {
            [Equal] = "=",
            [GreaterThan] = ">",
            [LessThan] = "<"
        };
    }
}