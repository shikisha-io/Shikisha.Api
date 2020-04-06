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
        : base(
            mockService => new ProjectsController(null, mockService, _MockProductService.Object),
            () => new Models.Project("Test Project", "A new project"))
        { }
    }
}