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
    class CotizacionDAO
    {
        private ConnectionToMysql connection;

        public CotizacionDAO()
        {
            connection = new ConnectionToMysql();
        }

        public List<Cotizacion> GetAllCotizaciones()
        {
            List<Cotizacion> cotizaciones = new List<Cotizacion>();

            try
            {
                using (MySqlConnection con = connection.GetConnection())
                {
                    con.Open();
                    string query = "SELECT p.ID_Producto, p.NombreProducto, p.Descripcion, p.PrecioUnit, p.Tamaño, p.ID_Categoria, p.ID_Proveedor, c.NombreCategoria, pv.NombreProv, pv.NIT, pv.Direccion, pv.Contactos FROM producto p INNER JOIN categoria c ON p.ID_Categoria = c.ID_Categoria INNER JOIN proveedor pv ON p.ID_Proveedor = pv.ID_Proveedor";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idCotizacion = Convert.ToInt32(reader["ID_Producto"]);
                            string nombreProducto = reader["NombreProducto"].ToString();
                            string descripcion = reader["Descripcion"].ToString();
                            double precioUnit = Convert.ToDouble(reader["PrecioUnit"]);
                            string tamaño = reader["Tamaño"].ToString();
                            int idCategoria = Convert.ToInt32(reader["ID_Categoria"]);
                            int idProveedor = Convert.ToInt32(reader["ID_Proveedor"]);
                            string nombreCategoria = reader["NombreCategoria"].ToString();
                            string nombreProv = reader["NombreProv"].ToString();
                            string nit = reader["NIT"].ToString();
                            string direccion = reader["Direccion"].ToString();
                            string contactos = reader["Contactos"].ToString();

                            Categoria categoria = new Categoria(idCategoria, nombreCategoria);
                            Proveedor proveedor = new Proveedor(idProveedor, nombreProv, nit, direccion, contactos);
                            Cotizacion cotizacion = new Cotizacion(idCotizacion, nombreProducto, descripcion, precioUnit, tamaño, categoria, proveedor);
                            cotizaciones.Add(cotizacion);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los proveedores: " + ex.Message);
            }

            return cotizaciones;
        }

        public void InsertCotizacion(Cotizacion cotizacion)
        {
            try
            {
                using (MySqlConnection con = connection.GetConnection())
                {
                    con.Open();
                    string query = "INSERT INTO producto (NombreProducto, Descripcion, PrecioUnit, Tamaño, ID_Categoria, ID_Proveedor) VALUES (@NombreProducto, @Descripcion, @PrecioUnit, @Tamano, @ID_Categoria, @ID_Proveedor)";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@NombreProducto", cotizacion.NombreProducto);
                    cmd.Parameters.AddWithValue("@Descripcion", cotizacion.Descripcion);
                    cmd.Parameters.AddWithValue("@PrecioUnit", cotizacion.PrecioUnit);
                    cmd.Parameters.AddWithValue("@Tamano", cotizacion.Tamaño);
                    cmd.Parameters.AddWithValue("@ID_Categoria", cotizacion.Categoria.ID_Categoria);
                    cmd.Parameters.AddWithValue("@ID_Proveedor", cotizacion.Proveedor.ID_Proveedor);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Cotizacion agregado exitosamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar el proveedor: " + ex.Message);
                MessageBox.Show("Cotizacion No agregado", "ERROR", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public void EliminarCotizacion(int idCotizacion)
        {
            try
            {
                using (MySqlConnection con = connection.GetConnection())
                {
                    con.Open();
                    string query = "DELETE FROM producto WHERE ID_Producto = @ID_Producto";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ID_Producto", idCotizacion);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Cotizacion Eliminado exitosamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar el cotizacion: " + ex.Message);
                MessageBox.Show("Cotizacion No Eliminado", "ERROR", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        public void ModificarCotizacion(Cotizacion cotizacion)
        {
            try
            {
                using (MySqlConnection con = connection.GetConnection())
                {
                    con.Open();
                    string query = "UPDATE producto SET NombreProducto = @NombreProducto, Descripcion = @Descripcion, PrecioUnit = @PrecioUnit, Tamaño = @Tamano, ID_Categoria = @ID_Categoria, ID_Proveedor = @ID_Proveedor WHERE ID_Producto = @ID_Cotizacion";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@NombreProducto", cotizacion.NombreProducto);
            cmd.Parameters.AddWithValue("@Descripcion", cotizacion.Descripcion);
            cmd.Parameters.AddWithValue("@PrecioUnit", cotizacion.PrecioUnit);
            cmd.Parameters.AddWithValue("@Tamano", cotizacion.Tamaño);
            cmd.Parameters.AddWithValue("@ID_Categoria", cotizacion.Categoria.ID_Categoria);
            cmd.Parameters.AddWithValue("@ID_Proveedor", cotizacion.Proveedor.ID_Proveedor);
            cmd.Parameters.AddWithValue("@ID_Cotizacion", cotizacion.IdCotizacion);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Cotizacion Modificado exitosamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al modificar el Cotizacion: " + ex.Message);
            }
        }
    }
}
