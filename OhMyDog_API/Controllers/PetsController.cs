using Microsoft.AspNetCore.Mvc;
using System.Data.Odbc;
using OhMyDog_API.Model.Pets;
using static OhMyDog_API.Model.Parameters;
using static OhMyDog_API.Controllers.ConexaoController;
using OhMyDog_API.Model.Clientes;
using System.Reflection.PortableExecutable;

namespace OhMyDog_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PetsController : ControllerBase
    {
        [HttpGet]
        [Route("RetornarInformacoesBasicas")]
        public async Task<IActionResult> GetInformacoesBasicas()
        {
            try
            {
                DadosPets pet;
                List<DadosPets> pets = new List<DadosPets>();

                OdbcDataReader reader = ExecutaQuery("SELECT * FROM pet");

                while (reader.Read())                
                    pets.Add(new DadosPets
                    {
                        codPet = reader["codCliente"].ToString(),
                        Sexo = reader["Sexo"].ToString(),
                        Cor = reader["Cor"].ToString(),
                        Nome = reader["Nome"].ToString(),
                        Especie = reader["Especie"].ToString(),
                        codCliente = reader["codCliente"].ToString()
                    });                    
                

                return Ok(pets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpPost]
        [Route("InserirNovoPet")]
        public async Task<IActionResult> PostNovoPet([FromBody] DadosPets pet)
        {
            try
            {
                if (pet == null)
                    return BadRequest();

                ExecutaQuery($"INSERT INTO pet (codPet, Sexo, Cor, Nome, Especie, codCliente) VALUES ('{pet.codPet}', '{pet.Sexo}', '{pet.Cor}', '{pet.Nome}', '{pet.Especie}', '{pet.codCliente}')");                

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpDelete]
        [Route("DeletarPet/{codPet}")]
        public async Task<IActionResult> DeletarPet([FromRoute] string codPet)
        {
            try
            {
                string queryString = $"SELECT codPet FROM Pet WHERE codPet = '{codPet}'";
                OdbcDataReader reader = ExecutaQuery(queryString);

                if (!reader.Read())
                    return BadRequest("Pet não encontrado!");

                queryString = $"DELETE FROM Pet WHERE codPet ='{codPet}'";
                ExecutaQuery(queryString);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }


        [HttpPatch]
        [Route("AtualizarPet")]
        public async Task<IActionResult> AtualizarPet([FromBody] DadosPets pet)
        {
            try
            {
                if (pet == null)
                    return BadRequest();

                string queryString = $"SELECT codPet FROM Pet WHERE codPet = '{pet.codPet}'";
                var reader = ExecutaQuery(queryString);

                if (!reader.Read())
                    return BadRequest("Pet não encontrado!");

                queryString = $"UPDATE Pet SET " +
                    $"Sexo = '{pet.Sexo}', " +
                    $"Cor = '{pet.Cor}', " +
                    $"Nome = '{pet.Nome}', " +
                    $"Especie = '{pet.Especie}' " +
                    $"WHERE codPet = '{pet.codPet}' ";
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
