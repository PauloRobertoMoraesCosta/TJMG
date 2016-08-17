using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

        #region "Construtores"
        public UCUsuarios(IAppServiceUsuario serviceUsuario)
        {
            try
            {
                _serviceUsuario = serviceUsuario;
                InitializeComponent();
                alterEnableForm(false);
                dgvUsuarios.ItemsSource = _serviceUsuario.RetornaTodos().OrderBy(u => u.Login);
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

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgvUsuarios.SelectedItems.Count == 1)
                {
                    populaFormulario((dgvUsuarios.SelectedItem as Usuario).Login, (dgvUsuarios.SelectedItem as Usuario).Senha, (dgvUsuarios.SelectedItem as Usuario).Nome, (dgvUsuarios.SelectedItem as Usuario).DataCadastro.ToString().Substring(0, 10), Convert.ToBoolean((dgvUsuarios.SelectedItem as Usuario).Super), Convert.ToBoolean((dgvUsuarios.SelectedItem as Usuario).Ativo));
                    alterEnableForm(true);
                    tbxLogin.IsEnabled = false;
                    incluindo = false;
                }
                else
                Mensagens.MensagemAlertaOk("Por favor, selecione um registro!");
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Erro ao carregar dados: " + exception.Message);
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
                        dgvUsuarios.ItemsSource = _serviceUsuario.RetornaTodos().OrderBy(u => u.Login);
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
                    _serviceUsuario.Adiciona(criaNewUsuario());
                    dgvUsuarios.ItemsSource = _serviceUsuario.RetornaTodos().OrderBy(u => u.Login);
                    incluindo = false;
                    alterEnableForm(false);
                    btnNovo.IsEnabled = true;
                    limpaText();
                }
                else
                {
                    Usuario usuarioBanco = _serviceUsuario.RetornarPorLogin(tbxLogin.Text);
                    _serviceUsuario.Alterar(popularUser(usuarioBanco));
                    dgvUsuarios.ItemsSource = _serviceUsuario.RetornaTodos().OrderBy(u => u.Login);
                    alterEnableForm(false);
                    limpaText();
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
                    populaFormulario((dgvUsuarios.SelectedItem as Usuario).Login, (dgvUsuarios.SelectedItem as Usuario).Senha, (dgvUsuarios.SelectedItem as Usuario).Nome, (dgvUsuarios.SelectedItem as Usuario).DataCadastro.ToString().Substring(0, 10), Convert.ToBoolean((dgvUsuarios.SelectedItem as Usuario).Super), Convert.ToBoolean((dgvUsuarios.SelectedItem as Usuario).Ativo));
                    alterEnableForm(true);
                    tbxLogin.IsEnabled = false;
                    incluindo = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region "Mêtodos diversos"
        private void populaFormulario(string login, string password, string nome, string dataCadastro, bool super, bool ativo)
        {
            try
            {
                tbxLogin.Text = login;
                pswSenha.Password = password;
                tbxNome.Text = nome;
                tbxDataCadastro.Text = dataCadastro;
                cbxSuper.IsChecked = super;
                cbxAtivo.IsChecked = ativo;
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
                        (griddados.Children[i] as TextBox).Text = "";
                    if (griddados.Children[i] is PasswordBox)
                        (griddados.Children[i] as PasswordBox).Password = "";
                    if (griddados.Children[i] is CheckBox)
                        (griddados.Children[i] as CheckBox).IsChecked = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private Usuario popularUser(Usuario user)
        {
            user.Senha = pswSenha.Password;
            user.Nome = tbxNome.Text;
            user.DataCadastro = Convert.ToDateTime(tbxDataCadastro.Text);
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
            newUser.Super = cbxSuper.IsChecked.ToString();
            newUser.Ativo = cbxAtivo.IsChecked.ToString();
            return newUser;
        }
        #endregion
    }
}
