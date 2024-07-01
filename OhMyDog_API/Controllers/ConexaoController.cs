using OhMyDog_API.Model;
using System.Data;
using System.Data.SqlClient;

namespace OhMyDog_API.Controllers
{
    public static class ConexaoController
    {
        private static string connectionString = Parameters.stringConexao;            
        internal static void Conectar()
        {
            try
            {                            
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        Console.WriteLine("Conexão com o banco de dados estabelecida com sucesso!");
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"SqlException ao estabelecer conexão com o banco de dados: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao conectar: " + ex.Message);
            }
        }

        public static async Task<DataTable> ExecutarQuery(string query)
        {
            DataTable table = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                    {
                        table.Load(rdr);
                    }
                }
            }
            return table;
        }
    }
}
