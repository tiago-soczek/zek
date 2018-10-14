using MediatR;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net.Http;

namespace Contoso.University.IntegrationTests
{
    public class IntegrationTestFixture : IDisposable
    {
        public IntegrationTestFixture()
        {
            // More info about MVC Testing https://blogs.msdn.microsoft.com/webdev/2017/12/07/testing-asp-net-core-mvc-web-apps-in-memory/

            var contentRoot = @"..\..\..\..\Contoso.University";

            var hostBuilder = WebHost.CreateDefaultBuilder()
                .UseContentRoot(Path.GetFullPath(contentRoot))
                .UseEnvironment("Test")
                .UseStartup<Startup>();

            TestServer = new TestServer(hostBuilder);

            Host = TestServer.Host;
            Client = TestServer.CreateClient();
            Mediator = GetService<IMediator>();
        }

        public TestServer TestServer { get; }

        public HttpClient Client { get; }

        public IWebHost Host { get; }

        public IMediator Mediator { get; }

        public TService GetService<TService>() => Host.Services.GetRequiredService<TService>();

        public void Dispose()
        {
            Client?.Dispose();
            TestServer?.Dispose();
        }
    }
}
