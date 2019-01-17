namespace Models.DtoModels
{
    public class EnrollmentDto
    {
        public int EnrollmentID { get; set; }

        public int CourseID { get; set; }

        public int StudentID { get; set; }

        public Grade? Grade { get; set; }

        public virtual CourseDto Course { get; set; }

        public virtual StudentDto Student { get; set; }
    }
}
