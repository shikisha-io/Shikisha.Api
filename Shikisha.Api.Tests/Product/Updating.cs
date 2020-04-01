using System;
using Models = Shikisha.DataAccess.DomainModels;
using Xunit;
using Shikisha.Tests.BaseEntity;

namespace Shikisha.Tests.Product
{
    /// <summary>
    /// Tests focused on logic around updating a Product entity within the data context.
    /// </summary>
    public class Updating : BaseEntityTests<Models.Product>
    {        
        [Fact]
        public void Fact_UpdatingProduct_ShouldAutoGenerateFields() 
            => Fact_UpdatingBaseEntity_ShouldAutoGenerateFields(
                new Models.Product("Test Product", "A product for testing things out."),
                dbContext.Products,
                entity => entity.Name = "Updated Tests Product"
            );
    }
}
