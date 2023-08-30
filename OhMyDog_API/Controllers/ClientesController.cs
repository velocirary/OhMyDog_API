using Microsoft.AspNetCore.Mvc;
using System.Data.Odbc;
using static OhMyDog_API.Model.Parameters;
using System.Web;
using OhMyDog_API.Model.Clientes;

namespace OhMyDog_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : ControllerBase
    {
        [HttpGet]
        [Route("RetornarInformacoesBasicas")]
        public async Task<IActionResult> GetInformacoesBasicas()
        {
            try
            {
                var loginCliente = new LoginCliente();

                string queryString = "SELECT * FROM Cliente";
                OdbcCommand command = new OdbcCommand(queryString, connection);
                OdbcDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    loginCliente.CodCliente = reader["codCliente"].ToString();
                    loginCliente.Nome = reader["Nome"].ToString();
                }

                return Ok(loginCliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpGet]
        [Route("InserirNovoCliente")]
        public LoginCliente PostNovoCliente([FromBody] LoginCliente cliente)
        {
            string nome = cliente.Nome;

            return null;
        }

    }
}
