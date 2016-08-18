using TJ.Apresentacao.InterfacesApp;
using TJ.Dominio.Entidades;
using TJ.Dominio.Interfaces.Servicos;

namespace TJ.Apresentacao.ServiceApp
{
    public class AppServiceEstado: AppServiceBase<Estado>, IAppServiceEstado
    {
        private readonly IServicoEstado _serviceEstado;

        public AppServiceEstado(IServicoEstado serviceEstado) : base(serviceEstado)
        {
            _serviceEstado = serviceEstado;
        }
    }
}
