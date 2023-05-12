using Golden.Raspberry.Awards.Api.Domain.Abstraction.Services;
using Golden.Raspberry.Awards.Api.Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Golden.Raspberry.Awards.Api.Controllers.v1
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : BaseController
    {
        private readonly ILogger<MovieController> _logger;
        private readonly IMovieService _movieService;

        public MovieController(ILogger<MovieController> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }

        [HttpGet(Name = "Get")]
        public async Task<IEnumerable<Movie>> Get()
        {
            return await _movieService.GetAll();
        }
    }
}
