using System.Collections.Generic;
using Moq;
using Shikisha.Api.Controllers;
using Shikisha.Services.Interfaces;
using Xunit;
using Models = Shikisha.DataAccess.DomainModels;

namespace Shikisha.Tests.Controllers.Project
{
    public class ControllerTests : ControllerBaseTests<Models.Project>
    {
        private static Mock<IService<Models.Product>> _MockProductService = new Mock<IService<Models.Product>>();
        public ControllerTests()
        : base(mockService => new ProjectsController(null, mockService, _MockProductService.Object))
        {}

        [Fact]
        public void Fact_GetAll_Success() =>
            Fact_BaseGetAll_Success(new List<Models.Project>
            {
                new Models.Project("Test Product A", "A new product"),
                new Models.Project("Test Product B", "A new product")
            });

        [Fact]
        public void Fact_GetById_Success() =>
            Fact_BaseGetById_Success(new Models.Project("Test Project A", "A new project"));
    }
}