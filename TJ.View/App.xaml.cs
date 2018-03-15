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
            try
            {
                ninject = new MinhaNinject();
            }
            catch (Exception)
            {
                MessageBox.Show("Aconteceu algum problema no App.");
            }
        }

        private void OnAppStartup(object sender, StartupEventArgs e)
        {
            try
            {
                Login login = new Login(); //MinhaNinject.Kernel.Get<Login>();
                login.Show();
                login.inicializar();
            }
            catch (Exception)
            {
                MessageBox.Show("Aconteceu algum problema no onAppStartup.");
            }
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
