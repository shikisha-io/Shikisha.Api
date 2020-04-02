using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shikisha.DataAccess.DomainModels;
using Shikisha.Services.Interfaces;

namespace Shikisha.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IService<Product> _service;

        public ProductsController(ILogger<ProductsController> logger, IService<Product> service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            return await _service.GetAll();
        }

        [HttpPost]
        public async Task<Product> Add(Product product)
        {
            return await _service.Add(product);
        }
    }
}
