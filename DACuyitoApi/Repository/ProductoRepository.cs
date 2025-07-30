using System;
using System.Data;
using DACuyitoApi.DBConnection;
using DACuyitoApi.Model;
using Microsoft.Data.SqlClient;
using System.Transactions;

namespace DACuyitoApi.Repository
{
    public class ProductoRepository : IRepository<Producto, int>
    {
        private readonly string _connectionString;

        public ProductoRepository()
        {
            _connectionString = ConnHelper.GetConnectionString();
        }

        public bool Create(Producto entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            using (var conn = new SqlConnection(_connectionString))
            {
                const string cmdText = @"
                    INSERT INTO SsApiProductos (
                        NombreProducto, Descripcion, UnidadDeMedida,
                        UsuarioCreacionID, FechaCreacion
                    ) VALUES (
                        @NombreProducto, @Descripcion, @UnidadDeMedida,
                        @UsuarioCreacionID, @FechaCreacion
                    )";

                using (var cmd = new SqlCommand(cmdText, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductoID", entity.ProductoID);
                    cmd.Parameters.AddWithValue("@NombreProducto", entity.NombreProducto);
                    cmd.Parameters.AddWithValue("@Descripcion", entity.Descripcion);
                    cmd.Parameters.AddWithValue("@UnidadDeMedida", entity.UnidadDeMedida.ToString());
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
                        throw new RepositoryException("Error creating producto", ex);
                    }
                }
            }
            
        }

        public Producto? GetByID(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                const string cmdText = "SELECT * FROM SsApiProductos WHERE ProductoID = @Id";

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
                                return new Producto
                                {
                                    ProductoID = reader.GetInt32(reader.GetOrdinal("ProductoID")),
                                    NombreProducto = reader.IsDBNull("NombreProducto") ? null : reader.GetString(reader.GetOrdinal("NombreProducto")),
                                    Descripcion = reader.IsDBNull("Descripcion") ? null : reader.GetString(reader.GetOrdinal("Descripcion")),
                                    UnidadDeMedida = reader.IsDBNull("UnidadDeMedida") ? null : Enum.Parse<UnidadDeMedida>(reader.GetString(reader.GetOrdinal("UnidadDeMedida"))),
                                    UsuarioCreacionID = reader.GetInt32(reader.GetOrdinal("UsuarioCreacionID")),
                                    FechaCreacion = reader.GetDateTime(reader.GetOrdinal("FechaCreacion")),
                                    UsuarioModificacion = reader.IsDBNull("UsuarioModifcacionID") ?
                                        null : reader.GetInt32(reader.GetOrdinal("UsuarioModifcacionID")),
                                    FechaModificacion = reader.IsDBNull("FechaModificacion") ?
                                        null : reader.GetDateTime(reader.GetOrdinal("FechaModificacion"))
                                };
                            }
                            return null;
                        }
                    }
                    catch (SqlException ex)
                    {
                        throw new RepositoryException("Error retrieving producto", ex);
                    }
                }
            }
        }

        public bool Update(Producto entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            using (var conn = new SqlConnection(_connectionString))
            {
                const string cmdText = @"
                    UPDATE SsApiProductos
                    SET 
                        NombreProducto = @NombreProducto,
                        Descripcion = @Descripcion,
                        UnidadDeMedida = @UnidadDeMedida,
                        UsuarioModifcacionID = @UsuarioModificacion,
                        FechaModificacion = @FechaModificacion
                    WHERE ProductoID = @ProductoID";

                using (var cmd = new SqlCommand(cmdText, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductoID", entity.ProductoID);
                    cmd.Parameters.AddWithValue("@NombreProducto", entity.NombreProducto);
                    cmd.Parameters.AddWithValue("@Descripcion", entity.Descripcion);
                    cmd.Parameters.AddWithValue("@UnidadDeMedida", entity.UnidadDeMedida.ToString());
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
                        throw new RepositoryException("Error updating producto", ex);
                    }
                }
            }
            
        }

        public bool DeleteById(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                const string cmdText = "DELETE FROM SsApiProductos WHERE ProductoID = @Id";

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
                        throw new RepositoryException("Error deleting producto", ex);
                    }
                }
            }
        }        
    }
}
