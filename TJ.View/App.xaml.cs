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
        private Usuario usuario;

        public App()
        {
            ninject = new MinhaNinject();
        }

        private void OnAppStartup(object sender, StartupEventArgs e)
        {
            Login login = new Login();//MinhaNinject.Kernel.Get<Login>();
            login.Show();
            login.inicializar();
        }

        private void OnAppStartup2(object sender, StartupEventArgs e)
        {
            using (IAppServiceUsuario _serviceUsuario = MinhaNinject.Kernel.Get<IAppServiceUsuario>())
            {
                usuario = _serviceUsuario.logaUsuario("Paulo", "123");
            }

            if (usuario != null)
            {
                Current.MainWindow = new WpfTelaPrincipal(usuario);
                Current.MainWindow.Show();
            }
        }
    }
}
