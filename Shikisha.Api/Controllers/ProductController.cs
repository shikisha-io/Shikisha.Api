using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shikisha.DataAccess;
using Shikisha.DataAccess.DomainModels;
using Shikisha.Services.Interfaces;

namespace Shikisha.Api.Controllers
{
    public sealed class ProductsController : ApiControllerBase<Product>
    {
        public ProductsController(ILogger<ApiControllerBase<Product>> logger, IService<Product> service) : base(logger, service)
        {
        }
    }
}
