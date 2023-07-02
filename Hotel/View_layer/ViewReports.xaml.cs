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
using Hotel.Entity_layer;
using Hotel.Bussines_Layer;

namespace Hotel.View_layer
{
    public partial class ViewReports : UserControl
    {
        private OrdenCompraBLL ordenCompraBLL;
        private GastosBLL gastosBLL;
        public ViewReports()
        {
            InitializeComponent();
            ordenCompraBLL = new OrdenCompraBLL();
            gastosBLL = new GastosBLL();
            LoadOrdenesCompra();
            LoadValesCompra();
        }
        public void LoadOrdenesCompra()
        {
            List<OrdenCompra> ordenescompra = ordenCompraBLL.GetAllCompras("WHERE Estado = 'Almacen'");
            listBoxOrdenes.ItemsSource = ordenescompra;
        }
        public void LoadValesCompra()
        {
            List<ValeCompra> valescompra = gastosBLL.GetAllVales();
            listBoxValesCompra.ItemsSource = valescompra;
        }
    }
}
