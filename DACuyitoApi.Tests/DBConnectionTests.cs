using DACuyitoApi.DBConnection;
using Microsoft.Data.SqlClient;
using System.Diagnostics.Metrics;

namespace DACuyitoApi.Tests
{
    [TestClass]
    public class DBConnectionTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void GenerateConnString()
        {            
            var connString = ConnHelper.GetConnectionString();
            TestContext.WriteLine($"Generated Connection String: \"{connString}\"");
            Assert.IsTrue(connString is string);
        }
        [TestMethod]
        public void ConnectToDB()
        {
            using (SqlConnection conn = new SqlConnection(ConnHelper.GetConnectionString()))
            {
                conn.Open();
                Assert.AreEqual(System.Data.ConnectionState.Open, conn.State, "Error: Connection was not opened");
            }
        }
    }
}