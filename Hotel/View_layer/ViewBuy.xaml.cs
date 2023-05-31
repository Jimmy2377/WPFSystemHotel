using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hotel.View_layer
{
    /// <summary>
    /// Lógica de interacción para ViewBuy.xaml
    /// </summary>
    public partial class ViewBuy : UserControl
    {
        public ViewBuy()
        {
            InitializeComponent();
        }

        private void btncontrolneworder_Click(object sender, RoutedEventArgs e)
        {
            ViewNewOrder viewneworder = new ViewNewOrder();
            contentControl.Content = viewneworder;
        }
    }
}
