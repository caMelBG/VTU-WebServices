using Models.Converters.Interface;
using Models.DtoModels;

namespace Models.Converters
{
    public class CourseConverter : IModelConverter<Course, CourseDto>
    {
        public Course ConvertTo(CourseDto course)
        {
            return new Course()
            {
                CourseID = course.CourseID,
                Title = course.Title,
                Credits = course.Credits,
            };
        }

        public CourseDto ConvertTo(Course course)
        {
            return new CourseDto()
            {
                CourseID = course.CourseID,
                Title = course.Title,
                Credits = course.Credits,
            };
        }
    }
}
