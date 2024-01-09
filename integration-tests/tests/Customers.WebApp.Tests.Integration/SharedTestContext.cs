using Ductus.FluentDocker.Model.Common;
using Ductus.FluentDocker.Services;
using Ductus.FluentDocker.Builders;
using Xunit;
using Microsoft.Playwright;

namespace Customers.WebApp.Tests.Integration
{
    public class SharedTestContext : IAsyncLifetime
    {
        public const string ValidGitHubUsername = "validuser";
        public const string AppUrl = "https://localhost:7780";

        public GitHubApiServer GitHubApiServer { get; } = new();

        // o path aponta para a pasta bin/debug/dotnet6 por isso o ../../../
        private static readonly string DockerComposeFile =
            Path.Combine(Directory.GetCurrentDirectory(), (TemplateString)"../../../docker-compose.integration.yml");

        private IPlaywright _playwright;

        public IBrowser Browser { get; private set; }

        private readonly ICompositeService _dokerService = new Builder()
            .UseContainer()
            .UseCompose()
            .FromFile(DockerComposeFile)
            .RemoveOrphans()
            .WaitForHttp("test-app", AppUrl)
            .Build();

        public async Task InitializeAsync()
        {
            GitHubApiServer.Start();
            GitHubApiServer.SetupUser(ValidGitHubUsername);
            _dokerService.Start();

            _playwright = await Playwright.CreateAsync();
            Browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                SlowMo = 1000
            });
        }

        public async Task DisposeAsync()
        {
            await Browser.DisposeAsync();
            _playwright.Dispose();
            _dokerService.Dispose();
            GitHubApiServer.Dispose();
        }        
    }
}
