namespace Golden.Raspberry.Awards.Api.InfraDb.Queries
{
    public static class ProducerQuery
    {
        public static string CmdSqlSelectFullQuery => @"
Select P.*, M.*
From Movie M
Inner Join MovieProducer MP On MP.MovieId = M.Id
Inner Join Producer       P On P.Id = MP.ProducerId
";

    }
}
