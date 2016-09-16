using System.Collections.Generic;

namespace TJ.Dominio.Entidades
{
    public class Cidade
    {
        public Cidade()
        {
            this.Enderecos = new List<Endereco>();
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public string EstadoSigla { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual ICollection<Endereco> Enderecos { get; set; }
    }
}
