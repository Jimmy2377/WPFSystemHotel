using Hotel.Entity_layer;
using Hotel.Bussines_Layer;
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
    public partial class ViewReturns : UserControl
    {
        private OrdenCompraBLL ordenCompraBLL;
        public ViewReturns()
        {
            InitializeComponent();
            ordenCompraBLL = new OrdenCompraBLL();
            LoadOrdenesCompra();
        }
        public void LoadOrdenesCompra()
        {
            List<OrdenCompra> ordenescompra = ordenCompraBLL.GetAllCompras("WHERE Estado = 'Almacen'");
            listBoxOrdenes.ItemsSource = ordenescompra;
            listBoxOrdenes.DisplayMemberPath = "Estado";
        }
        private void btnDevolucion_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxOrdenes.SelectedItem != null)
            {
                OrdenCompra ordenCompraSeleccionada = listBoxOrdenes.SelectedItem as OrdenCompra;

                // Crear una instancia de la ventana modal para ver el detalle de compra
                ModalDetalleCompra ventanaDetalle = new ModalDetalleCompra(ordenCompraSeleccionada);
                //Mostrar Elementos ocultos
                ventanaDetalle.btndevolver.Visibility = Visibility.Visible;
                ventanaDetalle.ElementosMotivo.Visibility = Visibility.Visible;

                // Suscribirse al evento Closed de la ventana modal
                ventanaDetalle.Closed += VentanaDetalleClosed;
                // Mostrar la ventana modal
                ventanaDetalle.ShowDialog();
            }
        }
        private void VentanaDetalleClosed(object sender, EventArgs e)
        {
            // Actualizar la primera ventana aquí
            LoadOrdenesCompra(); // Reemplaza esto con el método que actualiza la primera ventana
        }
        private void txtBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filtro = txtBusqueda.Text.ToLower();

            // Obtén todos los ordenes y filtra según el texto ingresado
            List<OrdenCompra> ordenesFiltrados = ordenCompraBLL.GetAllCompras("WHERE Estado <> 'Almacen'").Where(ordencompra =>

                ordencompra.FechaEntrega.ToString().Contains(filtro) ||
                ordencompra.Departamento.ToString().ToLower().Contains(filtro) ||
                ordencompra.TipoCompra.ToLower().Contains(filtro) ||
                ordencompra.MontoTotal.ToString().ToLower().Contains(filtro)

            ).ToList();

            // Actualiza la lista de ordenes mostrada en el ListBox
            listBoxOrdenes.ItemsSource = ordenesFiltrados;
        }
    }
}
