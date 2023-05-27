using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Hotel.Entity_layer;
using MySql.Data.MySqlClient;

namespace Hotel.Data_layer
{
    class CategoriaDAO
    {
        private ConnectionToMysql connection;

        public CategoriaDAO() 
        {
            connection = new ConnectionToMysql();
        }

        public List<Categoria> GetAllCategorias()
        {
            List<Categoria> categorias = new List<Categoria>();

            try
            {
                using (MySqlConnection con = connection.GetConnection())
                {
                    con.Open();
                    string query = "SELECT * FROM categoria";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idCategoria = Convert.ToInt32(reader["ID_Categoria"]);
                            string nombreCategoria = reader["NombreCategoria"].ToString();
                            

                            Categoria categoria = new Categoria(idCategoria, nombreCategoria);
                            categorias.Add(categoria);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los proveedores: " + ex.Message);
            }

            return categorias;
        }

        public void InsertCategoria(Categoria categoria)
        {
            try
            {
                using (MySqlConnection con = connection.GetConnection())
                {
                    con.Open();
                    string query = "INSERT INTO categoria (NombreCategoria) VALUES (@NombreCategoria)";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@NombreCategoria", categoria.NombreCategoria);
                    
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Categoria agregado exitosamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar el Categoria: " + ex.Message);
            }
        }

        public void EliminarCategoria(int idCategoria)
        {
            try
            {
                using (MySqlConnection con = connection.GetConnection())
                {
                    con.Open();
                    string query = "DELETE FROM categoria WHERE ID_Categoria = @ID_Categoria";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ID_Categoria", idCategoria);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Categoria Eliminado exitosamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar el Categoria: " + ex.Message);
            }
        }

        public void ModificarCategoria(Categoria categoria)
        {
            try
            {
                using (MySqlConnection con = connection.GetConnection())
                {
                    con.Open();
                    string query = "UPDATE categoria SET NombreCategoria = @NombreCategoria WHERE ID_Categoria = @ID_Categoria";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@NombreCategoria", categoria.NombreCategoria);
                    cmd.Parameters.AddWithValue("@ID_Categoria", categoria.ID_Categoria);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Categoria Modificado exitosamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al modificar el proveedor: " + ex.Message);
                MessageBox.Show("Categoria No Modificado", "ERROR", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
