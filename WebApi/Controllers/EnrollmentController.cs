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
    public class EnrollmentController : BaseController
    {
        public EnrollmentController(IUnitOfWork unitOfWork) : base(unitOfWork, LogManager.GetCurrentClassLogger())
        {
        }

        /// <summary>
        /// Gets all Enrollments from the database
        /// </summary>
        /// <returns>All Enrollment</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        [HttpGet]
        public IEnumerable<EnrollmentDto> Get()
        {
            var Enrollments = _db.Enrollments.All().ToList().Select(x => ConvertToEnrollmentDto(x));

            return Enrollments;
        }

        /// <summary>
        /// Gets a single Enrollment object from the database
        /// </summary>
        /// <param name="id">The id of the Enrollment object</param>
        /// <returns>The found Enrollment</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        /// <response coed="404">NotFound</response>
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var Enrollment = _db.Enrollments.All().FirstOrDefault(x => x.EnrollmentID == id);
            if (Enrollment == null)
            {
                var errorMessage = string.Format("Enrollment with Id:{0} dosent exist", id);
                _logger.Error(errorMessage);
                return BadRequest(errorMessage);
            }

            return Ok(ConvertToEnrollmentDto(Enrollment));
        }

        /// <summary>
        /// Creates new Enrollment
        /// </summary>
        /// <param name="dto">The Enrollment object to create</param>
        /// <returns>The created Enrollment</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        [HttpPost]
        public IHttpActionResult Post([FromBody]EnrollmentDto dto)
        {
            try
            {
                var Enrollment = ConvertToEnrollment(dto);
                _db.Enrollments.Add(Enrollment);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            string message = string.Format("Enrollment with grade {0} was created", dto.Grade);
            _logger.Info(message);
            return Ok(message);
        }

        /// <summary>
        /// Updates an existing Enrollment
        /// </summary>
        /// <param name="id">The id of the Enrollment to be updated</param>
        /// <param name="dto">The Enrollment object containing the update data</param>
        /// <returns>Status code 204 or corresponding error code</returns>
        /// <response code="204">NoContent</response>
        /// <response code="400">BadRequest</response>
        /// <response code="404">NotFound</response>
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]EnrollmentDto dto)
        {
            var Enrollment = _db.Enrollments.All().FirstOrDefault(x => x.EnrollmentID == id);
            if (Enrollment == null)
            {
                var errorMessage = string.Format("Enrollment with Id:{0} dosent exist", id);
                _logger.Error(errorMessage);
                NotFound();
            }

            try
            {
                Enrollment.StudentID = dto.StudentID;
                Enrollment.CourseID = dto.CourseID;
                Enrollment.Grade = dto.Grade;

                _db.Enrollments.Update(Enrollment);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            string message = string.Format("Enrollment with id {0} was updated", id);
            _logger.Info(message);
            return Ok(message);
        }

        /// <summary>
        /// Deletes a Enrollment
        /// </summary>
        /// <param name="id">The id of the Enrollment to delete</param>
        /// <returns>Status code 204 or corresponding error code</returns>
        /// <response code="204">NoContent</response>
        /// <response code="400">BadRequest</response>
        /// <response code="404">NotFound</response>
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            _logger.Info("Delete Enrollment with id:{0} - Start", id);
            var Enrollment = _db.Enrollments.All().FirstOrDefault(x => x.EnrollmentID == id);
            if (Enrollment == null)
            {
                _logger.Error("Enrollment with Id:{0} dosent exist", id);
                return NotFound();
            }

            try
            {
                _db.Enrollments.Delete(id);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            var message = string.Format("Enrollment with Id:{0} has been successfuly deleted", id);
            _logger.Info(message);
            return Ok(message);
        }

        private Enrollment ConvertToEnrollment(EnrollmentDto enrollment)
        {
            return new Enrollment()
            {
                EnrollmentID = enrollment.EnrollmentID,
                StudentID = enrollment.StudentID,
                CourseID = enrollment.CourseID,
                Grade = enrollment.Grade,
            };
        }

        private EnrollmentDto ConvertToEnrollmentDto(Enrollment enrollment)
        {
            return new EnrollmentDto()
            {
                EnrollmentID = enrollment.EnrollmentID,
                StudentID = enrollment.StudentID,
                CourseID = enrollment.CourseID,
                Grade = enrollment.Grade,
            };
        }
    }
}
