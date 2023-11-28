﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.Api.Auth;
using Movies.Api.Mapping;
using Movies.Application.Services;
using Movies.Contracts.Requests;

namespace Movies.Api.Controllers
{
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingsController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [Authorize]
        [HttpPut(ApiEndpoints.Movies.Rate)]
        public async Task<IActionResult> RateMovie([FromRoute] Guid id,
            [FromBody] RateMovieRequest request,
            CancellationToken token)
        {
            var userId = HttpContext.GetUserId();
            var result = await _ratingService.RateMovieAsync(id, request.Rating, userId!.Value, token);

            return result ? Ok() : NotFound();
        }

        [Authorize]
        [HttpDelete(ApiEndpoints.Movies.DeleteRating)]
        public async Task<IActionResult> DeleteRating([FromRoute] Guid id,
            CancellationToken token)
        {
            var userId = HttpContext.GetUserId();
            var result = await _ratingService.DeleteRatingAsync(id, userId!.Value, token);

            return result ? Ok() : NotFound();
        }

        [Authorize]
        [HttpGet(ApiEndpoints.Ratings.GetUserRatings)]
        public async Task<IActionResult> GetUserRatings(CancellationToken token)
        {
            var userId = HttpContext.GetUserId();
            var ratings = await _ratingService.GetRatingForUserAsync(userId!.Value, token);
            var ratingResponse = ratings.MapToResponse();
            return Ok(ratingResponse);
        }
    }
}
