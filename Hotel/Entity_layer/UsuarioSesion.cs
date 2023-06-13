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
        public UsuarioSesion(string nombreEmpleado, string apellidos, string cI, string direccion, string celular, string correo, string nombreUsuario, string claveUsuario, string pregunta, string respuesta, int iD_TipoUsuario, string departamento)
        {
            NombreEmpleado = nombreEmpleado;
            Apellidos = apellidos;
            CI = cI;
            Direccion = direccion;
            Celular = celular;
            Correo = correo;
            NombreUsuario = nombreUsuario;
            ClaveUsuario = claveUsuario;
            Pregunta = pregunta;
            Respuesta = respuesta;
            ID_TipoUsuario = iD_TipoUsuario;
            Departamento = departamento;
        }

        public static int IDuser { get; set; }
        public static string Nameuser { get; set; }
        public static string Lastnameuser { get; set; }
        public static TipoUsuario TipoUsuario { get; set; }

        public string NombreEmpleado { get; set; }
        public string Apellidos { get; set; }
        public string CI { get; set; }
        public string Direccion { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public string NombreUsuario { get; set; }
        public string ClaveUsuario { get; set; }
        public string Pregunta { get; set; }
        public string Respuesta { get; set; }
        public int ID_TipoUsuario { get; set; }
        public string Departamento { get; set; }
    }
}
