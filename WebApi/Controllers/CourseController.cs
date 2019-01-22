using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using NLog;

using Models;
using Models.DtoModels;
using Repositories.Interfaces;
using Models.Converters.Interface;

namespace WebClient.Controllers
{
    public class CourseController : BaseController
    {
        private IModelConverter<Course, CourseDto> _modelConverter;

        public CourseController(IUnitOfWork unitOfWork, IModelConverter<Course, CourseDto> modelConverter) 
            : base(unitOfWork, LogManager.GetCurrentClassLogger())
        {
            _modelConverter = modelConverter;
        }

        /// <summary>
        /// Gets all Courses from the database
        /// </summary>
        /// <returns>All Course</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        [HttpGet]
        public IEnumerable<CourseDto> Get()
        {
            var Courses = _db.Courses.All().ToList().Select(x => _modelConverter.ConvertTo(x));

            return Courses;
        }

        /// <summary>
        /// Gets a single Course object from the database
        /// </summary>
        /// <param name="id">The id of the Course object</param>
        /// <returns>The found Course</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        /// <response coed="404">NotFound</response>
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var Course = _db.Courses.All().FirstOrDefault(x => x.CourseID == id);
            if (Course == null)
            {
                var errorMessage = string.Format("Course with Id:{0} dosent exist", id);
                _logger.Error(errorMessage);
                return BadRequest(errorMessage);
            }

            return Ok(_modelConverter.ConvertTo(Course));
        }

        /// <summary>
        /// Creates new Course
        /// </summary>
        /// <param name="dto">The Course object to create</param>
        /// <returns>The created Course</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        [HttpPost]
        public IHttpActionResult Post([FromBody]CourseDto dto)
        {
            try
            {
                var Course = _modelConverter.ConvertTo(dto);
                _db.Courses.Add(Course);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            string message = string.Format("Course with title {0} was created", dto.Title);
            _logger.Info(message);
            return Ok(message);
        }

        /// <summary>
        /// Updates an existing Course
        /// </summary>
        /// <param name="id">The id of the Course to be updated</param>
        /// <param name="dto">The Course object containing the update data</param>
        /// <returns>Status code 204 or corresponding error code</returns>
        /// <response code="204">NoContent</response>
        /// <response code="400">BadRequest</response>
        /// <response code="404">NotFound</response>
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]CourseDto dto)
        {
            var Course = _db.Courses.All().FirstOrDefault(x => x.CourseID == id);
            if (Course == null)
            {
                var errorMessage = string.Format("Course with Id:{0} dosent exist", id);
                _logger.Error(errorMessage);
                NotFound();
            }

            try
            {
                Course.Title = dto.Title;
                Course.Credits = dto.Credits;

                _db.Courses.Update(Course);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            string message = string.Format("Course with id {0} was updated", id);
            _logger.Info(message);
            return Ok(message);
        }

        /// <summary>
        /// Deletes a Course
        /// </summary>
        /// <param name="id">The id of the Course to delete</param>
        /// <returns>Status code 204 or corresponding error code</returns>
        /// <response code="204">NoContent</response>
        /// <response code="400">BadRequest</response>
        /// <response code="404">NotFound</response>
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            _logger.Info("Delete Course with id:{0} - Start", id);
            var Course = _db.Courses.All().FirstOrDefault(x => x.CourseID == id);
            if (Course == null)
            {
                _logger.Error("Course with Id:{0} dosent exist", id);
                return NotFound();
            }

            try
            {
                _db.Courses.Delete(id);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            var message = string.Format("Course with Id:{0} has been successfuly deleted", id);
            _logger.Info(message);
            return Ok(message);
        }
    }
}
