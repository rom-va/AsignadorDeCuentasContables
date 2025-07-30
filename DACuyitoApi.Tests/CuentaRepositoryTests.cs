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
    public class CuentaRepositoryTests
    {
        [TestMethod]
        public void Create_ValidAccountId1099()
        {
            Cuenta cuenta = new Cuenta()
            {
                UsuarioID = 1099,
                BalanceSoles = 0,
                BalanceDolares = 0,
                UsuarioCreacionID = 1099, 
                FechaCreacion = DateTime.Now
            };
            
            IRepository<Cuenta, int> repo = new CuentaRepository();
            bool result = repo.Create(cuenta);

            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void Create_DuplicateId1099()
        {
            Cuenta cuenta = new Cuenta()
            {
                UsuarioID = 1099,
                BalanceSoles = 0,
                BalanceDolares = 0,
                UsuarioCreacionID = 1099,
                FechaCreacion = DateTime.Now
            };

            IRepository<Cuenta, int> repo = new CuentaRepository();
            bool result = repo.Create(cuenta);

            /* 
             * La cuenta no puede crearse, la excepción es manejada
             * mandando el mensaje de la excepción al stdout.
             * El catch block también devuelve un valor booleano
             * igual a false, por lo que este test termina siendo
             * exitoso.
             */

            Assert.AreEqual(result, false);
        }
        
        [TestMethod]
        public void Create_ValidAccountNoBalanceId1104()
        {
            /*
             * El valor por defecto de las columnas BalanceSoles 
             * y BalanceDolares es igual 0. Esto se encuentra
             * configurado en la base de datos.
             */
            
            Cuenta cuenta = new Cuenta()
            {
                UsuarioID = 1104,
                UsuarioCreacionID = 1099,
                FechaCreacion = DateTime.Now
            };

            IRepository<Cuenta, int> repo = new CuentaRepository();
            bool result = repo.Create(cuenta);

            Assert.AreEqual(result, true);
        }
        
        [TestMethod]
        public void Create_IncompleteAccountNoID()
        {
            Cuenta cuenta = new Cuenta()
            {
                UsuarioCreacionID = 1099,
                FechaCreacion = DateTime.Now
            };

            IRepository<Cuenta, int> repo = new CuentaRepository();
            bool result = repo.Create(cuenta);

            Assert.AreEqual(result, false);
        }
        
        [TestMethod]
        public void Create_IncompleteAccountNoCreation()
        {
            Cuenta cuenta = new Cuenta()
            {
                UsuarioID = 1099
            };

            IRepository<Cuenta, int> repo = new CuentaRepository();
            bool result = repo.Create(cuenta);

            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void GetById_Id1099()
        {
            IRepository<Cuenta, int> repo = new CuentaRepository();
            var result = repo.GetByID(1099);

            Assert.IsInstanceOfType(result, typeof(Cuenta));
            Assert.AreEqual(result.UsuarioID, 1099);            
            Assert.AreEqual(result.BalanceSoles, 0);
            Assert.AreEqual(result.BalanceDolares, 0);
            Assert.AreEqual(result.UsuarioCreacionID, 1099);
            Assert.IsInstanceOfType(result.FechaCreacion, typeof(DateTime));
            Assert.IsNull(result.UsuarioModificacionID);
            Assert.IsNull(result.FechaModificacion);

        }
        
        [TestMethod]
        public void GetById_NonexistentId()
        {
            IRepository<Cuenta, int> repo = new CuentaRepository();
            var result = repo.GetByID(0000);

            Assert.IsNull(result);
        }
        
        [TestMethod]
        public void DeleteById_Id1099()
        {
            IRepository<Cuenta, int> repo = new CuentaRepository();
            bool result = repo.DeleteById(1099);

            Assert.AreEqual(result, true);
        }
        
        [TestMethod]
        public void Update_ValidChangeBalanceId1104()
        {
            Cuenta cuenta = new Cuenta()
            {
                UsuarioID = 1104,
                BalanceSoles = 100,
                BalanceDolares = 100,
                UsuarioModificacionID = 1104,
                FechaModificacion = DateTime.Now
            };

            IRepository<Cuenta, int> repo = new CuentaRepository();
            bool result = repo.Update(cuenta);

            Assert.AreEqual(result, true);
        }
    }
}
