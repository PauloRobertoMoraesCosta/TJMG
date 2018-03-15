using System;
using System.Collections.Generic;
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

        public IEnumerable<Entidade> RetornaEntidadesAtivas()
        {
            return _serviceEntidade.RetornaEntidadesAtivas();
        }
    }
}
