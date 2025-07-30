using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DACuyitoApi.Repository;
using DACuyitoApi.Model;
using System.Transactions;

namespace DACuyitoApi.Tests
{
    [TestClass]
    public class CompletionRepositoryTests
    {
        private const int TestId = 4;
        private CompletionRepository _repo;

        [TestInitialize]
        public void Setup()
        {
            _repo = new CompletionRepository();
        }

        [TestMethod]
        public void Create_ValidCompletion()
        {
            var completion = new Completion
            {                
                UsuarioID = 1099,
                Modelo = ModeloLLM.DEEPSEEK,
                FechaCompletion = DateTime.Now,
                TotalTokens = 1500,
                CostoEstandar = 0.05,
                UsuarioCreacionID = 1099,
                FechaCreacion = DateTime.Now
            };

            bool result = _repo.Create(completion);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetById_ExistingCompletion()
        {
            var result = _repo.GetByID(TestId);

            Assert.IsNotNull(result);
            Assert.AreEqual(TestId, result.CompletionID);
            Assert.AreEqual(ModeloLLM.DEEPSEEK, result.Modelo);
        }

        [TestMethod]
        public void Update_ValidCompletion()
        {
            var completion = _repo.GetByID(TestId);
            completion.TotalTokens = 2000;
            completion.CostoEstandar = 0.07;
            completion.UsuarioModificacion = 1099;
            completion.FechaModificacion = DateTime.Now;

            bool result = _repo.Update(completion);
            Assert.IsTrue(result);

            var updated = _repo.GetByID(TestId);
            Assert.AreEqual(2000, updated.TotalTokens);
            Assert.AreEqual(0.07, updated.CostoEstandar);
        }

        [TestMethod]
        public void DeleteById_ExistingCompletion()
        {
            bool result = _repo.DeleteById(TestId);
            Assert.IsTrue(result);

            var deleted = _repo.GetByID(TestId);
            Assert.IsNull(deleted);
        }
    }
}