using Hotel.Data_layer;
using Hotel.Entity_layer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
  
    public partial class ViewNewOrder : UserControl
    {
        private List<Cotizacion> cotizacionesDisponibles;
        private ObservableCollection<Cotizacion> cotizacionesSeleccionadas;

        public ViewNewOrder()
        {
            InitializeComponent();

            cotizacionesDisponibles = new List<Cotizacion>();
            cotizacionesSeleccionadas = new ObservableCollection<Cotizacion>();
            
            CargarCotizacionesDisponibles();
        }

        private void CargarCotizacionesDisponibles()
        {
            CotizacionDAO cotizacionDAO = new CotizacionDAO();
            cotizacionesDisponibles = cotizacionDAO.GetAllCotizaciones();

            dgCotizacionesDisponibles.ItemsSource = cotizacionesDisponibles;
        }

        private void btnAgregarCotizacion_Click(object sender, RoutedEventArgs e)
        {
            Cotizacion cotizacionSeleccionada = (Cotizacion)dgCotizacionesDisponibles.SelectedItem;
            if (cotizacionSeleccionada != null)
            {
                // Mostrar ventana modal para ingresar la cantidad de elementos de la cotización
                int cantidad = MostrarVentanaModalCantidad();
                if (cantidad > 0)
                {
                    cotizacionSeleccionada.Cantidad = cantidad;
                    cotizacionesSeleccionadas.Add(cotizacionSeleccionada);

                    ActualizarTablaCotizacionesSeleccionadas();
                    CalcularMontoTotal();
                }
            }
        }

        private void ActualizarTablaCotizacionesSeleccionadas()
        {
            dgCotizacionesElegidas.ItemsSource = cotizacionesSeleccionadas;
        }

        private int MostrarVentanaModalCantidad()
        {
            VentanaModalCantidad ventanaModal = new VentanaModalCantidad();
            bool? result = ventanaModal.ShowDialog();

            if (result == true)
            {
                return ventanaModal.Cantidad;
            }

            return 0;
        }
        private void btnQuitarCotizacion_Click(object sender, RoutedEventArgs e)
        {
            Cotizacion cotizacionSeleccionada = (Cotizacion)dgCotizacionesElegidas.SelectedItem;
            if (cotizacionSeleccionada != null)
            {
                cotizacionesSeleccionadas.Remove(cotizacionSeleccionada);

                ActualizarTablaCotizacionesSeleccionadas();
                CalcularMontoTotal();
            }
        }

        private void CalcularMontoTotal()
        {
            double montoTotal = cotizacionesSeleccionadas.Sum(c => c.PrecioUnit * c.Cantidad);
            txtMontoTotal.Text = montoTotal.ToString("C");
        }

        private void btnRegistrarOrdenCompra_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Crear una instancia de OrdenCompraDAO
                //OrdenCompraDAO ordenCompraDAO = new OrdenCompraDAO();

                // Crear una nueva OrdenCompra
                OrdenCompra ordenCompra = new OrdenCompra(
                    DateTime.Now,
                    Convert.ToInt32(txtTiempoEntrega.Text),
                    cotizacionesSeleccionadas.Sum(c => c.PrecioUnit * c.Cantidad),
                    "Recibido",
                    cmbDepartamento.SelectedValue.ToString(),
                    cmbTipoCompra.SelectedValue.ToString(),
                    1
                );

                // Crear objetos DetalleCompra para cada producto en el carrito
                List<DetalleCompra> detallesCompra = new List<DetalleCompra>();

                foreach (Cotizacion cotizacion in cotizacionesSeleccionadas)
                {
                    DetalleCompra detalleCompra = new DetalleCompra
                    (
                        cotizacion.IdCotizacion,
                        cotizacion.Cantidad //obtener la cantidad elegida del producto
                    );

                    detallesCompra.Add(detalleCompra);
                }

                // Asignar los detalles de compra a la orden de compra
                ordenCompra.DetallesCompra = detallesCompra;

                // Insertar la orden de compra en la base de datos
                OrdenCompraDAO ordenCompraDAO = new OrdenCompraDAO();
                ordenCompraDAO.InsertOrdenCompra(ordenCompra);

                MessageBox.Show("Orden de Compra creada exitosamente.");

                // Limpiar los campos y reiniciar el carrito
                LimpiarCampos();
                cotizacionesSeleccionadas.Clear();
                ActualizarTablaCotizacionesSeleccionadas();
                CalcularMontoTotal();




                //// Llamar al método InsertOrdenCompra y pasar los argumentos necesarios
                //int iD_OrdenCompra = ordenCompraDAO.InsertOrdenCompra(ordenCompra);

                //// Crear una lista de detalles de compra
                //List<DetalleCompra> detallesCompra = cotizacionesSeleccionadas
                //    .Select(cotizacion => new DetalleCompra(ordenCompra.ID_OrdenCompra, cotizacion.IdCotizacion, cotizacion.Cantidad))
                //    .ToList();

                //// Insertar los detalles de compra en la base de datos
                //ordenCompraDAO.InsertDetallesCompra(detallesCompra);

                //// Mostrar mensaje de éxito
                //MessageBox.Show("Orden de compra registrada correctamente.");

                //// Limpiar la lista de cotizaciones seleccionadas y actualizar la interfaz
                //cotizacionesSeleccionadas.Clear();
                //ActualizarTablaCotizacionesSeleccionadas();
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error
                MessageBox.Show("Error al registrar la orden de compra: " + ex.Message);
            }
        }



        private bool ValidarCampos()
        {
            if (string.IsNullOrEmpty(txtTiempoEntrega.Text) ||
                cmbDepartamento.SelectedItem == null ||
                cmbTipoCompra.SelectedItem == null ||
                cotizacionesSeleccionadas.Count == 0)
            {
                return false;
            }

            return true;
        }

        private int ObtenerIDEmpleado()
        {
            int idEmpleado = UsuarioSesion.IDuser;
            return idEmpleado;
        }

        private void LimpiarCampos()
        {
            txtTiempoEntrega.Clear();
            cmbDepartamento.SelectedItem = null;
            cmbTipoCompra.SelectedItem = null;
            cotizacionesSeleccionadas.Clear();

            ActualizarTablaCotizacionesSeleccionadas();
            CalcularMontoTotal();
        }

        private void txtBuscarCotizacion_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filtro = txtBuscarCotizacion.Text.Trim().ToLower();

            List<Cotizacion> cotizacionesFiltradas = cotizacionesDisponibles
                .Where(c => c.NombreProducto.ToLower().Contains(filtro))
                .ToList();

            dgCotizacionesDisponibles.ItemsSource = cotizacionesFiltradas;
        }
    }
}
