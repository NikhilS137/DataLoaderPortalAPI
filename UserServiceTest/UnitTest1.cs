using Moq;
using UserService.Controllers;
using UserService.DBContext;

namespace UserServiceTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void ForgetPasswordController()
        {
            ///Arrange
            var context = new Mock<DBDataLoaderPortalContext>();



            Assert.Pass();
        }
    }
}