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
using Shikisha.Utilities;

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
        public async Task<ServiceResponse<List<TEntity>>> Get() => await _service.GetAll();
        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<ServiceResponse<TEntity>> GetById([FromRoute] Guid id, [FromQuery] bool expanded) => await _service.GetById(id, expanded);

        [HttpPost]
        public async Task<ServiceResponse<TEntity>> Add([FromBody] TEntity entity)
        {
            var createdEntity = await _service.Add(entity);
            // return CreatedAtAction(nameof(GetById), createdProduct.Id, createdProduct);
            return createdEntity;
        }

        [HttpPut]
        [Route("{Id:Guid}")]
        public async Task<ServiceResponse<TEntity>> Update([FromRoute] Guid id, [FromBody] TEntity entity)
        {
            var upatedEntity = await _service.Update(id, entity);
            return upatedEntity;
        }
    }
}
