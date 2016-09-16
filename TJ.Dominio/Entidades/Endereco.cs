namespace TJ.Dominio.Entidades
{
    public class Endereco
    {
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string PontoReferencia { get; set; }
        public string Responsavel { get; set; }
        public string Telefones { get; set; }
        public int BairroId { get; set; }
        public int CidadeId { get; set; }
        public int EntidadeId { get; set; }


        public virtual Cidade Cidade { get; set; }
        public virtual Entidade Entidade { get; set; }
        public virtual Bairro Bairro { get; set; }
    }
}
