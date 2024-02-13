using Microsoft.AspNetCore.Mvc;
using OhMyDog_API.Model.Clientes;
using OhMyDog_API.Model.Servicos;
using System.Data.Odbc;
using static OhMyDog_API.Model.Parameters;
using static OhMyDog_API.Controllers.ConexaoController;
using System.Data;

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

                foreach (DataRow row in reader.Rows)
                {
                    servicosPrestados.codServico = (int)row["codServico"];
                    servicosPrestados.Nome = row["Nome"].ToString();
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
                var table = ExecutaQuery($"SELECT codServico FROM Servico WHERE codPet = '{codServico}'");

                if (table.Rows.Count == 0)
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
