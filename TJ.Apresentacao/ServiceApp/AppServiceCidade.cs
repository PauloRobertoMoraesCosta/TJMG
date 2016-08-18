using TJ.Apresentacao.InterfacesApp;
using TJ.Dominio.Entidades;
using TJ.Dominio.Interfaces.Servicos;

namespace TJ.Apresentacao.ServiceApp
{
    public class AppServiceCidade : AppServiceBase<Cidade>, IAppServiceCidade
    {
        private readonly IServicoCidade _serviceCidade;

        public AppServiceCidade(IServicoCidade serviceCidade) : base(serviceCidade)
        {
            _serviceCidade = serviceCidade;
        }
    }
}
