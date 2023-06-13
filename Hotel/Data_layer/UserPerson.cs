using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using Hotel.Entity_layer;
using System.Windows;

namespace Hotel.Data_layer
{
    class UserPerson:ConnectionToMysql
    {
        private ConnectionToMysql connection;
        public UserPerson()
        {
            connection = new ConnectionToMysql();
        }
        public bool Login(string user, string pass)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using(var command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select * from empleado where NombreUsuario=@user and ClaveUsuario=@pass";
                    command.Parameters.AddWithValue("@user",user);
                    command.Parameters.AddWithValue("@pass",pass);
                    command.CommandType = CommandType.Text;
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            UsuarioSesion.IDuser = reader.GetInt32(0);
                            UsuarioSesion.Nameuser = reader.GetString(1);
                            UsuarioSesion.Lastnameuser = reader.GetString(2);
                            int tipoUsuario = reader.GetInt32(11);

                            if (tipoUsuario == 1)
                            {
                                UsuarioSesion.TipoUsuario = TipoUsuario.Administrador;
                            }
                            else
                            {
                                UsuarioSesion.TipoUsuario = TipoUsuario.Usuario;
                            }
                        }
                        return true;
                    }
                    else return false; 

                }
            }
        }

        public void RegistrarUsuario(UsuarioSesion usuario)
        {
            try
            {
                using (MySqlConnection con = connection.GetConnection())
                {
                    con.Open();
                    string query = "INSERT INTO empleado (NombreEmpleado, Apellidos, CI, Direccion, Celular, Correo, NombreUsuario, ClaveUsuario, Pregunta, Respuesta, ID_TipoUsuario, Departamento) " +
                                    "VALUES (@NombreEmpleado, @Apellidos, @CI, @Direccion, @Celular, @Correo, @NombreUsuario, @ClaveUsuario, @Pregunta, @Respuesta, @ID_TipoUsuario, @Departamento)";
                    MySqlCommand command = new MySqlCommand(query, con);
                    command.Parameters.AddWithValue("@NombreEmpleado", usuario.NombreEmpleado);
                    command.Parameters.AddWithValue("@Apellidos", usuario.Apellidos);
                    command.Parameters.AddWithValue("@CI", usuario.CI);
                    command.Parameters.AddWithValue("@Direccion", usuario.Direccion);
                    command.Parameters.AddWithValue("@Celular", usuario.Celular);
                    command.Parameters.AddWithValue("@Correo", usuario.Correo);
                    command.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                    command.Parameters.AddWithValue("@ClaveUsuario", usuario.ClaveUsuario);
                    command.Parameters.AddWithValue("@Pregunta", usuario.Pregunta);
                    command.Parameters.AddWithValue("@Respuesta", usuario.Respuesta);
                    command.Parameters.AddWithValue("@ID_TipoUsuario", usuario.ID_TipoUsuario);
                    command.Parameters.AddWithValue("@Departamento", usuario.Departamento);
                    command.ExecuteNonQuery();
                    
                }
                MessageBox.Show("Cotizacion agregado exitosamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar el Cotizacion: " + ex.Message);
                MessageBox.Show("Cotizacion No agregado", "ERROR", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
