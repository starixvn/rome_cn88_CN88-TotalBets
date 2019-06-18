using CN88.TotalBets.Shared.Models;
using Xunit;

namespace CN88.TotalBets.Tests.Shared.Models
{
    public class UserLevelTests
    {
        [Fact]
        public void IsAgentLevel_UserLevelIsAgent_ReturnsTrue()
        {
            // Arrange
            var userLevel = Enumeration.FromValue<UserLevel>(UserLevel.Agent.Value);

            // Act
            var result = userLevel.IsAgentLevel();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsNotAgentLevel_UserLevelIsAgent_ReturnsFalse()
        {
            // Arrange
            var userLevel = Enumeration.FromValue<UserLevel>(UserLevel.Agent.Value);

            // Act
            var result = userLevel.IsNotAgentLevel();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsMasterLevel_UserLevelIsMaster_ReturnsTrue()
        {
            // Arrange
            var userLevel = Enumeration.FromValue<UserLevel>(UserLevel.Master.Value);

            // Act
            var result = userLevel.IsMasterLevel();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsNotMasterLevel_UserLevelIsMaster_ReturnsFalse()
        {
            // Arrange
            var userLevel = Enumeration.FromValue<UserLevel>(UserLevel.Master.Value);

            // Act
            var result = userLevel.IsNotMasterLevel();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsSuperLevel_UserLevelIsSuper_ReturnsTrue()
        {
            // Arrange
            var userLevel = Enumeration.FromValue<UserLevel>(UserLevel.Super.Value);

            // Act
            var result = userLevel.IsSuperLevel();

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(3)]
        public void IsMasterOrSuperLevel_UserLevelIsSuperOrMaster_ReturnsTrue(byte levelId)
        {
            // Arrange
            var userLevel = Enumeration.FromValue<UserLevel>(levelId);

            // Act
            var result = userLevel.IsMasterOrSuperLevel();

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        public void IsMasterOrSuperLevel_UserLevelIsMemberOrMaster_ReturnsTrue(byte levelId)
        {
            // Arrange
            var userLevel = Enumeration.FromValue<UserLevel>(levelId);

            // Act
            var result = userLevel.IsMemberOrMasterLevel();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsMemberLevel_UserLevelIsMember_ReturnsTrue()
        {
            // Arrange
            var userLevel = Enumeration.FromValue<UserLevel>(UserLevel.Member.Value);

            // Act
            var result = userLevel.IsMemberLevel();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsNotSuperLevel_UserLevelIsSuper_ReturnsFalse()
        {
            // Arrange
            var userLevel = Enumeration.FromValue<UserLevel>(UserLevel.Super.Value);

            // Act
            var result = userLevel.IsNotSuperLevel();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsNotMemberLevel_UserLevelIsMember_ReturnsFalse()
        {
            // Arrange
            var userLevel = Enumeration.FromValue<UserLevel>(UserLevel.Member.Value);

            // Act
            var result = userLevel.IsNotMemberLevel();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsHeadline_UserLevelIsHeadline_ReturnsTrue()
        {
            // Arrange
            var userLevel = Enumeration.FromValue<UserLevel>(UserLevel.Headline.Value);

            // Act
            var result = userLevel.IsHeadline();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsNotHeadline_UserLevelIsHeadline_ReturnsFalse()
        {
            // Arrange
            var userLevel = Enumeration.FromValue<UserLevel>(UserLevel.Headline.Value);

            // Act
            var result = userLevel.IsNotHeadline();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsDirectUplineOf_CustomerLevelIsNull_ReturnsFalse()
        {
            // Arrange
            var userLevel = Enumeration.FromValue<UserLevel>(UserLevel.Super.Value);

            // Act
            var result = userLevel.IsDirectUplineOf(null);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [MemberData(nameof(UserLevelTestCases.UplineUserLevelAndDownlineValueDataSources), MemberType = typeof(UserLevelTestCases))]
        public void IsDirectUplineOf_Always_ReturnsValidResult(UserLevel uplineLevel, byte downlineLevelValue, bool result)
        {
            var actualResult = uplineLevel.IsDirectUplineOf(downlineLevelValue);

            Assert.Equal(result, actualResult);
        }

        [Theory]
        [MemberData(nameof(UserLevelTestCases.UplineUserLevelAndDownlineValueDataSources), MemberType = typeof(UserLevelTestCases))]
        public void IsDirectUplineOf_ValidCustomerLevel_ReturnsValidValue(UserLevel uplineLevel, byte downlineLevelValue, bool result)
        {
            // Act
            var actualResult = uplineLevel
                .IsDirectUplineOf(Enumeration.FromValue<UserLevel>(downlineLevelValue));

            // Assert
            Assert.Equal(result, actualResult);
        }

        [Fact]
        public void IsDirectDownlineOf_CustomerLevelIsNull_ReturnsFalse()
        {
            // Arrange
            var userLevel = Enumeration.FromValue<UserLevel>(UserLevel.Super.Value);

            // Act
            var result = userLevel.IsDirectDownlineOf(null);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [MemberData(nameof(UserLevelTestCases.DownlineUserLevelAndUplineValueDataSources), MemberType = typeof(UserLevelTestCases))]
        public void IsDirectDownlineOf_CustomerLevelIsNotNull_ReturnsValidResult(UserLevel downlineLevel, byte uplineLevelValue, bool result)
        {
            // Arrange
            var uplineLevel = Enumeration.FromValue<UserLevel>(uplineLevelValue);

            // Act
            var actualResult = downlineLevel.IsDirectDownlineOf(uplineLevel);

            // Assert
            Assert.Equal(result, actualResult);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(3)]
        [InlineData(2)]
        public void Is_UserLevelIsNull_ReturnsFalse(byte userLevelValue)
        {
            // Arrange
            var userLevel = Enumeration.FromValue<UserLevel>(userLevelValue);

            // Act
            var result = userLevel.Is(null);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [MemberData(nameof(UserLevelTestCases.SameUseLevelDataSource), MemberType = typeof(UserLevelTestCases))]
        public void Is_UserLevelIsNotNull_ReturnsExpectedResult(UserLevel userLevel1, UserLevel userLevel2, bool result)
        {
            var actualResult = userLevel1.Is(userLevel2);

            Assert.Equal(result, actualResult);
        }

        [Theory]
        [MemberData(nameof(UserLevelTestCases.SameUseLevelDataSource), MemberType = typeof(UserLevelTestCases))]
        public void IsNot_UserLevelIsNotNull_ReturnsExpectedResult(UserLevel userLevel1, UserLevel userLevel2, bool result)
        {
            var actualResult = userLevel1.IsNot(userLevel2);

            Assert.Equal(!result, actualResult);
        }

        [Theory]
        [InlineData(4, 3)]
        [InlineData(3, 2)]
        [InlineData(2, 1)]
        public void GetDirectDownLineLevel_SMALevel_ReturnsDirectDownline(byte roleId, byte expectedDirectDownlineRoleId)
        {
            // Arrange
            var userLevel = Enumeration.FromValue<UserLevel>(roleId);

            // Act
            var actualDirectDownlineRoleId = userLevel.GetDirectDownLineLevel().Value;

            // Assert
            Assert.Equal(expectedDirectDownlineRoleId, actualDirectDownlineRoleId);
        }

        [Fact]
        public void IsUplineOf_GapBetweenTwoLevelsLessThan1_ReturnsFalse()
        {
            // Arrange
            var userLevel = UserLevel.Master;

            // Act
            var actualIsUplineOf = userLevel.IsUplineOf(4);

            // Assert
            Assert.False(actualIsUplineOf);
        }

        [Fact]
        public void IsUplineOf_GapBetweenTwoLevelsFrom1_ReturnsTrue()
        {
            // Arrange
            var userLevel = UserLevel.Master;

            // Act
            var actualIsUplineOf = userLevel.IsUplineOf(2);

            // Assert
            Assert.True(actualIsUplineOf);
        }

        [Fact]
        public void IsUplineOf_DownlineLevelIsNull_ReturnsFalse()
        {
            // Arrange
            var userLevel = UserLevel.Master;
            UserLevel downlineLevel = null;

            // Act
            var actualIsUplineOf = userLevel.IsUplineOf(downlineLevel);

            // Assert
            Assert.False(actualIsUplineOf);
        }

        [Fact]
        public void IsUplineOf_ValidDownline_ReturnsTrue()
        {
            // Arrange
            var userLevel = UserLevel.Master;
            var downlineLevel = UserLevel.Agent;

            // Act
            var actualIsUplineOf = userLevel.IsUplineOf(downlineLevel);

            // Assert
            Assert.True(actualIsUplineOf);
        }
    }
}