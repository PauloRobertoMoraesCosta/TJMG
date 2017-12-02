using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TJ.Dados.Contexto;
using TJ.Dados.Verifications;
using TJ.Dominio.Entidades;
using TJ.Dominio.Interfaces.Repositorios;

namespace TJ.Dados.Repositorios
{
    public class RepositorioCumprimento : RepositorioBase<Cumprimento>, IRepositorioCumprimento
    {
        
    }
}
