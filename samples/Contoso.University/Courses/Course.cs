using System;
using Contoso.University.Courses.Commands;
using Zek.Model;

namespace Contoso.University.Courses
{
    public class Course
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTimeOffset RegisteredAt { get; private set; }

        public static Course From(RegisterCourse request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return new Course
            {
                Id = new Random().Next(),
                Title = request.Title,
                Description = request.Title,
                RegisteredAt = Current.Now
            };
        }
    }
}
