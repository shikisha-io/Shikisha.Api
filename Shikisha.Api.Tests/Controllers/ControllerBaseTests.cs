using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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

        protected async void SuccessTest<T>(T mockResult,
        Func<ServiceResponse<T>, Task<ActionResult<ServiceResponse<T>>>> controllerActionResponse)
        {
            var mockResponse = new ServiceResponse<T>(mockResult);

            var result = await controllerActionResponse(mockResponse);

            var actionResult = Assert.IsType<ActionResult<ServiceResponse<T>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(mockResponse, okResult.Value);
        }

        protected void Fact_BaseGetAll_Success(List<TEntity> mockResult)
        {
            SuccessTest(mockResult, async (mockResponse) =>
            {
                _mockService.Setup(x => x.GetAll()).ReturnsAsync(mockResponse);
                return await _controller.Get();
            });
        }

        protected void Fact_BaseGetById_Success(TEntity mockResult)
        {
            var id = new Guid();
            SuccessTest(mockResult, async (mockResponse) =>
            {
                _mockService.Setup(x => x.GetById(id, false)).ReturnsAsync(mockResponse);
                return await _controller.GetById(id, false);
            });
        }
    }
}