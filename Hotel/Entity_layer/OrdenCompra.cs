using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Entity_layer
{
    public class OrdenCompra
    {
        public OrdenCompra(int idOrdenCompra, DateTime fecha, int tiempoEntrega, double montoTotal, string estado, string departamento, string tipoCompra, int idEmpleado)
        {
            ID_OrdenCompra = idOrdenCompra;
            Fecha = fecha;
            TiempoEntrega = tiempoEntrega;
            MontoTotal = montoTotal;
            Estado = estado;
            Departamento = departamento;
            TipoCompra = tipoCompra;
            ID_Empleado = idEmpleado;
        }

        public int ID_OrdenCompra { get; set; }
        public DateTime Fecha { get; set; }
        public int TiempoEntrega { get; set; }
        public double MontoTotal { get; set; }
        public string Estado { get; set; }
        public string Departamento { get; set; }
        public string TipoCompra { get; set; }
        public int ID_Empleado { get; set; }
        public List<DetalleCompra> DetallesCompra { get; set; }
    }
}
