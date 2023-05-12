using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

namespace Golden.Raspberry.Awards.Api.Configuration
{
    public static class CompressionConfig
    {
        public static void AddCompression(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
            });

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });
        }
    }
}
