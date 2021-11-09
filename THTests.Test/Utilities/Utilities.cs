using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TH.Data;

namespace THTests.Test.Utilities
{
    public class Utilities
    {
        public static DbContextOptions<AppDBContext> TestDbContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase("InMemoryDb")
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }
}
