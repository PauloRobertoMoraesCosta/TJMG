using TJ.Apresentacao.InterfacesApp;
using TJ.Dominio.Entidades;
using TJ.Dominio.Interfaces.Servicos;

namespace TJ.Apresentacao.ServiceApp
{
    public class AppServiceBairro : AppServiceBase<Bairro>, IAppServiceBairro
    {
        private readonly IServicoBairro _serviceBairro;

        public AppServiceBairro(IServicoBairro serviceBairro)
            : base(serviceBairro)
        {
            _serviceBairro = serviceBairro;
        }
    }
}
