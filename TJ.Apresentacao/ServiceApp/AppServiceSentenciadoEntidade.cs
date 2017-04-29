using System.Collections.Generic;
using TJ.Apresentacao.InterfacesApp;
using TJ.Dominio.Entidades;
using TJ.Dominio.Interfaces.Servicos;

namespace TJ.Apresentacao.ServiceApp
{
    public class AppServiceSentenciadoEntidade : AppServiceBase<SentenciadoEntidade>, IAppServiceSentenciadoEntidade
    {
        protected readonly IServicoSentenciadoEntidade _serviceSentenciadoEntidade;
        public AppServiceSentenciadoEntidade(IServicoSentenciadoEntidade servicoSentenciadoEntidade
            )
            : base(servicoSentenciadoEntidade)
        {
            _serviceSentenciadoEntidade = servicoSentenciadoEntidade;
        }

        public IEnumerable<SentenciadoEntidade> RetornarPorSentenciado(int sentenciadoId)
        {
            return _serviceSentenciadoEntidade.RetornarPorSentenciado(sentenciadoId);
        }
    }
}
