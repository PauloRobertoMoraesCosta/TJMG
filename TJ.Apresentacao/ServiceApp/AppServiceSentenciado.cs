using TJ.Apresentacao.InterfacesApp;
using TJ.Dominio.Entidades;
using TJ.Dominio.Interfaces.Servicos;

namespace TJ.Apresentacao.ServiceApp
{
    public class AppServiceSentenciado : AppServiceBase<Sentenciado>, IAppServiceSentenciado
    {
        protected readonly IServicoSentenciado _serviceSentenciado;
        public AppServiceSentenciado(IServicoSentenciado servicoSentenciado) : base(servicoSentenciado)
        {
            _serviceSentenciado = servicoSentenciado;
        }
    }
}
