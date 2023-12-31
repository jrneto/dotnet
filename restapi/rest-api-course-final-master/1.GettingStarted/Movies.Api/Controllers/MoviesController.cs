﻿
/*
 * using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Movies.Api.Auth;
using Movies.Api.Mapping;
using Movies.Application.Services;
using Movies.Contracts.Requests;
using Movies.Contracts.Responses;

namespace Movies.Api.Controllers
{

    [ApiController]
    [ApiVersion(1.0)]
    //[ApiVersion(2.0)]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IOutputCacheStore _outputCacheStore;

        public MoviesController(
            IMovieService movieService, 
            IOutputCacheStore outputCacheStore)
        {
            _movieService = movieService;
            _outputCacheStore = outputCacheStore;
        }

        [Authorize(AuthConstants.TrustedMemberPolicyName)]
        [ServiceFilter(typeof(ApiKeyAuthFilter))]
        [HttpPost(ApiEndpoints.Movies.Create)]
        [ProducesResponseType(typeof(MovieResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationFailureResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(
            [FromBody] CreateMovieRequest request,
            CancellationToken token)
        {
            var movie = request.MapToMovie();

            await _movieService.CreateAsync(movie, token);

            // invalida o cache quando um novo filme é criado
            await _outputCacheStore.EvictByTagAsync("movies", token);

            var response = movie.MapToResponse();

            return CreatedAtAction(nameof(Create), new { idOrSlug = movie.Id }, response);
            //return Created($"/{ApiEndpoints.Movies.Create}/{movie.Id}", movie);
        }

        //[MapToApiVersion(1.0)]
        //[HttpGet(ApiEndpoints.Movies.Get)]
        //public async Task<IActionResult> GetV1([FromRoute] string idOrSlug,
        //    [FromServices] LinkGenerator linkGenerator,
        //    CancellationToken token)
        //{
        //    var userId = HttpContext.GetUserId();

        //    var movie = Guid.TryParse(idOrSlug, out var id)
        //        ? await _movieService.GetByIdAsync(id, userId, token)
        //        : await _movieService.GetBySlugAsync(idOrSlug, userId, token);

        //    if (movie is null)
        //    {
        //        return NotFound();
        //    }

        //    var response = movie.MapToResponse();

        //    var movieObj = new { id = movie.Id };

        //    response.Links.Add(new Link
        //    {
        //        Href = linkGenerator.GetPathByAction(HttpContext, nameof(GetV1), values: new { idOrSlug = movie.Id }),
        //        Rel = "self",
        //        Type = "GET"
        //    });

        //    response.Links.Add(new Link
        //    {
        //        Href = linkGenerator.GetPathByAction(HttpContext, nameof(Update), values: new { id = movie.Id }),
        //        Rel = "self",
        //        Type = "PUT"
        //    });

        //    response.Links.Add(new Link
        //    {
        //        Href = linkGenerator.GetPathByAction(HttpContext, nameof(Delete), values: new { id = movie.Id }),
        //        Rel = "self",
        //        Type = "DELETE"
        //    });

        //    return Ok(response);
        //}

        //[MapToApiVersion(2.0)]
        [HttpGet(ApiEndpoints.Movies.Get)]
        [Authorize(AuthConstants.TrustedMemberPolicyName)]
        // ResponseCahce é cache no cliente
        //[ResponseCache(Duration = 30, VaryByHeader = "Accept, Accetp-Encoding", Location = ResponseCacheLocation.Any)]
        // Cache no servidor
        [OutputCache(PolicyName = "MovieCache")]
        [ProducesResponseType(typeof(MovieResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetV1([FromRoute] string idOrSlug,
            [FromServices] LinkGenerator linkGenerator,
            CancellationToken token)
        {
            var userId = HttpContext.GetUserId();

            var movie = Guid.TryParse(idOrSlug, out var id)
                ? await _movieService.GetByIdAsync(id, userId, token)
                : await _movieService.GetBySlugAsync(idOrSlug, userId, token);

            if (movie is null)
            {
                return NotFound();
            }

            var response = movie.MapToResponse();
            
            return Ok(response);
        }

        [HttpGet(ApiEndpoints.Movies.GetAll)]
        //este é um cache no cliente. O parametro VaryByQueryKeys configura o cache de acordo com os parametros informados na query
        //[ResponseCache(Duration = 30, VaryByQueryKeys = new[] {"title","year","sortby","page","pageSize"}, Location = ResponseCacheLocation.Any)]
        //cache no servidor
        [OutputCache(PolicyName = "MovieCache")]
        [ProducesResponseType(typeof(MoviesResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetAllMoviesRequest request,
            CancellationToken token)
        {
            var userId = HttpContext.GetUserId();

            var options = request.MapToOptions()
                .WithUser(userId);

            var movies = await _movieService.GetAllAsync(options, token);
            var movieCount = await _movieService.GetCountAsync(options.Title, options.YearOfRelease, token);

            //var moviesResponse = movies.MapToResponse(request.Page, request.PageSize, movieCount);

            return Ok();
        }

        [Authorize(AuthConstants.TrustedMemberPolicyName)]
        [HttpPut(ApiEndpoints.Movies.Update)]
        [ProducesResponseType(typeof(MovieResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationFailureResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateMovieRequest request,
            CancellationToken token)
        {
            var userId = HttpContext.GetUserId();

            var movie = request.MapToMovie(id);

            var updatedMovie = await _movieService.UpdateAsync(movie, userId, token);

            if (updatedMovie is null)
            {
                return NotFound();
            }

            var response = updatedMovie.MapToResponse();

            // invalida o cache quando um novo filme é atualizado
            await _outputCacheStore.EvictByTagAsync("movies", token);

            return Ok(response);
        }

        [Authorize(AuthConstants.AdminUserPolicyName)]
        [HttpDelete(ApiEndpoints.Movies.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid id,
            CancellationToken token)
        {
            var deleted = await _movieService.DeleteByIdAsync(id, token);
            if (!deleted)
            {
                return NotFound();
            }

            // invalida o cache quando um novo filme é deletado
            await _outputCacheStore.EvictByTagAsync("movies", token);

            return Ok();
        }
    }
}
*/
