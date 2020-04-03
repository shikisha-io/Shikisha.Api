using System;
using Xunit;
using Dal = Shikisha.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Shikisha.Tests.DataAccess.EntityBase
{
    public abstract class EntityBaseTests<TEntity> : DataAccessTestBase
    where TEntity : Dal.EntityBase
    {      
        public void EntityBaseCreationAssertions<T>(T entity) where T : Dal.EntityBase
        {
            Assert.NotNull(entity.InsertedUtc);
            Assert.NotNull(entity.UpdatedUtc);
            Assert.True(entity.InsertedUtc >= DateTime.UtcNow.AddMinutes(-5));
            Assert.True(entity.UpdatedUtc >= DateTime.UtcNow.AddMinutes(-5));
            Assert.True(entity.Id != default(Guid));
        }
        public void EntityBaseUpdatingAssertions<T>(T entity, Guid initialId, DateTime initialInsertTimeStamp, DateTime initialUpdateTimeStamp) where T : Dal.EntityBase
        {
            Assert.Equal(initialId, entity.Id);
            Assert.Equal(initialInsertTimeStamp, entity.InsertedUtc);
            Assert.True(entity.UpdatedUtc > initialUpdateTimeStamp);
        }

        public TEntity Fact_CreatingEntityBase_ShouldAutoGenerateFields(TEntity entity, DbSet<TEntity> dbSet)
        {
            // Arrange

            // Act
            dbSet.Add(entity);
            dbContext.SaveChanges();

            // Assert
            EntityBaseCreationAssertions(entity);
            return entity;
        }

        public TEntity Fact_UpdatingEntityBase_ShouldAutoGenerateFields(TEntity entity, DbSet<TEntity> dbSet, Action<TEntity> updateAction)
        {
            // Arrange
            dbSet.Add(entity);
            dbContext.SaveChanges();
            var (initialId, initialInsertTimeStamp, initialUpdateTimeStamp) = (entity.Id, entity.InsertedUtc, entity.UpdatedUtc);

            // Act
            updateAction(entity);
            dbSet.Update(entity);
            dbContext.SaveChanges();

            // Assert
            EntityBaseUpdatingAssertions(entity, initialId, initialInsertTimeStamp, initialUpdateTimeStamp);
            return entity;
        }
    }
}
