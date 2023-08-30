using System.Data.Odbc;

namespace OhMyDog_API.Controllers
{
    public static class ConexaoController
    {
        internal static void Conectar()
        {
            string connectionString = "Driver={SQL Server};" +
                "Server=LAPTOP-HPM70D5A\\MSSQLSERVER17;" +
                "Database=Oh_My_Dog;" +
                "Trusted_Connection=yes;";

            OdbcConnection conn = new OdbcConnection(connectionString);

            try
            {
                conn.Open();
                Model.Parameters.connection = conn;
            }
            catch (OdbcException e)
            {
                throw new Exception("Erro ao abrir a conexão: " + e.Message);
            }
        }
    }
}
