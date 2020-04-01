using System;
using Xunit;
using Shikisha.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Shikisha.Tests.BaseEntity
{
    public class BaseEntityTests<TEntity> : DataAccessTestBase
    where TEntity : EntityBase
    {      
        public void Fact_CreatingBaseEntity_ShouldAutoGenerateFields(TEntity entity, DbSet<TEntity> dbSet)
        {
            // Arrange

            // Act
            dbSet.Add(entity);
            dbContext.SaveChanges();

            // Assert
            Assert.NotNull(entity.InsertedUtc);
            Assert.NotNull(entity.UpdatedUtc);
            Assert.True(entity.InsertedUtc >= DateTime.UtcNow.AddMinutes(-5));
            Assert.True(entity.UpdatedUtc >= DateTime.UtcNow.AddMinutes(-5));
            Assert.True(entity.Id != default(Guid));
        }

        public void Fact_UpdatingBaseEntity_ShouldAutoGenerateFields(TEntity entity, DbSet<TEntity> dbSet, Action<TEntity> updateAction)
        {
            // Arrange
            dbSet.Add(entity);
            dbContext.SaveChanges();
            var initialInsertTimeStamp = entity.InsertedUtc;
            var initialUpdateTimeStamp = entity.UpdatedUtc;
            var initialId = entity.Id;

            // Act
            updateAction(entity);
            dbSet.Update(entity);
            dbContext.SaveChanges();

            // Assert
            Assert.Equal(initialId, entity.Id);
            Assert.Equal(initialInsertTimeStamp, entity.InsertedUtc);
            Assert.True(entity.UpdatedUtc > initialUpdateTimeStamp);
        }
    }
}
