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
using Hotel.Data_layer;
using Hotel.Entity_layer;
using Hotel.Business_layer;
using Hotel.Bussines_Layer;
using static Hotel.Entity_layer.OrdenCompra;

namespace Hotel.View_layer
{
    public partial class ViewOrder : UserControl
    {
        private OrdenCompraBLL ordenCompraBLL;
        public ViewOrder()
        {
            InitializeComponent();
            ordenCompraBLL = new OrdenCompraBLL();
            LoadOrdenesCompra();
        }
        private void LoadOrdenesCompra()
        {
            List<OrdenCompra> ordenescompra = ordenCompraBLL.GetAllCompras();
            listBoxOrdenes.ItemsSource = ordenescompra;
            listBoxOrdenes.DisplayMemberPath = "Estado";
        }
        private void txtBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filtro = txtBusqueda.Text.ToLower();

            // Obtén todos los ordenes y filtra según el texto ingresado
            List<OrdenCompra> ordenesFiltrados = ordenCompraBLL.GetAllCompras().Where(ordencompra =>

                ordencompra.Fecha.ToString().Contains(filtro) ||
                ordencompra.Estado.ToString().ToLower().Contains(filtro) ||
                ordencompra.Departamento.ToLower().Contains(filtro) ||
                ordencompra.TipoCompra.ToLower().Contains(filtro) 
            ).ToList();

            // Actualiza la lista de ordenes mostrada en el ListBox
            listBoxOrdenes.ItemsSource = ordenesFiltrados;
        }
        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxOrdenes.SelectedItem != null)
            {
                OrdenCompra ordenSeleccionada = listBoxOrdenes.SelectedItem as OrdenCompra;

                MessageBoxResult result = MessageBox.Show("¿Estás seguro de Cancelar la Orden Compra?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    ordenCompraBLL.EliminarOrdenCompra(ordenSeleccionada.ID_OrdenCompra);

                    // Actualizar la lista de proveedores
                    LoadOrdenesCompra();
                }
            }
        }
        
        private void VerDetalleCompra_Click(object sender, RoutedEventArgs e)
        {

            if (listBoxOrdenes.SelectedItem != null)
            {
                OrdenCompra ordenCompraSeleccionada = listBoxOrdenes.SelectedItem as OrdenCompra;

                    // Crear una instancia de la ventana modal para ver el detalle de compra
                    ModalDetalleCompra ventanaDetalle = new ModalDetalleCompra(ordenCompraSeleccionada);

                    // Mostrar la ventana modal
                    ventanaDetalle.ShowDialog();
            }
            
        }
        private void CambiarEstadoButton_Click(object sender, EventArgs e)
        {
            // Obtener la orden de compra seleccionada
            OrdenCompra ordenCompraSeleccionada = listBoxOrdenes.SelectedItem as OrdenCompra;

            if (ordenCompraSeleccionada != null)
            {
                // Cambiar el estado de la orden de compra
                ordenCompraBLL.CambiarEstadoOrdenCompra(ordenCompraSeleccionada);

                // Actualizar la lista de órdenes de compra en la ventana
                LoadOrdenesCompra();
            }
        }
    }
}
