using System;
using System.Collections.Generic;

namespace TJ.Dominio.Entidades
{
    public class Sentenciado
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sexo { get; set; }
        public string Processo { get; set; }
        public string Origem { get; set; }
        public int PenaAnos { get; set; }
        public int PenaMeses { get; set; }
        public int PenaDias { get; set; }
        public string Filiacao { get; set; }
        public string EstadoCivil { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string ResponsavelSetor { get; set; }
        public string Endereco { get; set; }
        public int BairroId { get; set; }
        public int CidadeId { get; set; }
        public string PontoReferencia { get; set; }
        public string AtividadePSC { get; set; }
        public string Naturalidade { get; set; }
        public string OcupacaoExperiencia { get; set; }
        public string Escolaridade { get; set; }
        public string StatusPena { get; set; }
        public string Observacao { get; set; }
        public DateTime DataCadastro { get; set; }
        public int Comutacao { get; set; }
        public string ComutacaoObservacao { get; set; }
        public int Detracao { get; set; }
        public string DetracaoObservacao { get; set; }
        public string UsuarioCadastroLogin { get; set; }
        public string UsuarioAlteracaoLogin { get; set; }
        public virtual Cidade Cidade { get; set; }
        public virtual Bairro Bairro { get; set; }
        public virtual ICollection<SentenciadoEntidade> SentenciadoEntidades { get; set; }
    }
}
