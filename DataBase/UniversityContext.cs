using Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace DataAccess
{
    public class UniversityContext : IdentityDbContext<User>
    {
        public UniversityContext() : base("UniversityContext", throwIfV1Schema: false)
        {
            Database.SetInitializer<UniversityContext>(new UniversityInitializer<UniversityContext>());
        }
        
        public DbSet<Student> Students { get; set; }

        public DbSet<Enrollment> Enrollments { get; set; }

        public DbSet<Course> Courses { get; set; }

        public static UniversityContext Create()
        {
            return new UniversityContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
