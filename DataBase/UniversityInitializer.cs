using System.Data.Entity;

namespace DataAccess
{
    public class UniversityInitializer<TContext> : DropCreateDatabaseAlways<TContext>, IDatabaseInitializer<TContext>
        where TContext : UniversityContext
    {
        protected override void Seed(TContext context)
        {
        }
    }
}
