using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using Hotel.Bussines_Layer;
using Hotel.Data_layer;
using Hotel.Entity_layer;
using Hotel.Business_layer;
using Hotel.Negocio;

namespace Hotel.View_layer
{
    public partial class ViewProduct : UserControl
    {
        private CotizacionBLL cotizacionBLL;
        public ViewProduct()
        {
            InitializeComponent();
            cotizacionBLL = new CotizacionBLL();
            LoadCotizaciones();
            CargarCategorias();
            CargarProveedores();
        }
        private void LoadCotizaciones()
        {
            List<Cotizacion> cotizaciones = cotizacionBLL.GetAllCotizaciones();
            listBoxCotizaciones.ItemsSource = cotizaciones;
            listBoxCotizaciones.DisplayMemberPath = "NombreProducto";
        }
        private void CargarCategorias()
        {
            CategoriaBLL categoriaBLL = new CategoriaBLL();
            List<Categoria> categorias = categoriaBLL.GetAllCategorias();
            cmbCategoria.ItemsSource = categorias;
        }

        private void CargarProveedores()
        {
            ProveedorManager proveedorManager = new ProveedorManager();
            List<Proveedor> proveedores = proveedorManager.GetAllProveedores();
            cmbProveedor.ItemsSource = proveedores;
        }

        private void txtBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filtro = txtBusqueda.Text.ToLower();

            // Obtén todos los proveedores y filtra según el texto ingresado
            List<Cotizacion> cotizacionesFiltrados = cotizacionBLL.GetAllCotizaciones().Where(cotizacion =>

                cotizacion.NombreProducto.ToLower().Contains(filtro) ||
                cotizacion.Descripcion.ToLower().Contains(filtro) ||
                cotizacion.Tamaño.ToLower().Contains(filtro) ||
                cotizacion.PrecioUnit.ToString().Contains(filtro) ||
                cotizacion.Proveedor.ToString().Contains(filtro) ||
                cotizacion.Categoria.NombreCategoria.ToString().Contains(filtro)  
                
            ).ToList();

            // Actualiza la lista de proveedores mostrada en el ListBox
            listBoxCotizaciones.ItemsSource = cotizacionesFiltrados;
        }
        private void listBoxCotizaciones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxCotizaciones.SelectedItem != null)
            {
                
                Cotizacion cotizacionSeleccionada = (Cotizacion)listBoxCotizaciones.SelectedItem;
                // Asignar valores a los TextBox
                txtNombre.Text = cotizacionSeleccionada.NombreProducto;
                txtDescripcion.Text = cotizacionSeleccionada.Descripcion;
                txtPrecio.Text = cotizacionSeleccionada.PrecioUnit.ToString();
                txtTamaño.Text = cotizacionSeleccionada.Tamaño;

                // Seleccionar proveedor y categoría en los ComboBox
                cmbCategoria.SelectedItem = GetCategoriaById(cotizacionSeleccionada.Categoria.ID_Categoria);
                cmbProveedor.SelectedItem = GetProveedorById(cotizacionSeleccionada.Proveedor.ID_Proveedor);


            }
        }
        private Categoria GetCategoriaById(int idCategoria)
        {
            return cmbCategoria.Items.OfType<Categoria>().FirstOrDefault(c => c.ID_Categoria == idCategoria);
        }

        private Proveedor GetProveedorById(int idProveedor)
        {
            return cmbProveedor.Items.OfType<Proveedor>().FirstOrDefault(p => p.ID_Proveedor == idProveedor);
        }
        private void btnlimpiar_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtNombre.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            txtTamaño.Text = string.Empty;
            cmbCategoria.SelectedItem = null;
            cmbProveedor.SelectedItem = null;
        }
        
        private void btnAgregarCotizacion_Click(object sender, RoutedEventArgs e)
        {
            // Obtener los valores de los controles de entrada
            string nombreProducto = txtNombre.Text;
            string descripcion = txtDescripcion.Text;
            double precioUnitario = Convert.ToDouble(txtPrecio.Text);
            string tamano = txtTamaño.Text;
            Categoria categoria = (Categoria)cmbCategoria.SelectedItem;
            Proveedor proveedor = (Proveedor)cmbProveedor.SelectedItem;

            Cotizacion cotizacion = new Cotizacion(0, nombreProducto, descripcion, precioUnitario, tamano, categoria, proveedor);
            cotizacionBLL.InsertCotizacion(cotizacion);
            // Limpiar los controles de entrada después de la inserción exitosa
            ClearFields();

            //// Actualizar la lista de cotizaciones en el DataGrid
            LoadCotizaciones();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxCotizaciones.SelectedItem != null)
            {
                Cotizacion cotizacionSeleccionado = listBoxCotizaciones.SelectedItem as Cotizacion;

                MessageBoxResult result = MessageBox.Show($"¿Estás seguro de eliminar La Cotizacion? {cotizacionSeleccionado.NombreProducto}?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    cotizacionBLL.EliminarCotizacion(cotizacionSeleccionado.IdCotizacion);

                    // Actualizar la lista de proveedores
                    LoadCotizaciones();
                    ClearFields();
                }
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxCotizaciones.SelectedItem != null)
            {
                Cotizacion cotizacionSeleccionada = listBoxCotizaciones.SelectedItem as Cotizacion;

                // Obtener los datos modificados desde la interfaz de usuario
                cotizacionSeleccionada.NombreProducto = txtNombre.Text;
                cotizacionSeleccionada.Descripcion = txtDescripcion.Text;
                cotizacionSeleccionada.PrecioUnit = double.Parse(txtPrecio.Text);
                cotizacionSeleccionada.Tamaño = txtTamaño.Text;
                cotizacionSeleccionada.Categoria = (Categoria)cmbCategoria.SelectedItem;
                cotizacionSeleccionada.Proveedor = (Proveedor)cmbProveedor.SelectedItem;

                cotizacionBLL.ModificarCotizacion(cotizacionSeleccionada);

                // Actualizar la lista de proveedores
                LoadCotizaciones();
                ClearFields();
            }
        }

    }
}
