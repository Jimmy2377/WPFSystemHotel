using Hotel.Bussines_Layer;
using Hotel.Entity_layer;
using Hotel.View_layer;
using static Hotel.Entity_layer.OrdenCompra;

namespace TestHotel
{
    public class Tests
    {
        private OrdenCompraBLL ordenCompraBLL;
        [SetUp]
        public void Setup()
        {
            ordenCompraBLL = new OrdenCompraBLL();
        }
        [Test]
        public void InsertOrdenCompra_ShouldCreateNewOrder()
        {
            // Arrange
            OrdenCompra ordenCompra = new OrdenCompra(
                0, //ID OrdenCompra
                DateTime.Now, //Fecha Actual
                3, //Tiempo Entrega
                Convert.ToDouble(100), //Monto Total
                EstadoOrdenCompra.Recibido,//Estado inicial
                "Mantenimiento",
                "Directa",
                15, //Empleado
                null // Fecha de entrega
            );

            ordenCompra.DetallesCompra = new List<DetalleCompra>
            {
                new DetalleCompra(28, 5, null, 0),//Alicate de Tenaza
                new DetalleCompra(45, 10, null, 0)//Pintura Monopol Blanco
            };
        }
        [Test]
        public void InsertarCotizacion_ShouldCreateNew()
        {
            string nombreProducto = "Alicate de Aislado";
            string descripcion = "Made in Alemania";
            double precioUnitario = Convert.ToDouble("50,99");
            string tamano = "Nº 5";
            Categoria categoria = new Categoria(16, "Herramientas de mano");
            Proveedor proveedor = new Proveedor(36, "Carlos Martinez", "47584394", "Cochabamba", "76342453");

            Cotizacion cotizacion = new Cotizacion(0, nombreProducto, descripcion, precioUnitario, tamano, categoria, proveedor, Cotizacion.EstadoCotizacion.Pendiente);
        }
        [Test]
        public void EncryptPassword_ShouldEncryptPassword()
        {
            // Arrange
            string password = "abc123";
            string expectedHash = "6ca13d52ca70c883e0f0bb101e425a89e8624de51db2d2392593af6a84118090";
            WorkerModel workerModel = new();
            // Act
            string encryptedPassword = workerModel.EncryptPassword(password);

            // Assert
            Assert.AreEqual(expectedHash, encryptedPassword);
        }
        
    }
}