using Golden.Raspberry.Awards.Api.Domain.Abstraction.Repositories;
using Golden.Raspberry.Awards.Api.Domain.Abstraction.Services;
using Golden.Raspberry.Awards.Api.Domain.Entity;
using Golden.Raspberry.Awards.Api.Domain.Enum;
using Golden.Raspberry.Awards.Api.Domain.Responses;

namespace Golden.Raspberry.Awards.Api.Service.ProdutorServices
{
    public class ProducerService : IProducerService
    {
        private readonly IProducerRepository _producerRepository;

        public ProducerService(IProducerRepository producerRepository)
        {
            _producerRepository = producerRepository;
        }

        public async Task<Response<ProducerResponseViewModel>> GetProducersAsync()
        {
            var producers = await _producerRepository.GetProducer();

            if (!producers.Any())
            {
                var retorno = new Response<ProducerResponseViewModel>(MotivoErro.NotFound);
                retorno.AddErro($"Nenhum registro localizado.");
                return retorno;
            }
            return GetRankingProducers(producers);
        }

        private static Response<ProducerResponseViewModel> GetRankingProducers(IEnumerable<Producer> producers)
        {
            var allProducerIndications = new List<ProducerIndication>();
            foreach (var producer in producers)
            {
                var orderedMovies = producer.Movies.Where(m => m.Winner).OrderBy(m => m.Year).ToArray();
                var producerIndications = GetIntervals(producer, orderedMovies);
                allProducerIndications.AddRange(producerIndications);
            }

            if (allProducerIndications.Any())
            {
                var producersI = allProducerIndications.GroupBy(x => x.Producer)
                    .Select(g => new { g.Key, MinInterval = g.Min(x => x.Interval), MaxInterval = g.Max(x => x.Interval) })
                    .ToArray();

                var minInterval = producersI.Min(x => x.MinInterval);
                var maxInterval = producersI.Max(x => x.MaxInterval);

                var producerResponseViewModel = new ProducerResponseViewModel
                {
                    Min = allProducerIndications.Where(x => x.Interval == minInterval).ToList(),
                    Max = allProducerIndications.Where(x => x.Interval == maxInterval).ToList(),
                };

                return new Response<ProducerResponseViewModel>(producerResponseViewModel);
            }

            return new Response<ProducerResponseViewModel>() { };
        }

        private static IEnumerable<ProducerIndication> GetIntervals(Producer? producer, Movie[] orderedMovies)
        {
            var producerIndications = new List<ProducerIndication>();
            var previewMovie = orderedMovies.FirstOrDefault();
            foreach (var movie in orderedMovies.Skip(1))
            {
                var producerIndication = new ProducerIndication
                {
                    Producer = producer.Name,
                    PreviousWin = previewMovie.Year,
                    FollowingWin = movie.Year,
                };
                producerIndications.Add(producerIndication);
                previewMovie = movie;
            }
            return producerIndications;
        }
    }
}
