using Microsoft.AspNetCore.Mvc;
using static OhMyDog_API.Controllers.ConexaoController;
using System.Data;
using OhMyDog_API.Model.Doacoes;
using OhMyDog_API.Model.Usuarios;

namespace OhMyDog_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoacaoController : ControllerBase
    {
        [HttpGet]
        [Route("DoacaoUsuario/{idUsuarioDoador}")]
        public async Task<IActionResult> GetDoacoesUsuario([FromRoute] string idUsuarioDoador)
        {
            try
            {
                var infosDoacao = new DadosDoacao();

                var table = ExecutaQuery($"SELECT * FROM Doacao WHERE IdUsuario = '{idUsuarioDoador}'");

                foreach (DataRow reader in table.Rows)
                {
                    infosDoacao.IdDoacao = reader["IdDoacao"].ToString();
                    infosDoacao.ValorDoacao = reader["Valor"].ToString();
                    infosDoacao.Mensagem = reader["Mensagem"].ToString();
                    infosDoacao.ComprovantePix = reader["ComprovantePix"].ToString();
                    infosDoacao.DtDoacao = Convert.ToDateTime(reader["DtDoacao"]).ToString("dd/MM/yyyy");
                    infosDoacao.Status = reader["Status"].ToString();
                    infosDoacao.IdPostagem = reader["IdPostagem"].ToString();

                    return Ok(infosDoacao);
                }

                return StatusCode(204, "Doação inexistente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpGet]
        [Route("TodasDoacoes")]
        public async Task<IActionResult> GetTodasDoacoes()
        {
            try
            {
                var listDoacoes = new List<DadosDoacao>();
                var table = ExecutaQuery("SELECT * FROM Doacao");

                foreach (DataRow reader in table.Rows)
                {
                    listDoacoes.Add(
                        item: new DadosDoacao
                        {
                            IdDoacao = reader["IdDoacao"].ToString(),
                            ValorDoacao = reader["Valor"].ToString(),
                            Mensagem = reader["Mensagem"].ToString(),
                            ComprovantePix = reader["ComprovantePix"].ToString(),
                            DtDoacao = Convert.ToDateTime(reader["DtDoacao"]).ToString("dd/MM/yyyy"),
                            Status = reader["Status"].ToString(),
                            IdUsuarioDoador = reader["IdUsuario"].ToString()
                        }
                    );
                }

                return Ok(listDoacoes);
            }
            catch (Exception ex)
            {
                return StatusCode(204, "Doaçãoo inexistente");
            }
        }

        [HttpGet]
        [Route("DoacaoPostagem/{idPostagem}")]
        public async Task<IActionResult> GetDoacoesPostagem([FromRoute] string idPostagem)
        {
            try
            {
                var infosDoacao = new DadosDoacao();

                var table = ExecutaQuery($"SELECT * FROM Doacao WHERE idPostagem = '{idPostagem}'");

                foreach (DataRow reader in table.Rows)
                {
                    infosDoacao.IdDoacao = reader["IdDoacao"].ToString();
                    infosDoacao.ValorDoacao = reader["Valor"].ToString();
                    infosDoacao.Mensagem = reader["Mensagem"].ToString();
                    infosDoacao.ComprovantePix = reader["ComprovantePix"].ToString();
                    infosDoacao.DtDoacao = Convert.ToDateTime(reader["DtDoacao"]).ToString("dd/MM/yyyy");
                    infosDoacao.Status = reader["Status"].ToString();
                    infosDoacao.IdUsuarioDoador = reader["IdUsuario"].ToString();

                    return Ok(infosDoacao);
                }

                return StatusCode(204, "Doaçãoo inexistente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpPost]
        [Route("InserirNovaDoacao")]
        public async Task<IActionResult> PostNovaDoacao([FromBody] DadosDoacao doacao)
        {
            try
            {
                if (doacao == null)
                    return BadRequest();

                ExecutaQuery(query: $"INSERT INTO Doacao (Valor, Mensagem, IdUsuario, IdVoucher, IdPostagem, ComprovantePix, DtDoacao, Status) VALUES (" +
                                    $"'{doacao.ValorDoacao}', " +
                                    $"'{doacao.Mensagem}', " +
                                    $"'{doacao.IdUsuarioDoador}', " +
                                    $"'{doacao.IdVoucher}', " +
                                    $"'{doacao.IdPostagem}', " +
                                    $"'{doacao.ComprovantePix}', " +
                                    $"'{doacao.DtDoacao}', " +
                                    $"'P')");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpPatch]
        [Route("AprovarDoacao/{idDoacao}/{status}")]
        public async Task<IActionResult> AprovarDoacao([FromRoute] string idDoacao, [FromRoute] string statusDoacao)
        {
            try
            {
                string queryString = $"UPDATE Doacao SET " +
                                     $"Status = '{statusDoacao}' " +
                                     $"WHERE idPatrocinador = {idDoacao}";
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
