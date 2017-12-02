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
            kernel.Bind<IAppServiceEntidade>().To<AppServiceEntidade>().InTransientScope();
            kernel.Bind<IAppServiceEstado>().To<AppServiceEstado>().InTransientScope();
            kernel.Bind<IAppServiceBairro>().To<AppServiceBairro>().InTransientScope();
            kernel.Bind<IAppServiceSentenciado>().To<AppServiceSentenciado>().InTransientScope();
            kernel.Bind<IAppServiceSentenciadoEntidade>().To<AppServiceSentenciadoEntidade>().InTransientScope();
            kernel.Bind<IAppServiceCumprimento>().To<AppServiceCumprimento>().InTransientScope();
            kernel.Bind<IAppServiceJesp>().To<AppServiceJesp>().InTransientScope();
            kernel.Bind<IAppServiceCumprimentoMes>().To<AppServiceCumprimentoMes>().InTransientScope();

            kernel.Bind(typeof(IServicoBase<>)).To(typeof(ServicoBase<>)).InTransientScope();
            kernel.Bind<IServicoUsuario>().To<ServicoUsuario>().InTransientScope();
            kernel.Bind<IServicoCidade>().To<ServicoCidade>().InTransientScope();
            kernel.Bind<IServicoEntidade>().To<ServicoEntidade>().InTransientScope();
            kernel.Bind<IServicoEstado>().To<ServicoEstado>().InTransientScope();
            kernel.Bind<IServicoBairro>().To<ServicoBairro>().InTransientScope();
            kernel.Bind<IServicoSentenciado>().To<ServicoSentenciado>().InTransientScope();
            kernel.Bind<IServicoCumprimento>().To<ServicoCumprimento>().InTransientScope();
            kernel.Bind<IServicoSentenciadoEntidade>().To<ServicoSentenciadoEntidade>().InTransientScope();
            kernel.Bind<IServicoJesp>().To<ServicoJesp>().InTransientScope();
            kernel.Bind<IServicoCumprimentoMes>().To<ServicoCumprimentoMes>().InTransientScope();

            kernel.Bind(typeof(IRepositorioBase<>)).To(typeof(RepositorioBase<>)).InTransientScope();
            kernel.Bind<IRepositorioUsuario>().To<RepositorioUsuario>().InTransientScope();
            kernel.Bind<IRepositorioCidade>().To<RepositorioCidade>().InTransientScope();
            kernel.Bind<IRepositorioEntidade>().To<RepositorioEntidade>().InTransientScope();
            kernel.Bind<IRepositorioEstado>().To<RepositorioEstado>().InTransientScope();
            kernel.Bind<IRepositorioBairro>().To<RepositorioBairro>().InTransientScope();
            kernel.Bind<IRepositorioSentenciado>().To<RepositorioSentenciado>().InTransientScope();
            kernel.Bind<IRepositorioSentenciadoEntidade>().To<RepositorioSentenciadoEntidade>().InTransientScope();
            kernel.Bind<IRepositorioCumprimento>().To<RepositorioCumprimento>().InTransientScope();
            kernel.Bind<IRepositorioJesp>().To<RepositorioJesp>().InTransientScope();
            kernel.Bind<IRepositorioCumprimentoMes>().To<RepositorioCumprimentoMes>().InTransientScope();
        }
    }
}
