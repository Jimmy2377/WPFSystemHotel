using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Entity_layer
{
    public class Devolucion
    {
        public int ID_Devolucion { get; set; }
        public string Motivo { get; set; }
        public int ID_OrdenCompra { get; set; }
        public int ID_Producto { get; set; }
    }
}
