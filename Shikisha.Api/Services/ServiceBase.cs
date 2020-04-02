using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Shikisha.DataAccess;
using Shikisha.Services.Interfaces;

namespace Shikisha.Services
{
    public abstract class ServiceBase<TEntity> : IService<TEntity>
    where TEntity : EntityBase
    {
        protected readonly ShikishaDataContext _dbContext;
        protected abstract DbSet<TEntity> _dbSet {get;}
        private readonly AbstractValidator<TEntity> _validator;
        public ServiceBase(ShikishaDataContext dbContext, AbstractValidator<TEntity> validator) => (_dbContext, _validator) = (dbContext, validator);
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<TEntity>> GetAll() => await _dbSet.ToListAsync();

        public async Task<TEntity> GetById(Guid id) => await _dbSet.FindAsync(id);

        public async Task<TEntity> Add(TEntity entity)
        {
            await _validator.ValidateAndThrowAsync(entity);
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<TEntity> Update(Guid id, TEntity entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}