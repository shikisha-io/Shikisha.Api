using System;
using System.Collections.Generic;
using System.Linq;
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

        protected readonly TEntity _mockObject;
        protected readonly List<TEntity> _mockObjectList;

        protected readonly Guid _mockId = new Guid();
        protected ControllerBaseTests(Func<IService<TEntity>, ApiControllerBase<TEntity>> controllerConstructorFunc, Func<TEntity> generateMockObject, List<TEntity> mockObjectList = null)
        {
            _mockService = new Mock<IService<TEntity>>();
            _controller = controllerConstructorFunc(_mockService.Object);

            _mockObject = generateMockObject();
            _mockObjectList = mockObjectList ?? Enumerable.Range(0, 5).Select(x => generateMockObject()).ToList();
        }

        protected async Task<ActionResult<ServiceResponse<T>>> BaseTest<T, TObjectResult>(ServiceResponse<T> mockResponse,
        int expectedStatusCode,
        Expression<Func<IService<TEntity>, Task<ServiceResponse<T>>>> methodToCall,
        Func<ApiControllerBase<TEntity>, Task<ActionResult<ServiceResponse<T>>>> controllerAction)
        where TObjectResult : ObjectResult
        {
            _mockService.Setup(methodToCall).ReturnsAsync(mockResponse);

            var result = await controllerAction(_controller);

            var actionResult = Assert.IsType<ActionResult<ServiceResponse<T>>>(result);
            _mockService.Verify(methodToCall, Times.Once);
            var objectResult = Assert.IsType<TObjectResult>(result.Result);
            Assert.Equal(expectedStatusCode, objectResult.StatusCode);
            Assert.Equal(mockResponse, objectResult.Value);
            return result;
        }

        #region Success Tests
        protected async Task<ActionResult<ServiceResponse<T>>> SuccessTest<T>(T mockResult,
        Expression<Func<IService<TEntity>, Task<ServiceResponse<T>>>> methodToCall,
        Func<ApiControllerBase<TEntity>, Task<ActionResult<ServiceResponse<T>>>> controllerAction)
            => await BaseTest<T, OkObjectResult>(new ServiceResponse<T>(mockResult), 200, methodToCall, controllerAction);

        [Fact]
        public async Task<ActionResult<ServiceResponse<List<TEntity>>>> Fact_BaseGetAll_Success()
            => await SuccessTest(_mockObjectList, service => service.GetAll(), controller => controller.Get());

        [Fact]
        public async Task<ActionResult<ServiceResponse<TEntity>>> Fact_BaseGetById_Success()
            => await SuccessTest(_mockObject, service => service.GetById(_mockId, false), controller => controller.GetById(_mockId, false));

        [Fact]
        public async Task<ActionResult<ServiceResponse<TEntity>>> Fact_BaseAdd_Success()
            => await SuccessTest(_mockObject, service => service.Add(_mockObject), controller => controller.Add(_mockObject));

        [Fact]
        public async Task<ActionResult<ServiceResponse<TEntity>>> Fact_BaseUpdate_Success()
            => await SuccessTest(_mockObject, service => service.Update(_mockId, _mockObject), controller => controller.Update(_mockId, _mockObject));

        #endregion Success Tests

        #region Failure Tests
        protected async Task<ActionResult<ServiceResponse<T>>> FailureTest<T>(T mockResult,
        Expression<Func<IService<TEntity>, Task<ServiceResponse<T>>>> methodToCall,
        Func<ApiControllerBase<TEntity>, Task<ActionResult<ServiceResponse<T>>>> controllerAction)
            => await BaseTest<T, BadRequestObjectResult>(new ServiceResponse<T>(new List<ServiceError>(){new ServiceError("Test Error","Test Error")}),
            400, methodToCall, controllerAction);

        [Fact]
        public async Task<ActionResult<ServiceResponse<List<TEntity>>>> Fact_BaseGetAll_Failure()
            => await FailureTest(_mockObjectList, service => service.GetAll(), controller => controller.Get());

        [Fact]
        public async Task<ActionResult<ServiceResponse<TEntity>>> Fact_BaseGetById_Failure()
            => await FailureTest(_mockObject, service => service.GetById(_mockId, false), controller => controller.GetById(_mockId, false));

        [Fact]
        public async Task<ActionResult<ServiceResponse<TEntity>>> Fact_BaseAdd_Failure()
            => await FailureTest(_mockObject, service => service.Add(_mockObject), controller => controller.Add(_mockObject));

        [Fact]
        public async Task<ActionResult<ServiceResponse<TEntity>>> Fact_BaseUpdate_Failure()
            => await FailureTest(_mockObject, service => service.Update(_mockId, _mockObject), controller => controller.Update(_mockId, _mockObject));
        #endregion Failure Tests
    }
}