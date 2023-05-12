using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Golden.Raspberry.Awards.Api.Domain.Entity
{
    public class Studio
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString() => $"{Id} {Name}";

        [JsonIgnore]
        public List<Movie> Movies { get; }

        public Studio()
        {
            Movies = new List<Movie>();
        }

        public void Add(Movie movie)
        {
            if (!Movies.Any(m => m.Id == movie.Id))
            {
                Movies.Add(movie);
                movie.Add(this);
            }
        }
    }
}
