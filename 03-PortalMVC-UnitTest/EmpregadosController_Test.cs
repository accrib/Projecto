using _03_PortalMVC.Controllers;
using System;
using Xunit;

namespace _03_PortalMVC_UnitTest
{
    public class EmpregadosController_Test
    {
        [Fact]
        public void Get()
        {
            var controller = new EmpregadosController();

            // act
            var result = controller.Index();

            // assert
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void Get_Empregado()
        {

        }

        [Fact]
        public void Put()
        {

        }

        [Fact]
        public void Post()
        {

        }

        [Fact]
        public void Delete()
        {

        }
    }
}
