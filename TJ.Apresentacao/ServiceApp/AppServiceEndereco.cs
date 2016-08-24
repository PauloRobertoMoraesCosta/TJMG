﻿using TJ.Apresentacao.InterfacesApp;
using TJ.Dominio.Entidades;
using TJ.Dominio.Interfaces.Servicos;

namespace TJ.Apresentacao.ServiceApp
{
    public class AppServiceEndereco : AppServiceBase<Endereco>, IAppServiceEndereco
    {
        private readonly IServicoEndereco _serviceEndereco;

        public AppServiceEndereco(IServicoEndereco serviceEndereco) : base(serviceEndereco)
        {
            _serviceEndereco = serviceEndereco;
        }
    }
}