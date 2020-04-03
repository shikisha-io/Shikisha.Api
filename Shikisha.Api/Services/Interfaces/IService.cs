using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shikisha.DataAccess;

namespace Shikisha.Services.Interfaces
{
    public interface IService<TEntity> where TEntity : EntityBase
    {
        Task<TEntity> GetById(Guid id, bool includeSubCollections = false);
        Task<IList<TEntity>> GetAll();
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(Guid id, TEntity entity);
        void Delete(Guid id);
    }
}