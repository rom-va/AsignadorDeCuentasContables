using System;
using System.Data;
using DACuyitoApi.DBConnection;
using DACuyitoApi.Model;
using Microsoft.Data.SqlClient;
using System.Transactions;

namespace DACuyitoApi.Repository
{
    public class ProductoCostoRepository : IRepository<ProductoCosto, int>
    {
        private readonly string _connectionString;

        public ProductoCostoRepository()
        {
            _connectionString = ConnHelper.GetConnectionString();
        }

        public bool Create(ProductoCosto entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
        
            using (var conn = new SqlConnection(_connectionString))
            {
                const string cmdText = @"
                    INSERT INTO SsApiProductosCostos (
                        ProductoID, FechaInicioVigencia, FechaFinVigencia, Vigente,
                        Monto, MargenGanancia, UsuarioCreacionID, FechaCreacion
                    ) VALUES (
                        @ProductoID, @FechaInicioVigencia, @FechaFinVigencia, @Vigente,
                        @Monto, @MargenGanancia, @UsuarioCreacionID, @FechaCreacion
                    )";

                using (var cmd = new SqlCommand(cmdText, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductoID", entity.ProductoID);
                    cmd.Parameters.AddWithValue("@FechaInicioVigencia", entity.FechaInicioVigencia);
                    cmd.Parameters.AddWithValue("@FechaFinVigencia", entity.FechaFinVigencia);
                    cmd.Parameters.AddWithValue("@Vigente", entity.Vigente ? 1 : 0);
                    cmd.Parameters.AddWithValue("@Monto", entity.Monto);
                    cmd.Parameters.AddWithValue("@MargenGanancia", entity.MargenGanancia);
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
                        throw new RepositoryException("Error creating product cost", ex);
                    }
                }
            }
            
        }

        public ProductoCosto? GetByID(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                const string cmdText = "SELECT * FROM SsApiProductosCostos WHERE CostoID = @Id";

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
                                return new ProductoCosto
                                {
                                    CostoID = reader.GetInt32(reader.GetOrdinal("CostoID")),
                                    ProductoID = reader.GetInt32(reader.GetOrdinal("ProductoID")),
                                    FechaInicioVigencia = reader.GetDateTime(reader.GetOrdinal("FechaInicioVigencia")),
                                    FechaFinVigencia = reader.GetDateTime(reader.GetOrdinal("FechaFinVigencia")),
                                    Vigente = reader.GetByte(reader.GetOrdinal("Vigente")) == 1,
                                    Monto = (double)reader.GetDecimal(reader.GetOrdinal("Monto")),
                                    MargenGanancia = (double)reader.GetDecimal(reader.GetOrdinal("MargenGanancia")),
                                    UsuarioCreacionID = reader.GetInt32(reader.GetOrdinal("UsuarioCreacionID")),
                                    FechaCreacion = reader.GetDateTime(reader.GetOrdinal("FechaCreacion")),
                                    UsuarioModificacion = reader.IsDBNull("UsuarioModificacionID") ?
                                        null : reader.GetInt32(reader.GetOrdinal("UsuarioModificacionID")),
                                    FechaModificacion = reader.IsDBNull("FechaModificacion") ?
                                        null : reader.GetDateTime(reader.GetOrdinal("FechaModificacion"))
                                };
                            }
                            return null;
                        }
                    }
                    catch (SqlException ex)
                    {
                        throw new RepositoryException("Error retrieving product cost", ex);
                    }
                }
            }
        }

        public bool Update(ProductoCosto entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            using (var conn = new SqlConnection(_connectionString))
            {
                const string cmdText = @"
                    UPDATE SsApiProductosCostos
                    SET                         
                        FechaInicioVigencia = @FechaInicioVigencia,
                        FechaFinVigencia = @FechaFinVigencia,
                        Vigente = @Vigente,
                        Monto = @Monto,
                        MargenGanancia = @MargenGanancia,
                        UsuarioModificacionID = @UsuarioModificacion,
                        FechaModificacion = @FechaModificacion
                    WHERE CostoID = @CostoID";

                using (var cmd = new SqlCommand(cmdText, conn))
                {
                    cmd.Parameters.AddWithValue("@CostoID", entity.CostoID);                    
                    cmd.Parameters.AddWithValue("@FechaInicioVigencia", entity.FechaInicioVigencia);
                    cmd.Parameters.AddWithValue("@FechaFinVigencia", entity.FechaFinVigencia);
                    cmd.Parameters.AddWithValue("@Vigente", entity.Vigente ? 1 : 0);
                    cmd.Parameters.AddWithValue("@Monto", entity.Monto);
                    cmd.Parameters.AddWithValue("@MargenGanancia", entity.MargenGanancia);
                    cmd.Parameters.AddWithValue("@UsuarioModificacion", entity.UsuarioModificacion);
                    cmd.Parameters.AddWithValue("@FechaModificacion", entity.FechaModificacion);

                    try
                    {
                        conn.Open();
                        int result = cmd.ExecuteNonQuery();
                        return result == 1;
                    }
                    catch (SqlException ex)
                    {
                        throw new RepositoryException("Error updating product cost", ex);
                    }
                }
            }

        }

        public bool DeleteById(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                const string cmdText = "DELETE FROM SsApiProductosCostos WHERE CostoID = @Id";

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
                        throw new RepositoryException("Error deleting product cost", ex);
                    }
                }
            }
            
        }
    }
}