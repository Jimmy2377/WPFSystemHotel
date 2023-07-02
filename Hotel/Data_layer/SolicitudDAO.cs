using Hotel.Entity_layer;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hotel.Data_layer
{
    internal class SolicitudDAO : ConnectionToMysql
    {
        private ConnectionToMysql connection;

        public SolicitudDAO()
        {
            connection = new ConnectionToMysql();
        }
        public void InsertarSolicitud(Solicitud solicitud)
        {
            try
            {
                using (MySqlConnection con = connection.GetConnection())
                {
                    con.Open();
                    string query = "INSERT INTO Solicitudes (Descripcion, Cantidad, Antecedentes, Precauciones) " +
                                    "VALUES (@Descripcion, @Cantidad, @Antecedentes, @Precauciones)";
                    MySqlCommand command = new MySqlCommand(query, con);
                    command.Parameters.AddWithValue("@Descripcion", solicitud.Descripcion);
                    command.Parameters.AddWithValue("@Cantidad", solicitud.Cantidad);
                    command.Parameters.AddWithValue("@Antecedentes", solicitud.Antecedentes);
                    command.Parameters.AddWithValue("@Precauciones", solicitud.Precauciones);
                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar Solicitud: " + ex.Message);
                MessageBox.Show("Solicitud no Agregada", "ERROR", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        public List<Solicitud> GetAllSolicitudes()
        {
            List<Solicitud> solicitudes = new List<Solicitud>();

            try
            {
                using (MySqlConnection con = connection.GetConnection())
                {
                    con.Open();
                    string query = "SELECT * FROM Solicitudes";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idSolicitud = Convert.ToInt32(reader["ID_Solicitud"]);
                            string descripcion = reader["Descripcion"].ToString();
                            string cantidad = reader["Cantidad"].ToString();
                            string antecedentes = reader["Antecedentes"].ToString();
                            string precauciones = reader["Precauciones"].ToString();

                            Solicitud solicitud = new Solicitud(idSolicitud, descripcion, cantidad, antecedentes, precauciones);
                            solicitudes.Add(solicitud);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener las Solicitudes: " + ex.Message);
            }

            return solicitudes;
        }
        public void DeleteSolicitud(int idSolicitud)
        {
            try
            {
                using (MySqlConnection con = connection.GetConnection())
                {
                    con.Open();
                    string query = "DELETE FROM Solicitudes WHERE ID_Solicitud = @ID_Solicitud";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ID_Solicitud", idSolicitud);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Solicitud Eliminado exitosamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el Solicitud: " + ex.Message);
            }
        }
    }
}
