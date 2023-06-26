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
    public partial class ViewBuyUser : UserControl
    {
        public ViewBuyUser()
        {
            InitializeComponent();
        }
        private void btncontrolDirectaClick(object sender, RoutedEventArgs e)
        {
            ViewNewOrder viewneworder = new ViewNewOrder();
            contentControl.Content = viewneworder;
        }
        private void btncontrolDiaria_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
