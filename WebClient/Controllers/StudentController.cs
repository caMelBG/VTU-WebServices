using Models;
using Models.DtoModels;
using NLog;
using Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebClient.Controllers
{
    public class StudentController : ApiController
    {
        private IUnitOfWork _db;
        private ILogger _logger = LogManager.GetCurrentClassLogger();

        public StudentController(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }

        [HttpGet]
        public IEnumerable<StudentDto> Get()
        {
            var students = _db.Students.All().ToList().Select(x => ConvertToStudentDto(x));

            return students;
        }

        [HttpGet]
        public StudentDto Get(int id)
        {
            var student = _db.Students.All().FirstOrDefault(x => x.StudentID == id);
            if (student == null)
            {
                _logger.Error("Student with Id:{0} dosent exist", id);
            }

            return ConvertToStudentDto(student);
        }

        [HttpPost]
        public void Post([FromBody]StudentDto dto)
        {
            var student = ConvertToStudent(dto);
            _db.Students.Add(student);
            _db.SaveChanges();

            _logger.Info("Student with first name {0} was created", dto.FirstMidName);
        }

        [HttpPut]
        public void Put(int id, [FromBody]StudentDto dto)
        {
            var student = _db.Students.All().FirstOrDefault(x => x.StudentID == id);
            if (student == null)
            {
                _logger.Error("Student with Id:{0} dosent exist", id);
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
            _logger.Info("Delete student with id:{0} - Start", id);
            var student = _db.Students.All().FirstOrDefault(x => x.StudentID == id);
            if (student == null)
            {
                _logger.Error("Student with Id:{0} dosent exist", id);
            }

            _db.Students.Delete(id);
            _db.SaveChanges();

            _logger.Info("Student with Id:{0} has been successfuly deleted", id);
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
