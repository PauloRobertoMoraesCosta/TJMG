using System.Windows;
using Ninject;
using TJ.Apresentacao;

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
    }

}
