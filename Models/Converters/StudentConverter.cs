using Models.Converters.Interface;
using Models.DtoModels;

namespace Models.Converters
{
    public class StudentConverter : IModelConverter<Student, StudentDto>
    {
        public Student ConvertTo(StudentDto student)
        {
            return new Student()
            {
                StudentID = student.StudentID,
                FirstMidName = student.FirstMidName,
                LastName = student.LastName,
                EnrollmentDate = student.EnrollmentDate,
            };
        }

        public StudentDto ConvertTo(Student student)
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
