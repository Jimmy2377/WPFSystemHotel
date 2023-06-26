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
using Hotel.Bussines_Layer;
using Hotel.Entity_layer;
using MySql.Data.MySqlClient;

namespace Hotel.View_layer
{
    public partial class ViewProviders : UserControl
    {
        private ProveedorManager proveedorManager;

        public ViewProviders()
        {
            InitializeComponent();
            proveedorManager = new ProveedorManager();
            LoadProveedores();
        }

        private void LoadProveedores()
        {
            List<Proveedor> proveedores = proveedorManager.GetAllProveedores();
            listBoxProveedores.ItemsSource = proveedores;
        }

        private void btnAgregarProveedor_Click(object sender, RoutedEventArgs e)
        {
            string nombre = txtNombre.Text;
            string nit = txtNit.Text;
            string direccion = txtDireccion.Text;
            string contactos = txtContactos.Text;

            Proveedor proveedor = new Proveedor(0, nombre, nit, direccion, contactos);

            try
            {
                proveedorManager.InsertProveedor(proveedor);

                // Actualiza la lista de proveedores
                LoadProveedores();

                // Limpia los campos de texto
                ClearFields();

                MessageBox.Show("Proveedor agregado exitosamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el proveedor: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxProveedores.SelectedItem != null)
            {
                Proveedor proveedorSeleccionado = listBoxProveedores.SelectedItem as Proveedor;

                MessageBoxResult result = MessageBox.Show($"¿Estás seguro de eliminar al proveedor: {proveedorSeleccionado.NombreProv}?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    proveedorManager.EliminarProveedor(proveedorSeleccionado.ID_Proveedor);

                    // Actualizar la lista de proveedores
                    LoadProveedores();
                    ClearFields();
                }
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxProveedores.SelectedItem != null)
            {
                Proveedor proveedorSeleccionado = listBoxProveedores.SelectedItem as Proveedor;

                // Obtener los datos modificados desde la interfaz de usuario
                proveedorSeleccionado.NombreProv = txtNombre.Text;
                proveedorSeleccionado.NIT = txtNit.Text;
                proveedorSeleccionado.Direccion = txtDireccion.Text;
                proveedorSeleccionado.Contactos = txtContactos.Text;
                proveedorManager.ModificarProveedor(proveedorSeleccionado);

                // Actualizar la lista de proveedores
                LoadProveedores();
                ClearFields();
            }
        }
        private void btnlimpiar_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtNombre.Text = string.Empty;
            txtNit.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtContactos.Text =  string.Empty;
        }

        private void listBoxProveedores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxProveedores.SelectedItem != null)
            {
                Proveedor proveedorSeleccionado = listBoxProveedores.SelectedItem as Proveedor;

                // Rellenar los campos de texto con los datos del proveedor seleccionado
                txtNombre.Text = proveedorSeleccionado.NombreProv;
                txtNit.Text = proveedorSeleccionado.NIT;
                txtDireccion.Text = proveedorSeleccionado.Direccion;
                txtContactos.Text = proveedorSeleccionado.Contactos;
            }
        }
        private void txtBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filtro = txtBusqueda.Text.ToLower();

            // Obtén todos los proveedores y filtra según el texto ingresado
            List<Proveedor> proveedoresFiltrados = proveedorManager.GetAllProveedores().Where(proveedor =>
            
                proveedor.NombreProv.ToLower().Contains(filtro) ||
                proveedor.NIT.ToLower().Contains(filtro) ||
                proveedor.Direccion.ToLower().Contains(filtro) ||
                proveedor.Contactos.ToLower().Contains(filtro)
            ).ToList();

            // Actualiza la lista de proveedores mostrada en el ListBox
            listBoxProveedores.ItemsSource = proveedoresFiltrados;
        }

    }
}
