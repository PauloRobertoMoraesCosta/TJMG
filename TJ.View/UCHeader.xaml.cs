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
            btnClose.Margin = new Thickness(lblTabTitle.ActualWidth + 5, 3, 4, 0);
        }

        private void btnClose_MouseDown(object sender, RoutedEventArgs e)
        {
            ((this.Parent as MyTabItem).Parent as TabControl).Items.Remove(this.Parent as MyTabItem);
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
            btnClose.Background = Brushes.Red;
        }

        private void btnClose_MouseLeave(object sender, MouseEventArgs e)
        {
            btnClose.Background = Brushes.White;
        }

    }
}
