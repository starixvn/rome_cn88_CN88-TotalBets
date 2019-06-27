using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using AutoFixture;
using CN88.TotalBets.Criteria;
using CN88.TotalBets.Features.Overview;
using CN88.TotalBets.Features.Overview.Models;
using CN88.TotalBets.Shared.Configurations;
using Fanex.Data.Repository;
using NSubstitute;
using Xunit;

namespace CN88.TotalBets.Tests.Features.Overview
{
    [Trait("Category", "Overview")]
    public class OverViewServiceTests
    {
        private readonly Fixture fixture;
        private readonly IDynamicRepository dynamicRepository;
        private readonly OverViewService overviewService;

        public OverViewServiceTests()
        {
            fixture = new Fixture();
            dynamicRepository = Substitute.For<IDynamicRepository>();
            overviewService = new OverViewService(dynamicRepository);
        }

        [Fact]
        public void Get_Always_CallDynamicRepositoryFetchMultipleAsync()
        {
            // Arrange
            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["OrderBySportTypeGroupInOverviewPage"] = "1-1;2-2;3-3"
            };
            var custId = Util.RandomValidCustId(fixture);
            var viewMode = 1;
            byte roleId = 4;
            dynamicRepository
                .FetchMultipleAsync<OverViewBySportTypeModel, OverViewByParlayModel, OverViewItemModel>(Arg.Compat.Any<GetOverViewCriteria>())
                .Returns(new Tuple<IEnumerable<OverViewBySportTypeModel>, IEnumerable<OverViewByParlayModel>, IEnumerable<OverViewItemModel>>(
                    Enumerable.Empty<OverViewBySportTypeModel>(),
                    Enumerable.Empty<OverViewByParlayModel>(),
                    Enumerable.Empty<OverViewItemModel>()));

            // Act
            overviewService.Get(custId, viewMode, roleId).ConfigureAwait(false);

            // Assert
            dynamicRepository.Received()
                .FetchMultipleAsync<OverViewBySportTypeModel, OverViewByParlayModel, OverViewItemModel>(
                Arg.Compat.Is<GetOverViewCriteria>(
                    x => x.CustId == custId
                    && x.PossType == viewMode
                    && x.UserLevel.Value == roleId));
        }
    }
}