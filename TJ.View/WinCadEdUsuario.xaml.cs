using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TJ.Apresentacao;
using TJ.Apresentacao.InterfacesApp;
using TJ.Dominio.Entidades;

namespace TJ.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class WinCadEdUsuario : Window
    {
        private readonly UCUsuariosLista _pai;
        private Usuario usuarioSelecionado;

        #region "Construtores"
        public WinCadEdUsuario(UCUsuariosLista pai)
        {
            _pai = pai;
            InitializeComponent();
            cbxAtivo.IsChecked = true;
        }

        public WinCadEdUsuario(Usuario usuario, UCUsuariosLista pai)
        {
            usuarioSelecionado = usuario;
            _pai = pai;
            InitializeComponent();
            popularFormulario(usuarioSelecionado);
            lblDataCadastro.Visibility = Visibility.Visible;
            cbxAtivo.Visibility = Visibility.Visible;
            Title = "Alteração de usuário";
            btnGravar.Content = "Alterar";
            tbxLogin.IsEnabled = false;
        }
        #endregion

        #region "Eventos disparados"
        private void btnGravar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Validacoes.validarCampos(new List<Control>() { tbxLogin, tbxNome, pswSenha }))
                    (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowInformation("Favor informar os dados obrigatórios.");
                else
                {
                    Usuario usuarioBanco;
                    if (usuarioSelecionado == null)
                    {
                        using (IAppServiceUsuario _serviceUsuario = MinhaNinject.Kernel.Get<IAppServiceUsuario>())
                        {
                            usuarioBanco = _serviceUsuario.RetornarPorLogin(tbxLogin.Text);
                        }
                        if (usuarioBanco != null)
                            (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowInformation(String.Format("Favor informar outro login, pois {0} já foi utilizado.", usuarioBanco.Login));
                        else
                        {
                            using (IAppServiceUsuario _serviceUsuario = MinhaNinject.Kernel.Get<IAppServiceUsuario>())
                            {
                                _serviceUsuario.Adiciona(popularUser(new Usuario()));
                            }
                            (_pai as UCUsuariosLista).carregaGridUsuario();
                            Close();
                            (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowSuccess("Usuário cadastrado com sucesso");
                        }
                    }
                    else
                    {
                        IEnumerable<Usuario> usuariosAtivos;
                        using (IAppServiceUsuario _serviceUsuario = MinhaNinject.Kernel.Get<IAppServiceUsuario>())
                        {
                            usuariosAtivos = _serviceUsuario.RetornaUsuariosAtivosAsNoTracking();
                            if (cbxAtivo.IsChecked == false && !usuarioSelecionado.Ativo.Equals(cbxAtivo.IsChecked.ToString(), StringComparison.OrdinalIgnoreCase) && usuariosAtivos.Count() <= 1)
                                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowInformation("Para inativar esse usuário, você deve primeiro ativar outro.");
                            else
                            {
                                usuarioSelecionado = (popularUser(_serviceUsuario.RetornaPorId(usuarioSelecionado.Id)));
                                _serviceUsuario.Alterar(usuarioSelecionado);
                                (_pai as UCUsuariosLista).carregaGridUsuario();
                                Close();
                                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowSuccess("Usuário alterado com sucesso");
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError(usuarioSelecionado == null ? "Ao gravar o usuário: " + exception.Message : "Ao alterar o usuário: " + exception.Message);
            }
        }

        private void tbxLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if ((e.Source as TextBox).Text.Any())
                    (e.Source as TextBox).BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999"));
            }
            catch (Exception ex)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError(ex.Message);
            }
        }

        private void pswSenha_PasswordChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((e.Source as PasswordBox).Password.Any())
                    (e.Source as PasswordBox).BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999"));
            }
            catch (Exception ex)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError(ex.Message);
            }
        }

        private void lblCancelar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError(ex.Message);
            }
        }

        private void lblCancelar_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                lblCancelar.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF0D9Cfa"));
                lblCancelar.FontWeight = FontWeights.Normal;
            }
            catch (Exception ex)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError(ex.Message);
            }
        }

        private void lblCancelar_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                lblCancelar.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF0D9Cff"));
                lblCancelar.FontWeight = FontWeights.Bold;
            }
            catch (Exception ex)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError(ex.Message);
            }
        }
        #endregion

        #region "Mêtodos diversos"
        private void popularFormulario(Usuario usuario)
        {
            tbxLogin.Text = usuario.Login;
            pswSenha.Password = usuario.Senha;
            tbxNome.Text = usuario.Nome;
            tbxDadosRegistro.Text = usuario.DadosRegistro;
            cbxSuper.IsChecked = Convert.ToBoolean(usuario.Super);
            cbxAtivo.IsChecked = Convert.ToBoolean(usuario.Ativo);
            lblDataCadastro.Content = String.Format("Cadastrado em {0} às {1}", usuario.DataCadastro.ToString().Substring(0, 10), usuario.DataCadastro.ToString().Substring(11, 5));
        }

        private Usuario popularUser(Usuario user)
        {
            if (usuarioSelecionado == null)
            {
                user.Login = tbxLogin.Text;
                user.DataCadastro = DateTime.Now.Date;
            }
            user.Senha = pswSenha.Password;
            user.Nome = tbxNome.Text;
            user.DadosRegistro = tbxDadosRegistro.Text;
            user.Super = cbxSuper.IsChecked.ToString();
            user.Ativo = cbxAtivo.IsChecked.ToString();
            return user;
        }
        #endregion
    }
}
