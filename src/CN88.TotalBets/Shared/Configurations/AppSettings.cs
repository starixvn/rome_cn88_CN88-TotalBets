namespace CN88.TotalBets.Shared.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;

    public static class AppSettings
    {
        [ThreadStatic]
        private static NameValueCollection appSettingValues;

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed")]
        internal static NameValueCollection AppSettingValues
        {
            get { return appSettingValues ?? ConfigurationManager.AppSettings; }

            set { appSettingValues = value; }
        }

        public static string StoredProcedurePath => GetConfigValue("StoredProcedurePath");

        public static string RemoteRefURL => GetConfigValue("RemoteRefURL");

        public static string TotalBetsAppPath => GetConfigValue("TotalBetAppPath");

        public static string FeaturesAppPath => GetConfigValue("FeaturesAppPath");

        public static int ReloadTimeInSeconds => int.Parse(GetConfigValue("ReloadTimeInSeconds") ?? "15");

        public static Dictionary<int, int> OrderBySportTypeGroupInOverviewPage
        {
            get
            {
                var orderBy = new Dictionary<int, int>();
                var items = GetConfigValue("OrderBySportTypeGroupInOverviewPage").Split(';');
                foreach (var item in items)
                {
                    var sportypeGroupId = item.Split('-')[0];
                    var orderId = item.Split('-')[1];

                    orderBy.Add(int.Parse(sportypeGroupId), int.Parse(orderId));
                }

                return orderBy;
            }
        }

        public static string GetConfigValue(string configName) => AppSettingValues[configName];

        public static void Verify()
        {
            Require(StoredProcedurePath, nameof(StoredProcedurePath));
        }

        private static void Require(string key, string keyName)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ConfigurationErrorsException($"{keyName} is null or whitespace");
            }
        }
    }
}