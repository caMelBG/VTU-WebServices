using System.Collections.Generic;

namespace Models.DtoModels
{
    public class CourseDto
    {
        public int CourseID { get; set; }

        public string Title { get; set; }

        public int Credits { get; set; }

        public virtual ICollection<EnrollmentDto> Enrollments { get; set; }
    }
}
