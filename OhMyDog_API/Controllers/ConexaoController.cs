using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;

namespace OhMyDog_API.Controllers
{
    public static class ConexaoController
    {

        private const string connectionString =
            "InsiraSuaConexaoBD";
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

        public static DataTable ExecutaQuery(string query)
        {
            DataTable table = new DataTable();
            using (SqlConnection conn = new(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new(query, conn))
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        table.Load(rdr);
                    }
                }
            }
            return table;
        }
    }
}
