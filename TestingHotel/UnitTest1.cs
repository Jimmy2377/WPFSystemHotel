namespace TestingHotel
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void SumShouldReturnCorrectSum()
        {
            // Arrange
            int a = 5;
            int b = 10;

            // Act
            int result = Sum(a, b);

            // Assert
            Assert.AreEqual(15, result, "La suma no es correcta");
        }

        public int Sum(int a, int b)
        {
            return a + b;
        }
    }
}