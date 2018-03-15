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
    /// Interaction logic for WinCadEdBairro.xaml
    /// </summary>
    public partial class WinCadEdBairro : Window
    {
        private readonly UCBairrosLista _pai;
        private Bairro bairroSelecionado;

        #region "Construtores"
        public WinCadEdBairro(UCBairrosLista pai)
        {
            _pai = pai;
            InitializeComponent();
        }

        public WinCadEdBairro(Bairro bairro, UCBairrosLista pai)
        {
            bairroSelecionado = bairro;
            _pai = pai;
            InitializeComponent();
            popularFormulario(bairroSelecionado);
            Title = "Alteração de bairro";
            btnGravar.Content = "Alterar";
        }
        #endregion

        #region "Eventos disparados"
        private void btnGravar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Validacoes.validarCampos(new List<Control>() { tbxNome }))
                    (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowInformation("Favor informar os dados obrigatórios.");
                else
                {
                    if (bairroSelecionado == null)
                    {
                        using (IAppServiceBairro _serviceBairro = MinhaNinject.Kernel.Get<IAppServiceBairro>())
                        {
                            _serviceBairro.Adiciona(popularUser(new Bairro()));
                        }
                        (_pai as UCBairrosLista).carregaGridBairro();
                        Close();
                        (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowSuccess("Bairro cadastrado com sucesso");
                    }
                    else
                    {
                        using (IAppServiceBairro _serviceBairro = MinhaNinject.Kernel.Get<IAppServiceBairro>())
                        {
                            bairroSelecionado = (popularUser(_serviceBairro.RetornaPorId(bairroSelecionado.Id)));
                            _serviceBairro.Alterar(bairroSelecionado);
                            (_pai as UCBairrosLista).carregaGridBairro();
                            Close();
                            (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowSuccess("Usuário alterado com sucesso");

                        }
                    }
                }
            }
            catch (Exception exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError(bairroSelecionado == null ? "Ao gravar o bairro: " + exception.Message : "Ao alterar o bairro: " + exception.Message);
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
        private void popularFormulario(Bairro bairro)
        {
            tbxNome.Text = bairro.Nome;
        }

        private Bairro popularUser(Bairro bairro)
        {
            bairro.Nome = tbxNome.Text;
            return bairro;
        }
        #endregion
    }
}
