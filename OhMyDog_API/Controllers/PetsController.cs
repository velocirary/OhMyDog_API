using Microsoft.AspNetCore.Mvc;
using System.Data.Odbc;
using OhMyDog_API.Model.Pets;
using static OhMyDog_API.Model.Parameters;
using static OhMyDog_API.Controllers.ConexaoController;
using OhMyDog_API.Model.Clientes;
using System.Reflection.PortableExecutable;
using System.Data;

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

                var table = ExecutaQuery("SELECT * FROM pet");

                foreach (DataRow reader in table.Rows)
                {
                    pets.Add(new DadosPets
                    {
                        codPet = reader["codPet"].ToString(),
                        Sexo = reader["Sexo"].ToString(),
                        Cor = reader["Cor"].ToString(),
                        Nome = reader["Nome"].ToString(),
                        Especie = reader["Especie"].ToString(),
                        Peso = reader["Peso"].ToString(),
                        Castrado = reader["Castrado"].ToString(),
                        Observacao = reader["Observacao"].ToString(),
                        codCliente = reader["codCliente"].ToString()
                    });
                }

                return Ok(pets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpGet]
        [Route("Pet/{codPet}")]
        public async Task<IActionResult> RetornarPet([FromRoute] string codPet)
        {
            try
            {
                DadosPets pet = null;

                DataTable table = ExecutaQuery($"SELECT * FROM pet WHERE codPet = '{codPet}'");
                foreach (DataRow row in table.Rows)
                {
                    pet = new DadosPets
                    {
                        codPet = row["codPet"].ToString(),
                        Sexo = row["Sexo"].ToString(),
                        Cor = row["Cor"].ToString(),
                        Nome = row["Nome"].ToString(),
                        Especie = row["Especie"].ToString(),
                        Peso = row["Peso"].ToString(),
                        Castrado = row["Castrado"].ToString(),
                        Observacao = row["Observacao"].ToString(),
                        codCliente = row["codCliente"].ToString()
                    };
                }

                return Ok(pet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpGet]
        [Route("PetPorCliente/{codCliente}")]
        public async Task<IActionResult> RetornarPetPorCliente([FromRoute] string codCliente)
        {
            try
            {
                DataTable table = ExecutaQuery($"SELECT * FROM pet WHERE codCliente = '{codCliente}'");

                List<DadosPets> pets = new();

                foreach (DataRow row in table.Rows)
                {
                    pets.Add(new DadosPets
                    {
                        codPet = row["codPet"].ToString(),
                        Nome = row["Nome"].ToString()
                    });
                }

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
                
                DataTable table = ExecutaQuery($"SELECT MAX(codPet) + 1 FROM pet");

                ExecutaQuery($"INSERT INTO pet (codPet, Sexo, Cor, Nome, Especie, Observacao, Castrado, Peso, codCliente) VALUES ('{table.Rows[0][0]}', '{pet.Sexo}', '{pet.Cor}', '{pet.Nome}', '{pet.Especie}', '{pet.Observacao}', '{pet.Castrado}', '{pet.Peso}', '{pet.codCliente}')");

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
                DataTable table = ExecutaQuery(queryString);

                if (table.Rows.Count == 0)
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
                var table = ExecutaQuery(queryString);

                if (table.Rows.Count == 0)
                    return BadRequest("Pet não encontrado!");

                queryString = $"UPDATE Pet SET " +
                    $"Sexo = '{pet.Sexo}', " +
                    $"Cor = '{pet.Cor}', " +
                    $"Nome = '{pet.Nome}', " +
                    $"Especie = '{pet.Especie}', " +
                    $"Observacao = '{pet.Observacao}', " +
                    $"Castrado = '{pet.Castrado}', " +
                    $"Peso = '{pet.Peso.Replace(",", ".")}' " +
                    $"WHERE codPet = '{pet.codPet}' ";

                ExecutaQuery(queryString.Replace("''", "null"));

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }
    }
}
