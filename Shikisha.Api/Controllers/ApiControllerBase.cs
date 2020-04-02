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
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase<TEntity> : ControllerBase
    where TEntity : EntityBase
    {
        protected readonly ILogger<ApiControllerBase<TEntity>> _logger;
        protected readonly IService<TEntity> _service;

        public ApiControllerBase(ILogger<ApiControllerBase<TEntity>> logger, IService<TEntity> service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<TEntity>> Get()
        {
            return await _service.GetAll();
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<TEntity> GetById(Guid id)
        {
            return await _service.GetById(id);
        }

        [HttpPost]
        public async Task<TEntity> Add(TEntity entity)
        {
            var createdEntity = await _service.Add(entity);
            // return CreatedAtAction(nameof(GetById), createdProduct.Id, createdProduct);
            return createdEntity;
        }
    }
}
