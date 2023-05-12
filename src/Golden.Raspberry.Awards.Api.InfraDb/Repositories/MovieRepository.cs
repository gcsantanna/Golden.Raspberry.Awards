using Dapper;
using Golden.Raspberry.Awards.Api.Domain.Abstraction.Repositories;
using Golden.Raspberry.Awards.Api.Domain.Entity;
using Golden.Raspberry.Awards.Api.InfraDb.Queries;
using System.Data;

namespace Golden.Raspberry.Awards.Api.InfraDb.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IDbConnection _dbConnection;

        public MovieRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Movie>> GetAll()
        {
            var dapperCache = new DapperCache<int, Movie>();
            
            await _dbConnection.QueryAsync<Movie, Studio, Producer, Movie>(MovieQuery.CmdSqlSelectFullQuery, Map);
            
            return dapperCache.GetValues();

            Movie Map(Movie movie, Studio studio, Producer producer)
            {
                movie = dapperCache.Get(movie, m => m.Id);
                movie.Add(studio);
                movie.Add(producer);
                return movie;
            }
        }
    }
}
