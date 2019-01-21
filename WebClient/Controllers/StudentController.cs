using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using NLog;

using Models;
using Models.DtoModels;
using Repositories.Interfaces;

namespace WebClient.Controllers
{
    public class StudentController : BaseController
    {
        public StudentController(IUnitOfWork unitOfWork) : base(unitOfWork, LogManager.GetCurrentClassLogger())
        {
        }

        /// <summary>
        /// Gets all students from the database
        /// </summary>
        /// <returns>All student</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        [HttpGet]
        public IEnumerable<StudentDto> Get()
        {
            var students = _db.Students.All().ToList().Select(x => ConvertToStudentDto(x));

            return students;
        }

        /// <summary>
        /// Gets a single student object from the database
        /// </summary>
        /// <param name="id">The id of the student object</param>
        /// <returns>The found student</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        /// <response coed="404">NotFound</response>
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var student = _db.Students.All().FirstOrDefault(x => x.StudentID == id);
            if (student == null)
            {
                var errorMessage = string.Format("Student with Id:{0} dosent exist", id);
                _logger.Error(errorMessage);
                return BadRequest(errorMessage);
            }

            return Ok(ConvertToStudentDto(student));
        }

        /// <summary>
        /// Creates new student
        /// </summary>
        /// <param name="student">The student object to create</param>
        /// <returns>The created student</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        [HttpPost]
        public IHttpActionResult Post([FromBody]StudentDto dto)
        {
            try
            {
                var student = ConvertToStudent(dto);
                _db.Students.Add(student);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            string message = string.Format("Student with first name {0} was created", dto.FirstMidName);
            _logger.Info(message);
            return Ok(message);
        }

        /// <summary>
        /// Updates an existing student
        /// </summary>
        /// <param name="id">The id of the student to be updated</param>
        /// <param name="student">The student object containing the update data</param>
        /// <returns>Status code 204 or corresponding error code</returns>
        /// <response code="204">NoContent</response>
        /// <response code="400">BadRequest</response>
        /// <response code="404">NotFound</response>
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]StudentDto dto)
        {
            var student = _db.Students.All().FirstOrDefault(x => x.StudentID == id);
            if (student == null)
            {
                var errorMessage = string.Format("Student with Id:{0} dosent exist", id);
                _logger.Error(errorMessage);
                NotFound();
            }

            try
            {
                student.FirstMidName = dto.FirstMidName;
                student.LastName = dto.LastName;
                student.EnrollmentDate = dto.EnrollmentDate;

                _db.Students.Update(student);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            string message = string.Format("Student with id {0} was updated", id);
            _logger.Info(message);
            return Ok(message);
        }

        /// <summary>
        /// Deletes a student
        /// </summary>
        /// <param name="id">The id of the student to delete</param>
        /// <returns>Status code 204 or corresponding error code</returns>
        /// <response code="204">NoContent</response>
        /// <response code="400">BadRequest</response>
        /// <response code="404">NotFound</response>
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            _logger.Info("Delete student with id:{0} - Start", id);
            var student = _db.Students.All().FirstOrDefault(x => x.StudentID == id);
            if (student == null)
            {
                _logger.Error("Student with Id:{0} dosent exist", id);
                return NotFound();
            }

            try
            {
                _db.Students.Delete(id);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            var message = string.Format("Student with Id:{0} has been successfuly deleted", id);
            _logger.Info(message);
            return Ok(message);
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
