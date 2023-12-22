using Movies.Api.EndPoints.Movies;
using Movies.Api.EndPoints.Ratings;

namespace Movies.Api.EndPoints
{
    public static class EndPointsExtensions
    {
        public static IEndpointRouteBuilder MapApiEndPoints(this IEndpointRouteBuilder app)
        {
            app.MapMovieEndPoints();
            app.MapRatingEndPoints();

            return app;
        }
    }
}
