namespace TJ.Dominio.Entidades
{
    public class Endereco
    {
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public virtual Cidade Cidade { get; set; }
        public virtual Entidade Entidade { get; set; }
    }
}
