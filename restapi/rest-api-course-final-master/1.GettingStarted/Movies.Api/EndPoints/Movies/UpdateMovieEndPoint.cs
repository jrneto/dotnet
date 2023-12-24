using Microsoft.AspNetCore.OutputCaching;
using Movies.Api.Auth;
using Movies.Api.Mapping;
using Movies.Application.Services;
using Movies.Contracts.Requests;
using Movies.Contracts.Responses;

namespace Movies.Api.EndPoints.Movies
{
    public static class UpdateMovieEndPoint
    {
        public const string Name = "UpdateMovie";

        public static IEndpointRouteBuilder MapUpdateMovie(this IEndpointRouteBuilder app)
        {
            app.MapPut(ApiEndpoints.Movies.Update, async (
                Guid id, UpdateMovieRequest request, IMovieService movieService,
                IOutputCacheStore outputCacheStore, HttpContext context, CancellationToken token ) =>
                {
                    var userId = context.GetUserId();

                    var movie = request.MapToMovie(id);

                    var updatedMovie = await movieService.UpdateAsync(movie, userId, token);

                    if (updatedMovie is null)
                    {
                        return Results.NotFound();
                    }

                    var response = updatedMovie.MapToResponse();

                    // invalida o cache quando um novo filme é atualizado
                    await outputCacheStore.EvictByTagAsync("movies", token);

                    return TypedResults.Ok(response);
                })
                .WithName(Name)
                .Produces<MovieResponse>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
                .RequireAuthorization(AuthConstants.TrustedMemberPolicyName);

            return app;
        }
    }
}
