using System;
using Models = Shikisha.DataAccess.DomainModels;
using Xunit;
using Shikisha.Tests.BaseEntity;
using Microsoft.EntityFrameworkCore;

namespace Shikisha.Tests.Product
{
    /// <summary>
    /// Tests focused on logic around creating a Product entity within the data context.
    /// </summary>
    public class Creation : BaseEntityTests<Models.Product>
    {
        [Fact]
        public void Fact_CreatingProduct_ShouldAutoGenerateFields() 
            => Fact_CreatingBaseEntity_ShouldAutoGenerateFields(
                new Models.Product("Test Product", "A product for testing things out."),
                dbContext.Products
            );
    }
}
