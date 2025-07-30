using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using DACuyitoApi.DBConnection;
using DACuyitoApi.Model;
using Microsoft.Data.SqlClient;

namespace DACuyitoApi.Repository
{
    public class CuentaRepository : IRepository<Cuenta, int>
    {
        
        private readonly string _connectionString;
        public CuentaRepository()
        {
            _connectionString = ConnHelper.GetConnectionString();
        }

        public bool Create(Cuenta entity)
        {
            // Pre-condition validations
            
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (entity.UsuarioID <= 0)
                throw new ArgumentOutOfRangeException(nameof(entity.UsuarioID),
                    entity.UsuarioID,
                    "UsuarioID deebe ser mayor que cero.");
            if (entity.UsuarioCreacionID <= 0)
                throw new ArgumentOutOfRangeException(nameof(entity.UsuarioCreacionID),
                    entity.UsuarioCreacionID,
                    "UsuarioCreacionID deebe ser mayor que cero.");

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                const string cmdText = @"
                    INSERT INTO SsApiCuentas (UsuarioID, BalanceSoles, BalanceDolares, UsuarioCreacionID, FechaCreacion)
                    VALUES (@Id, @BalanceSoles, @BalanceDolares, @UsuarioCreacionID, @FechaCreacion)"
                    ;
                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    var parameters = new[]
                    {
                        new SqlParameter("@Id", SqlDbType.Int) { Value = entity.UsuarioID },
                        new SqlParameter("@BalanceSoles", SqlDbType.Decimal) { Value = entity.BalanceSoles },
                        new SqlParameter("@BalanceDolares", SqlDbType.Decimal) { Value = entity.BalanceDolares },
                        new SqlParameter("@UsuarioCreacionID", SqlDbType.Int) { Value = entity.UsuarioCreacionID },
                        new SqlParameter("@FechaCreacion", SqlDbType.DateTime2) { Value = entity.FechaCreacion }
                    };

                    cmd.Parameters.AddRange(parameters);

                    try
                    {
                        conn.Open();
                        int result = cmd.ExecuteNonQuery();
                        if (result == 1)
                            return true;
                        else return false;
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error creating account: " + ex.Message);
                        return false;
                    }
                }
            }
        }

        public bool DeleteById(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                const string cmdText = @"
                    DELETE FROM SsApiCuentas
                    WHERE UsuarioID = @Id"
                    ;
                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

                    try
                    {
                        conn.Open();
                        int result = cmd.ExecuteNonQuery();
                        if (result == 1) // A single row was affected
                            return true;
                        else return false;
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error when deleting an account: " + ex.Message);
                        return false;
                    }
                }
            }
        }

        public Cuenta? GetByID(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                const string cmdText = @"SELECT *
                    FROM SsApiCuentas
                    WHERE UsuarioID = @Id"
                    ;
                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

                    try
                    {
                        conn.Open();
                        var reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            Cuenta cuenta = new Cuenta
                            {
                                UsuarioID = reader.GetInt32(reader.GetOrdinal("UsuarioID")),
                                BalanceSoles = (Double) reader.GetDecimal(reader.GetOrdinal("BalanceSoles")),
                                BalanceDolares = (Double) reader.GetDecimal(reader.GetOrdinal("BalanceDolares")),
                                UsuarioCreacionID = reader.GetInt32(reader.GetOrdinal("UsuarioCreacionID")),
                                FechaCreacion = reader.GetDateTime(reader.GetOrdinal("FechaCreacion"))
                            };

                            if (!reader.IsDBNull("UsuarioModificacionID"))
                                cuenta.UsuarioModificacionID = reader.GetInt32(reader.GetOrdinal("UsuarioModificacionID"));

                            if (!reader.IsDBNull("FechaModificacion"))
                                cuenta.FechaModificacion = reader.GetDateTime(reader.GetOrdinal("FechaModificacion"));
                            
                            return cuenta;
                        }
                        return null;
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error when finding an account by ID: " + ex.Message);
                        return null;
                    }
                }
            }
        }

        public bool Update(Cuenta entity)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                const string cmdText = @"
                    UPDATE SsApiCuentas
                    SET BalanceSoles = @BalanceSoles,
                        BalanceDolares = @BalanceDolares,
                        UsuarioModificacionID = @UsuarioModificacionID,
                        FechaModificacion = @FechaModificacion
                    WHERE UsuarioID = @UsuarioID"
                    ;
                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    var parameters = new[]
                    {
                        new SqlParameter("@UsuarioID", SqlDbType.Int) { Value = entity.UsuarioID },
                        new SqlParameter("@BalanceSoles", SqlDbType.Decimal) { Value = entity.BalanceSoles },
                        new SqlParameter("@BalanceDolares", SqlDbType.Decimal) { Value = entity.BalanceDolares },
                        new SqlParameter("@UsuarioModificacionID", SqlDbType.Int) { Value = entity.UsuarioModificacionID },
                        new SqlParameter("@FechaModificacion", SqlDbType.DateTime2) { Value = entity.FechaModificacion}
                    };

                    cmd.Parameters.AddRange(parameters);

                    try
                    {
                        conn.Open();
                        int result = cmd.ExecuteNonQuery();
                        if (result == 1)
                            return true;
                        else return false;
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error when updating an account: " + ex.Message);
                        return false;
                    }
                }
            }
        }
    }
}
