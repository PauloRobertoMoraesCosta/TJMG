﻿using System.Collections.Generic;
using TJ.Dominio.Entidades;
using TJ.Dominio.Interfaces.Repositorios;
using TJ.Dominio.Interfaces.Servicos;

namespace TJ.Dominio.Servicos
{
    public class ServicoSentenciadoEntidade : ServicoBase<SentenciadoEntidade>, IServicoSentenciadoEntidade
    {
        protected readonly IRepositorioSentenciadoEntidade _reposiotorioSentenciadoEntidade;
        public ServicoSentenciadoEntidade(IRepositorioSentenciadoEntidade repositorioSentenciadoEntidade)
            : base(repositorioSentenciadoEntidade)
        {
            _reposiotorioSentenciadoEntidade = repositorioSentenciadoEntidade;
        }

        public IEnumerable<SentenciadoEntidade> RetornarPorSentenciado(int sentenciadoId)
        {
            return _reposiotorioSentenciadoEntidade.RetornarPorSentenciado(sentenciadoId);
        }
    }
}