using Hotel.Data_layer;
using Hotel.Entity_layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Hotel.Business_layer
{
    public class CategoriaBLL
    {
        private CategoriaDAO categoriaDAO;

        public CategoriaBLL()
        {
            categoriaDAO = new CategoriaDAO();
        }

        public List<Categoria> GetAllCategorias()
        {
            return categoriaDAO.GetAllCategorias();  
        }

        public void InsertCategoria(Categoria categoria)
        {
            try
            {
                categoriaDAO.InsertCategoria(categoria);
                MessageBox.Show("Categoría agregada exitosamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar la categoría: " + ex.Message);
                MessageBox.Show("Error al insertar la categoría", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void EliminarCategoria(int idCategoria)
        {
            try
            {
                categoriaDAO.EliminarCategoria(idCategoria);
                MessageBox.Show("Categoría eliminada exitosamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar la categoría: " + ex.Message);
                MessageBox.Show("Error al eliminar la categoría", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ModificarCategoria(Categoria categoria)
        {
            try
            {
                categoriaDAO.ModificarCategoria(categoria);
                MessageBox.Show("Categoría modificada exitosamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al modificar la categoría: " + ex.Message);
                MessageBox.Show("Error al modificar la categoría", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

