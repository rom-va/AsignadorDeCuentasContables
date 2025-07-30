using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DACuyitoApi.Repository;
using DACuyitoApi.Model;
using System.Transactions;

namespace DACuyitoApi.Tests
{
    [TestClass]
    public class MovimientoRepositoryTests
    {
        private const int TestId = 2;
        private MovimientoRepository _repo;

        [TestInitialize]
        public void Setup()
        {
            _repo = new MovimientoRepository();            
        }

        [TestMethod]
        public void Create_ValidMovimiento()
        {
            var movimiento = new Movimiento
            {
                UsuarioID = 1099,
                FechaMovimiento = DateTime.Now,
                Monto = 150.50,
                Moneda = Moneda.PEN,
                Tipo = TipoMovimiento.RECARGA,
                Descripcion = "Test recarga",
                UsuarioCreacionID = 1099,
                FechaCreacion = DateTime.Now
            };

            bool result = _repo.Create(movimiento);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetById_ExistingMovimiento()
        {
            var result = _repo.GetByID(TestId);

            Assert.IsNotNull(result);
            Assert.AreEqual(TestId, result.MovimientoID);
            Assert.AreEqual(Moneda.PEN, result.Moneda);
        }

        [TestMethod]
        public void Update_ValidMovimiento()
        {   
            var movimiento = _repo.GetByID(TestId);
            movimiento.Monto = 200.75;
            movimiento.Tipo = TipoMovimiento.CONSUMO;
            movimiento.UsuarioModificacionID = 1099;
            movimiento.FechaModificacion = DateTime.Now;

            bool result = _repo.Update(movimiento);
            Assert.IsTrue(result);

            var updated = _repo.GetByID(TestId);
            Assert.AreEqual(200.75, updated.Monto);
            Assert.AreEqual(TipoMovimiento.CONSUMO, updated.Tipo);
        }

        [TestMethod]
        public void DeleteById_ExistingMovimiento()
        {
            bool result = _repo.DeleteById(TestId);
            Assert.IsTrue(result);

            var deleted = _repo.GetByID(TestId);
            Assert.IsNull(deleted);
        }
    }
}