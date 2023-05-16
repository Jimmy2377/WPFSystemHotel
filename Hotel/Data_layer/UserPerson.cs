using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using Hotel.Entity_layer;

namespace Hotel.Data_layer
{
    class UserPerson:ConnectionToMysql
    {
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
                        }
                        return true;
                    }
                    else return false; 

                }
            }
        }
    }
}
