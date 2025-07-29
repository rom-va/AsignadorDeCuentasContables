using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DACuyitoApi.Model;
using DACuyitoApi.Repository;

namespace DACuyitoApi.Tests
{
    [TestClass]
    public class UsuarioRepositoryTests
    {
        [TestMethod]
        public void GetById_GetId24()
        {
            var repo = new UsuarioRepository();
            var result = repo.GetByID(24);
            
            Assert.IsInstanceOfType(result, typeof(Usuario));

            Assert.AreEqual(24, result.UsuarioID);
            Assert.AreEqual("Administrador abasoft cloud", result.NombreCompleto);
            Assert.AreEqual("admin@abasoft.website", result.Email);
            Assert.AreEqual("grQdaL3fMnAQ4lOIoSYUJpfRSvk=", result.Password);            
        }

        [TestMethod]
        public void GetById_NonexistentId()
        {
            var repo = new UsuarioRepository();
            var result = repo.GetByID(0000);
            Assert.IsNull(result);
        }
    }
}
