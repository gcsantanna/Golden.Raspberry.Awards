using Golden.Raspberry.Awards.Api.Domain.Abstraction;
using Golden.Raspberry.Awards.Api.Domain.Enum;

namespace Golden.Raspberry.Awards.Api.Domain.Responses
{
    public class Response<TDados> : IResponse
    {
        private readonly IList<string> _mensagens = new List<string>();

        private readonly bool possuiErro;

        public Response()
        {
            Dados = default;
            MotivoErro = null;
        }

        public Response(MotivoErro motivoFalha, string mensagemErro)
        {
            MotivoErro = motivoFalha;

            _mensagens.Add(string.IsNullOrWhiteSpace(mensagemErro)
                ? motivoFalha.ToString()
                : mensagemErro);

            DetalheErro = _mensagens.Any() ? string.Join(" | ", _mensagens.ToList()) : string.Empty;
        }

        public Response(MotivoErro motivoFalha)
        {
            Dados = default;
            MotivoErro = motivoFalha;

            possuiErro = true;
        }

        public Response(TDados dados)
        {
            Dados = dados;
            DetalheErro = string.Empty;
            MotivoErro = null;
        }

        public string DetalheErro { get; set; }

        public TDados Dados { get; set; }

        public bool PossuiErro => (!string.IsNullOrWhiteSpace(DetalheErro) || possuiErro);

        public MotivoErro? MotivoErro { get; private set; }

        public IResponse AddErro(string mensagem)
        {
            _mensagens.Add(mensagem);
            DetalheErro = _mensagens.Any() ? string.Join(" | ", _mensagens.ToList()) : string.Empty;
            return this;
        }

        public void DefinirMotivoErro(MotivoErro motivoFalha)
        {
            MotivoErro = motivoFalha;
        }
    }
}
