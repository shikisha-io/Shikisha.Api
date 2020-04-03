using System;
using Models = Shikisha.DataAccess.DomainModels;
using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace Shikisha.Tests.DataAccess.Project
{
    /// <summary>
    /// Tests focused on logic around creating a Project entity within the data context.
    /// </summary>
    public sealed class Creation : EntityBaseTests<Models.Project>
    {
        [Fact]
        public void Fact_CreatingProduct_ShouldAutoGenerateFields()
            => Fact_CreatingEntityBase_ShouldAutoGenerateFields(
                new Models.Project("Test Project", "A project for testing things out."),
                dbContext.Projects
            );
    }
}
