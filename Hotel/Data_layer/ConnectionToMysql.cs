using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Hotel.Data_layer
{
    public abstract class ConnectionToMysql
    {
        private readonly string servidor = "bx03xbcc3anbn4qledgy-mysql.services.clever-cloud.com";
        private readonly string puerto = "3306"; 
        private readonly string usuario = "u1mm5br4gmrdryoa"; 
        private readonly string password = "D33bt4Jje6LVYbpTRzms";
        private readonly string nombreDB = "bx03xbcc3anbn4qledgy"; 
        private readonly string connectionString;
        public ConnectionToMysql()
        {
            connectionString = ($"server={servidor};port={puerto};user id={usuario};password={password};database={nombreDB};");
        }
        protected MySqlConnection GetConnection()
        {
            return new MySqlConnection( connectionString );
        }
    }
}
