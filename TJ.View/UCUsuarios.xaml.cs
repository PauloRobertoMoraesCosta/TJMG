using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TJ.Apresentacao.InterfacesApp;

namespace TJ.View
{
    /// <summary>
    /// Interaction logic for UCMovimentacoesLencoes.xaml
    /// </summary>
    public partial class UCUsuarios : UserControl
    {
        protected readonly IAppServiceUsuario _serviceUsuario;
        public UCUsuarios(IAppServiceUsuario serviceUsuario)
        {
            _serviceUsuario = serviceUsuario;
            InitializeComponent();
            dgvUsuarios.ItemsSource = _serviceUsuario.RetornaTodos();
        }
    }
}
