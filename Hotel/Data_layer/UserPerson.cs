using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;


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
                        return true;
                    }
                    else return false; 

                }
            }
        }
    }
}
