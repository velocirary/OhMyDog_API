using Microsoft.AspNetCore.Mvc;
using OhMyDog_API.Model.Patrocinador;
using static OhMyDog_API.Controllers.ConexaoController;
using System.Data;
using static OhMyDog_API.Constantes.PatrocinadorConstantes;

namespace OhMyDog_API.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class PatrocinadorController : ControllerBase
    {
        [HttpGet]
        [Route("TodosPatrocinadores")]
        public async Task<IActionResult> GetTodosPatrocinadores()
        {
            try
            {
                var listPatrocinador = new List<PatrocinadorModel>();

                var table = await ExecutarQuery("SELECT * FROM Patrocinador");

                foreach (DataRow reader in table.Rows)
                {
                    listPatrocinador.Add(
                        item: new PatrocinadorModel
                        {
                            IdPatrocinador = reader[Tabela.IdPatrocinador].ToString(),
                            CNPJ = reader[Tabela.CNPJ].ToString(),
                            RazaoSocial = reader[Tabela.RazaoSocial].ToString(),
                            IdUsuario = reader[Tabela.IdUsuario].ToString(),
                            InscricaoEstadual = reader[Tabela.InscricaoEstadual].ToString(),
                            Observacao = reader[Tabela.Observacao].ToString(),
                            MsgAdministrador = reader[Tabela.MsgAdministrador].ToString(),
                            Status = reader[Tabela.Status].ToString()
                        }
                    );
                }

                return Ok(listPatrocinador);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpGet]
        [Route("Patrocinador/{idPatrocinador}")]
        public async Task<IActionResult> RetornaPatrocinador([FromRoute] string idPatrocinador)
        {
            try
            {
                var infosPatrocinador = new PatrocinadorModel();

                var table = await ExecutarQuery($"SELECT * FROM Patrocinador WHERE {Tabela.IdPatrocinador} = '{idPatrocinador}'");

                foreach (DataRow reader in table.Rows)
                {
                    infosPatrocinador.IdPatrocinador = reader[Tabela.IdPatrocinador].ToString();
                    infosPatrocinador.CNPJ = reader[Tabela.CNPJ].ToString();
                    infosPatrocinador.RazaoSocial = reader[Tabela.RazaoSocial].ToString();
                    infosPatrocinador.IdUsuario = reader[Tabela.IdUsuario].ToString();
                    infosPatrocinador.InscricaoEstadual = reader[Tabela.InscricaoEstadual].ToString();
                    infosPatrocinador.Observacao = reader[Tabela.Observacao].ToString();
                    infosPatrocinador.MsgAdministrador = reader[Tabela.MsgAdministrador].ToString();
                    infosPatrocinador.Status = reader[Tabela.Status].ToString();

                    return Ok(infosPatrocinador);
                }

                return StatusCode(204, "Patrocinador inexistente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpPost]
        [Route("InserirNovoPatrocinador")]
        public async Task<IActionResult> PostNovoPatrocinador([FromBody] NovoPatrocinadorModel patrocinador)
        {
            try
            {
                if (patrocinador == null)
                    return BadRequest();

                await ExecutarQuery(query:
                    $"INSERT INTO Patrocinador ({Tabela.CNPJ}, {Tabela.RazaoSocial}, {Tabela.IdUsuario}, {Tabela.InscricaoEstadual}, {Tabela.Observacao}) VALUES (" +
                    $"'{patrocinador.CNPJ}', " +
                    $"'{patrocinador.RazaoSocial}', " +
                    $"'{patrocinador.IdUsuario}', " +
                    $"'{patrocinador.InscricaoEstadual}', " +
                    $"'{patrocinador.Observacao}' )");

                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpPatch]
        [Route("AprovarPatrocinador/{idPatrocinador}/{statusPatrocinador}/{msgAdministrador}")]
        public async Task<IActionResult> AprovarPatrocinador([FromRoute] string idPatrocinador, [FromRoute] string statusPatrocinador, [FromRoute] string msgAdministrador) 
        {
            try
            {
                string queryString = $"UPDATE Patrocinador SET " +
                                     $"{Tabela.Status} = '{statusPatrocinador}', " +
                                     $"{Tabela.MsgAdministrador} = '{msgAdministrador}' " +
                                     $"WHERE {Tabela.IdPatrocinador} = {idPatrocinador}";
                
                await ExecutarQuery(queryString);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpGet]
        [Route("UltimosPatrocinadoresPendentesAprovacao")]
        public async Task<IActionResult> GetUltimosPatrocinadoresPendentes()
        {
            try
            {
                var listPatrocinadoresPendentes = new List<PatrocinadorModel>();
                var table = await ExecutarQuery($"SELECT TOP 5 * FROM {Tabela.NomeTabela} WHERE {Tabela.Status} = 'P' ORDER BY IdPatrocinador DESC");

                foreach (DataRow reader in table.Rows)
                {
                    listPatrocinadoresPendentes.Add(
                        new PatrocinadorModel
                        {
                            IdPatrocinador = reader[Tabela.IdPatrocinador].ToString(),
                            CNPJ = reader[Tabela.CNPJ].ToString(),
                            RazaoSocial = reader[Tabela.RazaoSocial].ToString(),
                            IdUsuario = reader[Tabela.IdUsuario].ToString(),
                            InscricaoEstadual = reader[Tabela.InscricaoEstadual].ToString(),
                            Observacao = reader[Tabela.Observacao].ToString(),
                            MsgAdministrador = reader[Tabela.MsgAdministrador].ToString(),
                            Status = reader[Tabela.Status].ToString()
                        }
                    );
                }

                return Ok(listPatrocinadoresPendentes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }
    }
}
