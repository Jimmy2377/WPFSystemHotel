using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Entity_layer
{
    internal class Solicitud
    {
        public Solicitud(int iD_Solicitud, string descripcion, string cantidad, string antecedentes, string precauciones)
        {
            ID_Solicitud = iD_Solicitud;
            Descripcion = descripcion;
            Cantidad = cantidad;
            Antecedentes = antecedentes;
            Precauciones = precauciones;
        }

        public int ID_Solicitud { get; set; }
        public string Descripcion { get; set; }
        public string Cantidad { get; set; }
        public string Antecedentes { get; set; }
        public string Precauciones { get; set; }
    }
}
