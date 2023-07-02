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
    internal class GastosDAO : ConnectionToMysql
    {
        private ConnectionToMysql connection;
        public GastosDAO()
        {
            connection = new ConnectionToMysql();
        }
        public void InsertarValeCompra(ValeCompra valeCompra)
        {
            try
            {
                using (MySqlConnection con = connection.GetConnection())
                {
                    con.Open();
                    string query = "INSERT INTO valescompra (Fecha, Monto, Motivo, Empleado_ID_Empleado, Departamento) " +
                                    "VALUES (@Fecha, @Monto, @Motivo, @Empleado, @Departamento)";
                    MySqlCommand command = new MySqlCommand(query, con);
                    command.Parameters.AddWithValue("@Fecha", valeCompra.Fecha);
                    command.Parameters.AddWithValue("@Monto", valeCompra.Monto);
                    command.Parameters.AddWithValue("@Motivo", valeCompra.Descripcion);
                    command.Parameters.AddWithValue("@Empleado", valeCompra.ID_Empleado);
                    command.Parameters.AddWithValue("@Departamento", valeCompra.Departamento);
                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar Vale de Compra: " + ex.Message);
                MessageBox.Show("Error al insertar Vale de Compra ", "ERROR", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        public List<ValeCompra> GetAllValesCompra()
        {
            List<ValeCompra> valescompra = new List<ValeCompra>();

            try
            {
                using (MySqlConnection con = connection.GetConnection())
                {
                    con.Open();
                    string query = "SELECT * FROM valescompra";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idVale = Convert.ToInt32(reader["ID_Vales"]);
                            DateTime fecha = Convert.ToDateTime(reader["Fecha"]);
                            double montoTotal = Convert.ToDouble(reader["Monto"]);
                            string motivo = reader["Motivo"].ToString();
                            string departamento = reader["Departamento"].ToString();
                            int idEmpleado = Convert.ToInt32(reader["Empleado_ID_Empleado"]);

                            ValeCompra valeCompra = new ValeCompra(idVale, fecha, motivo, montoTotal, idEmpleado, departamento);

                            valescompra.Add(valeCompra);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener Vales de compra: " + ex.Message);
                MessageBox.Show("Error al obtener Vales de compra: " + ex.Message);
            }

            return valescompra;
        }
    }
}
