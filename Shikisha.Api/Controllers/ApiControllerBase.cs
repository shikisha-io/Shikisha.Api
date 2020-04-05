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

        protected ActionResult<ServiceResponse<T>> ApiResponse<T>(ServiceResponse<T> serviceResponse)
        {
            if(serviceResponse.Errors != null)
                return BadRequest(serviceResponse);
            return Ok(serviceResponse);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<TEntity>>>> Get() => ApiResponse(await _service.GetAll());
        
        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<ActionResult<ServiceResponse<TEntity>>> GetById([FromRoute] Guid id, [FromQuery] bool expanded) => ApiResponse(await _service.GetById(id, expanded));

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<TEntity>>> Add([FromBody] TEntity entity)
            => ApiResponse(await _service.Add(entity));

        [HttpPut]
        [Route("{Id:Guid}")]
        public async Task<ServiceResponse<TEntity>> Update([FromRoute] Guid id, [FromBody] TEntity entity)
        {
            var upatedEntity = await _service.Update(id, entity);
            return upatedEntity;
        }
    }
}
