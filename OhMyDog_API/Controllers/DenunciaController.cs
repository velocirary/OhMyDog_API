using Microsoft.AspNetCore.Mvc;
using static OhMyDog_API.Controllers.ConexaoController;
using System.Data;
using OhMyDog_API.Model.Doacoes;
using OhMyDog_API.Model.Denuncia;
using static OhMyDog_API.Constantes.DenunciaConstantes;
using OhMyDog_API.Constantes;

namespace OhMyDog_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DenunciaController : ControllerBase
    {
        [HttpGet]
        [Route("TodasDenuncias")]
        public async Task<IActionResult> GetTodasDenuncias()
        {
            try
            {
                var listDenuncias = new List<DenunciaModel>();
                var table = await ExecutarQuery($"SELECT * FROM {Tabela.NomeTabela}");

                foreach (DataRow reader in table.Rows)
                {
                    listDenuncias.Add(
                        new DenunciaModel
                        {
                            IdDenuncia = Convert.ToInt32(reader[Tabela.IdDenuncia]),
                            IdUsuario = Convert.ToInt32(reader[Tabela.IdUsuario]),
                            IdPostagem = Convert.ToInt32(reader[Tabela.IdPostagem]),
                            TituloDenuncia = reader[Tabela.TituloDenuncia].ToString(),
                            DescricaoDenuncia = reader[Tabela.DescricaoDenuncia].ToString(),
                            DataDenuncia = reader[Tabela.DataDenuncia].ToString()
                        }
                    );
                }

                return Ok(listDenuncias);
            }
            catch (Exception ex)
            {
                return StatusCode(204, "Denúncia inexistente. Erro: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("DenunciaPostagem/{idPostagem}")]
        public async Task<IActionResult> GetDenunciasPostagem([FromRoute] int idPostagem)
        {
            try
            {
                var denuncias = new List<DenunciaModel>();

                var table = await ExecutarQuery($"SELECT * FROM {Tabela.NomeTabela} WHERE {Tabela.IdPostagem} = {idPostagem}");

                foreach (DataRow reader in table.Rows)
                {
                    denuncias.Add(
                        new DenunciaModel()
                        {
                            IdDenuncia = Convert.ToInt32(reader[Tabela.IdDenuncia]),
                            IdUsuario = Convert.ToInt32(reader[Tabela.IdUsuario]),
                            IdPostagem = Convert.ToInt32(reader[Tabela.IdPostagem]),
                            TituloDenuncia = reader[Tabela.TituloDenuncia].ToString(),
                            DescricaoDenuncia = reader[Tabela.DescricaoDenuncia].ToString(),
                            DataDenuncia = reader[Tabela.DataDenuncia].ToString()
                        }
                    );
                }

                if (denuncias.Count > 0)
                    return Ok(denuncias);
                else
                    return StatusCode(204, "Postagem sem denúncias.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpGet]
        [Route("DenunciaIndividual/{idDenuncia}")]
        public async Task<IActionResult> GetDenunciaIdDenuncia([FromRoute] int idDenuncia)
        {
            try
            {
                var table = await ExecutarQuery($"SELECT * FROM {Tabela.NomeTabela} WHERE {Tabela.IdDenuncia} = {idDenuncia}");

                if (table.Rows.Count == 1)
                {
                    var denuncia = new DenunciaModel()
                    {
                        IdDenuncia = Convert.ToInt32(table.Rows[0][Tabela.IdDenuncia]),
                        IdUsuario = Convert.ToInt32(table.Rows[0][Tabela.IdUsuario]),
                        IdPostagem = Convert.ToInt32(table.Rows[0][Tabela.IdPostagem]),
                        TituloDenuncia = table.Rows[0][Tabela.TituloDenuncia].ToString(),
                        DescricaoDenuncia = table.Rows[0][Tabela.DescricaoDenuncia].ToString(),
                        DataDenuncia = table.Rows[0][Tabela.DataDenuncia].ToString()
                    };

                    return Ok(denuncia);
                }
                else if (table.Rows.Count == 0)                
                    return StatusCode(204, "Denúncia não encontrada.");
                
                else                
                    return StatusCode(500, "Mais de uma denúncia encontrada com o mesmo ID. O banco de dados está inconsistente.");                
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpGet]
        [Route("DenunciaUsuario/{idUsuario}")]
        public async Task<IActionResult> GetDenunciaUsuario([FromRoute] int idUsuario)
        {
            try
            {
                var table = await ExecutarQuery($"SELECT * FROM {Tabela.NomeTabela} WHERE {Tabela.IdUsuario} = {idUsuario}");

                if (table.Rows.Count == 1)
                {
                    var denuncia = new DenunciaModel()
                    {
                        IdDenuncia = Convert.ToInt32(table.Rows[0][Tabela.IdDenuncia]),
                        IdUsuario = Convert.ToInt32(table.Rows[0][Tabela.IdUsuario]),
                        IdPostagem = Convert.ToInt32(table.Rows[0][Tabela.IdPostagem]),
                        TituloDenuncia = table.Rows[0][Tabela.TituloDenuncia].ToString(),
                        DescricaoDenuncia = table.Rows[0][Tabela.DescricaoDenuncia].ToString(),
                        DataDenuncia = table.Rows[0][Tabela.DataDenuncia].ToString()
                    };

                    return Ok(denuncia);
                }
                else if (table.Rows.Count == 0)
                    return StatusCode(204, "Usuario sem Denuncia");

                else
                    return StatusCode(500, "Mais de uma denúncia encontrada com o mesmo ID. O banco de dados está inconsistente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpGet]
        [Route("TodasDenunciasParaAnalise")]
        public async Task<IActionResult> TodasDenunciasRecentes()
        {
            try
            {
                var listDenuncias = new List<DenunciaModel>();
                var table = await ExecutarQuery($"SELECT * FROM {Tabela.NomeTabela} ORDER BY {Tabela.DataDenuncia} DESC");

                foreach (DataRow reader in table.Rows)
                {
                    listDenuncias.Add(
                        new DenunciaModel
                        {
                            IdDenuncia = Convert.ToInt32(reader[Tabela.IdDenuncia]),
                            IdUsuario = Convert.ToInt32(reader[Tabela.IdUsuario]),
                            IdPostagem = Convert.ToInt32(reader[Tabela.IdPostagem]),
                            TituloDenuncia = reader[Tabela.TituloDenuncia].ToString(),
                            DescricaoDenuncia = reader[Tabela.DescricaoDenuncia].ToString(),
                            DataDenuncia = reader[Tabela.DataDenuncia].ToString()
                        }
                    );
                }

                return Ok(listDenuncias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpGet]
        [Route("UltimasDenunciasParaAnalise")]
        public async Task<IActionResult> GetDenunciasRecentes()
        {
            try
            {
                var listDenuncias = new List<DenunciaModel>();
                var table = await ExecutarQuery($"SELECT TOP 3 * FROM {Tabela.NomeTabela} ORDER BY {Tabela.DataDenuncia} DESC");

                foreach (DataRow reader in table.Rows)
                {
                    listDenuncias.Add(
                        new DenunciaModel
                        {
                            IdDenuncia = Convert.ToInt32(reader[Tabela.IdDenuncia]),
                            IdUsuario = Convert.ToInt32(reader[Tabela.IdUsuario]),
                            IdPostagem = Convert.ToInt32(reader[Tabela.IdPostagem]),
                            TituloDenuncia = reader[Tabela.TituloDenuncia].ToString(),
                            DescricaoDenuncia = reader[Tabela.DescricaoDenuncia].ToString(),
                            DataDenuncia = reader[Tabela.DataDenuncia].ToString()
                        }
                    );
                }

                return Ok(listDenuncias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }        

        [HttpPost]
        [Route("InserirNovaDenuncia")]
        public async Task<IActionResult> PostNovaDenuncia([FromBody] NovaDenunciaModel denuncia)
        {
            try
            {
                if (denuncia == null)
                    return BadRequest();

                await ExecutarQuery(query:
                    $"INSERT INTO {Tabela.NomeTabela} ({Tabela.IdUsuario}, {Tabela.IdPostagem}, {Tabela.TituloDenuncia}, {Tabela.DescricaoDenuncia}) VALUES (" +
                    $"{denuncia.IdUsuario}, " +
                    $"{denuncia.IdPostagem}, " +
                    $"'{denuncia.TituloDenuncia}', " +                    
                    $"'{denuncia.DescricaoDenuncia}')");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }
    }
}
