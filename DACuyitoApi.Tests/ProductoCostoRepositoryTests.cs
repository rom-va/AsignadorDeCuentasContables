using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DACuyitoApi.Repository;
using DACuyitoApi.Model;
using System.Transactions;

namespace DACuyitoApi.Tests
{
    [TestClass]
    public class ProductoCostoRepositoryTests
    {
        private const int TestId = 1;
        private ProductoCostoRepository _repo;

        [TestInitialize]
        public void Setup()
        {
            _repo = new ProductoCostoRepository();
        }

        [TestMethod]
        public void Create_ValidProductoCosto()
        {
            var costo = new ProductoCosto
            {
                ProductoID = 2,
                FechaInicioVigencia = DateTime.Today,
                FechaFinVigencia = DateTime.Today.AddYears(1),
                Vigente = true,
                Monto = 10.99,
                MargenGanancia = 0.3,
                UsuarioCreacionID = 1099,
                FechaCreacion = DateTime.Now
            };

            bool result = _repo.Create(costo);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetById_ExistingProductoCosto()
        {
            var result = _repo.GetByID(TestId);

            Assert.IsNotNull(result);
            Assert.AreEqual(TestId, result.CostoID);
            Assert.AreEqual(10.99, result.Monto);
        }

        [TestMethod]
        public void Update_ValidProductoCosto()
        {
            var costo = _repo.GetByID(TestId);
            costo.Monto = 12.99;
            costo.Vigente = false;
            costo.UsuarioModificacion = 1099;
            costo.FechaModificacion = DateTime.Now;

            bool result = _repo.Update(costo);
            Assert.IsTrue(result);

            var updated = _repo.GetByID(TestId);
            Assert.AreEqual(12.99, updated.Monto);
            Assert.IsFalse(updated.Vigente);
        }

        [TestMethod]
        public void DeleteById_ExistingProductoCosto()
        {
            bool result = _repo.DeleteById(TestId);
            Assert.IsTrue(result);

            var deleted = _repo.GetByID(TestId);
            Assert.IsNull(deleted);
        }
    }
}