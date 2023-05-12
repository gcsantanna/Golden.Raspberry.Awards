using System.Text.Json.Serialization;

namespace Golden.Raspberry.Awards.Api.Domain.Entity
{
    public class Producer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString() => $"{Id} {Name}";

        [JsonIgnore]
        public List<Movie> Movies { get; }

        public Producer()
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
