using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DACuyitoApi.Repository;
using DACuyitoApi.Model;
using System.Transactions;

namespace DACuyitoApi.Tests
{
    [TestClass]
    public class ProductoRepositoryTests
    {
        private const int TestId = 1;
        private ProductoRepository _repo;

        [TestInitialize]
        public void Setup()
        {
            _repo = new ProductoRepository();
        }

        [TestMethod]
        public void Create_ValidProducto()
        {
            var producto = new Producto
            {
                NombreProducto = "Test Product",
                Descripcion = "Test description",
                UnidadDeMedida = UnidadDeMedida.TOKEN,
                UsuarioCreacionID = 1099,
                FechaCreacion = DateTime.Now
            };

            bool result = _repo.Create(producto);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetById_ExistingProducto()
        {
            var result = _repo.GetByID(TestId);

            Assert.IsNotNull(result);
            Assert.AreEqual(TestId, result.ProductoID);
            Assert.AreEqual("Test Product", result.NombreProducto);
        }

        [TestMethod]
        public void Update_ValidProducto()
        {
            var producto = _repo.GetByID(TestId);
            producto.Descripcion = "Updated description";
            producto.UsuarioModificacion = 1099;
            producto.FechaModificacion = DateTime.Now;

            bool result = _repo.Update(producto);
            Assert.IsTrue(result);

            var updated = _repo.GetByID(TestId);
            Assert.AreEqual("Updated description", updated.Descripcion);
        }

        [TestMethod]
        public void DeleteById_ExistingProducto()
        {
            bool result = _repo.DeleteById(TestId);
            Assert.IsTrue(result);

            var deleted = _repo.GetByID(TestId);
            Assert.IsNull(deleted);
        }
    }
}