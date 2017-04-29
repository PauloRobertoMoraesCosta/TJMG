using TJ.Apresentacao.InterfacesApp;
using TJ.Dominio.Entidades;
using TJ.Dominio.Interfaces.Servicos;

namespace TJ.Apresentacao.ServiceApp
{
    public class AppServiceJesp : AppServiceBase<Jesp>, IAppServiceJesp
    {
        protected readonly IServicoJesp _serviceJesp;

        public AppServiceJesp(IServicoJesp serviceJesp)
            : base(serviceJesp)
        {
            _serviceJesp = serviceJesp;
        }
    }
}
