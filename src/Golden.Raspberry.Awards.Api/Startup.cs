using Golden.Raspberry.Awards.Api.Configuration;
using Golden.Raspberry.Awards.Api.Domain.Abstraction;
using Golden.Raspberry.Awards.Api.Domain.Abstraction.Repositories;
using Golden.Raspberry.Awards.Api.Domain.Abstraction.Services;
using Golden.Raspberry.Awards.Api.Extensions;
using Golden.Raspberry.Awards.Api.InfraDb;
using Golden.Raspberry.Awards.Api.InfraDb.Repositories;
using Golden.Raspberry.Awards.Api.Middleware;
using Golden.Raspberry.Awards.Api.Service.MovieServices;
using Golden.Raspberry.Awards.Api.Service.ProdutorServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Golden.Raspberry.Awards.Api
{
    public class Startup
    {
        private const string CORS_DEFAULT_POLICY = "DEFAULT_POLICY";

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHeaderPropagation();
            services.AddControllers();
            services.AddCompression();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddSystemSQLiteConnection(Configuration.GetConnectionString("Sqlite"));
            services.AddDataBaseManager(new FileInfo(Configuration["SeedFileInfo"]));

            #region Services
            services.AddScoped<IProducerService, ProducerService>();
            services.AddScoped<IMovieService, MovieService>();
            #endregion

            #region Repositories
            services.AddScoped<IProducerRepository, ProdutorRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseResponseCompression();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
