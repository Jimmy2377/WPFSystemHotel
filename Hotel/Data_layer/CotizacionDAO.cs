using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Entity_layer;
using MySql.Data.MySqlClient;

namespace Hotel.Data_layer
{
    class CotizacionDAO
    {
        private ConnectionToMysql connection;

        public CotizacionDAO()
        {
            connection = new ConnectionToMysql();
        }

        


    }
}
