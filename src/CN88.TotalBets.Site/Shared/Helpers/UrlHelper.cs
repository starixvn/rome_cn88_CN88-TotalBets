namespace CN88.TotalBets.Site.Shared.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web;
    using TotalBets.Shared.Configurations;
    using Web.Helpers;

    public static class UrlHelper
    {
        [ThreadStatic]
        private static Func<string, string> mapTotalBetsPath;

        [ThreadStatic]
        private static Func<string, string> mapFeaturesPath;

        public static Func<string, string> MapTotalBetsPath
        {
            get
            {
                return mapTotalBetsPath ?? HttpContext.Current.Response.ApplyAppPathModifier;
            }

            internal set
            {
                mapTotalBetsPath = value;
            }
        }

        public static Func<string, string> MapFeaturesPath
        {
            get
            {
                return mapFeaturesPath ?? GetFeaturesPath;
            }

            internal set
            {
                mapFeaturesPath = value;
            }
        }

        [ThreadStatic]
        private static Func<string, IDictionary<string, object>, string> replaceOrAddQueryStringParamsFunc;

        [ThreadStatic]
        private static Func<string, IDictionary<string, object>, string> addQueryStringParamsFunc;

        public static Func<string, IDictionary<string, object>, string> AddQueryStringParamsFunc
        {
            get
            {
                return addQueryStringParamsFunc ?? AddQueryStringParams;
            }

            internal set
            {
                addQueryStringParamsFunc = value;
            }
        }

        public static Func<string, IDictionary<string, object>, string> ReplaceOrAddQueryStringParamsFunc
        {
            get
            {
                return replaceOrAddQueryStringParamsFunc ?? ReplaceOrAddQueryStringParams;
            }

            internal set
            {
                replaceOrAddQueryStringParamsFunc = value;
            }
        }

        public static Func<string, string, string> GetSiteUrl { get; internal set; } = GetLink;

        internal static string ReplaceOrAddQueryStringParams(string link, IDictionary<string, object> routeValues)
        {
            if (!routeValues.Any())
            {
                return link;
            }

            if (!link.Contains("?"))
            {
                return AddQueryStringParams(link, routeValues);
            }

            foreach (var routeValue in routeValues)
            {
                link = ReplaceOrAddQueryStringParam(link, routeValue.Key, routeValue.Value);
            }

            return link;
        }

        internal static string AddQueryStringParams(string link, IDictionary<string, object> routeValues)
        {
            if (!routeValues.Any())
            {
                return link;
            }

            var queryString = string.Join("&", routeValues.Select(kvp => $"{kvp.Key}={kvp.Value}"));

            return $"{link}?{queryString}";
        }

        internal static string ReplaceOrAddQueryStringParam(string link, string paramName, object paramValue)
        {
            if (string.IsNullOrWhiteSpace(paramName) || paramValue == null)
            {
                return link;
            }

            var regexParamName = @"([?&]" + paramName + ")=[^?&]+";
            if (Regex.IsMatch(link, regexParamName, RegexOptions.IgnoreCase))
            {
                return Regex.Replace(link, regexParamName, "$1=" + paramValue, RegexOptions.IgnoreCase);
            }

            return link + $"&{paramName}={paramValue}";
        }

        public static string GetTotalBetsAppPath(string virtualPath = "~/")
            => AppPathHelper.GetAppPath(AppSettings.TotalBetsAppPath, virtualPath);

        internal static string GetLink(string action, string currentQuery)
            => GetTotalBetsAppPath(action + currentQuery);

        internal static string GetFeaturesPath(string virtualPath = "~/")
            => AppPathHelper.GetAppPath(AppSettings.FeaturesAppPath, virtualPath);
    }
}
