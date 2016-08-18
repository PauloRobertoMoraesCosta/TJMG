using TJ.Apresentacao.InterfacesApp;
using TJ.Dominio.Entidades;
using TJ.Dominio.Interfaces.Servicos;

namespace TJ.Apresentacao.ServiceApp
{
    public class AppServiceEntidade : AppServiceBase<Entidade>, IAppServiceEntidade
    {
        private readonly IServicoEntidade _serviceEntidade;

        public AppServiceEntidade(IServicoEntidade serviceEntidade) : base(serviceEntidade)
        {
            _serviceEntidade = serviceEntidade;
        }
    }
}
