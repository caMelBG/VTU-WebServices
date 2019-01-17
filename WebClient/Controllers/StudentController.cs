﻿using Models;
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
        public void Post([FromBody]StudentDto dto)
        {
            var student = ConvertToStudent(dto);
            _db.Students.Add(student);
            _db.SaveChanges();

            _logger.Info("Student with first name {0} was created", dto.FirstMidName);
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
                BadRequest(errorMessage);
            }
            
            student.FirstMidName = dto.FirstMidName;
            student.LastName = dto.LastName;
            student.EnrollmentDate = dto.EnrollmentDate;
            
            _db.Students.Update(student);
            _db.SaveChanges();

            _logger.Info("Student with id {0} was updated", id);
            return Ok();
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
