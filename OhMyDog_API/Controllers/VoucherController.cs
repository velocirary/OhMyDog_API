using Microsoft.AspNetCore.Mvc;
using OhMyDog_API.Model.Voucher;
using static OhMyDog_API.Controllers.ConexaoController;
using System.Data;
using static OhMyDog_API.Constantes.VoucherConstantes;
using OhMyDog_API.Model.Doacoes;
using OhMyDog_API.Model.Usuarios;

namespace OhMyDog_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VoucherController : ControllerBase
    {
        [HttpGet]
        [Route("TodosVouchers")]
        [ProducesResponseType(typeof(List<VoucherModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTodosVouchers()
        {
            try
            {
                var listVoucher = new List<VoucherModel>();

                var table = await ExecutarQuery("SELECT * FROM Voucher");

                foreach (DataRow reader in table.Rows)
                {
                    listVoucher.Add(
                        item: new VoucherModel
                        {
                            IdVoucher = reader[Tabela.IdVoucher].ToString(),
                            Valor = reader[Tabela.Valor].ToString(),
                            IdPatrocinador = reader[Tabela.IdPatrocinador].ToString(),
                            DtVencimento = Convert.ToDateTime(reader[Tabela.DtVencimento]).ToString("dd/MM/yyyy"),
                            Cupom = reader[Tabela.Cupom].ToString(),
                            Status = reader[Tabela.Status].ToString()
                        }
                    );
                }

                return Ok(listVoucher);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpGet]
        [Route("VoucherPatronicador/{idPatrocinador}")]
        [ProducesResponseType(typeof(List<VoucherModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVoucherPatronicador([FromRoute] string idPatrocinador)
        {
            try
            {
                var listVoucher = new List<VoucherModel>();

                var table = await ExecutarQuery($"SELECT * FROM Voucher WHERE {Tabela.IdPatrocinador} = '{idPatrocinador}'");

                foreach (DataRow reader in table.Rows)
                {
                    listVoucher.Add(
                        item: new VoucherModel
                        {
                            IdVoucher = reader[Tabela.IdVoucher].ToString(),
                            Valor = reader[Tabela.Valor].ToString(),
                            IdPatrocinador = reader[Tabela.IdPatrocinador].ToString(),
                            DtVencimento = Convert.ToDateTime(reader[Tabela.DtVencimento]).ToString("dd/MM/yyyy"),
                            Cupom = reader[Tabela.Cupom].ToString(),
                            Status = reader[Tabela.Status].ToString()
                        }
                    );
                }

                return Ok(listVoucher);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }        
        
        [HttpGet]
        [Route("VoucherRandom")]
        [ProducesResponseType(typeof(VoucherRandom), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVoucherRandom()
        {
            try
            {               
                var table = await ExecutarQuery($"SELECT TOP 1 IdVoucher, Cupom FROM Voucher WHERE Status = 'A' ORDER BY NEWID()");

                if (table.Rows.Count > 0)
                {
                    var voucher = new VoucherRandom
                    {
                        IdVoucher = table.Rows[0][Tabela.IdVoucher].ToString(),
                        Cupom = table.Rows[0][Tabela.Cupom].ToString()
                    };

                    await ExecutarQuery($"UPDATE Voucher SET Status = 'I' WHERE IdVoucher = '{voucher.IdVoucher}'");
                    return Ok(voucher);
                }

                else
                    return StatusCode(204, "Não existem Voucher cadastrados no momento");                
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpGet]
        [Route("{idVoucher}")]
        [ProducesResponseType(typeof(VoucherModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVoucherPorVoucher([FromRoute] string idVoucher)
        {
            try
            {               
                var table = await ExecutarQuery($"SELECT * FROM Voucher WHERE {Tabela.IdVoucher} = '{idVoucher}'");

                if (table.Rows.Count > 0)
                {
                    DataRow reader = table.Rows[0];

                    var voucher = new VoucherModel
                    {
                        IdVoucher = reader[Tabela.IdVoucher].ToString(),
                        Valor = reader[Tabela.Valor].ToString(),
                        IdPatrocinador = reader[Tabela.IdPatrocinador].ToString(),
                        DtVencimento = Convert.ToDateTime(reader[Tabela.DtVencimento]).ToString("dd/MM/yyyy"),
                        Cupom = reader[Tabela.Cupom].ToString(),
                        Status = reader[Tabela.Status].ToString()
                    };

                    return Ok(voucher);
                }

                else
                    return StatusCode(204, "Não existe um Voucher cadastrados com esse ID");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpGet]
        [Route("Cupom/{cupom}")]
        [ProducesResponseType(typeof(VoucherModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVoucherPorCupom([FromRoute] string cupom)
        {
            try
            {
                var table = await ExecutarQuery($"SELECT * FROM Voucher WHERE {Tabela.Cupom} = '{cupom}'");

                if (table.Rows.Count > 0)
                {
                    DataRow reader = table.Rows[0];

                    var voucher = new VoucherModel
                    {
                        IdVoucher = reader[Tabela.IdVoucher].ToString(),
                        Valor = reader[Tabela.Valor].ToString(),
                        IdPatrocinador = reader[Tabela.IdPatrocinador].ToString(),
                        DtVencimento = Convert.ToDateTime(reader[Tabela.DtVencimento]).ToString("dd/MM/yyyy"),
                        Cupom = reader[Tabela.Cupom].ToString(),
                        Status = reader[Tabela.Status].ToString()
                    };

                    return Ok(voucher);
                }

                else
                    return StatusCode(204, "Não existe um Voucher cadastrados com esse Cupom");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpPost]
        [Route("InserirNovoVoucher")]
        public async Task<IActionResult> PostNovoVoucher([FromBody] NovoVoucherModel voucher)
        {
            try
            {
                if (voucher == null)
                    return BadRequest();

                await ExecutarQuery(query: $"INSERT INTO Voucher ({Tabela.IdPatrocinador}, {Tabela.Valor}, {Tabela.DtVencimento}, {Tabela.Cupom}) VALUES (" +
                                    $"'{voucher.IdPatrocinador}', " +
                                    $"'{voucher.Valor}', " +
                                    $"'{Convert.ToDateTime(voucher.DtVencimento).ToString("yyyyMMdd")}', " +                                    
                                    $"'{voucher.Cupom}')");
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpPatch]
        [Route("AtualizarVoucher")]
        public async Task<IActionResult> AtualizaVoucher([FromBody] AtualizaVoucher voucher)
        {
            try
            {
                string queryString = $"UPDATE Voucher SET " +
                                     $"{Tabela.Cupom} = '{voucher.Cupom}', " +                                     
                                     $"{Tabela.Valor} = '{voucher.Valor}', " +                                     
                                     $"{Tabela.DtVencimento} = '{Convert.ToDateTime(voucher.DtVencimento).ToString("yyyyMMdd")}', " +
                                     $"{Tabela.Status} = '{voucher.Status}' " +
                                     $"WHERE {Tabela.IdVoucher} = {voucher.IdVoucher}";
                await ExecutarQuery(queryString);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpDelete]
        [Route("DeletarVoucher/{idVoucher}")]
        public async Task<IActionResult> DeletarPostagem([FromRoute] string idVoucher)
        {
            try
            {
                var table = await ExecutarQuery($"SELECT {Tabela.IdVoucher} FROM voucher WHERE {Tabela.IdVoucher} = '{idVoucher}'");

                if (table.Rows.Count == 0)
                    return BadRequest("Voucher não encontrado!");
                
                await ExecutarQuery($"UPDATE Voucher SET " +
                                     $"{Tabela.Status} = 'I' " +
                                     $"WHERE {Tabela.IdVoucher} = {idVoucher}");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }
    }
}
