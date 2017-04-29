using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TJ.Apresentacao.InterfacesApp;
using TJ.Dominio.Entidades;

namespace TJ.View
{
    /// <summary>
    /// Interaction logic for UCMovimentacoesLencoes.xaml
    /// </summary>
    public partial class UCUsuarios : UserControl
    {
        protected readonly IAppServiceUsuario _serviceUsuario;
        private bool incluindo;
        private Usuario usuarioSelecionado;
        private IEnumerable<Usuario> usuariosBanco;

        #region "Construtores"
        public UCUsuarios(IAppServiceUsuario serviceUsuario)
        {
            try
            {
                _serviceUsuario = serviceUsuario;
                InitializeComponent();
                alterEnableForm(false);
                carregaGridUsuario();
            }
            catch (Exception exception)
            {
                throw new Exception("Aconteceu algo errado ao iniciar a tela de Usuários: " + exception.Message);
            }
        }
        #endregion

        #region "Eventos disparados"
        private void btnNovo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                incluindo = true;
                limpaText();
                alterEnableForm(true);
                tbxDataCadastro.Text = DateTime.Now.Date.ToString().Substring(0, 10);
                cbxAtivo.IsChecked = true;
                btnNovo.IsEnabled = false;
                tbxLogin.Focus();
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Erro ao clicar no botão Novo: " + exception.Message);
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                incluindo = false;
                alterEnableForm(false);
                btnNovo.IsEnabled = true;
                limpaText();
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Erro ao clicar no botão Cancelar: " + exception.Message);
            }
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgvUsuarios.SelectedItems.Count > 0)
                {
                    if (Mensagens.MensagemConfirmOkCancel("Quer excluir os usuários selecionados caso isso seja possível?") == MessageBoxResult.OK)
                    {
                        for (int i = 0; i < dgvUsuarios.SelectedItems.Count; i++)
                        {
                            _serviceUsuario.Remover((dgvUsuarios.SelectedItems[i] as Usuario));
                        }
                        carregaGridUsuario();
                        alterEnableForm(false);
                        btnNovo.IsEnabled = true;
                        limpaText();
                    }
                }
                else
                    Mensagens.MensagemErroOk("Você deve selecionar ao menos um usuário!");
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Erro ao excluir usuário: " + exception.Message);
            }
        }

        private void btnGravar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (incluindo)
                {
                    if (!Validacoes.validarCampos(new List<Control>() { tbxLogin, tbxNome, pswSenha }))
                        Mensagens.MensagemAlertaOk("Favor informar os dados obrigatórios.");
                    else
                    {
                        Usuario usuarioComLoginRepetido = _serviceUsuario.RetornarPorLogin(tbxLogin.Text);
                        if (usuarioComLoginRepetido != null)
                            Mensagens.MensagemAlertaOk("Favor informar outro login pois esse já foi utilizado.");
                        else
                        {
                            _serviceUsuario.Adiciona(criaNewUsuario());
                            carregaGridUsuario();
                            incluindo = false;
                            alterEnableForm(false);
                            btnNovo.IsEnabled = true;
                            limpaText();
                        }
                    }
                }
                else
                {
                    if (!Validacoes.validarCampos(new List<Control>() { tbxLogin, tbxNome, pswSenha }))
                        Mensagens.MensagemAlertaOk("Favor informar os dados obrigatórios.");
                    else
                    {
                        Usuario usuarioBanco = _serviceUsuario.RetornarPorLogin(tbxLogin.Text);
                        _serviceUsuario.Alterar(popularUser(usuarioBanco));
                        carregaGridUsuario();
                        alterEnableForm(false);
                        btnNovo.IsEnabled = true;
                        limpaText();
                    }
                }
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Erro ao gravar o usuário: " + exception.Message);
            }
        }

        private void dgvUsuarios_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dgvUsuarios.SelectedItems.Count == 1)
                {
                    usuarioSelecionado = dgvUsuarios.SelectedItem as Usuario;
                    populaFormulario(usuarioSelecionado);
                    alterEnableForm(true);
                    tbxLogin.IsEnabled = false;
                    btnNovo.IsEnabled = false;
                    incluindo = false;
                }
                else
                    Mensagens.MensagemAlertaOk("Para editar algum usuário, clique duas vezes no mesmo!");
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Algo inesperado aconteceu ao clicar duas vezes no Grid: " + exception.Message);
            }
        }

        private void tbxLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((e.Source as TextBox).Text.Any())
                (e.Source as TextBox).BorderBrush = new SolidColorBrush(Colors.Blue);
        }

        private void pswSenha_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if ((e.Source as PasswordBox).Password.Any())
                (e.Source as PasswordBox).BorderBrush = new SolidColorBrush(Colors.Blue);
        }
        #endregion

        #region "Mêtodos diversos"

        private void carregaGridUsuario()
        {
            dgvUsuarios.ItemsSource = _serviceUsuario.RetornaTodos().OrderBy(u => u.Login);
        }
        private void populaFormulario(Usuario usuario)
        {
            try
            {
                tbxLogin.Text = usuario.Login;
                pswSenha.Password = usuario.Senha;
                tbxNome.Text = usuario.Nome;
                tbxDataCadastro.Text = usuario.DataCadastro.ToString();
                tbxDadosRegistro.Text = usuario.DadosRegistro;
                cbxSuper.IsChecked = Convert.ToBoolean(usuario.Super);
                cbxAtivo.IsChecked = Convert.ToBoolean(usuario.Ativo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void alterEnableForm(bool status)
        {
            try
            {
                for (int i = 0; i < griddados.Children.Count; i++)
                {
                    if (griddados.Children[i] is TextBox || griddados.Children[i] is CheckBox ||
                        griddados.Children[i] is PasswordBox)
                        griddados.Children[i].IsEnabled = status;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void limpaText()
        {
            try
            {
                for (int i = 0; i < griddados.Children.Count; i++)
                {
                    if (griddados.Children[i] is TextBox)
                    {
                        (griddados.Children[i] as TextBox).Text = "";
                        (griddados.Children[i] as TextBox).BorderBrush = new SolidColorBrush(Colors.Blue);
                    }
                    if (griddados.Children[i] is PasswordBox)
                    {
                        (griddados.Children[i] as PasswordBox).Password = "";
                        (griddados.Children[i] as PasswordBox).BorderBrush = new SolidColorBrush(Colors.Blue);
                    }
                    if (griddados.Children[i] is CheckBox)
                        (griddados.Children[i] as CheckBox).IsChecked = false;
                }
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Problema inesperado: " + exception.Message);
            }
        }

        private Usuario popularUser(Usuario user)
        {
            user.Senha = pswSenha.Password;
            user.Nome = tbxNome.Text;
            user.DataCadastro = Convert.ToDateTime(tbxDataCadastro.Text);
            user.DadosRegistro = tbxDadosRegistro.Text;
            user.Super = cbxSuper.IsChecked.ToString();
            user.Ativo = cbxAtivo.IsChecked.ToString();
            return user;
        }

        private Usuario criaNewUsuario()
        {
            Usuario newUser = new Usuario();
            newUser.Login = tbxLogin.Text;
            newUser.Senha = pswSenha.Password;
            newUser.Nome = tbxNome.Text;
            newUser.DataCadastro = Convert.ToDateTime(tbxDataCadastro.Text);
            newUser.DadosRegistro = tbxDadosRegistro.Text;
            newUser.Super = cbxSuper.IsChecked.ToString();
            newUser.Ativo = cbxAtivo.IsChecked.ToString();
            return newUser;
        }

        #endregion
    }
}
