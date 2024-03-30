using Microsoft.AspNetCore.Mvc;
using OhMyDog_API.Model.Postagem;
using static OhMyDog_API.Controllers.ConexaoController;
using System.Data;

namespace OhMyDog_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostagemController : ControllerBase
    {
        [HttpGet]
        [Route("TodasPostagens")]
        public async Task<IActionResult> GetTodasPostagens()
        {
            try
            {
                var listPostagens = new List<DadosPostagem>();

                var table = ExecutaQuery("SELECT * FROM Postagem");

                foreach (DataRow reader in table.Rows)
                {
                    listPostagens.Add(
                        item: new DadosPostagem
                        {
                            CodPostagem = reader["codPostagem"].ToString(),
                            Titulo = reader["Titulo"].ToString(),
                            Conteudo = reader["Conteudo"].ToString(),
                            DataPublicacao = Convert.ToDateTime(reader["DataPublicacao"]).ToString("dd/MM/yyyy"),
                            idUsuario = reader["Conteudo"].ToString(),
                            Foto = reader["Foto"].ToString(),
                            Doacao = reader["Doacao"].ToString(),
                            ChavePix = reader["ChavePix"].ToString(),
                            Status = reader["Status"].ToString()
                        }
                    );
                }

                return Ok(listPostagens);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpGet]
        [Route("Postagem/{codPostagem}")]
        public async Task<IActionResult> RetornarPostagem([FromRoute] string codPostagem)
        {
            try
            {
                var infosPostagem = new DadosPostagem();

                var table = ExecutaQuery($"SELECT * FROM Postagem WHERE codPostagem = '{codPostagem}'");
                
                foreach (DataRow reader in table.Rows)
                {
                    infosPostagem.CodPostagem = reader["CodPostagem"].ToString();
                    infosPostagem.Titulo = reader["Titulo"].ToString();
                    infosPostagem.Conteudo = reader["Conteudo"].ToString();
                    infosPostagem.DataPublicacao = Convert.ToDateTime(reader["DataPublicacao"]).ToString("dd/MM/yyyy");
                    infosPostagem.idUsuario = reader["Conteudo"].ToString();
                    infosPostagem.Foto = reader["Foto"].ToString();
                    infosPostagem.Doacao = reader["Doacao"].ToString();
                    infosPostagem.ChavePix = reader["ChavePix"].ToString();

                    return Ok(infosPostagem);
                }

                return StatusCode(204, "Usuário inexistente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpPost]
        [Route("InserirNovapostagem")]
        public async Task<IActionResult> PostNovaPostagem([FromBody] DadosPostagem postagem)
        {
            try
            {
                if (postagem == null)
                    return BadRequest();

                ExecutaQuery(query: $"INSERT INTO Postagem (Titulo, Conteudo, DataPublicacao, idUsuario, Foto, Doacao, ChavePix, Status) VALUES (" +
                                    $"'{postagem.Titulo}', " +
                                    $"'{postagem.Conteudo}', " +
                                    $"'{Convert.ToDateTime(postagem.DataPublicacao).ToString("yyyyMMdd")}', " +
                                    $"'{postagem.idUsuario}', " +
                                    $"'{postagem.Foto}', " +
                                    $"{1}, " +
                                    $"'{postagem.ChavePix}', " +
                                    $"'{postagem.Status}')");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpPatch]
        [Route("AtualizarPostagem")]
        public async Task<IActionResult> AtualizarPostagem([FromBody] DadosPostagem postagem)
        {
            try
            {
                string queryString = $"UPDATE Postagem SET " +
                                     $"Titulo = '{postagem.Titulo}', " +
                                     $"Conteudo = '{postagem.Conteudo}', " +
                                     $"DataPublicacao = '{Convert.ToDateTime(postagem.DataPublicacao).ToString("yyyyMMdd")}', " +
                                     $"idUsuario = {postagem.idUsuario}, " +
                                     $"Foto = '{postagem.Foto}', " +
                                     $"Doacao = {postagem.Doacao}, " +
                                     $"ChavePix = '{postagem.ChavePix}', " +
                                     $"Status = '{postagem.Status}' " +
                                     $"WHERE codPostagem = {postagem.CodPostagem}";
                ExecutaQuery(queryString);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpDelete]
        [Route("DeletarPostagem/{codPostagem}")]
        public async Task<IActionResult> DeletarPostagem([FromRoute] string codPostagem)
        {
            try
            {
                var table = ExecutaQuery($"SELECT codPostagem FROM Postagem WHERE codPostagem = '{codPostagem}'");

                if (table.Rows.Count == 0)
                    return BadRequest("Postagem não encontrada!");

                ExecutaQuery($"UPDATE Postagem SET Status = 'N' WHERE codPostagem = '{codPostagem}'");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

    }
}
