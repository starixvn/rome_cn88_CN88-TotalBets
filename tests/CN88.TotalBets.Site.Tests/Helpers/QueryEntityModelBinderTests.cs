using System;
using System.Linq;
using System.Web.Mvc;
using CN88.TotalBets.Site.Features.Events;
using CN88.TotalBets.Site.Features.WagerAccount;
using CN88.TotalBets.Site.Shared.Helpers;
using CN88.TotalBets.Site.Shared.Models;
using CN88.TotalBets.Site.Shared.Requests;
using Xunit;

namespace CN88.TotalBets.Site.Tests.Helpers
{
    public class QueryEntityModelBinderTests : QueryEntityModelBinder
    {
        protected override object CreateInstanceOfAssignableBaseQuery(Type modelType)
        {
            return "CreateInstanceOfAssignableBaseQuery";
        }

        protected override object CallBaseCreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            return "CallBaseCreateModel";
        }

        protected override object CallBaseBindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // Simulate case when model is not ReportBaseQuery
            if (bindingContext != null)
            {
                return "ModelIsNotBaseQuery";
            }

            var baseQuery = new WagerAccountBaseQuery();

            return baseQuery;
        }

        protected override string GetBreadcrumbLevelJson(ControllerContext controllerContext)
        {
            if (controllerContext != null)
            {
                return ",[{'CustId':1176070,'CustName':'SUPERTEST1','CustLevelId':4,'IsLink':true}]";
            }

            return "[{'CustId':1176070,'CustName':'SUPERTEST1','CustLevelId':4,'IsLink':true}]";
        }

        [Fact]
        public void CreateModel_ConstructorParamIsAssignableFromBaseQuery_CallCreateInstanceOfAssignableBaseQuery()
        {
            // Arrange
            var eventQuery = typeof(EventQuery);

            // Act
            var createdInstance = CreateModel(null, null, eventQuery);

            // Assert
            Assert.Equal("CreateInstanceOfAssignableBaseQuery", createdInstance);
        }

        [Fact]
        public void CreateModel_ConstructorParamIsNotAssignableFromBaseQuery_CallBaseCreateModel()
        {
            // Arrange
            var userType = typeof(User);

            // Act
            var createdInstance = CreateModel(null, null, userType);

            // Assert
            Assert.Equal("CallBaseCreateModel", createdInstance);
        }

        [Fact]
        public void BindModel_ModelIsNotBaseQuery_ReturnStubData()
        {
            // Act
            var model = BindModel(null, new ModelBindingContext());

            // Assert
            Assert.Equal("ModelIsNotBaseQuery", model);
        }

        [Fact]
        public void BindModel_ModelIsBaseQuery_ReturnReportBaseQuery()
        {
            // Act
            var model = BindModel(null, null);

            // Assert
            Assert.True(model is BaseQuery);
        }

        [Fact]
        public void BindModel_BreadcrumbLevelJsonHasData_ReturnDeserializedBreadcrumLevel()
        {
            // Act
            var breadcrumLevel = (BindModel(null, null) as WagerAccountBaseQuery).BreadcrumbLevel;

            // Assert
            var superInfo = breadcrumLevel.ElementAt(0);
            Assert.True(breadcrumLevel.Count() == 1
                && superInfo.CustName == "SUPERTEST1"
                && superInfo.CustLevelId == 4
                && superInfo.CustId == 1176070
                && superInfo.IsLink == true);
        }

        [Fact]
        public void BindModel_BreadcrumbLevelJsonHasValidData_ReturnEmptyBreadcrumbLevel()
        {
            // Arrange
            var controllerContext = new ControllerContext();

            // Act
            var breadcrumbLevel = (BindModel(controllerContext, null) as WagerAccountBaseQuery).BreadcrumbLevel;

            // Assert
            Assert.Null(breadcrumbLevel);
        }
    }
}