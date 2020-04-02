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
    public sealed class ProductService : ServiceBase<Product>
    {
        public ProductService(ShikishaDataContext dbContext) : base(dbContext, new ValidatorBase<Product>())
        {
        }

        protected override DbSet<Product> _dbSet => _dbContext.Products;
    }
}