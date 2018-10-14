﻿using MediatR;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using Xunit;

namespace Contoso.University.IntegrationTests
{
    public abstract class BaseTest : IClassFixture<IntegrationTestFixture>
    {
        private readonly IntegrationTestFixture fixture;

        protected BaseTest(IntegrationTestFixture fixture)
        {
            this.fixture = fixture;
        }

        protected HttpClient Client => fixture.Client;

        protected IWebHost Host => fixture.Host;

        protected IMediator Mediator => fixture.Mediator;

        protected TService GetService<TService>() => fixture.GetService<TService>();
    }
}
