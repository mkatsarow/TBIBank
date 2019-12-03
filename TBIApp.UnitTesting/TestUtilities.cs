using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TBIApp.Data;

namespace TBIApp.UnitTesting
{
    public static class TestUtilities
    {
        public static DbContextOptions<TBIAppDbContext> GetOptions(string databaseName)
        {
            return new DbContextOptionsBuilder<TBIAppDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
        }
    }
}
