namespace DataAccess.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<UniversityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(UniversityContext context)
        {
            var students = new List<Student>
            {
                new Student{StudentID=1,FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
                new Student{StudentID=2,FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{StudentID=3,FirstMidName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01")},
                new Student{StudentID=4,FirstMidName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{StudentID=5,FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{StudentID=6,FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
                new Student{StudentID=7,FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01")},
                new Student{StudentID=8,FirstMidName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01")}
            };

            students.ForEach(s => context.Students.AddOrUpdate(s));
            context.SaveChanges();

            var courses = new List<Course>
            {
                new Course{CourseID=1,Title="Chemistry",Credits=3,},
                new Course{CourseID=2,Title="Microeconomics",Credits=3,},
                new Course{CourseID=3,Title="Macroeconomics",Credits=3,},
                new Course{CourseID=4,Title="Calculus",Credits=4,},
                new Course{CourseID=5,Title="Trigonometry",Credits=4,},
                new Course{CourseID=6,Title="Composition",Credits=3,},
                new Course{CourseID=7,Title="Literature",Credits=4,}
            };
            courses.ForEach(s => context.Courses.AddOrUpdate(s));
            context.SaveChanges();

            var enrollments = new List<Enrollment>
            {
                new Enrollment{StudentID=1,CourseID=1,Grade=Grade.A},
                new Enrollment{StudentID=1,CourseID=2,Grade=Grade.C},
                new Enrollment{StudentID=1,CourseID=3,Grade=Grade.B},
                new Enrollment{StudentID=2,CourseID=1,Grade=Grade.B},
                new Enrollment{StudentID=2,CourseID=4,Grade=Grade.F},
                new Enrollment{StudentID=2,CourseID=2,Grade=Grade.F},
                new Enrollment{StudentID=3,CourseID=1},
                new Enrollment{StudentID=4,CourseID=1,},
                new Enrollment{StudentID=4,CourseID=3,Grade=Grade.F},
                new Enrollment{StudentID=5,CourseID=5,Grade=Grade.C},
                new Enrollment{StudentID=6,CourseID=6},
                new Enrollment{StudentID=7,CourseID=7,Grade=Grade.A},
            };
            enrollments.ForEach(s => context.Enrollments.AddOrUpdate(s));
            context.SaveChanges();
        }
    }
}
