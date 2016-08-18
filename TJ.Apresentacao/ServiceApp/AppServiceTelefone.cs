using TJ.Apresentacao.InterfacesApp;
using TJ.Dominio.Entidades;
using TJ.Dominio.Interfaces.Servicos;

namespace TJ.Apresentacao.ServiceApp
{
    public class AppServiceTelefone: AppServiceBase<Telefone>, IAppServiceTelefone
    {
        private readonly IServicoTelefone _serviceTelefone;

        public AppServiceTelefone(IServicoTelefone serviceTelefone) : base(serviceTelefone)
        {
            _serviceTelefone = serviceTelefone;
        }
    }
}
