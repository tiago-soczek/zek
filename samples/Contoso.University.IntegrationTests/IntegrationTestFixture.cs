using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace Contoso.University.IntegrationTests
{
    public class IntegrationTestFixture : WebApplicationFactory<Startup>, IDisposable
    {
        public IntegrationTestFixture()
        {
            Client = CreateClient();
        }

        public HttpClient Client { get; }

        public TService GetService<TService>() => Server.Host.Services.GetRequiredService<TService>();

        protected override void Dispose(bool disposing)
        {
            Client?.Dispose();

            base.Dispose(disposing);
        }
    }
}
