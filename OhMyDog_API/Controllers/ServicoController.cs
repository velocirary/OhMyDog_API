using Microsoft.AspNetCore.Mvc;
using OhMyDog_API.Model.Clientes;
using OhMyDog_API.Model.Servicos;
using System.Data.Odbc;
using static OhMyDog_API.Model.Parameters;

namespace OhMyDog_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicoController : ControllerBase
    {
        [HttpGet]
        [Route("RetornarServicosPrestados")]
        public async Task<IActionResult> GetServicosPrestados()
        {
            try
            {
                var servicosPrestados = new ServicoPrestados();

                string queryString = "SELECT * FROM Servico";
                OdbcCommand command = new OdbcCommand(queryString, connection);
                OdbcDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    servicosPrestados.codServico = (int)reader["codServico"];
                    servicosPrestados.Nome = reader["Nome"].ToString();
                }

                return Ok(servicosPrestados);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpPost]
        [Route("InserirNovoServico")]
        public async Task<IActionResult> PostNovoServico([FromBody] ServicoPrestados servicoPrestados)
        {
            try
            {
                string queryString = $"INSERT INTO Servico (codServico, Nome) VALUES ('{servicoPrestados.codServico}', '{servicoPrestados.Nome}')";
                OdbcCommand command = new OdbcCommand(queryString, connection);
                command.ExecuteReader();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpDelete]
        [Route("DeletarServico/{codServico}")]
        public async Task<IActionResult> DeletarServico([FromRoute] string codServico)
        {
            try
            {
                string queryString = $"SELECT codServico FROM Servico WHERE codPet = '{codServico}'";
                OdbcCommand command = new OdbcCommand(queryString, connection);
                OdbcDataReader reader = command.ExecuteReader();

                if (!reader.Read())
                    return BadRequest("Servico não encontrado!");

                queryString = $"DELETE FROM Servico WHERE codPet ='{codServico}'";
                command = new OdbcCommand(queryString, connection);
                command.ExecuteReader();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpPatch]
        [Route("AtualizarServico")]
        public async Task<IActionResult> AtualizarServico([FromBody] ServicoPrestados servicoPrestados)
        {
            try
            {
                string queryString = $"UPDATE Servico SET " +
                    $"CodServico = '{servicoPrestados.codServico}', " +
                    $"Nome = '{servicoPrestados.Nome}' ";

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
    }
}
