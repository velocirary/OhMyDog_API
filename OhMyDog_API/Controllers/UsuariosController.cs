using Microsoft.AspNetCore.Mvc;
using OhMyDog_API.Model.Usuarios;
using static OhMyDog_API.Controllers.ConexaoController;
using System.Data;
using static OhMyDog_API.Constantes.UsuarioConstantes;

namespace OhMyDog_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
        [HttpGet]
        [Route("TodosUsuarios")]
        public async Task<IActionResult> GetTodosUsuarios()
        {
            try
            {
                var listLoginUsuario = new List<UsuarioModel>();

                var table = await ExecutarQuery("SELECT * FROM Usuario");

                foreach (DataRow reader in table.Rows)
                {
                    listLoginUsuario.Add(
                        item: new UsuarioModel
                        {
                            idUsuario = reader[Tabela.IdUsuario].ToString(),
                            Nome = reader[Tabela.Nome].ToString(),
                            CPF = reader[Tabela.CPF].ToString(),
                            DtNascimento = Convert.ToDateTime(reader[Tabela.DtNascimento]).ToString("dd/MM/yyyy"),
                            CEP = reader[Tabela.CEP].ToString(),
                            Logradouro = reader[Tabela.Logradouro].ToString(),
                            Numero = reader[Tabela.Numero].ToString(),
                            Bairro = reader[Tabela.Bairro].ToString(),
                            Municipio = reader[Tabela.Municipio].ToString(),
                            Estado = reader[Tabela.Estado].ToString(),
                            Email = reader[Tabela.Email].ToString(),
                            Senha = reader[Tabela.Senha].ToString(),
                            Celular = reader[Tabela.Celular].ToString(),
                            UrlFoto = reader[Tabela.UrlFoto].ToString(),
                            Status = reader[Tabela.Status].ToString(),
                        }
                    );
                }

                return Ok(listLoginUsuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpGet]
        [Route("Usuario/{idUsuario}")]
        [ProducesResponseType(typeof(UsuarioModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> RetornarUsuario([FromRoute] string idUsuario)
        {
            try
            {
                var usuario = new UsuarioModel();

                var table = await ExecutarQuery($"SELECT * FROM Usuario WHERE {Tabela.IdUsuario} = '{idUsuario}'");
                
                foreach (DataRow reader in table.Rows)
                {
                    usuario.idUsuario = reader[Tabela.IdUsuario].ToString();
                    usuario.Nome = reader[Tabela.Nome].ToString();
                    usuario.CPF = reader[Tabela.CPF].ToString();
                    usuario.DtNascimento = Convert.ToDateTime(reader[Tabela.DtNascimento]).ToString("dd/MM/yyyy");
                    usuario.CEP = reader[Tabela.CEP].ToString();
                    usuario.Logradouro = reader[Tabela.Logradouro].ToString();
                    usuario.Numero = reader[Tabela.Numero].ToString();
                    usuario.Bairro = reader[Tabela.Bairro].ToString();
                    usuario.Municipio = reader[Tabela.Municipio].ToString();
                    usuario.Estado = reader[Tabela.Estado].ToString();
                    usuario.Email = reader[Tabela.Email].ToString();
                    usuario.Senha = reader[Tabela.Senha].ToString();
                    usuario.Celular = reader[Tabela.Celular].ToString();
                    usuario.UrlFoto = reader[Tabela.UrlFoto].ToString();
                    usuario.Status = reader[Tabela.Status].ToString();

                    return Ok(usuario);
                }

                return StatusCode(204, "Usuário inexistente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpPost]
        [Route("LoginUsuario")]
        [ProducesResponseType(typeof(UsuarioLogado), StatusCodes.Status200OK)]
        public async Task<IActionResult> PostLogarUsuario([FromBody] LoginUsuario login)
        {
            try
            {
                var loginUsuario = new UsuarioModel();

                var table = await ExecutarQuery($"exec LogarUsuario '{login.Email}', '{login.Senha}'");                

                if (table.Rows.Count > 0)
                {
                    return Ok(
                        new UsuarioLogado
                        {
                            IdUsuario = table.Rows[0][Tabela.IdUsuario].ToString(),
                            Nome = table.Rows[0][Tabela.Nome].ToString(),
                            TipoUsuario = table.Rows[0]["TipoUsuario"].ToString(),
                            IdPatrocinador = table.Rows[0]["IdPatrocinador"].ToString()
                        });
                }
                else
                    return StatusCode(204, "Credenciais inválidas");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpPost]
        [Route("InserirNovoUsuario")]
        public async Task<IActionResult> PostNovoUsuario([FromBody] NovoUsuarioModel usuario)
        {
            try
            {
                if (usuario == null)
                    return BadRequest();

                await ExecutarQuery(
                    $"INSERT INTO Usuario ({Tabela.Nome}, {Tabela.CPF}, {Tabela.DtNascimento}, {Tabela.CEP}, {Tabela.Logradouro}, {Tabela.Numero}, {Tabela.Bairro}, {Tabela.Municipio}, {Tabela.Estado}, {Tabela.Email}, {Tabela.Senha}, {Tabela.Celular}, {Tabela.UrlFoto}) VALUES (" +
                    $"'{usuario.Nome}', " +
                    $"'{usuario.CPF}', " +
                    $"'{Convert.ToDateTime(usuario.DtNascimento).ToString("yyyyMMdd")}', " +
                    $"'{usuario.CEP}', " +
                    $"'{usuario.Logradouro}', " +
                    $"'{usuario.Numero}', " +
                    $"'{usuario.Bairro}', " +
                    $"'{usuario.Municipio}', " +
                    $"'{usuario.Estado}', " +
                    $"'{usuario.Email}', " +
                    $"'{usuario.Senha}', " +
                    $"'{usuario.Celular}', " +                    
                    $"'{usuario.UrlFoto}')");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpPatch]
        [Route("AtualizarUsuario")]
        public async Task<IActionResult> AtualizarUsuario([FromBody] AtualizarUsuario usuario)
        {
            try
            {
                string queryString = $"UPDATE Usuario SET " +
                    $"{Tabela.Nome} = '{usuario.Nome}', " +
                    $"{Tabela.CPF} = '{usuario.CPF}', " +                
                    $"{Tabela.DtNascimento} = '{Convert.ToDateTime(usuario.DataNasc).ToString("yyyyMMdd")}', " +
                    $"{Tabela.CEP} = '{usuario.CEP}', " +
                    $"{Tabela.Logradouro} = '{usuario.Logradouro}', " +
                    $"{Tabela.Numero} = '{usuario.Numero}', " +
                    $"{Tabela.Bairro} = '{usuario.Bairro}', "+
                    $"{Tabela.Municipio} = '{usuario.Municipio}', "+
                    $"{Tabela.Estado} = '{usuario.Estado}', " +
                    $"{Tabela.Celular} = '{usuario.Celular}', " +
                    $"{Tabela.Senha} = '{usuario.Senha}', " +
                    $"{Tabela.UrlFoto} = '{usuario.UrlFoto}' " +
                    $"WHERE {Tabela.IdUsuario} = '{usuario.IdUsuario}'";

                await ExecutarQuery(queryString);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpDelete]
        [Route("DeletarUsuario/{idUsuario}")]
        public async Task<IActionResult> DeletarUsuario([FromRoute] string idUsuario)
        {
            try
            {
                var table = await ExecutarQuery($"SELECT {Tabela.IdUsuario} FROM Usuario WHERE {Tabela.IdUsuario} = '{idUsuario}'");

                if (table.Rows.Count == 0)
                    return BadRequest("Agendamento não encontrado!");

                await ExecutarQuery($"UPDATE Usuario SET {Tabela.Status} = 'N' WHERE {Tabela.IdUsuario} = '{idUsuario}'");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

    }
}
