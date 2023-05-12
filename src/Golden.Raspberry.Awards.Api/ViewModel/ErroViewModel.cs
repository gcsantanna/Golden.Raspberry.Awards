namespace Golden.Raspberry.Awards.Api.ViewModel
{
    public class ErroViewModel
    {
        /// <summary>
        /// Retorna as notificações separadas por "|" em caso de erros 400 (Bad Request) ou 500 (Server Error)
        /// </summary>
        public string DetalheErro { get; set; }
    }
}
