using Microsoft.AspNetCore.Mvc;
using OhMyDog_API.Model.Patrocinador;
using static OhMyDog_API.Controllers.ConexaoController;
using System.Data;


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
                var listPatrocinador = new List<DadosPatrocinador>();

                var table = ExecutaQuery("SELECT * FROM Patrocinador");

                foreach (DataRow reader in table.Rows)
                {
                    listPatrocinador.Add(
                        item: new DadosPatrocinador
                        {
                            IdPatrocinador = reader["IdPatrocinador"].ToString(),
                            CNPJ = reader["CNPJ"].ToString(),
                            RazaoSocial = reader["RazaoSocial"].ToString(),                            
                            IdUsuario = reader["IdUsuario"].ToString(),
                            Celular = reader["Celular"].ToString(),
                            Status = reader["Status"].ToString()
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
                var infosPatrocinador = new DadosPatrocinador();

                var table = ExecutaQuery($"SELECT * FROM Patrocinador WHERE idPatrocinador = '{idPatrocinador}'");

                foreach (DataRow reader in table.Rows)
                {
                    infosPatrocinador.IdPatrocinador = reader["IdPatrocinador"].ToString();
                    infosPatrocinador.CNPJ = reader["CNPJ"].ToString();
                    infosPatrocinador.RazaoSocial = reader["RazaoSocial"].ToString();                    
                    infosPatrocinador.IdUsuario = reader["IdUsuario"].ToString();
                    infosPatrocinador.Celular = reader["Celular"].ToString();
                    infosPatrocinador.Status = reader["Status"].ToString();


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
        public async Task<IActionResult> PostNovoPatrocinador([FromBody] DadosPatrocinador patrocinador)
        {
            try
            {
                if (patrocinador == null)
                    return BadRequest();

                ExecutaQuery(query: $"INSERT INTO Patrocinador (CNPJ, RazaoSocial, IdUsuario, Celular, Status) VALUES (" +
                                    $"'{patrocinador.CNPJ}', " +
                                    $"'{patrocinador.RazaoSocial}', " +                                    
                                    $"'{patrocinador.IdUsuario}', " +                                    
                                    $"'{patrocinador.Celular}', " +                                    
                                    $"'N')");
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpPatch]
        [Route("AprovarPatrocinador/{idPatrocinador}/{statusPatrocinador}")]
        public async Task<IActionResult> AprovarPatrocinador([FromRoute] string idPatrocinador, [FromRoute] string statusPatrocinador) 
        {
            var patrocinador = new AprovaPatrocinador();

            try
            {
                string queryString = $"UPDATE Patrocinador SET " +                                    
                                     $"Status = '{statusPatrocinador}' " +
                                     $"WHERE idPatrocinador = {idPatrocinador}";
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
