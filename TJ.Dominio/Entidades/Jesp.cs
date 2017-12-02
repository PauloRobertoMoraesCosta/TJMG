namespace TJ.Dominio.Entidades
{
    public class Jesp
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string HorarioDeFuncionamento { get; set; }
        public int BairroId { get; set; }
        public int CidadeId { get; set; }
        public Bairro Bairro { get; set; }
        public Cidade Cidade { get; set; }
    }
}
