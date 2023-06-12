using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Entity_layer
{
    public enum TipoUsuario
    {
        Administrador,
        Usuario
    }
    public class UsuarioSesion
    {
        public static int IDuser { get; set; }
        public static string Nameuser { get; set; }
        public static string Lastnameuser { get; set; }
        public static TipoUsuario TipoUsuario { get; set; }
    }
}
