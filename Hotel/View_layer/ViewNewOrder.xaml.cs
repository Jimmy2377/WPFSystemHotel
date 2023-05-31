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
            if (ValidarCampos())
            {
                
                // Crear una instancia de la clase OrdenCompra
                
                DateTime fecha = DateTime.Now;
                int tiempoEntrega = Convert.ToInt32(txtTiempoEntrega.Text);
                double montoTotal = Convert.ToDouble(cotizacionesSeleccionadas.Sum(c => c.PrecioUnit * c.Cantidad));
                string estado = "Recibido";
                string departamento = cmbDepartamento.SelectedItem.ToString();
                string tipoCompra = cmbTipoCompra.SelectedItem.ToString();
                int idEmpleado = 1;

                OrdenCompra ordenCompra = new OrdenCompra(0, fecha, tiempoEntrega, montoTotal, estado, departamento, tipoCompra, idEmpleado);

                // Obtener los detalles de compra
    List<DetalleCompra> detallesCompra = cotizacionesSeleccionadas.Select(cotizacion => new DetalleCompra(ordenCompra.ID_OrdenCompra, cotizacion.IdCotizacion, cotizacion.Cantidad)).ToList();

    // Llamar al método InsertOrdenCompra de la clase OrdenCompraDAO
    OrdenCompraDAO ordenCompraDAO = new OrdenCompraDAO();
    int resultado = ordenCompraDAO.InsertOrdenCompra(ordenCompra, detallesCompra);

    if (resultado > 0)
    {
        MessageBox.Show("La orden de compra se registró correctamente.", "Registro Exitoso");
        LimpiarCampos();
    }
    else
    {
        MessageBox.Show("Error al registrar la orden de compra.", "Error");
    }
            }
            else
            {
                MessageBox.Show("Por favor, complete todos los campos antes de registrar la orden de compra.");
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
