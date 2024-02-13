using Microsoft.AspNetCore.Mvc;
using System.Data.Odbc;
using static OhMyDog_API.Model.Parameters;
using System.Web;
using OhMyDog_API.Model.Clientes;
using OhMyDog_API.Model.Pets;
using static OhMyDog_API.Controllers.ConexaoController;
using System.Data;

namespace OhMyDog_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : ControllerBase
    {
        [HttpGet]
        [Route("TodosClientes")]
        public async Task<IActionResult> GetTodosClientes()
        {
            try
            {
                var loginCliente = new DadosClientes();

                var table = ExecutaQuery("SELECT * FROM Cliente");

                foreach (DataRow reader in table.Rows)
                {
                    loginCliente.CodCliente = reader["codCliente"].ToString();
                    loginCliente.Nome = reader["Nome"].ToString();
                    loginCliente.CPF = reader["CPF"].ToString();
                    loginCliente.DataNasc = Convert.ToDateTime(reader["DataNascimento"]).ToString("dd/MM/yyyy");
                    loginCliente.CEP = reader["CEP"].ToString();
                    loginCliente.Logradouro = reader["Logradouro"].ToString();
                    loginCliente.Numero = reader["Numero"].ToString();
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

        [HttpGet]
        [Route("Cliente/{codCliente}")]
        public async Task<IActionResult> RetornarCliente([FromRoute] string codCliente)
        {
            try
            {
                var loginCliente = new DadosClientes();

                var table = ExecutaQuery($"SELECT * FROM Cliente WHERE codCliente = '{codCliente}'");
                
                foreach (DataRow reader in table.Rows)
                {
                    loginCliente.CodCliente = reader["codCliente"].ToString();
                    loginCliente.Nome = reader["Nome"].ToString();
                    loginCliente.CPF = reader["CPF"].ToString();
                    loginCliente.DataNasc = Convert.ToDateTime(reader["DataNascimento"]).ToString("dd/MM/yyyy");
                    loginCliente.CEP = reader["CEP"].ToString();
                    loginCliente.Logradouro = reader["Logradouro"].ToString();
                    loginCliente.Numero = reader["Numero"].ToString();
                    loginCliente.Bairro = reader["Bairro"].ToString();
                    loginCliente.Municipio = reader["Municipio"].ToString();
                    loginCliente.Estado = reader["Estado"].ToString();
                    loginCliente.Email = reader["Email"].ToString();
                    loginCliente.Senha = reader["Senha"].ToString();
                    loginCliente.Celular = reader["Celular"].ToString();

                    return Ok(loginCliente);
                }

                return StatusCode(204, "Usuário inexistente");
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

                var table = ExecutaQuery($"SELECT * FROM Cliente WHERE Email = '{login.Email}' and Senha = '{login.Senha}'");

                if (table.Rows.Count > 0)
                {
                    return Ok($"{{\r\n  \"codCliente\":\"{table.Rows[0]["codCliente"]}\",\r\n  \"nome\":\"{table.Rows[0]["Nome"]}\"\r\n}}");
                }
                else
                    return StatusCode(204, "Credenciais inválidas");
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

                ExecutaQuery($"INSERT INTO cliente (codCLiente, Nome) VALUES ('{cliente.CodCliente}', '{cliente.Nome}')");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpPatch]
        [Route("AtualizarCliente")]
        public async Task<IActionResult> AtualizarCliente([FromBody] AtualizarCliente cliente)
        {
            try
            {
                string queryString = $"UPDATE Cliente SET " +
                    $"Nome = '{cliente.Nome}', " +
                    $"CPF = '{cliente.CPF}', " +
                    $"DataNascimento = '{cliente.DataNasc}', " +
                    $"CEP = '{cliente.CEP}', " +
                    $"Logradouro = '{cliente.Logradouro}', " +
                    $"Numero = '{cliente.Numero}', " +
                    $"Bairro = '{cliente.Bairro}', "+
                    $"Municipio = '{cliente.Municipio}', "+
                    $"Estado = '{cliente.Estado}' "+
                    $"WHERE CodCliente = '{cliente.CodCliente}'";

                ExecutaQuery(queryString);

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
                var table = ExecutaQuery($"SELECT codCliente FROM Cliente WHERE codCliente = '{codCliente}'");

                if (table.Rows.Count == 0)
                    return BadRequest("Agendamento não encontrado!");

                ExecutaQuery($"DELETE FROM Cliente WHERE codCliente ='{codCliente}'");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

    }
}
