using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shikisha.Api.Controllers;
using Shikisha.DataAccess;
using Shikisha.Services.Interfaces;
using Shikisha.Utilities;
using Xunit;
using Models = Shikisha.DataAccess.DomainModels;

namespace Shikisha.Tests.Controllers
{
    public abstract class ControllerBaseTests<TEntity>
    where TEntity : EntityBase
    {
        protected readonly Mock<IService<TEntity>> _mockService;
        protected readonly ApiControllerBase<TEntity> _controller;
        protected ControllerBaseTests(Func<IService<TEntity>, ApiControllerBase<TEntity>> controllerConstructorFunc)
        {
            _mockService = new Mock<IService<TEntity>>();
            _controller = controllerConstructorFunc(_mockService.Object);
        }

        protected async void Fact_BaseGetAll_Success(List<TEntity> mockResult)
        {
            var mockResponse = new ServiceResponse<List<TEntity>>(mockResult);
            _mockService.Setup(x => x.GetAll()).ReturnsAsync(mockResponse);
            var result = await _controller.Get();

            var actionResult = Assert.IsType<ActionResult<Task<ServiceResponse<List<TEntity>>>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(mockResponse, okResult.Value);
        }
    }
}