namespace Movies.Api.EndPoints.Movies
{
    public static class MovieEndPointExtensions
    {
        public static IEndpointRouteBuilder MapMovieEndPoints(this IEndpointRouteBuilder app) 
        {
            app.MapCreateMovie();
            app.MapGetMovie();
            app.MapGetAllMovies();
            app.MapUpdateMovie();
            app.MapDeleteMovie();

            return app;
        }
    }
}
