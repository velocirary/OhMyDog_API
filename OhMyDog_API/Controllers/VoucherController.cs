using Microsoft.AspNetCore.Mvc;
using OhMyDog_API.Model.Voucher;
using static OhMyDog_API.Controllers.ConexaoController;
using System.Data;

namespace OhMyDog_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VoucherController : ControllerBase
    {
        [HttpGet]
        [Route("TodosVouchers")]
        public async Task<IActionResult> GetTodosVouchers()
        {
            try
            {
                var listVoucher = new List<DadosVoucher>();

                var table = ExecutaQuery("SELECT * FROM Voucher");

                foreach (DataRow reader in table.Rows)
                {
                    listVoucher.Add(
                        item: new DadosVoucher
                        {
                            IdVoucher = reader["IdVoucher"].ToString(),
                            IdPatrocinador = reader["IdPatrocinador"].ToString(),
                            DtVencimento = Convert.ToDateTime(reader["DtVencimento"]).ToString("dd/MM/yyyy"),
                            Cupom = reader["Cupom"].ToString(),
                            Status = reader["Status"].ToString()                            
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
        public async Task<IActionResult> GetVoucherPatronicador([FromRoute] string idPatrocinador)
        {
            try
            {
                var listVoucher = new List<DadosVoucher>();

                var table = ExecutaQuery($"SELECT * FROM Voucher WHERE idPatrocinador = '{idPatrocinador}'");

                foreach (DataRow reader in table.Rows)
                {
                    listVoucher.Add(
                        item: new DadosVoucher
                        {
                            IdVoucher = reader["IdVoucher"].ToString(),
                            IdPatrocinador = reader["IdPatrocinador"].ToString(),
                            DtVencimento = Convert.ToDateTime(reader["DtVencimento"]).ToString("dd/MM/yyyy"),
                            Cupom = reader["Cupom"].ToString(),
                            Status = reader["Status"].ToString()
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


        [HttpPost]
        [Route("InserirNovoVoucher")]
        public async Task<IActionResult> PostNovoVoucher([FromBody] DadosVoucher voucher)
        {
            try
            {
                if (voucher == null)
                    return BadRequest();

                ExecutaQuery(query: $"INSERT INTO Voucher (IdPatrocinador, Valor, DtVencimento, Cupom, Status) VALUES (" +
                                    $"'{voucher.IdPatrocinador}', " +
                                    $"'{voucher.Valor}', " +
                                    $"'{Convert.ToDateTime(voucher.DtVencimento).ToString("yyyyMMdd")}', " +
                                    $"'{voucher.Cupom}', " +
                                    $"'{voucher.Status}')");
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
                                     $"Cupom = '{voucher.Cupom}', " +                                     
                                     $"DtVencimento = '{Convert.ToDateTime(voucher.DtVencimento).ToString("yyyyMMdd")}', " +
                                     $"Status = '{voucher.Status}' " +
                                     $"WHERE IdVoucher = {voucher.IdVoucher}";
                ExecutaQuery(queryString);

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
                var table = ExecutaQuery($"SELECT idVoucher FROM voucher WHERE idVoucher = '{idVoucher}'");

                if (table.Rows.Count == 0)
                    return BadRequest("Voucher não encontrado!");

                ExecutaQuery($"DELETE FROM Voucher WHERE idVoucher = '{idVoucher}'");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }
    }
}
