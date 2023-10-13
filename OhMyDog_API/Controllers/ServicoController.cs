using Microsoft.AspNetCore.Mvc;
using OhMyDog_API.Model.Clientes;
using OhMyDog_API.Model.Servicos;
using System.Data.Odbc;
using static OhMyDog_API.Model.Parameters;
using static OhMyDog_API.Controllers.ConexaoController;

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

                var reader = ExecutaQuery("SELECT * FROM Servico");

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
                ExecutaQuery($"INSERT INTO Servico (codServico, Nome) VALUES ('{servicoPrestados.codServico}', '{servicoPrestados.Nome}')");
                
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
                OdbcDataReader reader = ExecutaQuery($"SELECT codServico FROM Servico WHERE codPet = '{codServico}'");

                if (!reader.Read())
                    return BadRequest("Servico não encontrado!");

                ExecutaQuery($"DELETE FROM Servico WHERE codPet ='{codServico}'");

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

                ExecutaQuery(queryString);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }
    }
}
