using Microsoft.AspNetCore.Mvc;
using System.Data.Odbc;
using static OhMyDog_API.Model.Parameters;
using System.Web;
using OhMyDog_API.Model.Clientes;
using OhMyDog_API.Model.Pets;
using static OhMyDog_API.Controllers.ConexaoController;

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
                var loginCliente = new DadosClientes();

                string queryString = "SELECT * FROM Cliente";
                OdbcCommand command = new OdbcCommand(queryString, connection);
                OdbcDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    loginCliente.CodCliente = reader["codCliente"].ToString();
                    loginCliente.Nome = reader["Nome"].ToString();
                    loginCliente.CPF = reader["CPF"].ToString();
                    loginCliente.DataNasc = Convert.ToDateTime( reader["DataNascimento"]);
                    loginCliente.CEP = reader["CEP"].ToString();
                    loginCliente.Logradouro = reader["Logradouro"].ToString();
                    loginCliente.Bairro = reader["Bairro"].ToString();
                    loginCliente.Municipio = reader["Municipio"].ToString();
                    loginCliente.Estado = reader["Estado"].ToString();
                    loginCliente.Email = reader["Email"].ToString();
                    loginCliente.Senha = reader["Senha"].ToString();
                    loginCliente.Celular = reader["Celular"].ToString();
                }

                return Ok(loginCliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpPost]
        [Route("LoginCliente")]
        public async Task<IActionResult> PostLogarCliente([FromBody] LoginCliente login)
        {
            try
            {
                var loginCliente = new DadosClientes();

                OdbcDataReader reader = ExecutaQuery($"SELECT * FROM Cliente WHERE Email = '{login.Email}' and Senha = '{login.Senha}'");

                if (reader.Read())
                {
                    return Ok("{\r\n  \"codCliente\":" + reader["codCliente"].ToString() + "\r\n}");
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpPost]
        [Route("InserirNovoCliente")]
        public async Task<IActionResult> PostNovoCliente([FromBody] DadosClientes cliente)
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
        public async Task<IActionResult> AtualizarCliente([FromBody] DadosClientes cliente)
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

                queryString = $"DELETE FROM Cliente WHERE codCliente ='{codCliente}'";
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
