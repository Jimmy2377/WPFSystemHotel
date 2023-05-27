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
    public class ConnectionToMysql
    {
        private readonly string servidor = "btr3itqyjgkpayqsmsfx-mysql.services.clever-cloud.com";
        private readonly string puerto = "3306";
        private readonly string usuario = "uurra9sjisnoi7br";
        private readonly string password = "8VviXT9zhB87DtwZSwFZ";
        private readonly string nombreDB = "btr3itqyjgkpayqsmsfx";
        private readonly string connectionString;

        public ConnectionToMysql()
        {
            connectionString = ($"server={servidor};port={puerto};user id={usuario};password={password};database={nombreDB};");
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
//C:\xampp\mysql\bin
//mysql -h btr3itqyjgkpayqsmsfx-mysql.services.clever-cloud.com -P 3306 -u uurra9sjisnoi7br -p btr3itqyjgkpayqsmsfx
