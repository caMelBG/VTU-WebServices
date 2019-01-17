using Models;
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
        public IEnumerable<Student> Get()
        {
            var students = _db.Students.All();
            return students;
        }

        [HttpGet]
        public Student Get(int id)
        {
            var student = _db.Students.All().FirstOrDefault(x => x.StudentID == id);

            return student;
        }

        [HttpPost]
        public void Post([FromBody]Student value)
        {
        }

        [HttpPut]
        public void Put(int id, [FromBody]Student value)
        {
        }

        [HttpDelete]
        public void Delete(int id)
        {
        }
    }
}
