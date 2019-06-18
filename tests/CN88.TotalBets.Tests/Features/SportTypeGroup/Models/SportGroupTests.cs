using System.Collections.Generic;
using System.Linq;
using CN88.TotalBets.Features.SportTypeGroup.Models;
using Xunit;

namespace CN88.TotalBets.Tests.Features.SportTypeGroup.Models
{
    public class SportGroupTests
    {
        [Fact]
        public void Id_Always_ReturnsIdFromCtor()
        {
            // Arrange
            var id = 1;
            var product = new SportGroup(id, Enumerable.Empty<int>());

            // Act
            var actual = product.Id;

            // Assert
            Assert.Equal(id, actual);
        }

        [Fact]
        public void DisplaySportIds_Always_ReturnsDisplaySportIdsFromCtor()
        {
            // Arrange
            var displaySportIds = new List<int> { 1, 2 };
            var product = new SportGroup(0, displaySportIds);

            // Act
            var actual = product.DisplaySportIds;

            // Assert
            Assert.Equal(displaySportIds, actual);
        }

        [Fact]
        public void DisplayName_IsParlay_ReturnsCorrectDisplayName()
        {
            // Arrange
            var product = new SportGroup(9, Enumerable.Empty<int>());

            // Act
            var actual = product.DisplayName;

            // Assert
            Assert.Equal("Parlay", actual);
        }

        [Fact]
        public void DisplayName_IsNotParlay_ReturnsCorrectDisplayName()
        {
            // Arrange
            var productDisplayName = "productDisplayName";
            var product = new SportGroup(1, new List<int> { 1 }, (id) => productDisplayName);

            // Act
            var actual = product.DisplayName;

            // Assert
            Assert.Equal(productDisplayName, actual);
        }
    }
}