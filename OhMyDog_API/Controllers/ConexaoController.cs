using System.Data;
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

        internal static void Conectar()
        {
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
    
        //public static OdbcDataReader ExecutaQuery(string query)
        //{
        //    OdbcDataReader result = null;

        //    if (Model.Parameters.connection.State == System.Data.ConnectionState.Open) Model.Parameters.connection.Close();

        //    Model.Parameters.connection.Open();

        //    OdbcCommand command = new(query, Model.Parameters.connection);

        //    result = command.ExecuteReader();

        //    return result;
        //}

        public static DataTable ExecutaQuery(string query)
        {
            DataTable table = new DataTable();
            using (OdbcConnection conn = new(connectionString))
            {
                conn.Open();
                using (OdbcCommand cmd = new(query, conn))
                {
                    using (OdbcDataReader rdr = cmd.ExecuteReader())
                    {
                        table.Load(rdr);
                    }
                }
            }
            return table;
        }
    }
}
