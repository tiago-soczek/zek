using AutoMapper;
using Contoso.University.Courses.Api.Dtos;
using Contoso.University.Courses.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zek.Api;

namespace Contoso.University.Courses.Api
{
    [Route(RouteConstants.Controller)]
    public class CoursesController : BaseController
    {
        public CoursesController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpPost]
        public async Task<ActionResult<CourseDto>> Register(RegisterCourse cmd)
        {
            var result = await Mediator.Send(cmd);

            return As<CourseDto>(result);
        }

        [HttpGet]
        public ActionResult<CourseDto[]> GetAll()
        {
            var courses = new[]
            {
                new CourseDto { Id = 1, Title = "Test #1" },
                new CourseDto { Id = 2, Title = "Test #2" }
            };

            return Ok(courses);
        }
    }
}
