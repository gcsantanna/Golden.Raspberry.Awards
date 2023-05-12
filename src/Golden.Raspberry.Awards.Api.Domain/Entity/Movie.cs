using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Golden.Raspberry.Awards.Api.Domain.Entity
{
    public class Movie
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string Name { get; set; }
        public List<Studio> Studios { get; set; }
        public List<Producer> Producers { get; set; }
        public bool Winner { get; set; }
        public override string ToString() => $"{Id} - {Year}; {Name}; {string.Join(", ", Studios)}; {string.Join(", ", Producers)}; {Winner}";

        public Movie()
        {
            Studios = new List<Studio>();
            Producers = new List<Producer>();
        }

        public void Add(Studio studio)
        {
            if (!Studios.Any(m => m.Id == studio.Id))
            {
                Studios.Add(studio);
                studio.Add(this);
            }
        }

        public void Add(Producer producer)
        {
            if (!Producers.Any(p => p.Id == producer.Id))
            {
                Producers.Add(producer);
                producer.Add(this);
            }
        }
    }
}
