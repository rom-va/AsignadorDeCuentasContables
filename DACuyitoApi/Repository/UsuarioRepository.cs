using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DACuyitoApi.Model;
using Microsoft.Data.SqlClient;

namespace DACuyitoApi.Repository
{
    public class UsuarioRepository
    {
        public Usuario? GetByID(int id)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection.ConnHelper.GetConnectionString()))
            {
                const string cmdText = @"
                    SELECT UsuarioID, NombreCompleto, Email, Pass
                    FROM SsUsuarios
                    WHERE UsuarioID = @Id
                    ";

                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value =  id });

                    try
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            return new Usuario
                            {
                                UsuarioID = reader.GetInt32(reader.GetOrdinal("UsuarioID")),
                                NombreCompleto = reader.GetString(reader.GetOrdinal("NombreCompleto")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Password = reader.GetString(reader.GetOrdinal("Pass"))
                            };
                        }
                        return null;
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                        return null;
                    }
                }
            }
        }
    }
}
