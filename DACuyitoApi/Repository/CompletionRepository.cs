using System;
using System.Data;
using DACuyitoApi.DBConnection;
using DACuyitoApi.Model;
using Microsoft.Data.SqlClient;
using System.Transactions;

namespace DACuyitoApi.Repository
{
    public class CompletionRepository : IRepository<Completion, int>
    {
        private readonly string _connectionString;

        public CompletionRepository()
        {
            _connectionString = ConnHelper.GetConnectionString();
        }

        public bool Create(Completion entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            using (var conn = new SqlConnection(_connectionString))
            {
                const string cmdText = @"
                    INSERT INTO SsApiCompletions (
                        MovimientoID, UsuarioID, Modelo, FechaCompletion,
                        InputTokensCacheHit, InputTokensCacheMiss, TotalInputTokens, TotalOutputTokens, TotalTokens,
                        CostoUnitarioCacheHit, CostoUnitarioCacheMiss, CostoUnitarioInput, CostoUnitarioOutput,
                        CostoEstandar, CostoReal, MargenCacheHit, MargenCacheMiss, MargenInput, MargenOutput,
                        UsuarioCreacionID, FechaCreacion
                    ) VALUES (
                        @MovimientoID, @UsuarioID, @Modelo, @FechaCompletion,
                        @InputTokensCacheHit, @InputTokensCacheMiss, @TotalInputTokens, @TotalOutputTokens, @TotalTokens,
                        @CostoUnitarioCacheHit, @CostoUnitarioCacheMiss, @CostoUnitarioInput, @CostoUnitarioOutput,
                        @CostoEstandar, @CostoReal, @MargenCacheHit, @MargenCacheMiss, @MargenInput, @MargenOutput,
                        @UsuarioCreacionID, @FechaCreacion
                    )";

                using (var cmd = new SqlCommand(cmdText, conn))
                {
                    AddCompletionParameters(cmd, entity);

                    try
                    {
                        conn.Open();
                        int result = cmd.ExecuteNonQuery();
                        return result == 1;
                    }
                    catch (SqlException ex)
                    {
                        throw new RepositoryException("Error creating completion", ex);
                    }
                }
            }            
        }

