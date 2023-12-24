using Microsoft.AspNetCore.OutputCaching;
using Movies.Api.Auth;
using Movies.Api.Mapping;
using Movies.Application.Services;

namespace Movies.Api.EndPoints.Movies
{
    public static class DeleteMovieEndPoint
    {
        public const string Name = "DeleteMovie";

        public static IEndpointRouteBuilder MapDeleteMovie(this IEndpointRouteBuilder app)
        {
            app.MapDelete(ApiEndpoints.Movies.Delete, async (
                Guid id, IMovieService movieService,
                IOutputCacheStore outputCacheStore, CancellationToken token) =>
            {
                var deleted = await movieService.DeleteByIdAsync(id, token);
                if (!deleted)
                {
                    return Results.NotFound();
                }

                // invalida o cache quando um novo filme é deletado
                await outputCacheStore.EvictByTagAsync("movies", token);

                return Results.Ok();
            })
            .WithName(Name)
            .RequireAuthorization(AuthConstants.AdminUserPolicyName);

            return app;
        }
    }
}
