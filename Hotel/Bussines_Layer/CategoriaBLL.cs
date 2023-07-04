using Hotel.Data_layer;
using Hotel.Entity_layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using NLog;

namespace Hotel.Bussines_Layer
{
    public class CategoriaBLL
    {
        private CategoriaDAO categoriaDAO;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();//Crea una instancia estática de la clase Logger 
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
                logger.Info("Se Registro una nueva cateroria: " + categoria.NombreCategoria);
                MessageBox.Show("Categoría agregada exitosamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                logger.Error("Error al insertar la categoría: " + ex.Message);
                MessageBox.Show("Error al insertar la categoría" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void EliminarCategoria(int idCategoria)
        {
            try
            {
                categoriaDAO.EliminarCategoria(idCategoria);
                logger.Info("Se Elimino una cateroria: ");
                MessageBox.Show("Categoría eliminada exitosamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                logger.Error("Error al Eliminar la categoría: " + ex.Message);
                MessageBox.Show("Error al eliminar la categoría", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ModificarCategoria(Categoria categoria)
        {
            try
            {
                categoriaDAO.ModificarCategoria(categoria);
                logger.Info("Se Modifico la categoria: " + categoria.NombreCategoria);
                MessageBox.Show("Categoría modificada exitosamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                logger.Error("No se pudo eliminar la categoria: " + ex.Message);
                MessageBox.Show("Error al modificar la categoría", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

