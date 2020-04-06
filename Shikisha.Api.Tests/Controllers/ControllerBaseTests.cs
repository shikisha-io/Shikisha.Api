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

        protected async Task<ActionResult<ServiceResponse<T>>> SuccessTest<T>(T mockResult,
        Expression<Func<IService<TEntity>, Task<ServiceResponse<T>>>> methodToCall,
        Func<ApiControllerBase<TEntity>, Task<ActionResult<ServiceResponse<T>>>> controllerAction)
        {
            var mockResponse = new ServiceResponse<T>(mockResult);
            
            _mockService.Setup(methodToCall).ReturnsAsync(mockResponse);

            var result = await controllerAction(_controller);

            var actionResult = Assert.IsType<ActionResult<ServiceResponse<T>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(mockResponse, okResult.Value);
            _mockService.Verify(methodToCall, Times.Once);
            return result;
        }

        protected async Task<ActionResult<ServiceResponse<List<TEntity>>>> Fact_BaseGetAll_Success(List<TEntity> mockResult)
        {
            return await SuccessTest(mockResult, service => service.GetAll(), controller => controller.Get());
            // return await SuccessTest(mockResult, async (mockResponse) =>
            // {
            //     _mockService.Setup(x => x.GetAll()).ReturnsAsync(mockResponse);
            //     return await _controller.Get();
            // }, service => service.GetAll(), controller => controller.Get(), service => service.GetAll());
        }

        protected async Task<ActionResult<ServiceResponse<TEntity>>> Fact_BaseGetById_Success(TEntity mockResult)
        {
            var id = new Guid();
            return await SuccessTest(mockResult, service => service.GetById(id, false), controller => controller.GetById(id, false));
            // return await SuccessTest(mockResult, async (mockResponse) =>
            // {
            //     _mockService.Setup(x => x.GetById(id, false)).ReturnsAsync(mockResponse);
            //     return await _controller.GetById(id, false);
            // }, x => x.GetById(id, false));
        }

        protected async Task<ActionResult<ServiceResponse<TEntity>>> Fact_BaseAdd_Success(TEntity mockResult)
        {
            return await SuccessTest(mockResult, service => service.Add(mockResult), controller => controller.Add(mockResult));
            // var response = await SuccessTest(mockResult, async (mockResponse) =>
            // {
            //     _mockService.Setup(x => x.Add(mockResult)).ReturnsAsync(mockResponse);
            //     return await _controller.Add(mockResult);
            // });

            // return response;
        }
    }
}