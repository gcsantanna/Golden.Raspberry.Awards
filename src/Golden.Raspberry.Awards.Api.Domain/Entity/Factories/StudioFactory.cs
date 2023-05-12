namespace Golden.Raspberry.Awards.Api.Domain.Entity.Factories
{
    public static class StudioFactory
    {
        private static readonly List<Studio> _studios = new();

        public static List<Studio> Create(string[] studioNames)
        {
            foreach (var studioName in studioNames)
            {
                var producer = _studios.SingleOrDefault(p => p.Name == studioName);
                if (producer == null)
                    _studios.Add(new Studio { Name = studioName });
            }

            return studioNames.Select(studioName => _studios.SingleOrDefault(p => p.Name == studioName)).ToList();
        }
    }
}
