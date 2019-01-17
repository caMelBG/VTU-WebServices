using Models;
using Models.DtoModels;
using Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebClient.Controllers
{
    public class StudentController : ApiController
    {
        private IUnitOfWork _db;

        public StudentController(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }

        [HttpGet]
        public IEnumerable<StudentDto> Get()
        {
            var students = _db.Students.All().Select(x => ConvertToStudentDto(x));

            return students;
        }

        [HttpGet]
        public StudentDto Get(int id)
        {
            var student = _db.Students.All().FirstOrDefault(x => x.StudentID == id);
            if (student == null)
            {

            }

            return ConvertToStudentDto(student);
        }

        [HttpPost]
        public void Post([FromBody]StudentDto dto)
        {
            _db.Students.Add(ConvertToStudent(dto));
            _db.SaveChanges();
        }

        [HttpPut]
        public void Put(int id, [FromBody]StudentDto dto)
        {
            var student = _db.Students.All().FirstOrDefault(x => x.StudentID == id);
            if (student == null)
            {

            }

            if (dto.StudentID != id)
            {

            }

            student.FirstMidName = dto.FirstMidName;
            student.LastName = dto.LastName;
            student.EnrollmentDate = dto.EnrollmentDate;
            
            _db.Students.Update(student);
            _db.SaveChanges();
        }

        [HttpDelete]
        public void Delete(int id)
        {
            var student = _db.Students.All().FirstOrDefault(x => x.StudentID == id);
            if (student == null)
            {

            }

            _db.Students.Delete(id);
            _db.SaveChanges();
        }

        private Student ConvertToStudent(StudentDto student)
        {
            return new Student()
            {
                StudentID = student.StudentID,
                FirstMidName = student.FirstMidName,
                LastName = student.LastName,
                EnrollmentDate = student.EnrollmentDate,
            };
        }

        private StudentDto ConvertToStudentDto(Student student)
        {
            return new StudentDto()
            {
                StudentID = student.StudentID,
                FirstMidName = student.FirstMidName,
                LastName = student.LastName,
                EnrollmentDate = student.EnrollmentDate,
            };
        }
    }
}
