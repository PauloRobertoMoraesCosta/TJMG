using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TJ.View
{
    /// <summary>
    /// Interaction logic for CloseableHeader.xaml
    /// </summary>
    public partial class UCHeader : UserControl
    {
        public UCHeader()
        {
            InitializeComponent();
        }

        private void lbTabTitle_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            btnClose.Margin = new Thickness(lblTabTitle.ActualWidth + 2, 1, 1, 0);
        }
        
        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            btnClose.Visibility = Visibility.Hidden;
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            btnClose.Visibility = Visibility.Visible;
        }

        private void btnClose_MouseEnter(object sender, MouseEventArgs e)
        {
            btnClose.FontWeight = FontWeights.Bold;
        }

        private void btnClose_MouseLeave(object sender, MouseEventArgs e)
        {
            btnClose.FontWeight = FontWeights.Normal;
        }

        private void btnClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ((this.Parent as MyTabItem).Parent as TabControl).Items.Remove(this.Parent as MyTabItem);
        }
    }
}
