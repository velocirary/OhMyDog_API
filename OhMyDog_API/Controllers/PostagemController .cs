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
                            IdPostagem = reader["IdPostagem"].ToString(),
                            Titulo = reader["Titulo"].ToString(),
                            Conteudo = reader["Conteudo"].ToString(),
                            DtPublicacao = Convert.ToDateTime(reader["DtPublicacao"]).ToString("dd/MM/yyyy"),
                            DtAprovacao = Convert.ToDateTime(reader["DtAprovacao"]).ToString("dd/MM/yyyy"),
                            IdUsuario = reader["IdUsuario"].ToString(),
                            IdAdminstrador = reader["IdAdminstrador"].ToString(),
                            Imagem = reader["Imagem"].ToString(),
                            ChavePix = reader["ChavePix"].ToString(),
                            TipoDoacao = reader["TipoDoacao"].ToString(),
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
        [Route("Postagem/{idPostagem}")]
        public async Task<IActionResult> RetornarPostagem([FromRoute] string idPostagem)
        {
            try
            {
                var infosPostagem = new DadosPostagem();

                var table = ExecutaQuery($"SELECT * FROM Postagem WHERE idPostagem = '{idPostagem}'");

                foreach (DataRow reader in table.Rows)
                {
                    infosPostagem.IdPostagem = reader["IdPostagem"].ToString();
                    infosPostagem.IdAdminstrador = reader["IdAdminstrador"].ToString();
                    infosPostagem.Titulo = reader["Titulo"].ToString();
                    infosPostagem.Conteudo = reader["Conteudo"].ToString();
                    infosPostagem.DtPublicacao = Convert.ToDateTime(reader["DtPublicacao"]).ToString("dd/MM/yyyy");
                    infosPostagem.DtAprovacao = Convert.ToDateTime(reader["DtAprovacao"]).ToString("dd/MM/yyyy");
                    infosPostagem.IdUsuario = reader["IdUsuario"].ToString();
                    infosPostagem.Imagem = reader["Imagem"].ToString();
                    infosPostagem.ChavePix = reader["ChavePix"].ToString();
                    infosPostagem.Status = reader["Status"].ToString();

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

                ExecutaQuery(query: $"INSERT INTO Postagem (Titulo, Conteudo, DtPublicacao, DtAprovacao, idUsuario, IdAdminstrador, Imagem, TipoDoacao, ChavePix, Status) VALUES (" +
                                    $"'{postagem.Titulo}', " +
                                    $"'{postagem.Conteudo}', " +
                                    $"'{Convert.ToDateTime(postagem.DtPublicacao).ToString("yyyyMMdd")}', " +
                                    $"'{null}', " +
                                    $"'{postagem.IdUsuario}', " +
                                    $"'{postagem.IdAdminstrador}', " +
                                    $"'{postagem.Imagem}', " +
                                    $"'{postagem.TipoDoacao}', " +
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
                                     $"DtAprovacao = '{Convert.ToDateTime(postagem.DtAprovacao).ToString("yyyyMMdd")}', " +
                                     $"idUsuario = {postagem.IdUsuario}, " +
                                     $"Imagem = '{postagem.Imagem}', " +
                                     $"ChavePix = '{postagem.ChavePix}', " +
                                     $"Status = '{postagem.Status}' " +
                                     $"WHERE idPostagem = {postagem.IdPostagem}";
                ExecutaQuery(queryString);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpDelete]
        [Route("DeletarPostagem/{idPostagem}")]
        public async Task<IActionResult> DeletarPostagem([FromRoute] string idPostagem)
        {
            try
            {
                var table = ExecutaQuery($"SELECT idPostagem FROM Postagem WHERE idPostagem = '{idPostagem}'");

                if (table.Rows.Count == 0)
                    return BadRequest("Postagem não encontrada!");

                ExecutaQuery($"UPDATE Postagem SET Status = 'N' WHERE idPostagem = '{idPostagem}'");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

    }
}
