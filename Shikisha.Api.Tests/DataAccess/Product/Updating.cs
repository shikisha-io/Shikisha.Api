using Models = Shikisha.DataAccess.DomainModels;
using Xunit;
using Shikisha.Tests.DataAccess.EntityBase;

namespace Shikisha.Tests.DataAccess.Product
{
    /// <summary>
    /// Tests focused on logic around updating a Product entity within the data context.
    /// </summary>
    public sealed class Updating : EntityBaseTests<Models.Product>
    {        
        [Fact]
        public void Fact_UpdatingProduct_ShouldAutoGenerateFields() 
            => Fact_UpdatingEntityBase_ShouldAutoGenerateFields(
                new Models.Product("Test Product", "A product for testing things out."),
                dbContext.Products,
                entity => entity.Name = "Updated Tests Product"
            );
    }
}
