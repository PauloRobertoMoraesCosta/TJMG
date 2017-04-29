using System.Collections.Generic;
using TJ.Apresentacao.InterfacesApp;
using TJ.Dominio.Entidades;
using TJ.Dominio.Interfaces.Servicos;

namespace TJ.Apresentacao.ServiceApp
{
    public class AppServiceCumprimento : AppServiceBase<Cumprimento>, IAppServiceCumprimento
    {
        protected readonly IServicoCumprimento _serviceCumprimento;
        public AppServiceCumprimento(IServicoCumprimento servicoCumprimento
            )
            : base(servicoCumprimento)
        {
            _serviceCumprimento = servicoCumprimento;
        }

        public IEnumerable<Cumprimento> RetornarPorSentenciado(int sentenciadoId)
        {
            return _serviceCumprimento.RetornarPorSentenciado(sentenciadoId);
        }
    }
}
