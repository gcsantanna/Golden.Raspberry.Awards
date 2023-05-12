using Dapper;
using Golden.Raspberry.Awards.Api.Domain.Abstraction;
using Golden.Raspberry.Awards.Api.Domain.Entity;
using Golden.Raspberry.Awards.Api.Domain.Entity.Factories;
using System.Data;

namespace Golden.Raspberry.Awards.Api.InfraDb
{
    public class DataBaseManager : IDataBaseManager
    {
        private readonly IDbConnection _dbConnection;
        private readonly FileInfo _seedFileInfo;

        public DataBaseManager(IDbConnection dbConnection, FileInfo seedFileInfo)
        {
            _dbConnection = dbConnection;
            _seedFileInfo = seedFileInfo;
        }

        public async Task Seed()
        {
            await CreateDatabaseIfNotExists();
            await SeedIfDataBaseIsEmpty();
        }

        private async Task<int> CreateDatabaseIfNotExists()
        {
            var i = 0;
            var tabela = await _dbConnection.ExecuteScalarAsync("Select * From sqlite_schema Limit 1;");
            if (tabela is null)
            {
                i += await _dbConnection.ExecuteAsync("Create Table Movie         (Id Integer Not Null Primary Key AutoIncrement, Name VarChar(100) Not Null, Year Integer Not Null, Winner SmallInt Not Null);");
                i += await _dbConnection.ExecuteAsync("Create Table Studio        (Id Integer Not Null Primary Key AutoIncrement, Name VarChar(100) Not Null);");
                i += await _dbConnection.ExecuteAsync("Create Table Producer      (Id Integer Not Null Primary Key AutoIncrement, Name VarChar(100) Not Null);");
                i += await _dbConnection.ExecuteAsync("Create Table MovieStudio   (Id Integer Not Null Primary Key AutoIncrement, MovieId Integer Not Null, StudioId   Integer Not Null);");
                i += await _dbConnection.ExecuteAsync("Create Table MovieProducer (Id Integer Not Null Primary Key AutoIncrement, MovieId Integer Not Null, ProducerId Integer Not Null);");
            }
            return i;
        }

        private async Task SeedIfDataBaseIsEmpty()
        {
            var movie = await _dbConnection.ExecuteScalarAsync("Select Id From Movie Limit 1;");
            if (movie is null)
            {
                var linhas = File.ReadAllLines(_seedFileInfo.FullName);
                foreach (var line in linhas.Skip(1))
                    await ImportMovie(line);
            }
        }

        private async Task ImportMovie(string line)
        {
            var fields = line.Split(";", StringSplitOptions.TrimEntries);
            var movie = MovieFactory.Create(fields);
            await Save(movie);
        }

        private async Task Save(Movie movie)
        {
            await SaveMovie(movie);
            await SaveStudios(movie);
            await SaveProducers(movie);
        }

        private async Task SaveStudios(Movie movie)
        {
            foreach (var studio in movie.Studios)
            {
                await SaveStudio(studio);

                var parameters = new { movieId = movie.Id, studioId = studio.Id };
                var id = await _dbConnection.ExecuteScalarAsync("Select Id From MovieStudio Where (MovieId = @movieId) And (StudioId = @studioId)", parameters);
                id ??= await _dbConnection.ExecuteScalarAsync("Insert Into MovieStudio (MovieId, StudioId) Values (@movieId, @studioId); Select Last_Insert_RowId();", parameters);
                Convert.ToInt32(id);
            }
        }

        private async Task SaveProducers(Movie movie)
        {
            foreach (var producer in movie.Producers)
            {
                await SaveProducer(producer);

                var parameters = new { movieId = movie.Id, producerId = producer.Id };
                var id = await _dbConnection.ExecuteScalarAsync("Select Id From MovieProducer Where (MovieId = @movieId) And (ProducerId = @producerId)", parameters);
                id ??= await _dbConnection.ExecuteScalarAsync("Insert Into MovieProducer (MovieId, ProducerId) Values (@movieId, @producerId); Select Last_Insert_RowId();", parameters);
                Convert.ToInt32(id);
            }
        }

        private async Task SaveMovie(Movie movie)
        {
            if (movie.Id == 0)
            {
                var id = await _dbConnection.ExecuteScalarAsync("Select Id From Movie Where (Name = @name)", movie);
                id ??= await _dbConnection.ExecuteScalarAsync("Insert Into Movie (Name, Year, Winner) Values (@name, @year, @winner); Select Last_Insert_RowId();", movie);
                movie.Id = Convert.ToInt32(id);
            }
        }

        private async Task SaveStudio(Studio studio)
        {
            if (studio.Id == 0)
            {
                var id = await _dbConnection.ExecuteScalarAsync("Select Id From Studio Where (Name = @name)", studio);
                id ??= await _dbConnection.ExecuteScalarAsync("Insert Into Studio (Name) Values (@name); Select Last_Insert_RowId();", studio);
                studio.Id = Convert.ToInt32(id);
            }
        }

        private async Task SaveProducer(Producer producer)
        {
            if (producer.Id == 0)
            {
                var id = await _dbConnection.ExecuteScalarAsync("Select Id From Producer Where (Name = @name)", producer);
                id ??= await _dbConnection.ExecuteScalarAsync("Insert Into Producer (Name) Values (@name); Select Last_Insert_RowId();", producer);
                producer.Id = Convert.ToInt32(id);
            }
        }
    }
}
