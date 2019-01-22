using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;

using NLog;

using DataAccess;
using Models.Converters;
using Models.DtoModels;
using Repositories;
using Repositories.Interfaces;

namespace SOAP
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class StudentsWebService : WebService
    {
        private StudentConverter _converter;
        private IUnitOfWork _db;
        private ILogger _logger = LogManager.GetCurrentClassLogger();


        public StudentsWebService()
        {
            _converter = new StudentConverter();
            _db = new UnitOfWork(new UniversityContext());
        }

        [WebMethod]
        public IEnumerable<StudentDto> GetAll()
        {
            var students = _db.Students.All().ToList().Select(x => _converter.ConvertTo(x));

            return students;
        }

        [WebMethod]
        public StudentDto Get(int id)
        {
            var student = _db.Students.All().FirstOrDefault(x => x.StudentID == id);
            if (student == null)
            {
                var errorMessage = string.Format("Student with Id:{0} dosent exist", id);
                _logger.Error(errorMessage);
                throw new Exception(errorMessage);
            }

            return _converter.ConvertTo(student);
        }

        [WebMethod]
        public string Add(StudentDto dto)
        {
            var student = _converter.ConvertTo(dto);
            _db.Students.Add(student);
            _db.SaveChanges();

            string message = string.Format("Student with first name {0} was created", dto.FirstMidName);
            _logger.Info(message);
            return message;
        }

        [WebMethod]
        public string Edit(StudentDto dto)
        {
            var student = _db.Students.All().FirstOrDefault(x => x.StudentID == dto.StudentID);
            if (student == null)
            {
                var errorMessage = string.Format("Student with Id:{0} dosent exist", dto.StudentID);
                _logger.Error(errorMessage);
                throw new Exception(errorMessage);
            }

            student.FirstMidName = dto.FirstMidName;
            student.LastName = dto.LastName;
            student.EnrollmentDate = dto.EnrollmentDate;

            _db.Students.Update(student);
            _db.SaveChanges();

            string message = string.Format("Student with id {0} was updated", dto.StudentID);
            _logger.Info(message);
            return message;
        }

        [WebMethod]
        public string Delete(int id)
        {
            _logger.Info("Delete student with id:{0} - Start", id);
            var student = _db.Students.All().FirstOrDefault(x => x.StudentID == id);
            if (student == null)
            {
                var errorMessage = string.Format("Student with Id:{0} dosent exist", id);
                _logger.Error(errorMessage);
                throw new Exception(errorMessage);
            }

            _db.Students.Delete(id);
            _db.SaveChanges();

            var message = string.Format("Student with Id:{0} has been successfuly deleted", id);
            _logger.Info(message);
            return message;
        }
    }
}
