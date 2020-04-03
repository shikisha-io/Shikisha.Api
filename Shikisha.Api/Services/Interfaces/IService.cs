using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shikisha.DataAccess;
using Shikisha.Utilities;

namespace Shikisha.Services.Interfaces
{
    public interface IService<TEntity> where TEntity : EntityBase
    {
        Task<ServiceResponse<TEntity>> GetById(Guid id, bool includeSubCollections = false);
        Task<ServiceResponse<List<TEntity>>> GetAll();
        Task<ServiceResponse<TEntity>> Add(TEntity entity);
        Task<ServiceResponse<TEntity>> Update(Guid id, TEntity entity);
        void Delete(Guid id);
    }
}