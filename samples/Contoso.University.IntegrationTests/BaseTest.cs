using MediatR;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net.Http;
using Xunit;

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
        }

        public TestServer TestServer { get; }

        public void Dispose()
        {
            TestServer?.Dispose();
        }
    }

    public abstract class BaseTest : IClassFixture<IntegrationTestFixture>
    {
        protected BaseTest(IntegrationTestFixture fixture)
        {
            Host = fixture.TestServer.Host;
            Client = fixture.TestServer.CreateClient();
            Mediator = GetService<IMediator>();
        }

        protected HttpClient Client { get; }
        protected IWebHost Host { get; }
        protected IMediator Mediator { get; }

        protected TService GetService<TService>() => Host.Services.GetRequiredService<TService>();
    }
}
