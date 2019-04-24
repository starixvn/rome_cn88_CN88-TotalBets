namespace CN88.TotalBets.Shared.Constants
{
    using System.Collections.Generic;

    public static class EventTypeConstants
    {
        public const int InPlay = 1;
        public const int Today = 2;
        public const int Early = 3;
        public const int Outright = 4;
        public const int Started = 5;

        public static IDictionary<int, string> EventTypes { get; } = new Dictionary<int, string>
        {
            [InPlay] = nameof(InPlay),
            [Today] = nameof(Today),
            [Early] = nameof(Early),
            [Outright] = nameof(Outright),
            [Started] = nameof(Started)
        };
    }
}