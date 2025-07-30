using System;
using System.Data;
using DACuyitoApi.DBConnection;
using DACuyitoApi.Model;
using Microsoft.Data.SqlClient;
using System.Transactions;

namespace DACuyitoApi.Repository
{
    public class MovimientoRepository : IRepository<Movimiento, int>
    {
        private readonly string _connectionString;

        public MovimientoRepository()
        {
            _connectionString = ConnHelper.GetConnectionString();
        }

        public bool Create(Movimiento entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            using (var conn = new SqlConnection(_connectionString))
            {
                const string cmdText = @"
                    INSERT INTO SsApiMovimientos (
                        UsuarioID, FechaMovimiento, Monto, Moneda, Tipo, 
                        Descripcion, UsuarioCreacionID, FechaCreacion
                    ) VALUES (
                        @UsuarioID, @FechaMovimiento, @Monto, @Moneda, @Tipo,
                        @Descripcion, @UsuarioCreacionID, @FechaCreacion
                    )";

                using (var cmd = new SqlCommand(cmdText, conn))
                {                   
                    cmd.Parameters.AddWithValue("@UsuarioID", entity.UsuarioID);
                    cmd.Parameters.AddWithValue("@FechaMovimiento", entity.FechaMovimiento);
                    cmd.Parameters.AddWithValue("@Monto", entity.Monto);
                    cmd.Parameters.AddWithValue("@Moneda", entity.Moneda.ToString());
                    cmd.Parameters.AddWithValue("@Tipo", entity.Tipo.ToString());
                    cmd.Parameters.AddWithValue("@Descripcion", (object) entity.Descripcion ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@UsuarioCreacionID", entity.UsuarioCreacionID);
                    cmd.Parameters.AddWithValue("@FechaCreacion", entity.FechaCreacion);

                    try
                    {
                        conn.Open();
                        int result = cmd.ExecuteNonQuery();
                        return result == 1;
                    }
                    catch (SqlException ex)
                    {
                        throw new RepositoryException("Error creating movimiento.", ex);
                    }
                }
            }            
        }

        public Movimiento? GetByID(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                const string cmdText = @"
                    SELECT * FROM SsApiMovimientos 
                    WHERE MovimientoID = @Id";

                using (var cmd = new SqlCommand(cmdText, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    try
                    {
                        conn.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Movimiento
                                {
                                    MovimientoID = reader.GetInt32(reader.GetOrdinal("MovimientoID")),
                                    UsuarioID = reader.GetInt32(reader.GetOrdinal("UsuarioID")),
                                    FechaMovimiento = reader.GetDateTime(reader.GetOrdinal("FechaMovimiento")),
                                    Monto = (double)reader.GetDecimal(reader.GetOrdinal("Monto")),
                                    Moneda = Enum.Parse<Moneda>(reader.GetString(reader.GetOrdinal("Moneda"))),
                                    Tipo = Enum.Parse<TipoMovimiento>(reader.GetString(reader.GetOrdinal("Tipo"))),
                                    Descripcion = reader.IsDBNull("Descripcion") ? null : reader.GetString(reader.GetOrdinal("Descripcion")),
                                    UsuarioCreacionID = reader.GetInt32(reader.GetOrdinal("UsuarioCreacionID")),
                                    FechaCreacion = reader.GetDateTime(reader.GetOrdinal("FechaCreacion")),
                                    UsuarioModificacionID = reader.IsDBNull("UsuarioModificacionID") ?
                                        null : reader.GetInt32(reader.GetOrdinal("UsuarioModificacionID")),
                                    FechaModificacion = reader.IsDBNull("FechaModificacion") ?
                                        null : (DateTime) reader.GetDateTime(reader.GetOrdinal("FechaModificacion"))
                                };
                            }
                            return null;
                        }
                    }
                    catch (SqlException ex)
                    {
                        throw new RepositoryException("Error retrieving movimiento.", ex);
                    }
                }
            }
        }

        public bool Update(Movimiento entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

           using (var conn = new SqlConnection(_connectionString))
           {
                const string cmdText = @"
                    UPDATE SsApiMovimientos
                    SET 
                        FechaMovimiento = @FechaMovimiento,
                        Monto = @Monto,
                        Moneda = @Moneda,
                        Tipo = @Tipo,
                        Descripcion = @Descripcion,
                        UsuarioModificacionID = @UsuarioModificacionID,
                        FechaModificacion = @FechaModificacion
                    WHERE MovimientoID = @MovimientoID";

                using (var cmd = new SqlCommand(cmdText, conn))
                {
                    cmd.Parameters.AddWithValue("@MovimientoID", entity.MovimientoID);
                    cmd.Parameters.AddWithValue("@FechaMovimiento", entity.FechaMovimiento);
                    cmd.Parameters.AddWithValue("@Monto", entity.Monto);
                    cmd.Parameters.AddWithValue("@Moneda", entity.Moneda.ToString());
                    cmd.Parameters.AddWithValue("@Tipo", entity.Tipo.ToString());
                    cmd.Parameters.AddWithValue("@Descripcion", (object) entity.Descripcion ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@UsuarioModificacionID", entity.UsuarioModificacionID);
                    cmd.Parameters.AddWithValue("@FechaModificacion", entity.FechaModificacion);

                    try
                    {
                        conn.Open();
                        int result = cmd.ExecuteNonQuery();                        
                        return result == 1;
                    }
                    catch (SqlException ex)
                    {
                        throw new RepositoryException("Error updating movimiento", ex);
                    }
                }
           }
            
        }

        public bool DeleteById(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                const string cmdText = "DELETE FROM SsApiMovimientos WHERE MovimientoID = @Id";

                using (var cmd = new SqlCommand(cmdText, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    try
                    {
                        conn.Open();
                        int result = cmd.ExecuteNonQuery();
                        return result == 1;
                    }
                    catch (SqlException ex)
                    {
                        throw new RepositoryException("Error deleting movimiento", ex);
                    }
                }
            }            
        }
    }
}
