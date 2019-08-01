using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using AutoFixture;
using CN88.TotalBets.Criteria;
using CN88.TotalBets.Features.Overview;
using CN88.TotalBets.Features.Overview.Models;
using CN88.TotalBets.Features.Overview.Params;
using CN88.TotalBets.Shared.Configurations;
using CN88.TotalBets.Shared.Models;
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
            dynamicRepository
                .FetchMultipleAsync<OverViewBySportTypeModel, OverViewByParlayModel, OverViewItemModel>(Arg.Compat.Any<GetOverViewCriteria>())
                .Returns(new Tuple<IEnumerable<OverViewBySportTypeModel>, IEnumerable<OverViewByParlayModel>, IEnumerable<OverViewItemModel>>(
                    Enumerable.Empty<OverViewBySportTypeModel>(),
                    Enumerable.Empty<OverViewByParlayModel>(),
                    Enumerable.Empty<OverViewItemModel>()));
            var param = new OverViewParam(Util.RandomValidCustId(fixture), 0, false, 1, UserLevel.Super);

            // Act
            overviewService.Get(param).ConfigureAwait(false);

            // Assert
            dynamicRepository.Received()
                .FetchMultipleAsync<OverViewBySportTypeModel, OverViewByParlayModel, OverViewItemModel>(
                Arg.Compat.Is<GetOverViewCriteria>(
                    x => x.CustId == param.CustId
                    && x.SubAccId == param.SubAccId
                    && x.IsAllDownline == param.IsAllDownline
                    && x.PossType == param.PossType
                    && x.UserLevel == param.UserLevel));
        }
    }
}