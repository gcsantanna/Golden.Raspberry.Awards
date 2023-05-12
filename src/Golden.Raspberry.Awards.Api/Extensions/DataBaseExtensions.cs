using Golden.Raspberry.Awards.Api.Domain.Abstraction;
using Golden.Raspberry.Awards.Api.InfraDb;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Data.SQLite;

namespace Golden.Raspberry.Awards.Api.Extensions
{
    public static class DataBaseExtensions
    {
        public static IServiceCollection AddSystemSQLiteConnection(this IServiceCollection services, string connectionString)
        {
            return services.AddSingleton<IDbConnection>(sp =>
            {
                var dbConnection = new SQLiteConnection(connectionString);
                dbConnection.Open();
                return dbConnection;
            });
        }

        public static IServiceCollection AddDataBaseManager(this IServiceCollection services, FileInfo seedFileInfo)
        {
            return services.AddTransient<IDataBaseManager>(sp =>
            {
                var dbConnection = sp.GetRequiredService<IDbConnection>();
                return new DataBaseManager(dbConnection, seedFileInfo);
            });
        }

        public static IHost Seed(this IHost app)
        {
            using var scope = app.Services.CreateScope();
            var dataBaseManager = scope.ServiceProvider.GetRequiredService<IDataBaseManager>();
            dataBaseManager.Seed().ConfigureAwait(false).GetAwaiter().GetResult();
            return app;
        }
    }
}
