using Golden.Raspberry.Awards.Api.Domain.Enum;

namespace Golden.Raspberry.Awards.Api.Domain.Abstraction
{
    public interface IResponse
    {
        IResponse AddErro(string mensagem);
        void DefinirMotivoErro(MotivoErro motivoFalha);
    }
}
