using CN88.TotalBets.Site.Shared.Models;
using NSubstitute;
using Sunplus.Agent.Model.Entities;
using Xunit;

namespace CN88.TotalBets.Site.Tests.Features.Shared.Models
{
    public class UserTests
    {
        private readonly IUserSession userSession;

        public UserTests()
        {
            userSession = Substitute.For<IUserSession>();
        }

        [Fact]
        public void Id_Always_SetValueFromUserSession()
        {
            // Arrange
            userSession.CustId.Returns(1);

            // Act
            var user = new User(userSession);

            // Assert
            Assert.Equal(1, user.Id);
        }

        [Fact]
        public void Name_Always_SetValueFromUserSession()
        {
            // Arrange
            userSession.UserName.Returns("Username");

            // Act
            var user = new User(userSession);

            // Assert
            Assert.Equal("Username", user.Name);
        }

        [Fact]
        public void NickName_Always_SetValueFromUserSession()
        {
            // Arrange
            userSession.NickName.Returns("NickName");

            // Act
            var user = new User(userSession);

            // Assert
            Assert.Equal("NickName", user.NickName);
        }

        [Fact]
        public void Language_Always_SetValueFromUserSession()
        {
            // Arrange
            userSession.Language.Returns("en-US");

            // Act
            var user = new User(userSession);

            // Assert
            Assert.Equal("en-US", user.Language);
        }

        [Fact]
        public void LanguageKey_Always_SetValueFromUserSession()
        {
            // Arrange
            userSession.LangKey.Returns("E");

            // Act
            var user = new User(userSession);

            // Assert
            Assert.Equal("E", user.LanguageKey);
        }

        [Fact]
        public void LevelId_Always_SetValueFromUserSession()
        {
            // Arrange
            userSession.RoleId.Returns((byte)2);

            // Act
            var user = new User(userSession);

            // Assert
            Assert.Equal(2, user.LevelId);
        }

        [Fact]
        public void AdminId_Always_SetValueFromUserSession()
        {
            // Arrange
            userSession.AdminId.Returns(2);

            // Act
            var user = new User(userSession);

            // Assert
            Assert.Equal(2, user.AdminId);
        }

        [Fact]
        public void IsInternal_Always_SetValueFromUserSession()
        {
            // Arrange
            userSession.IsInternal.Returns(false);

            // Act
            var user = new User(userSession);

            // Assert
            Assert.False(user.IsInternal);
        }

        [Fact]
        public void SubAccountId_Always_SetValueFromUserSession()
        {
            // Arrange
            userSession.SubAccId.Returns(1);

            // Act
            var user = new User(userSession);

            // Assert
            Assert.Equal(1, user.SubAccountId);
        }

        [Fact]
        public void IP_Always_SetValueFromUserSession()
        {
            // Arrange
            userSession.UserLoginIp.Returns("127.0.0.1");

            // Act
            var user = new User(userSession);

            // Assert
            Assert.Equal("127.0.0.1", user.IP);
        }

        [Fact]
        public void IsAdminCanViewVNIP_Always_SetValueFromUserSession()
        {
            // Arrange
            userSession.IsAdminCanViewVNIP.Returns(false);

            // Act
            var user = new User(userSession);

            // Assert
            Assert.False(user.IsAdminCanViewVNIP);
        }
    }
}