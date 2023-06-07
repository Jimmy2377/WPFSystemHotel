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
using System.Windows.Shapes;
using Hotel.Data_layer;
using Hotel.Entity_layer;

namespace Hotel.View_layer
{
    /// <summary>
    /// Lógica de interacción para ModalDetalleCompra.xaml
    /// </summary>
    public partial class ModalDetalleCompra : Window
    {
        private OrdenCompraDAO ordencompraDAO;
        public ModalDetalleCompra(OrdenCompra ordenCompra)
        {
            InitializeComponent();
            ordencompraDAO = new OrdenCompraDAO();
            CargarDetalleCompra(ordenCompra);
        }

        private void CargarDetalleCompra(OrdenCompra ordenCompra)
        {
            // Obtener los detalles de compra de la orden de compra
            List<DetalleCompra> detallesCompra = ordencompraDAO.ObtenerDetallesCompra(ordenCompra.ID_OrdenCompra);

            // Asignar la lista de detalles de compra como origen de datos del DataGrid
            tablaDetalleCompra.ItemsSource = detallesCompra;
        }
        

        private void Cerrar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
