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
    public partial class UCEntidades : UserControl
    {
        protected readonly IAppServiceEntidade _serviceEntidade;
        protected readonly IAppServiceEndereco _serviceEndereco;
        protected readonly IAppServiceCidade _serviceCidade;
        protected readonly IAppServiceBairro _serviceBairro;
        private bool incluindo;
        private Endereco enderecoSelecionado;
        private Entidade entidadeSelecionada;

        #region "Construtores"
        public UCEntidades(IAppServiceEntidade serviceEntidade, IAppServiceEndereco serviceEndereco, IAppServiceCidade serviceCidade, IAppServiceBairro serviceBairro)
        {
            try
            {
                _serviceEntidade = serviceEntidade;
                _serviceEndereco = serviceEndereco;
                _serviceCidade = serviceCidade;
                _serviceBairro = serviceBairro;
                InitializeComponent();
                carregarDadosIniciais();
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Aconteceu algo errado ao iniciar a tela de Entidades: " + exception.Message);
            }
        }
        #endregion

        #region "Eventos disparados"
        private void btnNovoEntidade_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                incluindo = true;
                limpaText(gridEntidade);
                limpaText(gridEndereco);
                tbxNomeEntidade.IsEnabled = true;
                alterEnableForm(false, gridEndereco);
                btnNovoEntidade.IsEnabled = false;
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
                    carregarDgvEndereco(entidadeSelecionada.Id);
                    alterEnableForm(true, gridEntidade);
                    btnNovoEntidade.IsEnabled = true;
                    incluindo = false;
                }
                else
                    Mensagens.MensagemAlertaOk("Para editar alguma entidade, clique duas vezes na mesma!");
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Algo aconteceu errado ao clicar duas vezes no Grid: " + exception.Message);
            }
        }
        private void btnNovoEndereco_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                alterEnableForm(true, gridEndereco);
                limpaText(gridEndereco);
                incluindo = true;
                btnNovoEndereco.IsEnabled = false;
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Erro ao clicar no botão novo " + exception.Message);
            }
        }
        private void btnCancelaEntidade_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                limpaText(gridEndereco);
                limpaText(gridEntidade);
                alterEnableForm(false, gridEndereco);
                alterEnableForm(false, gridEntidade);
                dgvEnderecos.Items.Clear();
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
                            if (_serviceEndereco.RetornarPorEntidade(((Entidade)dgvEntidades.SelectedItems[i]).Id) ==
                                null)
                                _serviceEntidade.Remover((Entidade)dgvEntidades.SelectedItems[i]);
                            else
                                Mensagens.MensagemAlertaOk(String.Format("Não é possível exlcuir o item {0} pois o mesmo já foi usado!", ((Entidade)dgvEntidades.SelectedItems[i]).Nome));
                        }
                        carregarDgvEntidade();
                        btnNovoEntidade.IsEnabled = true;
                        limpaText(gridEntidade);
                        limpaText(gridEndereco);
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
        private void btnGravaEntidade_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (incluindo)
                {
                    _serviceEntidade.Adiciona(criaNewEntidade());
                    carregarDgvEntidade();
                    incluindo = false;
                    alterEnableForm(false, gridEntidade);
                    alterEnableForm(false, gridEndereco);
                    btnNovoEntidade.IsEnabled = true;
                    limpaText(gridEntidade);
                    limpaText(gridEndereco);
                }
                else
                {
                    Entidade EntidadeBanco = _serviceEntidade.RetornaPorId(Convert.ToInt32(tbxCodigoEntidade.Text));
                    _serviceEntidade.Alterar(popularEntidade(EntidadeBanco));
                    carregarDgvEntidade();
                    alterEnableForm(false, gridEntidade);
                    alterEnableForm(false, gridEndereco);
                    limpaText(gridEntidade);
                    limpaText(gridEndereco);
                }
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Erro ao gravar a entidade: " + exception.Message);
            }
        }
        private void btnCancelarEndereco_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                limpaText(gridEndereco);
                alterEnableForm(false, gridEndereco);
                btnNovoEndereco.IsEnabled = true;
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Erro ao cancelar o endereço: " + exception.Message);
            }
        }
        private void btnExcluirEndereco_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgvEnderecos.SelectedItems.Count > 0)
                {
                    if (Mensagens.MensagemConfirmOkCancel("Quer excluir os endereços selecionados caso isso seja possível?") == MessageBoxResult.OK)
                    {
                        for (int i = 0; i < dgvEnderecos.SelectedItems.Count; i++)
                        {
                            _serviceEndereco.Remover((dgvEnderecos.SelectedItems[i] as Endereco));
                        }
                        carregarDgvEndereco(entidadeSelecionada.Id);
                        btnNovoEndereco.IsEnabled = true;
                        limpaText(gridEndereco);
                    }
                }
                else
                    Mensagens.MensagemErroOk("Você deve selecionar ao menos um endereço!");
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Erro ao excluir endereço: " + exception.Message);
            }
        }
        private void btnGravarEndereco_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (incluindo)
                {
                    _serviceEndereco.Adiciona(criaNewEndereco());
                    carregarDgvEndereco(entidadeSelecionada.Id);
                    incluindo = false;
                    alterEnableForm(false, gridEndereco);
                    btnNovoEndereco.IsEnabled = true;
                    limpaText(gridEndereco);
                }
                else
                {
                    Endereco EnderecoBanco = _serviceEndereco.RetornaPorId(enderecoSelecionado.Id);
                    _serviceEndereco.Alterar(popularEndereco(EnderecoBanco));
                    carregarDgvEndereco(entidadeSelecionada.Id);
                    alterEnableForm(false, gridEndereco);
                    limpaText(gridEndereco);
                }
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Erro ao gravar o endereço: " + exception.Message);
            }
        }
        private void dgvEnderecos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dgvEnderecos.SelectedItems.Count == 1)
                {
                    enderecoSelecionado = dgvEnderecos.SelectedItem as Endereco;
                    populaEndereco(enderecoSelecionado);
                    carregarDgvEndereco(entidadeSelecionada.Id);
                    alterEnableForm(true, gridEndereco);
                    btnNovoEndereco.IsEnabled = true;
                    incluindo = false;
                }
                else
                    Mensagens.MensagemAlertaOk("Para editar algum endereço, clique duas vezes no mesmo!");
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Algo aconteceu errado ao clicar duas vezes no Grid: " + exception.Message);
            }
        }
        #endregion

        #region "Mêtodos diversos"
        private void populaEntidade(Entidade entidade)
        {
            try
            {
                tbxCodigoEntidade.Text = entidade.Id.ToString();
                tbxNomeEntidade.Text = entidade.Nome;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void populaEndereco(Endereco endereco)
        {
            for (int i = 0; i < cbxBairro.Items.Count; i++)
            {
                if ((cbxBairro.Items[i] as Bairro).Id == endereco.Bairro.Id)
                {
                    cbxBairro.SelectedItem = cbxBairro.Items[i];
                    i = cbxBairro.Items.Count;
                }
            }
            for (int i = 0; i < cbxCidade.Items.Count; i++)
            {
                if ((cbxCidade.Items[i] as Cidade).Id == endereco.Cidade.Id)
                {
                    cbxCidade.SelectedItem = cbxCidade.Items[i];
                    i = cbxCidade.Items.Count;
                }
            }
            tbxLogradouro.Text = endereco.Logradouro;
            tbxNumero.Text = endereco.Numero;
            tbxReferencia.Text = endereco.PontoReferencia;
            tbxResponsavel.Text = endereco.Responsavel;
            tbxTelefone.Text = endereco.Telefones;
        }
        private void carregarDadosIniciais()
        {
            cbxCidade.ItemsSource = _serviceCidade.RetornaTodosAsNoTracking().OrderBy(c => c.Nome);
            cbxCidade.DisplayMemberPath = "Nome";
            cbxBairro.ItemsSource = _serviceBairro.RetornaTodosAsNoTracking().OrderBy(b => b.Nome);
            cbxBairro.DisplayMemberPath = "Nome";
            carregarDgvEntidade();
            alterEnableForm(false, gridEndereco);
            tbxNomeEntidade.IsEnabled = false;
        }
        private void carregarDgvEntidade()
        {
            dgvEntidades.ItemsSource = _serviceEntidade.RetornaTodos().OrderByDescending(e => e.Id);
        }
        private void carregarDgvEndereco(int idEntidade)
        {
            dgvEnderecos.ItemsSource = _serviceEndereco.RetornarPorEntidade(idEntidade).OrderBy(e => e.BairroId);
        }
        private void alterEnableForm(bool status, Grid gridAlterar)
        {
            try
            {
                for (int i = 0; i < gridAlterar.Children.Count; i++)
                {
                    if (gridAlterar.Children[i] is TextBox)
                        gridAlterar.Children[i].IsEnabled = status;
                }
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Erro inesperado: " + exception.Message);
            }
        }
        private void limpaText(Grid alterarGrid)
        {
            try
            {
                for (int i = 0; i < alterarGrid.Children.Count; i++)
                {
                    if (alterarGrid.Children[i] is TextBox)
                        (alterarGrid.Children[i] as TextBox).Text = "";
                }
                cbxBairro.SelectedIndex = -1;
                cbxCidade.SelectedIndex = -1;
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Erro inesperado ao limpar texto: " + exception.Message);
            }
        }
        private Entidade popularEntidade(Entidade entidade)
        {
            entidade.Nome = tbxNomeEntidade.Text;
            return entidade;
        }
        private Endereco popularEndereco(Endereco endereco)
        {
            endereco.Logradouro = tbxLogradouro.Text;
            endereco.Numero = tbxNumero.Text;
            endereco.PontoReferencia = tbxReferencia.Text;
            endereco.Responsavel = tbxResponsavel.Text;
            endereco.Telefones = tbxTelefone.Text;
            endereco.BairroId = ((Bairro)cbxBairro.SelectedItem).Id;
            endereco.CidadeId = ((Cidade)cbxCidade.SelectedItem).Id;
            return endereco;
        }
        private Entidade criaNewEntidade()
        {
            Entidade newEntidade = new Entidade();
            newEntidade.Nome = tbxNomeEntidade.Text;
            return newEntidade;
        }
        private Endereco criaNewEndereco()
        {
            Endereco newAdress = new Endereco();
            newAdress.Logradouro = tbxLogradouro.Text;
            newAdress.Numero = tbxNumero.Text;
            newAdress.PontoReferencia = tbxReferencia.Text;
            newAdress.Responsavel = tbxResponsavel.Text;
            newAdress.Telefones = tbxTelefone.Text;
            newAdress.BairroId = ((Bairro)cbxBairro.SelectedItem).Id;
            newAdress.CidadeId = ((Cidade)cbxCidade.SelectedItem).Id;
            newAdress.EntidadeId = (Convert.ToInt32(tbxCodigoEntidade.Text));
            return newAdress;
        }
        #endregion
    }
}
