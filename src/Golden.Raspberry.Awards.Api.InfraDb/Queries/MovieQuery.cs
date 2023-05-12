namespace Golden.Raspberry.Awards.Api.InfraDb.Queries
{
    public static class MovieQuery
    {
        public static string CmdSqlSelectFullQuery => @"
Select M.*, S.*, P.*
From Movie M
Inner Join MovieStudio   MS On MS.MovieId = M.Id
Inner Join Studio         S On S.Id = MS.StudioId
Inner Join MovieProducer MP On MP.MovieId = M.Id
Inner Join Producer       P On P.Id = MP.ProducerId
";

    }
}
