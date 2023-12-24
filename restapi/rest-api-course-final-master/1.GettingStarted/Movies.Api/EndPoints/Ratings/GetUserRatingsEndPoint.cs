using Movies.Api.Auth;
using Movies.Api.Mapping;
using Movies.Application.Services;

namespace Movies.Api.EndPoints.Ratings
{
    public static class GetUserRatingsEndPoint
    {
        public const string Name = "GetUserRatings";

        public static IEndpointRouteBuilder MapGetUserRatings(this IEndpointRouteBuilder app)
        {
            app.MapGet(ApiEndpoints.Ratings.GetUserRatings, async (
                IRatingService ratingService,
                HttpContext context, CancellationToken token) =>
            {
                var userId = context.GetUserId();
                var ratings = await ratingService.GetRatingForUserAsync(userId!.Value, token);
                var ratingResponse = ratings.MapToResponse();
                return TypedResults.Ok(ratingResponse);
            })
            .WithName(Name)
            .RequireAuthorization();

            return app;
        }
    }
}
