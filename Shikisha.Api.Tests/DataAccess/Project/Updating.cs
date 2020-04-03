using Models = Shikisha.DataAccess.DomainModels;
using Xunit;

namespace Shikisha.Tests.DataAccess.Project
{
    /// <summary>
    /// Tests focused on logic around updating a Project entity within the data context.
    /// </summary>
    public sealed class Updating : EntityBaseTests<Models.Project>
    {        
        [Fact]
        public void Fact_UpdatingProject_ShouldAutoGenerateFields() 
            => Fact_UpdatingEntityBase_ShouldAutoGenerateFields(
                new Models.Project("Test Project", "A project for testing things out."),
                dbContext.Projects,
                entity => entity.Name = "Updated Tests Project"
            );
    }
}
