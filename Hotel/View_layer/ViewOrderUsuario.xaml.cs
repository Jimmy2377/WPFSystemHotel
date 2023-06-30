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
using static Hotel.Entity_layer.OrdenCompra;

namespace Hotel.View_layer
{
    public partial class ViewOrderUsuario : UserControl
    {
        private OrdenCompraBLL ordenCompraBLL;
        public ViewOrderUsuario()
        {
            InitializeComponent();
            ordenCompraBLL = new OrdenCompraBLL();
            LoadOrdenesCompra();
        }
        private void LoadOrdenesCompra()
        {
            List<OrdenCompra> ordenescompra = ordenCompraBLL.GetAllCompras("WHERE Estado <> 'Almacen' AND Empleado_ID_Empleado = " + ObtenerIDEmpleado());
            listBoxOrdenes.ItemsSource = ordenescompra;
            listBoxOrdenes.DisplayMemberPath = "Estado";
        }
        private int ObtenerIDEmpleado()
        {
            int idEmpleado = UsuarioSesion.IDuser;
            return idEmpleado;
        }
        private void txtBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filtro = txtBusqueda.Text.ToLower();

            // Obtén todos los ordenes y filtra según el texto ingresado
            List<OrdenCompra> ordenesFiltrados = ordenCompraBLL.GetAllCompras("WHERE Estado <> 'Almacen' AND TipoCompra = 'Programada'").Where(ordencompra =>

                ordencompra.Fecha.ToString().Contains(filtro) ||
                ordencompra.Estado.ToString().ToLower().Contains(filtro) ||
                ordencompra.Departamento.ToLower().Contains(filtro)

            ).ToList();

            // Actualiza la lista de ordenes mostrada en el ListBox
            listBoxOrdenes.ItemsSource = ordenesFiltrados;
        }
        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxOrdenes.SelectedItem != null)
            {
                OrdenCompra ordenSeleccionada = listBoxOrdenes.SelectedItem as OrdenCompra;

                // Verificar si el estado de la orden es "Recibido" o "Enviado"
                if (ordenSeleccionada.Estado == EstadoOrdenCompra.Recibido || ordenSeleccionada.Estado == EstadoOrdenCompra.Enviado)
                {
                    MessageBoxResult result = MessageBox.Show("¿Estás seguro de Cancelar la Orden Compra?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        ordenCompraBLL.EliminarOrdenCompra(ordenSeleccionada.ID_OrdenCompra);

                        // Actualizar la lista de proveedores
                        LoadOrdenesCompra();
                    }
                }
                else
                {
                    MessageBox.Show("Solo se pueden eliminar órdenes en estado 'Recibido' o 'Enviado'.", "Un Momento", MessageBoxButton.OK, MessageBoxImage.Stop);
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