        public Completion? GetByID(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                const string cmdText = "SELECT * FROM SsApiCompletions WHERE CompletionID = @Id";

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
                                return MapCompletionFromReader(reader);
                            }
                            return null;
                        }
                    }
                    catch (SqlException ex)
                    {
                        throw new RepositoryException("Error retrieving completion", ex);
                    }
                }
            }
        }

        public bool Update(Completion entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            
            using (var conn = new SqlConnection(_connectionString))
            {
                const string cmdText = @"
                    UPDATE SsApiCompletions
                    SET 
                        MovimientoID = @MovimientoID,
                        UsuarioID = @UsuarioID,
                        Modelo = @Modelo,
                        FechaCompletion = @FechaCompletion,
                        InputTokensCacheHit = @InputTokensCacheHit,
                        InputTokensCacheMiss = @InputTokensCacheMiss,
                        TotalInputTokens = @TotalInputTokens,
                        TotalOutputTokens = @TotalOutputTokens,
                        TotalTokens = @TotalTokens,
                        CostoUnitarioCacheHit = @CostoUnitarioCacheHit,
                        CostoUnitarioCacheMiss = @CostoUnitarioCacheMiss,
                        CostoUnitarioInput = @CostoUnitarioInput,
                        CostoUnitarioOutput = @CostoUnitarioOutput,
                        CostoEstandar = @CostoEstandar,
                        CostoReal = @CostoReal,
                        MargenCacheHit = @MargenCacheHit,
                        MargenCacheMiss = @MargenCacheMiss,
                        MargenInput = @MargenInput,
                        MargenOutput = @MargenOutput,
                        UsuarioModificacionID = @UsuarioModificacionID,
                        FechaModificacion = @FechaModificacion
                    WHERE CompletionID = @CompletionID";

                using (var cmd = new SqlCommand(cmdText, conn))
                {
                    AddCompletionParameters(cmd, entity);
                    cmd.Parameters.AddWithValue("@CompletionID", entity.CompletionID);
                    cmd.Parameters.AddWithValue("@UsuarioModificacionID", entity.UsuarioModificacion);
                    cmd.Parameters.AddWithValue("@FechaModificacion", entity.FechaModificacion);

                    try
                    {
                        conn.Open();
                        int result = cmd.ExecuteNonQuery();
                        return result == 1;
                    }
                    catch (SqlException ex)
                    {
                        throw new RepositoryException("Error updating completion", ex);
                    }
                }
            }
            
        }

        public bool DeleteById(int id)
        {  
            using (var conn = new SqlConnection(_connectionString))
            {
                const string cmdText = "DELETE FROM SsApiCompletions WHERE CompletionID = @Id";

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
                        throw new RepositoryException("Error deleting completion", ex);
                    }
                }
            }
            
        }

        private void AddCompletionParameters(SqlCommand cmd, Completion entity)
        {
            cmd.Parameters.AddWithValue("@MovimientoID", (object) entity.MovimientoID ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UsuarioID", entity.UsuarioID);            
            cmd.Parameters.AddWithValue("@Modelo", entity.Modelo.ToString());
            cmd.Parameters.AddWithValue("@FechaCompletion", entity.FechaCompletion);
            cmd.Parameters.AddWithValue("@InputTokensCacheHit", (object) entity.InputTokensCacheHit ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@InputTokensCacheMiss", (object) entity.InputTokensCacheMiss ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TotalInputTokens", (object) entity.TotalInputTokens ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TotalOutputTokens", (object) entity.TotalOutputTokens ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TotalTokens", entity.TotalTokens);
            cmd.Parameters.AddWithValue("@CostoUnitarioCacheHit", (object) entity.CostoUnitarioCacheHit ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CostoUnitarioCacheMiss", (object) entity.CostoUnitarioCacheMiss ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CostoUnitarioInput", (object) entity.CostoUnitarioInput ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CostoUnitarioOutput", (object) entity.CostoUnitarioOutput ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CostoEstandar", entity.CostoEstandar);
            cmd.Parameters.AddWithValue("@CostoReal", (object) entity.CostoReal ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MargenCacheHit", (object) entity.MargenCacheHit ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MargenCacheMiss", (object) entity.MargenCacheMiss ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MargenInput", (object) entity.MargenInput ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MargenOutput", (object) entity.MargenOutput ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UsuarioCreacionID", entity.UsuarioCreacionID);
            cmd.Parameters.AddWithValue("@FechaCreacion", entity.FechaCreacion);
        }

        private Completion MapCompletionFromReader(SqlDataReader reader)
        {
            return new Completion
            {
                MovimientoID = reader.IsDBNull("MovimientoID") ? null : (int) reader.GetInt32("MovimientoID"),
                UsuarioID = reader.GetInt32("UsuarioID"),
                CompletionID = reader.GetInt32("CompletionID"),
                Modelo = Enum.Parse<ModeloLLM>(reader.GetString("Modelo")),
                FechaCompletion = reader.GetDateTime("FechaCompletion"),
                InputTokensCacheHit = reader.IsDBNull("InputTokensCacheHit") ? null : reader.GetInt32("InputTokensCacheHit"),
                InputTokensCacheMiss = reader.IsDBNull("InputTokensCacheMiss") ? null : reader.GetInt32("InputTokensCacheMiss"),
                TotalInputTokens = reader.IsDBNull("TotalInputTokens") ? null : reader.GetInt32("TotalInputTokens"),
                TotalOutputTokens = reader.IsDBNull("TotalOutputTokens") ? null : reader.GetInt32("TotalOutputTokens"),
                TotalTokens = reader.GetInt32("TotalTokens"),
                CostoUnitarioCacheHit = reader.IsDBNull("CostoUnitarioCacheHit") ? null : (double?)reader.GetDecimal("CostoUnitarioCacheHit"),
                CostoUnitarioCacheMiss = reader.IsDBNull("CostoUnitarioCacheMiss") ? null : (double?)reader.GetDecimal("CostoUnitarioCacheMiss"),
                CostoUnitarioInput = reader.IsDBNull("CostoUnitarioInput") ? null : (double?)reader.GetDecimal("CostoUnitarioInput"),
                CostoUnitarioOutput = reader.IsDBNull("CostoUnitarioOutput") ? null : (double?)reader.GetDecimal("CostoUnitarioOutput"),
                CostoEstandar = (double)reader.GetDecimal("CostoEstandar"),
                CostoReal = reader.IsDBNull("CostoReal") ? null : (double?)reader.GetDecimal("CostoReal"),
                MargenCacheHit = reader.IsDBNull("MargenCacheHit") ? null : (double?)reader.GetDecimal("MargenCacheHit"),
                MargenCacheMiss = reader.IsDBNull("MargenCacheMiss") ? null : (double?)reader.GetDecimal("MargenCacheMiss"),
                MargenInput = reader.IsDBNull("MargenInput") ? null : (double?)reader.GetDecimal("MargenInput"),
                MargenOutput = reader.IsDBNull("MargenOutput") ? null : (double?)reader.GetDecimal("MargenOutput"),
                UsuarioCreacionID = reader.GetInt32("UsuarioCreacionID"),
                FechaCreacion = reader.GetDateTime("FechaCreacion"),
                UsuarioModificacion = reader.IsDBNull("UsuarioModificacionID") ? null : reader.GetInt32("UsuarioModificacionID"),
                FechaModificacion = reader.IsDBNull("FechaModificacion") ? null : reader.GetDateTime("FechaModificacion")
            };
        }
    }
}