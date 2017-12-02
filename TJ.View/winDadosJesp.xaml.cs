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
    /// Interaction logic for winDadosJesp.xaml
    /// </summary>
    public partial class winDadosJesp : Window
    {
        private Jesp jesp;

        #region "Construtores"
        public winDadosJesp()
        {
            InitializeComponent();
            using (IAppServiceBairro serviceBairro = MinhaNinject.Kernel.Get<IAppServiceBairro>())
            {
                cbxBairro.ItemsSource = serviceBairro.RetornaTodosAsNoTracking().OrderBy(b => b.Nome);
            }
            cbxBairro.DisplayMemberPath = "Nome";
            using (IAppServiceCidade serviceCidade = MinhaNinject.Kernel.Get<IAppServiceCidade>())
            {
                cbxCidade.ItemsSource = serviceCidade.RetornaTodosAsNoTracking().OrderBy(b => b.Nome);
            }
            cbxCidade.DisplayMemberPath = "Nome";
            using (IAppServiceJesp serviceJesp = MinhaNinject.Kernel.Get<IAppServiceJesp>())
            {
                jesp = serviceJesp.RetornaTodos().FirstOrDefault();
            }
            if (jesp != null)
                popularTelaJesp(jesp);
        }

        #endregion

        #region "Metodos avulsos"
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

        #endregion

        #region "Metodos disparados"
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
        private void btnGravar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Validacoes.validarCampos(new List<Control>() { tbxDescricao, tbxEndereco, tbxHorarioFuncionamento, cbxBairro, cbxCidade, tbxEmail, tbxTelefone }))
                    Mensagens.MensagemAlertaOk("Favor informar os campos obrigatórios.");
                else
                {
                    if (jesp != null)
                    {
                        using (IAppServiceJesp serviceJesp = MinhaNinject.Kernel.Get<IAppServiceJesp>())
                        {
                            serviceJesp.Alterar(popularJesp(jesp));
                        }
                        (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowSuccess("Dados alterados com sucesso");
                        Close();
                    }
                    else
                    {
                        using (IAppServiceJesp serviceJesp = MinhaNinject.Kernel.Get<IAppServiceJesp>())
                        {
                            serviceJesp.Adiciona(popularJesp(new Jesp()));
                        }
                        (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowSuccess("Dados cadastrados com sucesso");
                        Close();
                    }
                }
            }
            catch (Exception ex)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError("Ao gravar os dados do Jesp: " + ex.Message);
            }
        }

        private void tbx_TextChanged(object sender, TextChangedEventArgs e)
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
        #endregion
    }
}
