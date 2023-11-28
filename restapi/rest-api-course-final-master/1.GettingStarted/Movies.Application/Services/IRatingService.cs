using Movies.Application.Models;

namespace Movies.Application.Services
{
    public interface IRatingService
    {
        Task<bool> RateMovieAsync(Guid movieId, int rating, Guid userId, CancellationToken token);

        public Task<bool> DeleteRatingAsync(Guid movieId, Guid userId, CancellationToken token = default);

        public Task<IEnumerable<MovieRating>> GetRatingForUserAsync(Guid userId, CancellationToken token = default);
    }
}
