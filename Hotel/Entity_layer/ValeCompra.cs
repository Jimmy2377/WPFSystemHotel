using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Entity_layer
{
    internal class ValeCompra
    {
        public ValeCompra(int iD_valecompra, DateTime fecha, string descripcion, double monto, int iD_Empleado, string departamento)
        {
            ID_valecompra = iD_valecompra;
            Fecha = fecha;
            Descripcion = descripcion;
            Monto = monto;
            ID_Empleado = iD_Empleado;
            Departamento = departamento;
        }

        public int ID_valecompra { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public Double Monto { get; set; }
        public int ID_Empleado { get; set; }
        public string Departamento { get; set; }
    }
}
