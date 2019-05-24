namespace CN88.TotalBets.Site.Shared.Helpers
{
    public interface BrowserHelper
    {
        bool IsMobile();

        bool IsTablet();

        BrowserInfo GetBrowserInfo();
    }
}