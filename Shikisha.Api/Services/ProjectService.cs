using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Shikisha.DataAccess;
using Shikisha.DataAccess.DomainModels;
using Shikisha.DataAccess.Validation;

namespace Shikisha.Services
{
    public sealed class ProjectService : ServiceBase<Project>
    {
        public ProjectService(ShikishaDataContext dbContext) : base(dbContext, new ValidatorBase<Project>())
        {
        }

        protected override DbSet<Project> _dbSet => _dbContext.Projects;
    }
}