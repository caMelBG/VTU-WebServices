using System;
using System.Collections.Generic;

namespace Models.DtoModels
{
    public class StudentDto
    {
        public int StudentID { get; set; }

        public string LastName { get; set; }

        public string FirstMidName { get; set; }

        public DateTime EnrollmentDate { get; set; }
    }
}
