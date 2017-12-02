using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TJ.Apresentacao.InterfacesApp;
using TJ.Dominio.Entidades;
using System.Data.Entity.Infrastructure;
using TJ.Apresentacao;
using Ninject;

namespace TJ.View
{
    /// <summary>
    /// Interaction logic for UCUsuariosLista.xaml
    /// </summary>
    public partial class UCUsuariosLista : UserControl
    {
        private Usuario usuarioSelecionado;

        #region "Construtores"
        public UCUsuariosLista()
        {
            InitializeComponent();
            carregaGridUsuario();
        }
        #endregion

        #region "Eventos disparados"
        private void btnNovo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WinCadEdUsuario cadEdUsuario = new WinCadEdUsuario(this);
                cadEdUsuario.ShowDialog();
            }
            catch (Exception exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError("Ao iniciar a inclusão de novo usuário: " + exception.Message);
            }
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgvUsuarios.SelectedItems.Count > 0)
                {
                    if (Mensagens.MensagemConfirmOkCancel("Deseja excluir os usuários selecionados caso isso seja possível?") == MessageBoxResult.OK)
                    {
                        for (int i = 0; i < dgvUsuarios.SelectedItems.Count; i++)
                        {
                            using (IAppServiceUsuario _serviceUsuario = MinhaNinject.Kernel.Get<IAppServiceUsuario>())
                            {
                                usuarioSelecionado = _serviceUsuario.RetornaPorId((dgvUsuarios.SelectedItems[i] as Usuario).Id);

                                if (usuarioSelecionado.SentenciadosCadastro.Count() > 0 || usuarioSelecionado.SentenciadosAlteracao.Count > 0)
                                {
                                    (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowInformation(string.Format("O usuário {0} foi utilizado em algum reeducando, por isso não posso excluí-lo.", usuarioSelecionado.Nome));
                                }
                                else if (usuarioSelecionado.EntidadesCadastro.Count() > 0 || usuarioSelecionado.EntidadesAlteracao.Count > 0)
                                {
                                    (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowInformation(string.Format("O usuário {0} foi utilizado em alguma entidade, por isso não posso excluí-lo.", usuarioSelecionado.Nome));
                                }
                                else
                                {
                                    _serviceUsuario.Remover(usuarioSelecionado);
                                    (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowSuccess("Usuário excluido com sucesso.");
                                }
                            }
                        }
                        carregaGridUsuario();
                    }
                    else
                        dgvUsuarios.SelectedItem = null;
                }
                else
                    (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowInformation("Você deve selecionar ao menos um usuário.");
            }
            catch (DbUpdateException dbUpdateException)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError("Ao excluir usuário: O mesmo deve ter sido utilizado em algum cadastro." + dbUpdateException.Message);
            }
            catch (Exception exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError("Ao excluir usuário: " + exception.Message);
            }
        }

        private void dgvUsuarios_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dgvUsuarios.SelectedItems.Count == 1)
                {
                    usuarioSelecionado = dgvUsuarios.SelectedItem as Usuario;
                    WinCadEdUsuario cadEdUsuario = new WinCadEdUsuario(usuarioSelecionado, this);
                    cadEdUsuario.ShowDialog();
                }
                else
                    (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowInformation("Para editar algum usuário, clique duas vezes no mesmo!");
            }
            catch (Exception exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError("Ao clicar duas vezes na lista: " + exception.Message);
            }
        }

        private void dgvUsuarios_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!btnExcluir.IsKeyboardFocused && !dgvUsuarios.IsKeyboardFocusWithin)
                {
                    dgvUsuarios.SelectedItem = null;
                }
            }
            catch (Exception exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError("Ao perder o foco: " + exception.Message);
            }
        }
        #endregion

        #region "Mêtodos diversos"

        public void carregaGridUsuario()
        {
            using (IAppServiceUsuario _serviceUsuario = MinhaNinject.Kernel.Get<IAppServiceUsuario>())
            {
                dgvUsuarios.ItemsSource = _serviceUsuario.RetornaTodos().OrderBy(u => u.Login).ToList();
            }
        }
        #endregion

    }
}
