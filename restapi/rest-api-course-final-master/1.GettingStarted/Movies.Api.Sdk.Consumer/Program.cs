using Microsoft.Extensions.DependencyInjection;
using Movies.Api.Sdk;
using Movies.Api.Sdk.Consumer;
using Movies.Contracts.Requests;
using Refit;
using System.Text.Json;

//var moviesApi = RestService.For<IMoviesApi>("https://localhost:5001");

var services = new ServiceCollection();

services
    .AddHttpClient()
    .AddSingleton<AuthTokenProvider>()
    .AddRefitClient<IMoviesApi>(x => new RefitSettings
    {
        AuthorizationHeaderValueGetter = async (HttpRequestMessage request, CancellationToken cancellationToken) =>
        {
            // Coloque aqui a lógica para obter o token de autenticação de forma assíncrona
            string token = await ObterTokenDeAutenticacao(x); // Exemplo: função que retorna o token

            return token;
        }
})
    .ConfigureHttpClient(x => x.BaseAddress = new Uri("https://localhost:5001"));

async Task<string> ObterTokenDeAutenticacao(IServiceProvider sp)
{
    var token = await sp.GetRequiredService<AuthTokenProvider>().GetTokenAsync();
    return token;
}

var provider = services.BuildServiceProvider();

var moviesApi = provider.GetRequiredService<IMoviesApi>();

var movie = await moviesApi.GetMovieAsync("6de0d777-06cf-427f-bb42-55f423268e0a");

Console.WriteLine(JsonSerializer.Serialize(movie));

var request = new GetAllMoviesRequest
{
    Title = null,
    Year = null,
    SortBy = null,
    Page = 1,
    PageSize = 3
};

var movies = await moviesApi.GetMoviesAsync(request);

foreach (var movieResponse in movies.Items)
{
    Console.WriteLine(JsonSerializer.Serialize(movieResponse));
}

Console.ReadLine();