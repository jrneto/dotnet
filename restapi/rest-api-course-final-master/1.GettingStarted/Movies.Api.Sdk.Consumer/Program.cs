using Microsoft.Extensions.DependencyInjection;
using Movies.Api.Sdk;
using Movies.Contracts.Requests;
using Refit;
using System.Text.Json;

//var moviesApi = RestService.For<IMoviesApi>("https://localhost:5001");

var services = new ServiceCollection();

services.AddRefitClient<IMoviesApi>(x => new RefitSettings
    {
        AuthorizationHeaderValueGetter = async (HttpRequestMessage request, CancellationToken cancellationToken) =>
        {
            // Coloque aqui a lógica para obter o token de autenticação de forma assíncrona
            string token = await ObterTokenDeAutenticacao(); // Exemplo: função que retorna o token

            return token;
        }
})
    .ConfigureHttpClient(x => x.BaseAddress = new Uri("https://localhost:5001"));

async Task<string> ObterTokenDeAutenticacao()
{
    return await Task.FromResult("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI2ZjI2NjhiNy1mOWM3LTQ0ZWMtYmE1OC1iYjlmMjVkNTMwZTMiLCJzdWIiOiJuaWNrQG5pY2tjaGFwc2FzLmNvbSIsImVtYWlsIjoibmlja0BuaWNrY2hhcHNhcy5jb20iLCJ1c2VyaWQiOiJkODU2NmRlMy1iMWE2LTRhOWItYjg0Mi04ZTM4ODdhODJlNDEiLCJhZG1pbiI6dHJ1ZSwidHJ1c3RlZF9tZW1iZXIiOnRydWUsIm5iZiI6MTcwMzA3MTI3MCwiZXhwIjoxNzAzMTAwMDcwLCJpYXQiOjE3MDMwNzEyNzAsImlzcyI6Imh0dHBzOi8vaWQubmlja2NoYXBzYXMuY29tIiwiYXVkIjoiaHR0cHM6Ly9tb3ZpZXMubmlja2NoYXBzYXMuY29tIn0.ajne1ijY6L1rNpbL92aJtKX4FJ_9C232HOcrAqYSUMw");
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