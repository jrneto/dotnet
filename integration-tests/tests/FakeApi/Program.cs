

using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

var wireMockServer = WireMockServer.Start();

Console.WriteLine($"Wiremock server is now running on: {wireMockServer.Url}");

wireMockServer.Given(
    Request.Create()
    .WithPath("/users/jrneto")
    .UsingGet())
    .RespondWith(
        Response.Create()
        .WithBodyAsJson(@"{
            ""login"": ""jrneto"",
            ""id"": 44115369,
            ""node_id"": ""MDQ6VXNlcjQ0MTE1MzY5"",
            ""avatar_url"": ""https://avatars.githubusercontent.com/u/44115369?v=4"",
            ""gravatar_id"": """",
            ""url"": ""https://api.github.com/users/jrneto"",
            ""html_url"": ""https://github.com/jrneto"",
            ""followers_url"": ""https://api.github.com/users/jrneto/followers"",
            ""following_url"": ""https://api.github.com/users/jrneto/following{/other_user}"",
            ""gists_url"": ""https://api.github.com/users/jrneto/gists{/gist_id}"",
            ""starred_url"": ""https://api.github.com/users/jrneto/starred{/owner}{/repo}"",
            ""subscriptions_url"": ""https://api.github.com/users/jrneto/subscriptions"",
            ""organizations_url"": ""https://api.github.com/users/jrneto/orgs"",
            ""repos_url"": ""https://api.github.com/users/jrneto/repos"",
            ""events_url"": ""https://api.github.com/users/jrneto/events{/privacy}"",
            ""received_events_url"": ""https://api.github.com/users/jrneto/received_events"",
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
        }")
        .WithHeader( "Content-Type", "application/json; charset=utf-8" )
        .WithStatusCode(200));

Console.ReadKey();
wireMockServer.Dispose();