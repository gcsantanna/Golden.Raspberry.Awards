using Golden.Raspberry.Awards.Api.Domain.Entity;

namespace Golden.Raspberry.Awards.Api.Domain.Abstraction.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetAll();
    }
}
