namespace CN88.TotalBets.Constants
{
    using System.Collections.Generic;

    public static class StakeConditionConstants
    {
        public const int Equal = 0;
        public const int GreaterThan = 1;
        public const int LessThan = 2;

        public static IDictionary<int, string> Conditions { get; } = new Dictionary<int, string>
        {
            [Equal] = "=",
            [GreaterThan] = ">",
            [LessThan] = "<"
        };
    }
}