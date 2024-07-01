using Microsoft.AspNetCore.Mvc;
using OhMyDog_API.Model.Postagem;
using static OhMyDog_API.Controllers.ConexaoController;
using System.Data;
using static OhMyDog_API.Constantes.PostagemConstantes;

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
                var listPostagens = new List<PostagemModel>();

                var table = await ExecutarQuery("SELECT * FROM Postagem");

                foreach (DataRow reader in table.Rows)
                {
                    listPostagens.Add(
                        item: new PostagemModel
                        {
                            IdPostagem = reader[Tabela.IdPostagem].ToString(),
                            Titulo = reader[Tabela.Titulo].ToString(),
                            Conteudo = reader[Tabela.Conteudo].ToString(),
                            DtPublicacao = Convert.ToDateTime(reader[Tabela.DtPublicacao]).ToString("dd/MM/yyyy"),
                            DtAprovacao = string.IsNullOrEmpty(reader["DtAprovacao"].ToString()) ? null : Convert.ToDateTime(reader["DtAprovacao"]).ToString("dd/MM/yyyy"),
                            IdUsuario = reader[Tabela.IdUsuario].ToString(),
                            IdAdministrador = reader[Tabela.IdAdministrador].ToString(),
                            UrlFoto = reader[Tabela.UrlFoto].ToString(),
                            DoacaoPix = reader[Tabela.DoacaoPix].ToString(),
                            DoacaoRacao = reader[Tabela.DoacaoRacao].ToString(),
                            DoacaoMedicamento = reader[Tabela.DoacaoMedicamento].ToString(),
                            DoacaoOutros = reader[Tabela.DoacaoOutros].ToString(),
                            ChavePix = reader[Tabela.ChavePix].ToString(),
                            MetaDoacao = reader[Tabela.MetaDoacao].ToString().Replace(",", "."),
                            ValorArrecadado = reader[Tabela.ValorArrecadado].ToString().Replace(",", "."),
                            MsgAdministrador = reader[Tabela.MsgAdministrador].ToString(),
                            Status = reader[Tabela.Status].ToString()
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
                var infosPostagem = new PostagemModel();

                var table = await ExecutarQuery($"SELECT * FROM Postagem WHERE {Tabela.IdPostagem} = '{idPostagem}'");

                foreach (DataRow reader in table.Rows)
                {
                    infosPostagem.IdPostagem = reader[Tabela.IdPostagem].ToString();
                    infosPostagem.IdAdministrador = reader[Tabela.IdAdministrador].ToString();
                    infosPostagem.Titulo = reader[Tabela.Titulo].ToString();
                    infosPostagem.Conteudo = reader[Tabela.Conteudo].ToString();
                    infosPostagem.DtPublicacao = Convert.ToDateTime(reader[Tabela.DtPublicacao]).ToString("dd/MM/yyyy");
                    infosPostagem.DtAprovacao = reader[Tabela.DtAprovacao] != DBNull.Value ? Convert.ToDateTime(reader[Tabela.DtAprovacao]).ToString("dd/MM/yyyy") : null;
                    infosPostagem.IdUsuario = reader[Tabela.IdUsuario].ToString();
                    infosPostagem.UrlFoto = reader[Tabela.UrlFoto].ToString();
                    infosPostagem.DoacaoPix = reader[Tabela.DoacaoPix].ToString();
                    infosPostagem.DoacaoRacao = reader[Tabela.DoacaoRacao].ToString();
                    infosPostagem.DoacaoMedicamento = reader[Tabela.DoacaoMedicamento].ToString();
                    infosPostagem.DoacaoOutros = reader[Tabela.DoacaoOutros].ToString();
                    infosPostagem.ChavePix = reader[Tabela.ChavePix].ToString();
                    infosPostagem.MetaDoacao = reader[Tabela.MetaDoacao].ToString();
                    infosPostagem.ValorArrecadado = reader[Tabela.ValorArrecadado].ToString();
                    infosPostagem.MsgAdministrador = reader[Tabela.MsgAdministrador].ToString();
                    infosPostagem.Status = reader[Tabela.Status].ToString();

                    return Ok(infosPostagem);
                }

                return StatusCode(204, "Postagem inexistente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpPost]
        [Route("InserirNovapostagem")]
        public async Task<IActionResult> PostNovaPostagem([FromBody] NovaPostagemModel postagem)
        {
            try
            {
                if (postagem == null)
                    return BadRequest();

                await ExecutarQuery($"INSERT INTO Postagem ({Tabela.Titulo}, {Tabela.Conteudo}, {Tabela.IdUsuario}, {Tabela.UrlFoto}, {Tabela.ChavePix}, {Tabela.MetaDoacao}) " +
                    $"VALUES (" +
                    $"'{postagem.Titulo}', " +
                    $"'{postagem.Conteudo}', " +
                    $"'{postagem.IdUsuario}', " +
                    $"'{postagem.UrlFoto}', " +
                    $"'{postagem.ChavePix}', " +
                    $"'{postagem.MetaDoacao}') ");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpPatch]
        [Route("AprovarPostagem")]
        public async Task<IActionResult> AtualizarPostagem([FromBody] AprovarPostagem postagem)
        {
            try
            {
                string queryString = $"UPDATE Postagem SET " +
                     $"{Tabela.DtAprovacao} = '{(postagem.Status == "A" ? DateTime.Now.ToString("yyyyMMdd") : "NULL" )}', " +
                     $"{Tabela.MsgAdministrador} = '{postagem.MsgAdministrador}', " +
                     $"{Tabela.Status} = '{postagem.Status}' " +
                     $"WHERE {Tabela.IdPostagem} = {postagem.IdPostagem}";

                await ExecutarQuery(queryString);

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
                var table = await ExecutarQuery($"SELECT {Tabela.IdPostagem} FROM Postagem WHERE {Tabela.IdPostagem} = '{idPostagem}'");

                if (table.Rows.Count == 0)
                    return BadRequest("Postagem não encontrada!");

                await ExecutarQuery($"UPDATE Postagem SET {Tabela.Status} = 'N' WHERE {Tabela.IdPostagem} = '{idPostagem}'");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpGet]
        [Route("UltimasPostagensRealizadas")]
        public async Task<IActionResult> GetUltimasPostagensRealizadas()
        {
            try
            {
                var listPostagensPendentes = new List<PostagemModel>();
                var table = await ExecutarQuery($"SELECT TOP 3 * FROM {Tabela.NomeTabela} ORDER BY {Tabela.DtPublicacao} DESC");

                foreach (DataRow reader in table.Rows)
                {
                    listPostagensPendentes.Add(
                        new PostagemModel
                        {
                            IdPostagem = reader[Tabela.IdPostagem].ToString(),
                            Titulo = reader[Tabela.Titulo].ToString(),
                            Conteudo = reader[Tabela.Conteudo].ToString(),
                            DtPublicacao = Convert.ToDateTime(reader[Tabela.DtPublicacao]).ToString("dd/MM/yyyy"),
                            DtAprovacao = reader[Tabela.DtAprovacao] != DBNull.Value ? Convert.ToDateTime(reader[Tabela.DtAprovacao]).ToString("dd/MM/yyyy") : null,
                            IdUsuario = reader[Tabela.IdUsuario].ToString(),
                            IdAdministrador = reader[Tabela.IdAdministrador].ToString(),
                            UrlFoto = reader[Tabela.UrlFoto].ToString(),
                            ChavePix = reader[Tabela.ChavePix].ToString(),
                            MetaDoacao = reader[Tabela.MetaDoacao].ToString(),
                            ValorArrecadado = reader[Tabela.ValorArrecadado].ToString(),
                            MsgAdministrador = reader[Tabela.MsgAdministrador].ToString(),
                            Status = reader[Tabela.Status].ToString(),
                            DoacaoPix = reader[Tabela.DoacaoPix].ToString(),
                            DoacaoRacao = reader[Tabela.DoacaoRacao].ToString(),
                            DoacaoMedicamento = reader[Tabela.DoacaoMedicamento].ToString(),
                            DoacaoOutros = reader[Tabela.DoacaoOutros].ToString()
                        }
                    );
                }

                return Ok(listPostagensPendentes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpGet]
        [Route("UltimasPostagensRealizadasPorUsuario/{idUsuario}")]
        public async Task<IActionResult> GetUltimasPostagensRealizadasPorUsuario([FromRoute] string idUsuario)
        {
            try
            {
                var listPostagensPendentes = new List<PostagemModel>();
                var table = await ExecutarQuery($"SELECT TOP 3 * FROM {Tabela.NomeTabela} WHERE {Tabela.IdUsuario} = '{idUsuario}' ORDER BY {Tabela.DtPublicacao} DESC");

                foreach (DataRow reader in table.Rows)
                {
                    listPostagensPendentes.Add(
                        new PostagemModel
                        {
                            IdPostagem = reader[Tabela.IdPostagem].ToString(),
                            Titulo = reader[Tabela.Titulo].ToString(),
                            Conteudo = reader[Tabela.Conteudo].ToString(),
                            DtPublicacao = Convert.ToDateTime(reader[Tabela.DtPublicacao]).ToString("dd/MM/yyyy"),
                            DtAprovacao = reader[Tabela.DtAprovacao] != DBNull.Value ? Convert.ToDateTime(reader[Tabela.DtAprovacao]).ToString("dd/MM/yyyy") : null,
                            IdUsuario = reader[Tabela.IdUsuario].ToString(),
                            IdAdministrador = reader[Tabela.IdAdministrador].ToString(),
                            UrlFoto = reader[Tabela.UrlFoto].ToString(),
                            ChavePix = reader[Tabela.ChavePix].ToString(),
                            MetaDoacao = reader[Tabela.MetaDoacao].ToString(),
                            ValorArrecadado = reader[Tabela.ValorArrecadado].ToString(),
                            MsgAdministrador = reader[Tabela.MsgAdministrador].ToString(),
                            Status = reader[Tabela.Status].ToString(),
                            DoacaoPix = reader[Tabela.DoacaoPix].ToString(),
                            DoacaoRacao = reader[Tabela.DoacaoRacao].ToString(),
                            DoacaoMedicamento = reader[Tabela.DoacaoMedicamento].ToString(),
                            DoacaoOutros = reader[Tabela.DoacaoOutros].ToString()
                        }
                    );
                }

                return Ok(listPostagensPendentes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpGet]
        [Route("TodasPostagensPendentesAprovacao")]
        public async Task<IActionResult> GetTodasPostagensPendentesAprovacao()
        {
            try
            {
                var listPostagensPendentes = new List<PostagemModel>();
                var table = await ExecutarQuery($"SELECT * FROM {Tabela.NomeTabela} WHERE {Tabela.Status} = 'P' ORDER BY {Tabela.DtPublicacao} DESC");

                foreach (DataRow reader in table.Rows)
                {
                    listPostagensPendentes.Add(
                        new PostagemModel
                        {
                            IdPostagem = reader[Tabela.IdPostagem].ToString(),
                            Titulo = reader[Tabela.Titulo].ToString(),
                            Conteudo = reader[Tabela.Conteudo].ToString(),
                            DtPublicacao = Convert.ToDateTime(reader[Tabela.DtPublicacao]).ToString("dd/MM/yyyy"),
                            DtAprovacao = reader[Tabela.DtAprovacao] != DBNull.Value ? Convert.ToDateTime(reader[Tabela.DtAprovacao]).ToString("dd/MM/yyyy") : null,
                            IdUsuario = reader[Tabela.IdUsuario].ToString(),
                            IdAdministrador = reader[Tabela.IdAdministrador].ToString(),
                            UrlFoto = reader[Tabela.UrlFoto].ToString(),
                            ChavePix = reader[Tabela.ChavePix].ToString(),
                            MetaDoacao = reader[Tabela.MetaDoacao].ToString(),
                            ValorArrecadado = reader[Tabela.ValorArrecadado].ToString(),
                            MsgAdministrador = reader[Tabela.MsgAdministrador].ToString(),
                            Status = reader[Tabela.Status].ToString(),
                            DoacaoPix = reader[Tabela.DoacaoPix].ToString(),
                            DoacaoRacao = reader[Tabela.DoacaoRacao].ToString(),
                            DoacaoMedicamento = reader[Tabela.DoacaoMedicamento].ToString(),
                            DoacaoOutros = reader[Tabela.DoacaoOutros].ToString()
                        }
                    );
                }

                return Ok(listPostagensPendentes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpGet]
        [Route("UltimasPostagensPendentesAprovacao")]
        public async Task<IActionResult> GetUltimasPostagensPendentesAprovacao()
        {
            try
            {
                var listPostagensPendentes = new List<PostagemModel>();
                var table = await ExecutarQuery($"SELECT TOP 5 * FROM {Tabela.NomeTabela} WHERE {Tabela.Status} = 'P' ORDER BY {Tabela.DtPublicacao} DESC");

                foreach (DataRow reader in table.Rows)
                {
                    listPostagensPendentes.Add(
                        new PostagemModel
                        {
                            IdPostagem = reader[Tabela.IdPostagem].ToString(),
                            Titulo = reader[Tabela.Titulo].ToString(),
                            Conteudo = reader[Tabela.Conteudo].ToString(),
                            DtPublicacao = Convert.ToDateTime(reader[Tabela.DtPublicacao]).ToString("dd/MM/yyyy"),
                            DtAprovacao = reader[Tabela.DtAprovacao] != DBNull.Value ? Convert.ToDateTime(reader[Tabela.DtAprovacao]).ToString("dd/MM/yyyy") : null,
                            IdUsuario = reader[Tabela.IdUsuario].ToString(),
                            IdAdministrador = reader[Tabela.IdAdministrador].ToString(),
                            UrlFoto = reader[Tabela.UrlFoto].ToString(),
                            ChavePix = reader[Tabela.ChavePix].ToString(),
                            MetaDoacao = reader[Tabela.MetaDoacao].ToString(),
                            ValorArrecadado = reader[Tabela.ValorArrecadado].ToString(),
                            MsgAdministrador = reader[Tabela.MsgAdministrador].ToString(),
                            Status = reader[Tabela.Status].ToString(),
                            DoacaoPix = reader[Tabela.DoacaoPix].ToString(),
                            DoacaoRacao = reader[Tabela.DoacaoRacao].ToString(),
                            DoacaoMedicamento = reader[Tabela.DoacaoMedicamento].ToString(),
                            DoacaoOutros = reader[Tabela.DoacaoOutros].ToString()
                        }
                    );
                }

                return Ok(listPostagensPendentes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

    }
}
