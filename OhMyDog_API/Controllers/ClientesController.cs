using Microsoft.AspNetCore.Mvc;
using System.Data.Odbc;
using static OhMyDog_API.Model.Parameters;
using System.Web;
using OhMyDog_API.Model.Clientes;
using OhMyDog_API.Model.Pets;

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

        [HttpPost]
        [Route("InserirNovoCliente")]
        public async Task<IActionResult> PostNovoCliente([FromBody] LoginCliente cliente)
        {
            try
            {
                if (cliente == null)
                    return BadRequest();

                string queryString = $"INSERT INTO cliente (codCLiente, Nome) VALUES ('{cliente.CodCliente}', '{cliente.Nome}')";
                OdbcCommand command = new OdbcCommand(queryString, connection);
                command.ExecuteReader();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpPatch]
        [Route("AtualizarCliente")]
        public async Task<IActionResult> AtualizarCliente([FromBody] LoginCliente cliente)
        {
            try
            {
                string queryString = $"UPDATE Cliente SET " +
                    $"Nome = '{cliente.Nome}', " +
                    $"CPF = '{cliente.CPF}', " +
                    $"DataNascimento = '{cliente.DataNasc}', " +
                    $"CEP = '{cliente.CEP}', " +
                    $"Logradouro = '{cliente.Logradouro}', " +
                    $"Bairro = '{cliente.Bairro}', "+
                    $"Municipio = '{cliente.Municipio}', "+
                    $"Estado = '{cliente.Estado}', "+
                    $"Email = '{cliente.Email}', " +
                    $"Senha = '{cliente.Senha}', "+
                    $"Celular = '{cliente.Celular}' ";

                OdbcCommand command = new OdbcCommand(queryString, connection);
                command = new OdbcCommand(queryString, connection);
                command.ExecuteReader();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpDelete]
        [Route("DeletarCliente/{codCliente}")]
        public async Task<IActionResult> DeletarCliente([FromRoute] string codCliente)
        {
            try
            {
                string queryString = $"SELECT codCliente FROM Pet WHERE codCliente = '{codCliente}'";
                OdbcCommand command = new OdbcCommand(queryString, connection);

                queryString = $"DELETE FROM Cliente WHERE codPet ='{codCliente}'";
                command = new OdbcCommand(queryString, connection);
                command.ExecuteReader();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

    }
}
