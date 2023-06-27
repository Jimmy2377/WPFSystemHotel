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
using Hotel.Bussines_Layer;
using Hotel.Data_layer;
using Hotel.Entity_layer;
using static Hotel.Entity_layer.OrdenCompra;

namespace Hotel.View_layer
{
  
    public partial class ViewNewOrder : UserControl
    {
        private List<Cotizacion> cotizacionesDisponibles;
        private ObservableCollection<Cotizacion> cotizacionesSeleccionadas;
        private OrdenCompraBLL ordenCompraBLL;
        public ViewNewOrder()
        {
            InitializeComponent();

            cotizacionesDisponibles = new List<Cotizacion>();
            cotizacionesSeleccionadas = new ObservableCollection<Cotizacion>();
            ordenCompraBLL = new OrdenCompraBLL();
            CargarCotizacionesDisponibles();
        }

        private void CargarCotizacionesDisponibles()
        {
            string condicion = $"'Aprobado' AND AprobadoPor = " + ObtenerIDEmpleado();
            CotizacionBLL cotizacionBLL = new CotizacionBLL();
            cotizacionesDisponibles = cotizacionBLL.GetAllCotizacionesCondicionadas(condicion);
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

        private void btnRegistrarOrdenCompra_Click(object sender, RoutedEventArgs e)//Registrar la orden de compra completa
        {
            
                // Validar los campos
                if (!ValidarCampos())
                {
                    MessageBox.Show("Por favor, complete todos los campos requeridos.");
                    return;
                }
            if (!EsSoloNumeros(txtTiempoEntrega.Text))
            {
                MessageBox.Show("Los dias solo en Numeros enteros Porfavor");
                return;
            }
            // Mostrar mensaje de confirmación
            MessageBoxResult result = MessageBox.Show("¿Estás seguro/a de terminar la orden de compra?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                string tipoCompra = (rbDirecta.IsChecked == true) ? "Directa" : "Programada";
                // Crear una nueva OrdenCompra
                OrdenCompra ordenCompra = new OrdenCompra(
                    0,
                    DateTime.Now,
                    Convert.ToInt32(txtTiempoEntrega.Text),
                    cotizacionesSeleccionadas.Sum(c => c.PrecioUnit * c.Cantidad),
                    EstadoOrdenCompra.Recibido,
                    ObtenerDepartamento(),
                    tipoCompra,
                    ObtenerIDEmpleado()
                );

                // Crear objetos DetalleCompra para cada producto en el carrito
                List<DetalleCompra> detallesCompra = new List<DetalleCompra>();

                foreach (Cotizacion cotizacion in cotizacionesSeleccionadas)
                {
                    DetalleCompra detalleCompra = new DetalleCompra
                    (
                        cotizacion.IdCotizacion,
                        cotizacion.Cantidad, //obtener la cantidad elegida del producto
                        null
                    );

                    detallesCompra.Add(detalleCompra);
                }

                // Asignar los detalles de compra a la orden de compra
                ordenCompra.DetallesCompra = detallesCompra;

                // Interactuar con la clase de negocio 

                ordenCompraBLL.InsertOrdenCompra(ordenCompra);

                // Limpiar los campos y reiniciar el carrito
                LimpiarCampos();
                cotizacionesSeleccionadas.Clear();
                ActualizarTablaCotizacionesSeleccionadas();
                CalcularMontoTotal();
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrEmpty(txtTiempoEntrega.Text) ||
                cotizacionesSeleccionadas.Count == 0)
            {
                return false;
            }

            return true;
        }
        private bool EsSoloNumeros(string cadena)
        {
            return cadena.All(char.IsDigit);
        }
        private int ObtenerIDEmpleado()
        {
            int idEmpleado = UsuarioSesion.IDuser;
            return idEmpleado;
        }
        private string ObtenerDepartamento()
        {
            string Departamento = UsuarioSesion.Departament;
            return Departamento;
        }

        private void LimpiarCampos()
        {
            txtTiempoEntrega.Clear();
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
        private void msgDirecta(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Indica en cuantos dias maximos nesecitas la orden compra para poder darle prioridad");
        }
        private void msgProgramada(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("La compra programada, indica cada cuantos dias nesecitas la misma Orden de Compra");
        }
    }
}
