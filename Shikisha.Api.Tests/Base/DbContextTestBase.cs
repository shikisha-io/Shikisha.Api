using Shikisha.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Shikisha.Tests
{
    public class DataAccessTestBase
    {
        protected ShikishaDataContext dbContext;
        public DataAccessTestBase(ShikishaDataContext dbContext = null) => this.dbContext = dbContext ?? GetInMemoryDBContext();

        protected ShikishaDataContext GetInMemoryDBContext()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<ShikishaDataContext>();
            var options = builder.UseInMemoryDatabase("TestLibDb").UseInternalServiceProvider(serviceProvider).Options;

            ShikishaDataContext dbContext = new ShikishaDataContext(options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            return dbContext;
        }
    }
}