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

namespace Hotel.View_layer
{
    /// <summary>
    /// Lógica de interacción para ViewOrder.xaml
    /// </summary>
    public partial class ViewOrder : UserControl
    {
        private OrdenCompraDAO ordencompraDAO;
        public ViewOrder()
        {
            InitializeComponent();
            ordencompraDAO = new OrdenCompraDAO();
            LoadOrdenesCompra();
        }
        private void LoadOrdenesCompra()
        {
            List<OrdenCompra> ordenescompra = ordencompraDAO.GetAllOrdenCompras();
            listBoxOrdenes.ItemsSource = ordenescompra;
            listBoxOrdenes.DisplayMemberPath = "Estado";
        }
        private void txtBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filtro = txtBusqueda.Text.ToLower();

            // Obtén todos los ordenes y filtra según el texto ingresado
            List<OrdenCompra> ordenesFiltrados = ordencompraDAO.GetAllOrdenCompras().Where(ordencompra =>

                ordencompra.Fecha.ToString().Contains(filtro) ||
                ordencompra.Estado.ToLower().Contains(filtro) ||
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
                    ordencompraDAO.EliminarOrdenCompra(ordenSeleccionada.ID_OrdenCompra);

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

    }
}
