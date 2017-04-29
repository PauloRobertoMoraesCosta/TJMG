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
    public partial class UCEntidades : UserControl
    {
        protected readonly IAppServiceEntidade _serviceEntidade;
        protected readonly IAppServiceCidade _serviceCidade;
        protected readonly IAppServiceBairro _serviceBairro;
        private Usuario usuarioLogado;
        private bool incluindoEntidade;
        private Entidade entidadeSelecionada;
        private Entidade entidadeCriada;

        #region "Construtores"
        public UCEntidades(IAppServiceEntidade serviceEntidade, IAppServiceCidade serviceCidade, IAppServiceBairro serviceBairro, Usuario usuario)
        {
            try
            {
                _serviceEntidade = serviceEntidade;
                _serviceCidade = serviceCidade;
                _serviceBairro = serviceBairro;
                usuarioLogado = usuario;
                InitializeComponent();
                carregarDadosIniciais();
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Aconteceu algo errado ao iniciar a tela de Entidades: " + exception.Message);
            }
        }
        #endregion

        #region "Mêtodos diversos"
        private void populaEntidade(Entidade entidade)
        {
            tbxNomeEntidade.Text = entidade.Nome;
            tbxAtividadePrincipal.Text = entidade.AtividadePrincipal;
            tbxLogradouro.Text = entidade.Endereco;
            tbxReferencia.Text = entidade.PontoReferencia;
            tbxResponsavel.Text = entidade.Responsavel;
            tbxTelefone.Text = entidade.Telefone;
            tbxUsuarioInclusao.Text = entidade.UsuarioCadastroLogin;
            tbxUsuarioAlteracao.Text = entidade.UsuarioAlteracaoLogin;
            
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

        private void carregarDadosIniciais()
        {
            cbxCidade.ItemsSource = _serviceCidade.RetornaTodosAsNoTracking().OrderBy(c => c.Nome);
            cbxCidade.DisplayMemberPath = "Nome";
            cbxBairro.ItemsSource = _serviceBairro.RetornaTodosAsNoTracking().OrderBy(b => b.Nome);
            cbxBairro.DisplayMemberPath = "Nome";
            carregarDgvEntidade();
            alterEnableForm(false,gridEntidade);
        }
        private void carregarDgvEntidade()
        {
            dgvEntidades.ItemsSource = _serviceEntidade.RetornaTodos().OrderByDescending(e => e.Id);
        }

        private void alterEnableForm(bool status, Grid gridAlterar)
        {
            for (int i = 0; i < gridAlterar.Children.Count; i++)
            {
                if (gridAlterar.Children[i] is TextBox)
                    gridAlterar.Children[i].IsEnabled = status;
                if (gridAlterar.Children[i] is ComboBox)
                    gridAlterar.Children[i].IsEnabled = status;
            }
        }
        private void limpaText(Grid alterarGrid)
        {
            try
            {
                for (int i = 0; i < alterarGrid.Children.Count; i++)
                {
                    if (alterarGrid.Children[i] is TextBox)
                    {
                        (alterarGrid.Children[i] as TextBox).Text = "";
                        (alterarGrid.Children[i] as TextBox).BorderBrush = new SolidColorBrush(Colors.Blue);
                    }
                    if (alterarGrid.Children[i] is ComboBox)
                    {
                        (alterarGrid.Children[i] as ComboBox).SelectedIndex = -1;
                        (alterarGrid.Children[i] as ComboBox).BorderBrush = new SolidColorBrush(Colors.Blue);
                    }
                }

            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Erro inesperado ao limpar texto: " + exception.Message);
            }
        }
        private Entidade popularEntidade(Entidade entidade)
        {
            entidade.Nome = tbxNomeEntidade.Text;
            entidade.AtividadePrincipal = tbxAtividadePrincipal.Text;
            entidade.Endereco = tbxLogradouro.Text;
            entidade.CidadeId = (cbxCidade.SelectedItem as Cidade).Id;
            entidade.BairroId = (cbxBairro.SelectedItem as Bairro).Id;
            entidade.PontoReferencia = tbxReferencia.Text;
            entidade.Responsavel = tbxResponsavel.Text;
            entidade.Telefone = tbxTelefone.Text;
            if (incluindoEntidade)
                entidade.UsuarioCadastroLogin = usuarioLogado.Login;
            else
                entidade.UsuarioAlteracaoLogin = usuarioLogado.Login;
            return entidade;
        }
        #endregion

        #region "Eventos disparados"
        private void btnNovoEntidade_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                incluindoEntidade = true;
                limpaText(gridEntidade);
                alterEnableForm(true, gridEntidade);
                btnNovoEntidade.IsEnabled = false;
                tbxNomeEntidade.Focus();
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Erro ao iniciar a inclusão de nova entidade: " + exception.Message);
            }
        }
        private void dgvEntidades_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dgvEntidades.SelectedItems.Count == 1)
                {
                    entidadeSelecionada = (Entidade)dgvEntidades.SelectedItem;
                    populaEntidade(entidadeSelecionada);
                    alterEnableForm(true, gridEntidade);
                    btnNovoEntidade.IsEnabled = true;
                    incluindoEntidade = false;
                }
                else
                    Mensagens.MensagemAlertaOk("Para editar alguma entidade, clique duas vezes na mesma!");
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Algo aconteceu errado ao clicar duas vezes no Grid: " + exception.Message);
            }
        }

        private void btnCancelaEntidade_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                limpaText(gridEntidade);
                alterEnableForm(false, gridEntidade);
                btnNovoEntidade.IsEnabled = true;
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Ocorreu um erro ao cancelar entidade: " + exception.Message);
            }
        }
        private void btnExcluiEntidade_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgvEntidades.SelectedItems.Count > 0)
                {
                    if (
                        Mensagens.MensagemConfirmOkCancel(
                            "Quer excluir as entidades selecionadas caso isso seja possível?") == MessageBoxResult.OK)
                    {
                        for (int i = 0; i < dgvEntidades.SelectedItems.Count; i++)
                        {
                            _serviceEntidade.Remover((Entidade)dgvEntidades.SelectedItems[i]);
                        }
                        carregarDgvEntidade();
                        btnNovoEntidade.IsEnabled = true;
                        limpaText(gridEntidade);
                        alterEnableForm(false,gridEntidade);
                    }
                }
                else
                    Mensagens.MensagemErroOk("Você deve selecionar ao menos uma entidade!");
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Erro inesperado ao exlcuir o item: " + exception.Message);
            }
        }
        private void    btnGravaEntidade_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (incluindoEntidade)
                {
                    if (!Validacoes.validarCampos(new List<Control>() { tbxNomeEntidade, tbxAtividadePrincipal, tbxLogradouro, cbxBairro, cbxCidade, tbxResponsavel, tbxTelefone }))
                        Mensagens.MensagemAlertaOk("Favor informar os dados obrigatórios.");
                    else
                    {
                        _serviceEntidade.Adiciona(popularEntidade(new Entidade()));
                        carregarDgvEntidade();
                        incluindoEntidade = false;
                        alterEnableForm(false, gridEntidade);
                        btnNovoEntidade.IsEnabled = true;
                        limpaText(gridEntidade);
                    }
                }
                else
                {
                    if (!Validacoes.validarCampos(new List<Control>() { tbxNomeEntidade, tbxAtividadePrincipal, tbxLogradouro, cbxBairro, cbxCidade, tbxResponsavel, tbxTelefone }))
                        Mensagens.MensagemAlertaOk("Favor informar os dados obrigatórios.");
                    else
                    {
                        Entidade EntidadeBanco = _serviceEntidade.RetornaPorId(entidadeSelecionada.Id);
                        _serviceEntidade.Alterar(popularEntidade(EntidadeBanco));
                        carregarDgvEntidade();
                        alterEnableForm(false, gridEntidade);
                        limpaText(gridEntidade);
                    }
                }
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Erro ao gravar a entidade: " + exception.Message);
            }
        }

        private void tbxNomeEntidade_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((e.Source as TextBox).Text.Any())
                (e.Source as TextBox).BorderBrush = new SolidColorBrush(Colors.Blue);
        }

        private void cbxBairro_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.Source as ComboBox).SelectedIndex != -1)
                (e.Source as ComboBox).BorderBrush = new SolidColorBrush(Colors.Blue);
        }

        private void cbxBairro_DropDownClosed(object sender, EventArgs e)
        {
            if (cbxBairro.SelectedIndex != -1)
                cbxBairro.BorderBrush = new SolidColorBrush(Colors.Blue);
        }

        private void cbxCidade_DropDownClosed(object sender, EventArgs e)
        {
            if (cbxCidade.SelectedIndex != -1)
                cbxCidade.BorderBrush = new SolidColorBrush(Colors.Blue);
        }

        private void tbxNumero_KeyDown(object sender, KeyEventArgs e)
        {
            if ((!(e.Key >= Key.D0 && e.Key <= Key.D9) && e.Key != Key.Tab))
                e.Handled = true;
        }

        #endregion
    }
}
