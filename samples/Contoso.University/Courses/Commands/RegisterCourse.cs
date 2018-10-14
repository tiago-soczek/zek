using FluentValidation;
using MediatR;
using Zek.Model;

namespace Contoso.University.Courses.Commands
{
    public class RegisterCourse : IRequest<Result<Course>>
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public class Validator : AbstractValidator<RegisterCourse>
        {
            public Validator()
            {
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
            }
        }
    }
}
