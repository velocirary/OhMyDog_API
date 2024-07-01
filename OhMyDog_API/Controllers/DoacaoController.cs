using Microsoft.AspNetCore.Mvc;
using static OhMyDog_API.Controllers.ConexaoController;
using System.Data;
using OhMyDog_API.Model.Doacoes;
using OhMyDog_API.Model.Usuarios;
using static OhMyDog_API.Constantes.DoacaoConstantes;
using OhMyDog_API.Constantes;

namespace OhMyDog_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoacaoController : ControllerBase
    {
        [HttpGet]
        [Route("TodasDoacoes")]
        public async Task<IActionResult> GetTodasDoacoes()
        {
            try
            {
                var listDoacoes = new List<DoacaoModel>();
                var table = await ExecutarQuery($"SELECT * FROM {Tabela.NomeTabela}");

                foreach (DataRow reader in table.Rows)
                {
                    listDoacoes.Add(
                        item: new DoacaoModel
                        {
                            IdDoacao = reader[Tabela.IdDoacao].ToString(),
                            IdPostagem = reader[Tabela.IdPostagem].ToString(),
                            IdVoucher = reader[Tabela.IdVoucher].ToString(),
                            ValorDoacao = reader[Tabela.Valor].ToString(),
                            Mensagem = reader[Tabela.Mensagem].ToString(),
                            ComprovantePix = reader[Tabela.ComprovantePix].ToString(),
                            IdTipoDoacao = reader[Tabela.IdTipoDoacao].ToString(),
                            DtDoacao = Convert.ToDateTime(reader[Tabela.DtDoacao]).ToString("dd/MM/yyyy"),
                            Status = reader[Tabela.Status].ToString(),
                            IdUsuarioDoador = reader[Tabela.IdUsuario].ToString()
                        }
                    );
                }

                return Ok(listDoacoes);
            }
            catch (Exception ex)
            {
                return StatusCode(204, "Doação inexistente. Erro: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("DoacaoUsuario/{idUsuarioDoador}")]
        [ProducesResponseType(typeof(List<DoacaoUsuarioDoadorModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDoacoesUsuario([FromRoute] string idUsuarioDoador)
        {
            try
            {
                var doacoes = new List<DoacaoUsuarioDoadorModel>();

                var table = await ExecutarQuery(
                    $"SELECT *, Voucher.{VoucherConstantes.Tabela.Cupom} FROM {Tabela.NomeTabela} " +
                    $"LEFT JOIN Voucher ON Voucher.{VoucherConstantes.Tabela.IdVoucher} = Doacao.{Tabela.IdVoucher}" +
                    $"WHERE {Tabela.IdUsuario} = '{idUsuarioDoador}'");

                foreach (DataRow reader in table.Rows)
                {
                    doacoes.Add(
                        new DoacaoUsuarioDoadorModel()
                        {
                            IdDoacao = reader[Tabela.IdDoacao].ToString(),
                            IdPostagem = reader[Tabela.IdPostagem].ToString(),
                            IdVoucher = reader[Tabela.IdVoucher].ToString(),
                            ValorDoacao = reader[Tabela.Valor].ToString(),
                            Mensagem = reader[Tabela.Mensagem].ToString(),
                            ComprovantePix = reader[Tabela.ComprovantePix].ToString(),
                            IdTipoDoacao = reader[Tabela.IdTipoDoacao].ToString(),
                            DtDoacao = Convert.ToDateTime(reader[Tabela.DtDoacao]).ToString("dd/MM/yyyy"),
                            Status = reader[Tabela.Status].ToString(),
                            IdUsuarioDoador = reader[Tabela.IdUsuario].ToString(),
                            CupomVoucher = reader[VoucherConstantes.Tabela.Cupom].ToString(),
                        }
                    );
                }

                if (doacoes.Count > 0)
                    return Ok(doacoes);
                else
                    return StatusCode(204, "Usuário sem doações.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpGet]
        [Route("DoacaoPostagem/{idPostagem}")]
        public async Task<IActionResult> GetDoacoesPostagem([FromRoute] string idPostagem)
        {
            try
            {
                var doacoes = new List<DoacaoModel>();

                var table = await ExecutarQuery($"SELECT * FROM {Tabela.NomeTabela} WHERE {Tabela.IdPostagem} = '{idPostagem}'");

                foreach (DataRow reader in table.Rows)
                {
                    doacoes.Add(
                        new DoacaoModel()
                        {
                            IdDoacao = reader[Tabela.IdDoacao].ToString(),
                            IdPostagem = reader[Tabela.IdPostagem].ToString(),
                            IdVoucher = reader[Tabela.IdVoucher].ToString(),
                            ValorDoacao = reader[Tabela.Valor].ToString(),
                            Mensagem = reader[Tabela.Mensagem].ToString(),
                            ComprovantePix = reader[Tabela.ComprovantePix].ToString(),
                            IdTipoDoacao = reader[Tabela.IdTipoDoacao].ToString(),
                            DtDoacao = Convert.ToDateTime(reader[Tabela.DtDoacao]).ToString("dd/MM/yyyy"),
                            Status = reader[Tabela.Status].ToString(),
                            IdUsuarioDoador = reader[Tabela.IdUsuario].ToString(),
                        }
                    );
                }

                if (doacoes.Count > 0)
                    return Ok(doacoes);
                else
                    return StatusCode(204, "Postagem sem doações.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }        
        
        [HttpGet]
        [Route("TodasDoacoesUsuarioDoador/{idUsuarioDoador}")]
        public async Task<IActionResult> GetTodasDoacoesUsuario([FromRoute] string idUsuarioDoador)
        {
            try
            {
                var doacoes = new List<DoacaoUsuarioDoadorModel>();

                var table = await ExecutarQuery(
                  $"SELECT Doacao.IdDoacao, Doacao.IdPostagem, Doacao.IdVoucher, Doacao.Valor, \r\n" +
                  "   Doacao.Mensagem, Doacao.ComprovantePix, Doacao.IdTipoDoacao, Doacao.DtDoacao, Doacao.Status, Doacao.IdUsuario, \r\n" +
                  "   UsuarioPostagem.Nome AS NomeUsuarioPostagem, \r\n" +
                  "   Postagem.Titulo AS TituloPostagem, Postagem.Status AS StatusPostagem, \r\n" +
                  "   Voucher.Cupom, Voucher.IdPatrocinador \r\n" +
                  $"FROM Doacao \r\n" +
                  $"LEFT JOIN Voucher ON Voucher.IdVoucher = Doacao.IdVoucher \r\n" +
                  $"LEFT JOIN Postagem ON Doacao.IdPostagem = Postagem.IdPostagem \r\n" +
                  $"LEFT JOIN Usuario UsuarioPostagem ON UsuarioPostagem.IdUsuario = Postagem.IdUsuario \r\n" +
                  $"WHERE Doacao.IdUsuario = '{idUsuarioDoador}'");

                foreach (DataRow reader in table.Rows)
                {
                    doacoes.Add(
                        new DoacaoUsuarioDoadorModel()
                        {
                            IdDoacao = reader["IdDoacao"].ToString(),
                            IdPostagem = reader["IdPostagem"].ToString(),
                            IdVoucher = reader["IdVoucher"].ToString(),
                            ValorDoacao = reader["Valor"].ToString(),
                            Mensagem = reader["Mensagem"].ToString(),
                            ComprovantePix = reader["ComprovantePix"].ToString(),
                            IdTipoDoacao = reader["IdTipoDoacao"].ToString(),
                            DtDoacao = Convert.ToDateTime(reader["DtDoacao"]).ToString("dd/MM/yyyy"),
                            Status = reader["Status"].ToString(),
                            IdUsuarioDoador = reader["IdUsuario"].ToString(),
                            NomeUsuarioSolicitante = reader["NomeUsuarioPostagem"].ToString(),
                            TituloPostagem = reader["TituloPostagem"].ToString(),
                            StatusPostagem = reader["StatusPostagem"].ToString(),
                            CupomVoucher = reader["Cupom"].ToString(),
                            IdPatrocinador = reader["IdPatrocinador"].ToString(),
                        }
                    );
                }

                if (doacoes.Count > 0)
                    return Ok(doacoes);
                else
                    return StatusCode(204, "Postagem sem doações.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpGet]
        [Route("TodasDoacoesUsuarioSolicitante/{idUsuarioSolicitante}")]
        public async Task<IActionResult> GetTodasDoacoesUsuarioSolicitante([FromRoute] string idUsuarioSolicitante)
        {
            try
            {
                var doacoes = new List<DoacaoUsuarioSolicitanteModel>();

                var table = await ExecutarQuery(
               $"SELECT \r\n" +
               $"    Doacao.IdDoacao, \r\n" +
               $"    Doacao.IdPostagem, \r\n" +
               $"    Doacao.IdVoucher, \r\n" +
               $"    Doacao.Valor, \r\n" +
               $"    Doacao.Mensagem, \r\n" +
               $"    Doacao.ComprovantePix, \r\n" +
               $"    Doacao.IdTipoDoacao, \r\n" +
               $"    Doacao.DtDoacao, \r\n" +
               $"    Doacao.Status, \r\n" +
               $"    Doacao.IdUsuario, \r\n" +
               $"    UsuarioDoador.Nome, \r\n" +
               $"    Postagem.Titulo, \r\n" +
               $"    Postagem.Status AS StatusPostagem, \r\n" +
               $"    Voucher.Cupom, Voucher.IdPatrocinador \r\n" +
               $"FROM Doacao \r\n" +
               $"INNER JOIN Postagem ON Doacao.IdPostagem = Postagem.IdPostagem \r\n" +
               $"INNER JOIN Usuario UsuarioSolicitante ON UsuarioSolicitante.IdUsuario = Postagem.IdUsuario \r\n" +
               $"INNER JOIN Usuario UsuarioDoador ON UsuarioDoador.IdUsuario = Doacao.IdUsuario \r\n" +
               $"LEFT JOIN Voucher ON Voucher.IdVoucher = Doacao.IdVoucher \r\n" +
               $"WHERE UsuarioSolicitante.IdUsuario = '{idUsuarioSolicitante}'");

                foreach (DataRow reader in table.Rows)
                {
                    doacoes.Add(
                        new DoacaoUsuarioSolicitanteModel()
                        {
                            IdDoacao = reader["IdDoacao"].ToString(),
                            IdPostagem = reader["IdPostagem"].ToString(),
                            IdVoucher = reader["IdVoucher"].ToString(),
                            ValorDoacao = reader["Valor"].ToString(),
                            Mensagem = reader["Mensagem"].ToString(),
                            ComprovantePix = reader["ComprovantePix"].ToString(),
                            IdTipoDoacao = reader["IdTipoDoacao"].ToString(),
                            DtDoacao = Convert.ToDateTime(reader["DtDoacao"]).ToString("dd/MM/yyyy"),
                            Status = reader["Status"].ToString(),
                            IdUsuarioDoador = reader["IdUsuario"].ToString(),
                            NomeUsuarioDoador = reader["Nome"].ToString(),
                            TituloPostagem = reader["Titulo"].ToString(),
                            StatusPostagem = reader["StatusPostagem"].ToString(),
                            CupomVoucher = reader["Cupom"].ToString(),
                            IdPatrocinador = reader["IdPatrocinador"].ToString(),
                        }
                    );
                }

                if (doacoes.Count > 0)
                    return Ok(doacoes);
                else
                    return StatusCode(204, "Postagem sem doações.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpGet]
        [Route("UltimasDoacaoUsuario/{idUsuarioDoador}")]
        [ProducesResponseType(typeof(List<DoacaoUsuarioDoadorModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUltimasDoacoesUsuario([FromRoute] string idUsuarioDoador)
        {
            try
            {
                var doacoes = new List<DoacaoUsuarioDoadorModel>();

                var table = await ExecutarQuery(
                $"SELECT TOP 5 Doacao.IdDoacao, Doacao.IdPostagem, Doacao.IdVoucher, Doacao.Valor, \r\n" +
                "   Doacao.Mensagem, Doacao.ComprovantePix, Doacao.IdTipoDoacao, Doacao.DtDoacao, Doacao.Status, Doacao.IdUsuario, \r\n" +
                "   UsuarioPostagem.Nome AS NomeUsuarioPostagem, \r\n" +
                "   Postagem.Titulo AS TituloPostagem, Postagem.Status AS StatusPostagem, \r\n" +
                "   Voucher.Cupom, Voucher.IdPatrocinador \r\n" +
                $"FROM Doacao \r\n" +
                $"LEFT JOIN Voucher ON Voucher.IdVoucher = Doacao.IdVoucher \r\n" +
                $"LEFT JOIN Postagem ON Doacao.IdPostagem = Postagem.IdPostagem \r\n" +
                $"LEFT JOIN Usuario UsuarioPostagem ON UsuarioPostagem.IdUsuario = Postagem.IdUsuario \r\n" +
                $"WHERE Doacao.IdUsuario = '{idUsuarioDoador}'");

                foreach (DataRow reader in table.Rows)
                {
                    doacoes.Add(
                        new DoacaoUsuarioDoadorModel()
                        {
                            IdDoacao = reader["IdDoacao"].ToString(),
                            IdPostagem = reader["IdPostagem"].ToString(),
                            IdVoucher = reader["IdVoucher"].ToString(),
                            ValorDoacao = reader["Valor"].ToString(),
                            Mensagem = reader["Mensagem"].ToString(),
                            ComprovantePix = reader["ComprovantePix"].ToString(),
                            IdTipoDoacao = reader["IdTipoDoacao"].ToString(),
                            DtDoacao = Convert.ToDateTime(reader["DtDoacao"]).ToString("dd/MM/yyyy"),
                            Status = reader["Status"].ToString(),
                            IdUsuarioDoador = reader["IdUsuario"].ToString(),
                            NomeUsuarioSolicitante = reader["NomeUsuarioPostagem"].ToString(),
                            TituloPostagem = reader["TituloPostagem"].ToString(),
                            StatusPostagem = reader["StatusPostagem"].ToString(),
                            CupomVoucher = reader["Cupom"].ToString(),
                            IdPatrocinador = reader["IdPatrocinador"].ToString(),
                        }
                    );
                }

                if (doacoes.Count > 0)
                    return Ok(doacoes);
                else
                    return StatusCode(204, "Usuário sem doações.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpGet]
        [Route("UltimasDoacoesUsuarioSolicitante/{idUsuarioSolicitante}")]
        [ProducesResponseType(typeof(List<DoacaoUsuarioSolicitanteModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUltimasDoacoesUsuarioSolicitante([FromRoute] string idUsuarioSolicitante)
        {
            try
            {
                var doacoes = new List<DoacaoUsuarioSolicitanteModel>();

                var table = await ExecutarQuery(
                $"SELECT TOP 5 \r\n" +
                $"    Doacao.IdDoacao, \r\n" +
                $"    Doacao.IdPostagem, \r\n" +
                $"    Doacao.IdVoucher, \r\n" +
                $"    Doacao.Valor, \r\n" +
                $"    Doacao.Mensagem, \r\n" +
                $"    Doacao.ComprovantePix, \r\n" +
                $"    Doacao.IdTipoDoacao, \r\n" +
                $"    Doacao.DtDoacao, \r\n" +
                $"    Doacao.Status, \r\n" +
                $"    Doacao.IdUsuario, \r\n" +
                $"    UsuarioDoador.Nome, \r\n" +
                $"    Postagem.Titulo, \r\n" +
                $"    Postagem.Status AS StatusPostagem, \r\n" +
                $"    Voucher.Cupom, Voucher.IdPatrocinador \r\n" +
                $"FROM Doacao \r\n" +
                $"INNER JOIN Postagem ON Doacao.IdPostagem = Postagem.IdPostagem \r\n" +
                $"INNER JOIN Usuario UsuarioSolicitante ON UsuarioSolicitante.IdUsuario = Postagem.IdUsuario \r\n" +
                $"INNER JOIN Usuario UsuarioDoador ON UsuarioDoador.IdUsuario = Doacao.IdUsuario \r\n" +
                $"LEFT JOIN Voucher ON Voucher.IdVoucher = Doacao.IdVoucher \r\n" +
                $"WHERE UsuarioSolicitante.IdUsuario = '{idUsuarioSolicitante}'");

                foreach (DataRow reader in table.Rows)
                {
                    doacoes.Add(
                        new DoacaoUsuarioSolicitanteModel()
                        {
                            IdDoacao = reader["IdDoacao"].ToString(),
                            IdPostagem = reader["IdPostagem"].ToString(),
                            IdVoucher = reader["IdVoucher"].ToString(),
                            ValorDoacao = reader["Valor"].ToString(),
                            Mensagem = reader["Mensagem"].ToString(),
                            ComprovantePix = reader["ComprovantePix"].ToString(),
                            IdTipoDoacao = reader["IdTipoDoacao"].ToString(),
                            DtDoacao = Convert.ToDateTime(reader["DtDoacao"]).ToString("dd/MM/yyyy"),
                            Status = reader["Status"].ToString(),
                            IdUsuarioDoador = reader["IdUsuario"].ToString(),
                            NomeUsuarioDoador = reader["Nome"].ToString(),
                            TituloPostagem = reader["Titulo"].ToString(),
                            StatusPostagem = reader["StatusPostagem"].ToString(),
                            CupomVoucher = reader["Cupom"].ToString(),
                            IdPatrocinador = reader["IdPatrocinador"].ToString(),
                        }
                    );
                }

                if (doacoes.Count > 0)
                    return Ok(doacoes);
                else
                    return StatusCode(204, "Usuário sem doações.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpPost]
        [Route("InserirNovaDoacao")]
        public async Task<IActionResult> PostNovaDoacao([FromBody] NovaDoacaoModel doacao)
        {
            try
            {
                if (doacao == null)
                    return BadRequest();

                await ExecutarQuery(query:
                    $"INSERT INTO {Tabela.NomeTabela} ({Tabela.Valor}, {Tabela.Mensagem}, {Tabela.IdUsuario}, {Tabela.IdPostagem}, {Tabela.ComprovantePix}, {Tabela.IdTipoDoacao}) VALUES (" +
                    $"'{doacao.ValorDoacao}', " +
                    $"'{doacao.Mensagem}', " +
                    $"'{doacao.IdUsuarioDoador}', " +
                    $"'{doacao.IdPostagem}', " +
                    $"'{doacao.ComprovantePix}', " +
                    $"'{doacao.IdTipoDoacao}' ) ");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpPatch]
        [Route("AprovarDoacao/{idDoacao}/{statusDoacao}")]
        public async Task<IActionResult> AprovarDoacao([FromRoute] string idDoacao, [FromRoute] string statusDoacao)
        {
            try
            {
                string queryString = $"UPDATE {Tabela.NomeTabela} SET " +
                                     $"{Tabela.Status} = '{statusDoacao}' " +
                                     $"WHERE {Tabela.IdDoacao} = {idDoacao}";
                await ExecutarQuery(queryString);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }
    }
}
