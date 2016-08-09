using System;
using Ninject;
using TJ.Apresentacao.InterfacesApp;
using TJ.Apresentacao.ServiceApp;
using TJ.Dados.Repositorios;
using TJ.Dominio.Interfaces.Repositorios;
using TJ.Dominio.Interfaces.Servicos;
using TJ.Dominio.Servicos;

namespace TJ.Apresentacao
{
    public class MinhaNinject
    {
        private static IKernel kernel;

        public static IKernel Kernel
        {
            get { return kernel; }
            set { kernel = value; }
        }

        public MinhaNinject()
        {
            Kernel = new StandardKernel();
            kernel.Bind(typeof(IAppServiceBase<>)).To(typeof(AppServiceBase<>)).InTransientScope();
            kernel.Bind<IAppServiceUsuario>().To<AppServiceUsuario>().InTransientScope();

            kernel.Bind(typeof(IServicoBase<>)).To(typeof(ServicoBase<>)).InTransientScope();
            kernel.Bind<IServicoUsuario>().To<ServicoUsuario>().InTransientScope();

            kernel.Bind(typeof(IRepositorioBase<>)).To(typeof(RepositorioBase<>)).InTransientScope();
            kernel.Bind<IRepositorioUsuario>().To<RepositorioUsuario>().InTransientScope();
        }
    }
}
