using MediatR;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Net.Http;

namespace Contoso.University.IntegrationTests
{
    public abstract class BaseTest
    {
        protected BaseTest()
        {
            // More info about MVC Testing https://blogs.msdn.microsoft.com/webdev/2017/12/07/testing-asp-net-core-mvc-web-apps-in-memory/

            var contentRoot = @"..\..\..\..\Contoso.University";

            var hostBuilder = WebHost.CreateDefaultBuilder()
                .UseContentRoot(Path.GetFullPath(contentRoot))
                .UseEnvironment("Test")
                .UseStartup<Startup>();

            var server = new TestServer(hostBuilder);

            Host = server.Host;
            Client = server.CreateClient();
            Mediator = GetService<IMediator>();
        }

        protected HttpClient Client { get; }
        protected IWebHost Host { get; }
        protected IMediator Mediator { get; }

        protected TService GetService<TService>() => Host.Services.GetRequiredService<TService>();
    }
}
