using System;
using System.Windows;
using Ninject;
using Ninject.Activation;
using TJ.Apresentacao;
using TJ.Apresentacao.InterfacesApp;
using TJ.Dominio.Entidades;

namespace TJ.View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected MinhaNinject ninject;

        public App()
        {
            ninject = new MinhaNinject();
        }

        private void OnAppStartup(object sender, StartupEventArgs e)
        {
            Login login = MinhaNinject.Kernel.Get<Login>();
            login.Show();
            login.inicializar();
        }

        private void OnAppStartup2(object sender, StartupEventArgs e)
        {
            IAppServiceCidade serviceCidade = MinhaNinject.Kernel.Get<IAppServiceCidade>();
            IAppServiceBairro serviceBairro = MinhaNinject.Kernel.Get<IAppServiceBairro>();
            IAppServiceCumprimento serviceCumprimento = MinhaNinject.Kernel.Get<IAppServiceCumprimento>();
            IAppServiceEntidade serviceEntidade = MinhaNinject.Kernel.Get<IAppServiceEntidade>();
            IAppServiceEstado serviceEstado = MinhaNinject.Kernel.Get<IAppServiceEstado>();
            IAppServiceSentenciado serviceSentenciado = MinhaNinject.Kernel.Get<IAppServiceSentenciado>();
            IAppServiceSentenciadoEntidade serviceSentenciadoEntidade =
                MinhaNinject.Kernel.Get<IAppServiceSentenciadoEntidade>();
            IAppServiceUsuario serviceUsuario = MinhaNinject.Kernel.Get<IAppServiceUsuario>();
            IAppServiceJesp serviceJesp = MinhaNinject.Kernel.Get<IAppServiceJesp>();
            Usuario usuario = serviceUsuario.logaUsuario("Paulo", "123");
            if (usuario != null)
            {
                Current.MainWindow = new WpfTelaPrincipal(serviceCidade, serviceBairro, serviceCumprimento,
                    serviceEntidade, serviceEstado, serviceSentenciado, serviceSentenciadoEntidade, serviceUsuario, serviceJesp,
                    usuario);
                Current.MainWindow.Show();
            }
        }
    }
}
