using DataAccess.Models;
using System;
using System.Data.Entity;

namespace Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; }

        IRepository<Student> Students { get; }

        IRepository<Enrollment> Enrollments { get; }

        IRepository<Course> Courses { get; }

        int SaveChanges();
    }
}
