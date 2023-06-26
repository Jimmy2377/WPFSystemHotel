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
        private const int MaxIntentosFallidos = 3;

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
                    command.CommandText = "SELECT * FROM empleado WHERE NombreUsuario=@user";
                    command.Parameters.AddWithValue("@user", user);
                    command.CommandType = CommandType.Text;
                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        int intentosFallidos = reader.GetInt32(13);
                        string estadoCuenta = reader.GetString(14);

                        if (estadoCuenta == "Bloqueado")
                        {
                            MessageBox.Show("La cuenta está bloqueada. Contacte a un administrador.");
                            return false;
                        }

                        if (intentosFallidos >= MaxIntentosFallidos)
                        {
                            MessageBox.Show("Ha excedido el número máximo de intentos fallidos. La cuenta ha sido bloqueada.");
                            BloquearCuenta(user);
                            return false;
                        }

                        // Verificar la contraseña
                        string contraseñaAlmacenada = reader.GetString(8);
                        if (pass == contraseñaAlmacenada)
                        {
                            // Restablecer el contador de intentos fallidos
                            ResetearIntentosFallidos(user);

                            // Asignar los valores a las propiedades de UsuarioSesion
                            UsuarioSesion.IDuser = reader.GetInt32(0);
                            UsuarioSesion.Nameuser = reader.GetString(1);
                            UsuarioSesion.Lastnameuser = reader.GetString(2);
                            UsuarioSesion.Departament = reader.GetString(12);
                            int tipoUsuario = reader.GetInt32(11);

                            if (tipoUsuario == 1)
                            {
                                UsuarioSesion.TipoUsuario = TipoUsuario.Administrador;
                            }
                            else
                            {
                                UsuarioSesion.TipoUsuario = TipoUsuario.Usuario;
                            }

                            return true;
                        }
                        else
                        {
                            // Incrementar el contador de intentos fallidos
                            IncrementarIntentosFallidos(user);
                            string mensaje = string.Format("Contraseña incorrecta. Intento fallido número {0}/{1}", intentosFallidos + 1, MaxIntentosFallidos);
                            MessageBox.Show(mensaje, "Inicio de sesión fallido", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return false;

                        }
                    }
                    else
                    {
                        Console.WriteLine("Usuario no encontrado.");
                        return false;
                    }

                }
            }
        }

        private void IncrementarIntentosFallidos(string usuario)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "UPDATE empleado SET IntentosFallidos = IntentosFallidos + 1 WHERE NombreUsuario = @user";
                    command.Parameters.AddWithValue("@user", usuario);
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }

        private void ResetearIntentosFallidos(string usuario)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "UPDATE empleado SET IntentosFallidos = 0 WHERE NombreUsuario = @user";
                    command.Parameters.AddWithValue("@user", usuario);
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }

        private void BloquearCuenta(string usuario)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "UPDATE empleado SET EstadoCuenta = 'Bloqueado' WHERE NombreUsuario = @user";
                    command.Parameters.AddWithValue("@user", usuario);
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
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
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar el Cotizacion: " + ex.Message);
                MessageBox.Show("Cotizacion No agregado", "ERROR", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public bool SearchUsuario(string nombreUsuario)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT COUNT(*) FROM empleado WHERE NombreUsuario = @nombreUsuario";
                    command.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                    command.CommandType = CommandType.Text;
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }
        public string GetPregunta(string nombreUsuario)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT Pregunta FROM empleado WHERE NombreUsuario = @nombreUsuario";
                    command.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                    command.CommandType = CommandType.Text;
                    string pregunta = command.ExecuteScalar() as string;
                    return pregunta;
                }
            }
        }

        public bool VerifyUserAndResetPassword(string ci, string respuesta, string encryptedPassword)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM empleado WHERE CI = @ci AND Respuesta = @respuesta";
                    command.Parameters.AddWithValue("@ci", ci);
                    command.Parameters.AddWithValue("@respuesta", respuesta);
                    command.CommandType = CommandType.Text;

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            int userId = reader.GetInt32(0);

                            // Actualizar la contraseña en la base de datos
                            bool passwordUpdated = UpdatePassword(userId, encryptedPassword);

                            return passwordUpdated;
                        }
                    }
                }
            }

            return false;
        }

        private bool UpdatePassword(int userId, string encryptedPassword)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "UPDATE empleado SET ClaveUsuario = @nuevaContraseña WHERE ID_Empleado = @userId";
                    command.Parameters.AddWithValue("@nuevaContraseña", encryptedPassword);
                    command.Parameters.AddWithValue("@userId", userId);
                    command.CommandType = CommandType.Text;

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }

        public List<UsuarioSesion> ObtenerTodosEmpleados()
        {
            List<UsuarioSesion> empleados = new List<UsuarioSesion>();

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT * FROM empleado";
                        command.CommandType = CommandType.Text;

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int iDEmpleado = reader.GetInt32(0);
                                string nombreEmpleado = reader.GetString(1);
                                string apellidos = reader.GetString(2);
                                int cI = reader.GetInt32(3);
                                string direccion = reader.GetString(4);
                                int celular = reader.GetInt32(5);
                                string correo = reader.GetString(6);
                                string nombreUsuario = reader.GetString(7);
                                string claveUsuario = reader.GetString(8);
                                string pregunta = reader.GetString(9);
                                string respuesta = reader.GetString(10);
                                int iD_TipoUsuario = reader.GetInt32(11);
                                string departamento = reader.GetString(12);
                                int intentosFallidos = reader.GetInt32(13);
                                string estadoCuenta = reader.GetString(14);

                                UsuarioSesion usuarioSesion = new UsuarioSesion(iDEmpleado, nombreEmpleado, apellidos, cI, direccion, celular, correo, nombreUsuario, claveUsuario, pregunta, respuesta, iD_TipoUsuario, departamento, intentosFallidos, estadoCuenta);
                                empleados.Add(usuarioSesion);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los empleados: " + ex.Message);
            }

            return empleados;
        }

        public bool CambiarAccesoBD(int idEmpleado, string nuevoEstado, int numerointentos)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string query = "UPDATE empleado SET EstadoCuenta = @NuevoEstado, IntentosFallidos = @Numerointentos WHERE ID_Empleado = @ID";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@NuevoEstado", nuevoEstado);
                    command.Parameters.AddWithValue("@ID", idEmpleado);
                    command.Parameters.AddWithValue("@Numerointentos", numerointentos);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar en la BD: " + ex.Message);
                return false;
            }
        }

    }
}
