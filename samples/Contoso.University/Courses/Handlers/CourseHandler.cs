using Contoso.University.Courses.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Zek.Model;

namespace Contoso.University.Courses.Handlers
{
    public class CourseHandler : IRequestHandler<RegisterCourse, Result<Course>>
    {
        public Task<Result<Course>> Handle(RegisterCourse request, CancellationToken cancellationToken)
        {
            var course = Course.From(request);

            return Task.FromResult(Result.Ok(course));
        }
    }
}
