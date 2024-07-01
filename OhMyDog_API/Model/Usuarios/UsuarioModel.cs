namespace OhMyDog_API.Model.Usuarios
{
    public class UsuarioModel
    {
        public string? idUsuario { get; set; }

        public string? Nome { get; set; }

        public string? CPF { get; set; }

        public string? DtNascimento { get; set; }

        public string? CEP { get; set; }

        public string? Logradouro { get; set; }

        public string? Bairro { get; set;}
        
        public string? Numero { get; set; }

        public string? Municipio { get; set; }

        public string? Estado { get; set; }

        public string? Email { get; set; }

        public string? Senha { get; set; }

        public string? Celular { get; set; }

        public string? UrlFoto { get; set; }

        public string? Status { get; set; }
    }

    public class NovoUsuarioModel
    {
        public string? Nome { get; set; }

        public string? CPF { get; set; }

        public string? DtNascimento { get; set; }

        public string? CEP { get; set; }

        public string? Logradouro { get; set; }

        public string? Bairro { get; set; }

        public string? Numero { get; set; }

        public string? Municipio { get; set; }

        public string? Estado { get; set; }

        public string? Email { get; set; }

        public string? Senha { get; set; }

        public string? Celular { get; set; }

        public string? UrlFoto { get; set; }        
    }


    public class AtualizarUsuario
    {
        public string? IdUsuario { get; set; }

        public string? Nome { get; set; }

        public string? CPF { get; set; }

        public string? DataNasc { get; set; }

        public string? CEP { get; set; }

        public string? Logradouro { get; set; }

        public string? Bairro { get; set; }

        public string? Numero { get; set; }

        public string? Municipio { get; set; }

        public string? Estado { get; set; }

        public string? Senha { get; set; }

        public string? Celular { get; set; }

        public string? UrlFoto { get; set; }

        public string? Status { get; set; }
    }

    public class LoginUsuario
    {
        public string? Email { get; set; }

        public string? Senha { get; set; }
    }
    public class UsuarioLogado
    {
        public string? IdUsuario { get; set; }

        public string? Nome { get; set; }

        public string? TipoUsuario { get; set; }

        public string? IdPatrocinador { get; set; }
    }

}
