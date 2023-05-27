using Hotel.Data_layer;
using Hotel.Entity_layer;
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

namespace Hotel.View_layer
{
    /// <summary>
    /// Lógica de interacción para ViewCategory.xaml
    /// </summary>
    public partial class ViewCategory : UserControl
    {
        private CategoriaDAO categoriaDAO;
        public ViewCategory()
        {
            InitializeComponent();
            categoriaDAO = new CategoriaDAO();
            LoadCategorias();
        }
        private void LoadCategorias()
        {
            List<Categoria> categorias = categoriaDAO.GetAllCategorias();
            listBoxCategorias.ItemsSource = categorias;
        }
        private void listBoxCategorias_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxCategorias.SelectedItem != null)
            {
                Categoria categoriaSeleccionado = listBoxCategorias.SelectedItem as Categoria;

                // Rellenar los campos de texto con los datos del proveedor seleccionado
                txtNombre.Text = categoriaSeleccionado.NombreCategoria;
                
            }
        }
        private void txtBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filtro = txtBusqueda.Text.ToLower();

            // Obtén todos los proveedores y filtra según el texto ingresado
            List<Categoria> categoriasFiltrados = categoriaDAO.GetAllCategorias().Where(categoria =>

                categoria.NombreCategoria.ToLower().Contains(filtro)
            ).ToList();

            // Actualiza la lista de proveedores mostrada en el ListBox
            listBoxCategorias.ItemsSource = categoriasFiltrados;
        }

        private void btnAgregarCategoria_Click(object sender, RoutedEventArgs e)
        {
            string nombre = txtNombre.Text;
            
            Categoria categoria = new Categoria(0, nombre);
            categoriaDAO.InsertCategoria(categoria);


            // Actualiza la lista de proveedores
            LoadCategorias();

            // Limpia los campos de texto
            ClearFields();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxCategorias.SelectedItem != null)
            {
                Categoria categoriaSeleccionado = listBoxCategorias.SelectedItem as Categoria;

                MessageBoxResult result = MessageBox.Show($"¿Estás seguro de eliminar la Categoria?: {categoriaSeleccionado.NombreCategoria}?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    categoriaDAO.EliminarCategoria(categoriaSeleccionado.ID_Categoria);

                    // Actualizar la lista de categorias
                    LoadCategorias();
                    ClearFields();
                }
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxCategorias.SelectedItem != null)
            {
                Categoria categoriaSeleccionado = listBoxCategorias.SelectedItem as Categoria;

                // Obtener los datos modificados desde la interfaz de usuario
                categoriaSeleccionado.NombreCategoria = txtNombre.Text;
                
                categoriaDAO.ModificarCategoria(categoriaSeleccionado);

                // Actualizar la lista de proveedores
                LoadCategorias();
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
        }
    }
}
