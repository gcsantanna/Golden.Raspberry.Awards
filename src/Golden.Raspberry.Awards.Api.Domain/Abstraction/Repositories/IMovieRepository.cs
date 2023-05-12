using Golden.Raspberry.Awards.Api.Domain.Entity;

namespace Golden.Raspberry.Awards.Api.Domain.Abstraction.Repositories
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAll();
    }
}
