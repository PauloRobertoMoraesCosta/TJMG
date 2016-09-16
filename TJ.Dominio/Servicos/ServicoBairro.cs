using TJ.Dominio.Entidades;
using TJ.Dominio.Interfaces.Repositorios;
using TJ.Dominio.Interfaces.Servicos;

namespace TJ.Dominio.Servicos
{
    public class ServicoBairro : ServicoBase<Bairro>, IServicoBairro
    {
        protected readonly IRepositorioBairro _repositorioBairro;

        public ServicoBairro(IRepositorioBairro repositorioBairro)
            : base(repositorioBairro)
        {
            _repositorioBairro = repositorioBairro;
        }
    }
}
