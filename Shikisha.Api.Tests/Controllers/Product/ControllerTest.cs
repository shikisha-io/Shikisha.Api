using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shikisha.Api.Controllers;
using Xunit;
using Models = Shikisha.DataAccess.DomainModels;

namespace Shikisha.Tests.Controllers.Product
{
    public class ControllerTests : ControllerBaseTests<Models.Product>
    {
        public ControllerTests()
        : base(mockService => new ProductsController(null, mockService))
        {}

        [Fact]
        public void Fact_GetAll_Success() =>
            Fact_BaseGetAll_Success(new List<Models.Product>
            {
                new Models.Product("Test Product A", "A new product"),
                new Models.Product("Test Product B", "A new product")
            });
    }
}