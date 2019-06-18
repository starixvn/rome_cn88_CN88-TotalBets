using System.Collections.Generic;
using System.Data;
using System.Linq;
using CN88.TotalBets.Features.SportTypeGroup;
using CN88.TotalBets.Features.SportTypeGroup.Criteria;
using CN88.TotalBets.Features.SportTypeGroup.Models;
using Fanex.Data;
using KellermanSoftware.CompareNetObjects;
using NSubstitute;
using Xunit;

namespace CN88.TotalBets.Tests.Features.SportTypeGroup
{
    public class SportTypeGroupDataAccessImplTests
    {
        private readonly IObjectDb objectDb;
        private readonly SportTypeGroupDataAccessImpl sportTypeGroupDataAccess;

        public SportTypeGroupDataAccessImplTests()
        {
            DbSettingProviderManager.StartTestingSession().Run();
            objectDb = Substitute.For<IObjectDb>();
            ObjectDbFactory.Mock((settingKey, commandTimeout) => objectDb);
            sportTypeGroupDataAccess = new SportTypeGroupDataAccessImpl();
        }

        [Fact]
        public void GetSportGroups_EmptyDataReturnFromDb_ReturnsEmpty()
        {
            // Arrange
            var dataTable = new DataTable();
            var dataReader = dataTable.CreateDataReader();
            objectDb.ExecuteReader(Arg.Compat.Any<GetSportGroupsCriteria>())
                .Returns(dataReader);
            var expectedResult = Enumerable.Empty<SportGroup>();

            // Act
            var actualResult = sportTypeGroupDataAccess.GetSportGroups(new GetSportGroupsCriteria());

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void GetSportGroups_HasData_ReturnsCorrectData()
        {
            // Arrange
            var compareLogic = new CompareLogic();
            var dataTable = new DataTable();
            dataTable.Columns.Add("SportTypeGroup", typeof(int));
            dataTable.Columns.Add("DisplaySport", typeof(string));
            dataTable.Rows.Add(9, "9");
            var dataReader = dataTable.CreateDataReader();
            objectDb.ExecuteReader(Arg.Compat.Any<GetSportGroupsCriteria>())
                .Returns(dataReader);
            var displaySportIds = "9".Split(',').Select(int.Parse);
            IEnumerable<SportGroup> expectedResult = new List<SportGroup>
            {
                new SportGroup(9, displaySportIds)
            };

            // Act
            var actualResult = sportTypeGroupDataAccess.GetSportGroups(new GetSportGroupsCriteria());

            // Assert
            Assert.True(compareLogic.Compare(expectedResult, actualResult).AreEqual);
        }
    }
}