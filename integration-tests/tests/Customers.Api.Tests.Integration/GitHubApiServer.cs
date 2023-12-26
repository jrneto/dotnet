using WireMock.ResponseBuilders;
using WireMock.Server;

namespace Customers.Api.Tests.Integration
{
    public class GitHubApiServer : IDisposable
    {
        private WireMockServer _server;

        public string Url => _server.Url!;

        public void Start()
        {
            _server = WireMockServer.Start();
        }

        public void SetupUser(string userName)
        {
            _server.Given(
                WireMock.RequestBuilders.Request.Create()
                .WithPath($"/users/{userName}")
                .UsingGet())
                .RespondWith(Response.Create()
                    .WithBody(GenereteGitHubUserResponseBody(userName))
                    .WithHeader("Content-Type", "application/json; charset=utf-8")
                    .WithStatusCode(200));
        }

        private static string GenereteGitHubUserResponseBody(string userName)
        {
            return $@"{{
                        ""login"": ""{userName}"",
                        ""id"": 44115369,
                        ""node_id"": ""MDQ6VXNlcjQ0MTE1MzY5"",
                        ""avatar_url"": ""https://avatars.githubusercontent.com/u/44115369?v=4"",
                        ""gravatar_id"": """",
                        ""url"": ""https://api.github.com/users/{userName}"",
                        ""html_url"": ""https://github.com/{userName}"",
                        ""followers_url"": ""https://api.github.com/users/{userName}/followers"",
                        ""following_url"": ""https://api.github.com/users/{userName}/following{{/other_user}}"",
                        ""gists_url"": ""https://api.github.com/users/{userName}/gists{{/gist_id}}"",
                        ""starred_url"": ""https://api.github.com/users/{userName}/starred{{/owner}}{{/repo}}"",
                        ""subscriptions_url"": ""https://api.github.com/users/{userName}/subscriptions"",
                        ""organizations_url"": ""https://api.github.com/users/{userName}/orgs"",
                        ""repos_url"": ""https://api.github.com/users/{userName}/repos"",
                        ""events_url"": ""https://api.github.com/users/{userName}/events{{/privacy}}"",
                        ""received_events_url"": ""https://api.github.com/users/{userName}/received_events"",
                        ""type"": ""User"",
                        ""site_admin"": false,
                        ""name"": ""José Reato Neto"",
                        ""company"": null,
                        ""blog"": """",
                        ""location"": null,
                        ""email"": null,
                        ""hireable"": null,
                        ""bio"": ""Apenas mais um DEV! :)"",
                        ""twitter_username"": null,
                        ""public_repos"": 8,
                        ""public_gists"": 0,
                        ""followers"": 2,
                        ""following"": 0,
                        ""created_at"": ""2018-10-13T12:05:30Z"",
                        ""updated_at"": ""2023-11-19T17:30:17Z""
                    }}";
        }

        public void Dispose()
        {
            _server.Stop();
            _server.Dispose();
        }
    }
}
