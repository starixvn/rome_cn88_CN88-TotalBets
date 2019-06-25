using CN88.TotalBets.Features.WagerAccount;
using Xunit;

namespace CN88.TotalBets.Tests.Features.WagerAccount
{
    public class WagerAccountModelTests
    {
        private readonly WagerAccountModel wagerAccountModel;

        public WagerAccountModelTests()
        {
            wagerAccountModel = new WagerAccountModel(1, "username", "firstname", 10, 1000);
        }

        [Fact]
        public void CustId_Always_ReturnsCorrectValue()
        {
            // Act & Assert
            Assert.Equal(1, wagerAccountModel.CustId);
        }

        [Fact]
        public void UserName_Always_ReturnsCorrectValue()
        {
            // Act & Assert
            Assert.Equal("username", wagerAccountModel.UserName);
        }

        [Fact]
        public void FirstName_Always_ReturnsCorrectValue()
        {
            // Act & Assert
            Assert.Equal("firstname", wagerAccountModel.FirstName);
        }

        [Fact]
        public void Wager_Always_ReturnsCorrectValue()
        {
            // Act & Assert
            Assert.Equal(10, wagerAccountModel.Wager);
        }

        [Fact]
        public void Stake_Always_ReturnsCorrectValue()
        {
            // Act & Assert
            Assert.Equal(1000, wagerAccountModel.Stake);
        }
    }
}