using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TJ.Apresentacao.InterfacesApp;
using TJ.Dominio.Entidades;

namespace TJ.View
{
    /// <summary>
    /// Interaction logic for UCDadosJesp.xaml
    /// </summary>
    public partial class UCDadosJesp : UserControl
    {
        protected readonly IAppServiceJesp _serviceJesp;
        protected readonly IAppServiceBairro _serviceBairro;
        protected readonly IAppServiceCidade _serviceCidade;
        private Jesp jesp;
        public UCDadosJesp(IAppServiceJesp serviceJesp, IAppServiceBairro serviceBairro, IAppServiceCidade serviceCidade)
        {
            InitializeComponent();
            _serviceJesp = serviceJesp;
            _serviceBairro = serviceBairro;
            _serviceCidade = serviceCidade;
            cbxBairro.ItemsSource = _serviceBairro.RetornaTodosAsNoTracking().OrderBy(b => b.Nome);
            cbxBairro.DisplayMemberPath = "Nome";
            cbxCidade.ItemsSource = _serviceCidade.RetornaTodosAsNoTracking().OrderBy(b => b.Nome);
            cbxCidade.DisplayMemberPath = "Nome";
            jesp = _serviceJesp.RetornaPorId(1);
            if (jesp != null)
                popularTelaJesp(jesp);
        }

        private void popularTelaJesp(Jesp jesp)
        {
            tbxDescricao.Text = jesp.Descricao;
            tbxEndereco.Text = jesp.Endereco;
            tbxEmail.Text = jesp.Email;
            tbxTelefone.Text = jesp.Telefone;
            tbxHorarioFuncionamento.Text = jesp.HorarioDeFuncionamento;
            for (int i = 0; i < cbxBairro.Items.Count; i++)
            {
                if ((cbxBairro.Items[i] as Bairro).Id == jesp.BairroId)
                {
                    cbxBairro.SelectedItem = cbxBairro.Items[i];
                    i = cbxBairro.Items.Count;
                }
            }

            for (int i = 0; i < cbxCidade.Items.Count; i++)
            {
                if ((cbxCidade.Items[i] as Cidade).Id == jesp.CidadeId)
                {
                    cbxCidade.SelectedItem = cbxCidade.Items[i];
                    i = cbxCidade.Items.Count;
                }
            }
        }

        private void limpaTela()
        {
            tbxDescricao.Text = "";
            tbxEndereco.Text = "";
            tbxEmail.Text = "";
            tbxTelefone.Text = "";
            tbxHorarioFuncionamento.Text = "";
            cbxBairro.SelectedIndex = -1;
            cbxCidade.SelectedIndex = -1;
        }

        private Jesp popularJesp(Jesp jesp)
        {
            jesp.Descricao = tbxDescricao.Text;
            jesp.Endereco = tbxEndereco.Text;
            jesp.Telefone = tbxTelefone.Text;
            jesp.Email = tbxEmail.Text;
            jesp.HorarioDeFuncionamento = tbxHorarioFuncionamento.Text;
            jesp.BairroId = (cbxBairro.SelectedItem as Bairro).Id;
            jesp.CidadeId = (cbxCidade.SelectedItem as Cidade).Id;
            return jesp;
        }

        private void btnCancelaJesp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (jesp != null)
                    popularTelaJesp(jesp);
                else
                    limpaTela();
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Ocorreu algum problema ao cancelar:" + exception.Message);
            }
        }

        private void btnGravaJesp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Validacoes.validarCampos(new List<Control>() { tbxDescricao, tbxEndereco, tbxHorarioFuncionamento, cbxBairro, cbxCidade, tbxEmail, tbxTelefone }))
                    Mensagens.MensagemAlertaOk("Favor informar os campos Obrigatórios.");
                else
                {
                    if (jesp != null)
                    {
                        _serviceJesp.Alterar(popularJesp(jesp));
                        Mensagens.MensagemAlertaOk("Operação realizada com sucesso.");
                    }
                    else
                    {
                        _serviceJesp.Adiciona(popularJesp(new Jesp()));
                        jesp = _serviceJesp.RetornaPorId(1);
                        Mensagens.MensagemAlertaOk("Operação realizada com sucesso.");
                    }
                }
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Ocorreu algum problema ao Gravar: " + exception.Message);
            }
        }
    }
}
