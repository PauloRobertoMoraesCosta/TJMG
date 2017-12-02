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
    public partial class WinCadEdEntidade : Window
    {
        private Usuario usuarioLogado;
        private UCEntidadesLista _pai;
        private Entidade entidadeSelecionada;

        #region "Construtores"
        public WinCadEdEntidade(Usuario usuario, UCEntidadesLista pai)
        {
            usuarioLogado = usuario;
            _pai = pai;
            InitializeComponent();
            carregaDadosIniciais();
            cbxAtivo.Visibility = Visibility.Hidden;
            cbxAtivo.IsChecked = true;
            lblInclusao.Visibility = Visibility.Hidden;
            lblUsuarioInclusao.Visibility = Visibility.Hidden;
            lblUsuarioEdicao.Visibility = Visibility.Hidden;
            lblEdicao.Visibility = Visibility.Hidden;
        }

        public WinCadEdEntidade(Usuario usuario, UCEntidadesLista pai, Entidade entidade)
        {
            usuarioLogado = usuario;
            _pai = pai;
            entidadeSelecionada = entidade;
            InitializeComponent();
            carregaDadosIniciais();
            populaTelaEntidade(entidade);
            lblDataCadastro.Visibility = Visibility.Visible;
            Title = "Alteração de entidade";
            btnGravar.Content = "Alterar";
            lblEdicao.Visibility = entidade.UsuarioAlteracaoId > 0 ? Visibility.Visible : Visibility.Hidden;
        }
        #endregion

        #region "Eventos disparados"
        private void btnGravaEntidade_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (entidadeSelecionada == null)
                {
                    if (!Validacoes.validarCampos(new List<Control>() { tbxNome, tbxAtividadePrincipal, tbxEndereco, cbxBairro, cbxCidade, tbxTelefones }))
                        (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowInformation("Favor informar os dados obrigatórios.");
                    else
                    {
                        using (IAppServiceEntidade serviceEntidade = MinhaNinject.Kernel.Get<IAppServiceEntidade>())
                        {
                            serviceEntidade.Adiciona(popularEntidade(new Entidade()));
                        }
                        (_pai as UCEntidadesLista).carregarDgvEntidade();
                        Close();
                        (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowSuccess("Entidade cadastrada com sucesso");
                    }
                }
                else
                {
                    if (!Validacoes.validarCampos(new List<Control>() { tbxNome, tbxAtividadePrincipal, tbxEndereco, cbxBairro, cbxCidade, tbxTelefones }))
                        (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowInformation("Favor informar os dados obrigatórios.");
                    else
                    {
                        using (IAppServiceEntidade serviceEntidade = MinhaNinject.Kernel.Get<IAppServiceEntidade>())
                        {
                            serviceEntidade.Alterar(popularEntidade(serviceEntidade.RetornaPorId(entidadeSelecionada.Id)));
                        }
                        (_pai as UCEntidadesLista).carregarDgvEntidade();
                        Close();
                        (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowSuccess("Entidade alterada com sucesso");
                    }
                }
            }
            catch (Exception exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError("Ao gravar a entidade: " + exception.Message);
            }
        }


        private void tbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if ((e.Source as TextBox).Text.Any())
                    (e.Source as TextBox).BorderBrush = new SolidColorBrush(Colors.Blue);
            }
            catch (Exception exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError(exception.Message);
            }
        }

        private void cbx_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.Source as ComboBox).SelectedIndex != -1)
                    (e.Source as ComboBox).BorderBrush = new SolidColorBrush(Colors.Blue);
            }
            catch (Exception exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError(exception.Message);
            }
        }

        private void cbx_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                if ((sender as ComboBox).SelectedIndex != -1)
                    (sender as ComboBox).BorderBrush = new SolidColorBrush(Colors.Blue);
            }
            catch (Exception exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError(exception.Message);
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
        private Entidade popularEntidade(Entidade entidade)
        {
            entidade.Nome = tbxNome.Text;
            entidade.AtividadePrincipal = tbxAtividadePrincipal.Text;
            entidade.Endereco = tbxEndereco.Text;
            entidade.CidadeId = (cbxCidade.SelectedItem as Cidade).Id;
            entidade.BairroId = (cbxBairro.SelectedItem as Bairro).Id;
            entidade.PontoReferencia = tbxPontoRef.Text;
            entidade.Responsavel = tbxResponsaveis.Text;
            entidade.Ativo = cbxAtivo.IsChecked.ToString();
            entidade.Telefone = tbxTelefones.Text;
            if (entidadeSelecionada == null)
                entidade.UsuarioCadastroId = usuarioLogado.Id;
            else
                entidade.UsuarioAlteracaoId = usuarioLogado.Id;
            return entidade;
        }

        private void populaTelaEntidade(Entidade entidade)
        {
            tbxNome.Text = entidade.Nome;
            tbxAtividadePrincipal.Text = entidade.AtividadePrincipal;
            tbxEndereco.Text = entidade.Endereco;
            tbxPontoRef.Text = entidade.PontoReferencia;
            tbxResponsaveis.Text = entidade.Responsavel;
            tbxTelefones.Text = entidade.Telefone;
            lblUsuarioInclusao.Content = entidade.UsuarioCadastro.Nome;
            lblUsuarioEdicao.Content = entidade.UsuarioAlteracao != null ? entidade.UsuarioAlteracao.Nome : "";
            cbxAtivo.IsChecked = Convert.ToBoolean(entidade.Ativo);
            for (int i = 0; i < cbxBairro.Items.Count; i++)
            {
                if ((cbxBairro.Items[i] as Bairro).Id == entidade.BairroId)
                {
                    cbxBairro.SelectedItem = cbxBairro.Items[i];
                    i = cbxBairro.Items.Count;
                }
            }

            for (int i = 0; i < cbxCidade.Items.Count; i++)
            {
                if ((cbxCidade.Items[i] as Cidade).Id == entidade.CidadeId)
                {
                    cbxCidade.SelectedItem = cbxCidade.Items[i];
                    i = cbxCidade.Items.Count;
                }
            }
        }

        private void carregaDadosIniciais()
        {
            using (IAppServiceBairro serviceBairro = MinhaNinject.Kernel.Get<IAppServiceBairro>())
            {
                cbxBairro.ItemsSource = serviceBairro.RetornaTodosAsNoTracking().OrderBy(b => b.Nome);
            }
            cbxBairro.DisplayMemberPath = "Nome";
            using (IAppServiceCidade serviceCidade = MinhaNinject.Kernel.Get<IAppServiceCidade>())
            {
                cbxCidade.ItemsSource = serviceCidade.RetornaTodosAsNoTracking().OrderBy(c => c.Nome);
            }
            cbxCidade.DisplayMemberPath = "Nome";
        }
            #endregion
        }
}
