using System;
using Models = Shikisha.DataAccess.DomainModels;
using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace Shikisha.Tests.DataAccess.Product
{
    /// <summary>
    /// Tests focused on logic around creating a Product entity within the data context.
    /// </summary>
    public sealed class Creation : EntityBaseTests<Models.Product>
    {
        [Fact]
        public void Fact_CreatingProduct_ShouldAutoGenerateFields()
            => Fact_CreatingEntityBase_ShouldAutoGenerateFields(
                new Models.Product("Test Product", "A product for testing things out."),
                dbContext.Products
            );

        [Fact]
        public void Fact_CreatingProduct_WithProject()
        {
            var createdEntity = this.Fact_CreatingEntityBase_ShouldAutoGenerateFields(
                new Models.Product("Test Product", "A product for testing things out.")
                {
                    Projects = new List<Models.Project> { new Models.Project("Web App", "A new web application") }
                },
                dbContext.Products
                );
            var project = createdEntity.Projects.FirstOrDefault();
            Assert.NotNull(project);
            EntityBaseCreationAssertions(project);
        }
    }
}
