using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Hotel.Data_layer
{
    public class ConnectionToMysql
    {
        private readonly string connectionString; //almacenar la cadena de conexión a la base de datos.

        public ConnectionToMysql()
        {
            var appSettings = ConfigurationManager.AppSettings;//proporcionar acceso al archivo de configuración de la aplicación
            connectionString = $"server={appSettings["Servidor"]};port={appSettings["Puerto"]};user id={appSettings["Usuario"]};password={appSettings["Password"]};database={appSettings["NombreDB"]};";

        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
//C:\xampp\mysql\bin
//mysql -h btr3itqyjgkpayqsmsfx-mysql.services.clever-cloud.com -P 3306 -u uurra9sjisnoi7br -p btr3itqyjgkpayqsmsfx
//fWH6qniK98EB3am5aHar
