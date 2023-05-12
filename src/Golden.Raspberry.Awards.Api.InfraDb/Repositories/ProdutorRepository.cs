using Dapper;
using Golden.Raspberry.Awards.Api.Domain.Abstraction.Repositories;
using Golden.Raspberry.Awards.Api.Domain.Entity;
using Golden.Raspberry.Awards.Api.Domain.Responses;
using Golden.Raspberry.Awards.Api.InfraDb.Queries;
using System.Data;

namespace Golden.Raspberry.Awards.Api.InfraDb.Repositories
{
    public class ProdutorRepository : IProducerRepository
    {
        private readonly IDbConnection _dbConnection;

        public ProdutorRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Producer>> GetProducer()
        {
            var dapperCache = new DapperCache<int, Producer>();
            
            await _dbConnection.QueryAsync<Producer, Movie, Producer>(ProducerQuery.CmdSqlSelectFullQuery, Map);

            return dapperCache.GetValues();

            Producer Map(Producer producer, Movie movie)
            {
                producer = dapperCache.Get(producer, m => m.Id);
                producer.Add(movie);
                return producer;
            }
        }
    }
}
