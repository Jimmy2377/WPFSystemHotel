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
    public class ProveedorDAO
    {
        private ConnectionToMysql connection;


        public ProveedorDAO()
        {
            connection = new ConnectionToMysql();
        }

        public List<Proveedor> GetAllProveedores()
        {
            List<Proveedor> proveedores = new List<Proveedor>();

            try
            {
                using (MySqlConnection con = connection.GetConnection())
                {
                    con.Open();
                    string query = "SELECT * FROM proveedor";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idProveedor = Convert.ToInt32(reader["ID_Proveedor"]);
                            string nombreProv = reader["NombreProv"].ToString();
                            string nit = reader["NIT"].ToString();
                            string direccion = reader["Direccion"].ToString();
                            string contactos = reader["Contactos"].ToString();

                            Proveedor proveedor = new Proveedor(idProveedor, nombreProv, nit, direccion, contactos);
                            proveedores.Add(proveedor);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los proveedores: " + ex.Message);
            }

            return proveedores;
        }

        public void InsertProveedor(Proveedor proveedor)
        {
            try
            {
                using (MySqlConnection con = connection.GetConnection())
                {
                    con.Open();
                    string query = "INSERT INTO proveedor (NombreProv, NIT, Direccion, Contactos) VALUES (@NombreProv, @NIT, @Direccion, @Contactos)";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@NombreProv", proveedor.NombreProv);
                    cmd.Parameters.AddWithValue("@NIT", proveedor.NIT);
                    cmd.Parameters.AddWithValue("@Direccion", proveedor.Direccion);
                    cmd.Parameters.AddWithValue("@Contactos", proveedor.Contactos);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Proveedor agregado exitosamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar el proveedor: " + ex.Message);
            }
        }

        public void EliminarProveedor(int idProveedor)
        {
            try
            {
                using (MySqlConnection con = connection.GetConnection())
                {
                    con.Open();
                    string query = "DELETE FROM proveedor WHERE ID_Proveedor = @ID_Proveedor";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ID_Proveedor", idProveedor);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Proveedor Eliminado exitosamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar el proveedor: " + ex.Message);
            }
        }

        public void ModificarProveedor(Proveedor proveedor)
        {
            try
            {
                using (MySqlConnection con = connection.GetConnection())
                {
                    con.Open();
                    string query = "UPDATE proveedor SET NombreProv = @NombreProv, NIT = @NIT, Direccion = @Direccion, Contactos = @Contactos WHERE ID_Proveedor = @ID_Proveedor";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@NombreProv", proveedor.NombreProv);
                    cmd.Parameters.AddWithValue("@NIT", proveedor.NIT);
                    cmd.Parameters.AddWithValue("@Direccion", proveedor.Direccion);
                    cmd.Parameters.AddWithValue("@ID_Proveedor", proveedor.ID_Proveedor);
                    cmd.Parameters.AddWithValue("@Contactos", proveedor.Contactos);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Proveedor Modificado exitosamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al modificar el proveedor: " + ex.Message);
            }
        }

    }
}
