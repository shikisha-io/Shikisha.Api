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
        : base(mockService => new ProductsController(null, mockService),
        () => new Models.Product("Test Product", "A new product"))
        { }
    }
}