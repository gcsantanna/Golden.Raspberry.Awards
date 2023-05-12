using Golden.Raspberry.Awards.Api.Domain.Responses;

namespace Golden.Raspberry.Awards.Api.Domain.Abstraction.Services
{
    public interface IProducerService
    {
        Task<Response<ProducerResponseViewModel>> GetProducersAsync();
    }
}
