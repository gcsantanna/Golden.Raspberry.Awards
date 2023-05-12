namespace Golden.Raspberry.Awards.Api.Domain.Entity.Factories
{
    public static class MovieFactory
    {
        private static readonly string[] _splits = new[] { ",", " and " };

        private static readonly StringSplitOptions _splitOption = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;

        public static Movie Create(string[] fields)
        {
            return new Movie
            {
                Year = int.Parse(fields[0]),
                Name = fields[1],
                Studios = StudioFactory.Create(fields[2].Split(_splits, _splitOption)),
                Producers = ProducerFactory.Create(fields[3].Split(_splits, _splitOption)),
                Winner = fields[4].Equals("yes"),
            };
        }
    }
}
