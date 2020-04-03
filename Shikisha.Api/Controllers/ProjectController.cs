using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shikisha.DataAccess;
using Shikisha.DataAccess.DomainModels;
using Shikisha.Services.Interfaces;

namespace Shikisha.Api.Controllers
{
    public sealed class ProjectsController : ApiControllerBase<Project>
    {
        private readonly IService<Product> _productService;
        public ProjectsController(ILogger<ApiControllerBase<Project>> logger, IService<Project> service, IService<Product> productService) : base(logger, service)
        {
            _productService = productService;
        }

        [HttpPost]
        [Route("{productId}")]
        public async Task<Project> AddToProduct(Guid productId, Project entity)
        {
            var foundProduct = await _productService.GetById(productId);
            entity.Product = foundProduct;
            var createdEntity = await _service.Add(entity);
            return createdEntity;
        }
    }
}
