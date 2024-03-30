using Microsoft.AspNetCore.Mvc;
using System.Data.Odbc;
using static OhMyDog_API.Model.Parameters;
using System.Web;
using OhMyDog_API.Model.Usuarios;
using static OhMyDog_API.Controllers.ConexaoController;
using System.Data;

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
                var listLoginUsuario = new List<DadosUsuario>();

                var table = ExecutaQuery("SELECT * FROM Usuario");

                foreach (DataRow reader in table.Rows)
                {
                    listLoginUsuario.Add(
                        item: new DadosUsuario
                        {
                            idUsuario = reader["idUsuario"].ToString(),
                            Nome = reader["Nome"].ToString(),
                            CPF = reader["CPF"].ToString(),
                            DataNasc = Convert.ToDateTime(reader["DataNascimento"]).ToString("dd/MM/yyyy"),
                            CEP = reader["CEP"].ToString(),
                            Logradouro = reader["Logradouro"].ToString(),
                            Numero = reader["Numero"].ToString(),
                            Bairro = reader["Bairro"].ToString(),
                            Municipio = reader["Municipio"].ToString(),
                            Estado = reader["Estado"].ToString(),
                            Email = reader["Email"].ToString(),
                            Senha = reader["Senha"].ToString(),
                            Celular = reader["Celular"].ToString(),
                            Status = reader["Status"].ToString()
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
        public async Task<IActionResult> RetornarUsuario([FromRoute] string idUsuario)
        {
            try
            {
                var loginUsuario = new DadosUsuario();

                var table = ExecutaQuery($"SELECT * FROM Usuario WHERE idUsuario = '{idUsuario}'");
                
                foreach (DataRow reader in table.Rows)
                {
                    loginUsuario.idUsuario = reader["idUsuario"].ToString();
                    loginUsuario.Nome = reader["Nome"].ToString();
                    loginUsuario.CPF = reader["CPF"].ToString();
                    loginUsuario.DataNasc = Convert.ToDateTime(reader["DataNascimento"]).ToString("dd/MM/yyyy");
                    loginUsuario.CEP = reader["CEP"].ToString();
                    loginUsuario.Logradouro = reader["Logradouro"].ToString();
                    loginUsuario.Numero = reader["Numero"].ToString();
                    loginUsuario.Bairro = reader["Bairro"].ToString();
                    loginUsuario.Municipio = reader["Municipio"].ToString();
                    loginUsuario.Estado = reader["Estado"].ToString();
                    loginUsuario.Email = reader["Email"].ToString();
                    loginUsuario.Senha = reader["Senha"].ToString();
                    loginUsuario.Celular = reader["Celular"].ToString();

                    return Ok(loginUsuario);
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
        public async Task<IActionResult> PostLogarUsuario([FromBody] LoginUsuario login)
        {
            try
            {
                var loginUsuario = new DadosUsuario();

                var table = ExecutaQuery($"exec sp_Login '{login.Email}', '{login.Senha}'");                

                if (table.Rows.Count > 0)
                {
                    var usuarioLogado = new
                    {
                        idUsuario = table.Rows[0]["IdUsuario"],
                        nome = table.Rows[0]["Nome"],
                        tipoUsuario = table.Rows[0]["TipoUsuario"]
                    };

                    return Ok(usuarioLogado);
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
        public async Task<IActionResult> PostNovoUsuario([FromBody] DadosUsuario usuario)
        {
            try
            {
                if (usuario == null)
                    return BadRequest();

                ExecutaQuery(query: $"INSERT INTO Usuario (Nome, CPF, DataNascimento, CEP, Logradouro, Numero, Bairro, Municipio, Estado, Email, Senha, Celular, Status) VALUES (" +                                    
                                    $"'{usuario.Nome}', " +                                   
                                    $"'{usuario.CPF}', " +
                                    $"'{Convert.ToDateTime(usuario.DataNasc).ToString("yyyyMMdd")}', " +
                                    $"'{usuario.CEP}', " +
                                    $"'{usuario.Logradouro}', " +
                                    $"'{usuario.Numero}', " +
                                    $"'{usuario.Bairro}', " +
                                    $"'{usuario.Municipio}', " +
                                    $"'{usuario.Estado}', " +
                                    $"'{usuario.Email}', " +
                                    $"'{usuario.Senha}', " +
                                    $"'{usuario.Celular}', " +
                                    $"'{usuario.Status}')");

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
                    $"Nome = '{usuario.Nome}', " +
                    $"CPF = '{usuario.CPF}', " +
                    $"DataNascimento = '{Convert.ToDateTime(usuario.DataNasc).ToString("yyyyMMdd")}', " +
                    $"CEP = '{usuario.CEP}', " +
                    $"Logradouro = '{usuario.Logradouro}', " +
                    $"Numero = '{usuario.Numero}', " +
                    $"Bairro = '{usuario.Bairro}', "+
                    $"Municipio = '{usuario.Municipio}', "+
                    $"Estado = '{usuario.Estado}' "+
                    $"WHERE idUsuario = '{usuario.idUsuario}'";

                ExecutaQuery(queryString);

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
                var table = ExecutaQuery($"SELECT idUsuario FROM Usuario WHERE idUsuario = '{idUsuario}'");

                if (table.Rows.Count == 0)
                    return BadRequest("Agendamento não encontrado!");

                ExecutaQuery($"DELETE FROM Usuario WHERE idUsuario ='{idUsuario}'");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

    }
}
