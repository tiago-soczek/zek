using Contoso.University.Courses.Commands;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Contoso.University.IntegrationTests.Course
{
    public class CourseApiTests : BaseTest
    {
        public CourseApiTests(IntegrationTestFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task Register_EmptyPayload()
        {
            var result = await Client.PostAsJsonAsync("/courses", new RegisterCourse());

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task Register_ValidInfo()
        {
            var result = await Client.PostAsJsonAsync("/courses", new RegisterCourse
            {
                Title = "CQRS",
                Description = "CQRS Course"
            });

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
