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
using Hotel.Bussines_Layer;
using Hotel.Data_layer;
using Hotel.Entity_layer;

namespace Hotel.View_layer
{
    public partial class ModalDetalleCompra : Window
    {
        private OrdenCompra ordenCompra;
        private OrdenCompraBLL ordenCompraBLL;
        public ModalDetalleCompra(OrdenCompra ordenCompra)
        {
            InitializeComponent();
            this.ordenCompra = ordenCompra;
            ordenCompraBLL = new OrdenCompraBLL();
            CargarDetalleCompra(ordenCompra);
        }

        private void CargarDetalleCompra(OrdenCompra ordenCompra)
        {
            // Obtener los detalles de compra de la orden de compra utilizando la capa de negocio
            List<DetalleCompra> detallesCompra = ordenCompraBLL.ObtenerDetallesCompra(ordenCompra.ID_OrdenCompra);

            // Asignar la lista de detalles de compra como origen de datos del DataGrid
            tablaDetalleCompra.ItemsSource = detallesCompra;
        }
        private void Devolver_Click(object sender, RoutedEventArgs e)
        {
            string motivo = txtMotivo.Text;
            if (tablaDetalleCompra.SelectedItem != null)
            {
                DetalleCompra detalleSeleccionado = tablaDetalleCompra.SelectedItem as DetalleCompra;

                // Llamar a la capa de negocio para realizar la devolución
                ordenCompraBLL.DevolverProducto(detalleSeleccionado, motivo);

                // Actualizar la interfaz
                CargarDetalleCompra(ordenCompra);
            }
        }

        private void Cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
