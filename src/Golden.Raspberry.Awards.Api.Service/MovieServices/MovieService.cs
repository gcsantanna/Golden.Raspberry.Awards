using Golden.Raspberry.Awards.Api.Domain.Abstraction.Repositories;
using Golden.Raspberry.Awards.Api.Domain.Abstraction.Services;
using Golden.Raspberry.Awards.Api.Domain.Entity;

namespace Golden.Raspberry.Awards.Api.Service.MovieServices
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<IEnumerable<Movie>> GetAll()
            => await _movieRepository.GetAll();

    }
}
