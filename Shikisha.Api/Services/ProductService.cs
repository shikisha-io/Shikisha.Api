using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Shikisha.DataAccess;
using Shikisha.DataAccess.DomainModels;
using Shikisha.DataAccess.Validation;
using Shikisha.Utilities;

namespace Shikisha.Services
{
    public sealed class ProductService : ServiceBase<Product>
    {
        public ProductService(ShikishaDataContext dbContext) : base(dbContext, new ValidatorBase<Product>())
        {
        }

        protected override DbSet<Product> _dbSet => _dbContext.Products;

        protected override IQueryable<Product> _dbSetWithSubCollections => _dbSet.Include(x => x.Projects);
    }
}