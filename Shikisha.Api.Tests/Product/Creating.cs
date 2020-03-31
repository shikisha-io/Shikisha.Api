using System;
using Models = Shikisha.DataAccess.DomainModels;
using Xunit;

namespace Shikisha.Tests.Product
{
    /// <summary>
    /// Tests focused on logic around creating a Product entity within the data context.
    /// </summary>
    public class ProductCreatingTests : DataAccessTestBase
    {        
        [Fact]
        public void Fact_CreatingBaseEntity_ShouldAutoGenerateFields()
        {
            // Arrange
            var product = new Models.Product("Test Product", "A product for testing things out.");

            // Act
            dbContext.Products.Add(product);
            dbContext.SaveChanges();

            // Assert
            Assert.NotNull(product.InsertedUtc);
            Assert.NotNull(product.UpdatedUtc);
            Assert.True(product.InsertedUtc >= DateTime.UtcNow.AddMinutes(-5));
            Assert.True(product.UpdatedUtc >= DateTime.UtcNow.AddMinutes(-5));
            Assert.True(product.Id != default(Guid));
        }
    }
}
