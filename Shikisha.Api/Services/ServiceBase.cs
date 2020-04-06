using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Shikisha.DataAccess;
using Shikisha.Services.Interfaces;
using Shikisha.Utilities;

namespace Shikisha.Services
{
    public abstract class ServiceBase<TEntity> : IService<TEntity>, IDisposable, IAsyncDisposable
    where TEntity : EntityBase
    {
        protected readonly ShikishaDataContext _dbContext;
        protected abstract DbSet<TEntity> _dbSet { get; }
        protected abstract IQueryable<TEntity> _dbSetWithSubCollections { get; }
        private readonly AbstractValidator<TEntity> _validator;
        public ServiceBase(ShikishaDataContext dbContext, AbstractValidator<TEntity> validator) => (_dbContext, _validator) = (dbContext, validator);

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<T>> ServiceAction<T>(Func<Task<T>> function)
        {
            var response = new ServiceResponse<T>();
            try
            {
                response.Data = await function();
            }
            catch(ValidationException ex)
            {
                response.Errors = ex.Errors
                    .Select(validationError => new ServiceError(validationError.ErrorCode, validationError.ErrorMessage))
                    .ToList();
            }
            catch (System.Exception ex)
            {
                response.Errors = new List<ServiceError>{new ServiceError("500", ex.Message)};
            }
            return response;
        }

        public async Task<ServiceResponse<List<TEntity>>> GetAll() => await ServiceAction(async () => await _dbSet.AsNoTracking().ToListAsync());

        public async Task<ServiceResponse<TEntity>> GetById(Guid id, bool includeSubCollections = false)
            => await ServiceAction(async () => 
                await (includeSubCollections ? _dbSetWithSubCollections.AsNoTracking() : _dbSet)
                .FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception($"Specified Entity {id} does not exist."));

        public async Task<ServiceResponse<TEntity>> Add(TEntity entity)
            => await ServiceAction(async () =>
            {
                await _validator.ValidateAndThrowAsync(entity);
                await _dbSet.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            });
        public async Task<ServiceResponse<TEntity>> Update(Guid id, TEntity entity)
            => await ServiceAction(async () =>
            {
                _dbSet.Update(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            });

        public void Dispose()
        {
            this._dbContext.Dispose();
        }
        ValueTask IAsyncDisposable.DisposeAsync()
        {
            return _dbContext.DisposeAsync();
        }
    }
}