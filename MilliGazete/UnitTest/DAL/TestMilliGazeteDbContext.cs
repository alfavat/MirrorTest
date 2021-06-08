using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace UnitTest.DAL
{
    public class TestMilliGazeteDbContext
    {
        public readonly MilliGazeteDbContext db;
        public static int dataCount = 12;
        public TestMilliGazeteDbContext()
        {
            if (db == null)
            {
                db = GetDbContext();
            }
        }
        public MilliGazeteDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<MilliGazeteDbContext>()
            .UseInMemoryDatabase(databaseName: "MilliGazeteDatabase")
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

            var context = new MilliGazeteDbContext(options);

            return context;
        }
    }
}
