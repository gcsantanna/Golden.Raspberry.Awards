using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Golden.Raspberry.Awards.Api.Domain.Entity.Factories
{
    public static class ProducerFactory
    {
        private static readonly List<Producer> _producers = new();

        public static List<Producer> Create(string[] producerNames)
        {
            foreach (var producerName in producerNames)
            {
                var producer = _producers.SingleOrDefault(p => p.Name == producerName);
                if (producer == null)
                    _producers.Add(new Producer { Name = producerName });
            }

            return producerNames.Select(producerName => _producers.SingleOrDefault(p => p.Name == producerName)).ToList();
        }
    }
}
