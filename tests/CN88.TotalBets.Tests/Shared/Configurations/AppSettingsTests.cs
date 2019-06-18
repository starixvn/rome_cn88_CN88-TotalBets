using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using CN88.TotalBets.Shared.Configurations;
using Xunit;

namespace CN88.TotalBets.Tests.Shared.Configurations
{
    public class AppSettingsTests
    {
        [Fact]
        public void Verify_MissingStoredProcedurePath_ThrowsConfigurationErrorsException()
        {
            // Arrange
            AppSettings.AppSettingValues = new NameValueCollection();

            // Act & Assert
            Assert.Throws<ConfigurationErrorsException>(() => AppSettings.Verify());
        }

        [Fact]
        public void Verify_HasEnoughSettings_PassVerify()
        {
            // Arrange
            AppSettings.AppSettingValues = new NameValueCollection();
            AppSettings.AppSettingValues.Add("StoredProcedurePath", "Local");
            var passVerify = false;

            // Act
            AppSettings.Verify();
            passVerify = true;

            // Assert
            Assert.True(passVerify);
        }

        [Fact]
        public void RemoteRefURL_Always_ReturnsCorrectSetting()
        {
            // Arrange
            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["RemoteRefURL"] = "RemoteRefURL"
            };
            var expectedSetting = "RemoteRefURL";

            // Act
            var actualSetting = AppSettings.RemoteRefURL;

            // Assert
            Assert.Equal(expectedSetting, actualSetting);
        }

        [Fact]
        public void TotalBetsAppPath_HasConfig_ReturnsCorrectSetting()
        {
            // Arrange
            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["TotalBetAppPath"] = "Path"
            };

            var expectedSetting = "Path";

            // Act
            var actualSetting = AppSettings.TotalBetsAppPath;

            // Assert
            Assert.Equal(expectedSetting, actualSetting);
        }

        [Fact]
        public void FeaturesAppPath_HasConfig_ReturnsCorrectSetting()
        {
            // Arrange
            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["FeaturesAppPath"] = "Path"
            };

            var expectedSetting = "Path";

            // Act
            var actualSetting = AppSettings.FeaturesAppPath;

            // Assert
            Assert.Equal(expectedSetting, actualSetting);
        }

        [Fact]
        public void ReloadTimeInSeconds_HasConfig_ReturnsCorrectSetting()
        {
            // Arrange
            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["ReloadTimeInSeconds"] = "30"
            };

            var expectedSetting = 30;

            // Act
            var actualSetting = AppSettings.ReloadTimeInSeconds;

            // Assert
            Assert.Equal(expectedSetting, actualSetting);
        }

        [Fact]
        public void OrderBySportTypeGroupInOverviewPage_HasConfig_ReturnsCorrectSetting()
        {
            // Arrange
            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["OrderBySportTypeGroupInOverviewPage"] = "1-1;2-2;3-3"
            };
            var expectedSetting = new Dictionary<int, int>
            {
                [1] = 1,
                [2] = 2,
                [3] = 3,
            };

            // Act
            var actualSetting = AppSettings.OrderBySportTypeGroupInOverviewPage;

            // Assert
            Assert.Equal(expectedSetting, actualSetting);
        }
    }
}