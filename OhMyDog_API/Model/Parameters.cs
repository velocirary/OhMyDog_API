using System.Data.Odbc;

namespace OhMyDog_API.Model
{
    public class Parameters
    {
        public static OdbcConnection connection { get; set; }
        public static string stringConexao { get; set; } = Environment.GetEnvironmentVariable("ConexaoODBC");
    }
}
