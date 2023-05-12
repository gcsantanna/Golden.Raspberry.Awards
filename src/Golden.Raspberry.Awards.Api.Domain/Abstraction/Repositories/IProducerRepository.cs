using Golden.Raspberry.Awards.Api.Domain.Entity;
using Golden.Raspberry.Awards.Api.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Golden.Raspberry.Awards.Api.Domain.Abstraction.Repositories
{
    public interface IProducerRepository
    {
        //Task<IEnumerable<ProducerResponse>> GetProducer();
        Task<IEnumerable<Producer>> GetProducer();
    }
}
