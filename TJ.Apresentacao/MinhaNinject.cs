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
            kernel.Bind<IAppServiceCidade>().To<AppServiceCidade>().InTransientScope();
            kernel.Bind<IAppServiceEndereco>().To<AppServiceEndereco>().InTransientScope();
            kernel.Bind<IAppServiceEntidade>().To<AppServiceEntidade>().InTransientScope();
            kernel.Bind<IAppServiceEstado>().To<AppServiceEstado>().InTransientScope();
            kernel.Bind<IAppServiceBairro>().To<AppServiceBairro>().InTransientScope();

            kernel.Bind(typeof(IServicoBase<>)).To(typeof(ServicoBase<>)).InTransientScope();
            kernel.Bind<IServicoUsuario>().To<ServicoUsuario>().InTransientScope();
            kernel.Bind<IServicoCidade>().To<ServicoCidade>().InTransientScope();
            kernel.Bind<IServicoEndereco>().To<ServicoEndereco>().InTransientScope();
            kernel.Bind<IServicoEntidade>().To<ServicoEntidade>().InTransientScope();
            kernel.Bind<IServicoEstado>().To<ServicoEstado>().InTransientScope();
            kernel.Bind<IServicoBairro>().To<ServicoBairro>().InTransientScope();

            kernel.Bind(typeof(IRepositorioBase<>)).To(typeof(RepositorioBase<>)).InTransientScope();
            kernel.Bind<IRepositorioUsuario>().To<RepositorioUsuario>().InTransientScope();
            kernel.Bind<IRepositorioCidade>().To<RepositorioCidade>().InTransientScope();
            kernel.Bind<IRepositorioEndereco>().To<RepositorioEndereco>().InTransientScope();
            kernel.Bind<IRepositorioEntidade>().To<RepositorioEntidade>().InTransientScope();
            kernel.Bind<IRepositorioEstado>().To<RepositorioEstado>().InTransientScope();
            kernel.Bind<IRepositorioBairro>().To<RepositorioBairro>().InTransientScope();
        }
    }
}
