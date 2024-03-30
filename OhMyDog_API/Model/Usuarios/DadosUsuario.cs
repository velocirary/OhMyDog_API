namespace OhMyDog_API.Model.Usuarios
{
    public class DadosUsuario
    {
        public string idUsuario { get; set; }

        public string Nome { get; set; }

        public string CPF { get; set; }

        public string DataNasc { get; set; }

        public string CEP { get; set; }

        public string Logradouro { get; set; }
        
        public string Numero { get; set; }

        public string Bairro { get; set;}

        public string Municipio { get; set; }

        public string Estado { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public string Celular { get; set; }

        public string Status { get; set; }
    }

    public class AtualizarUsuario
    {
        public string idUsuario { get; set; }

        public string Nome { get; set; }

        public string CPF { get; set; }

        public string DataNasc { get; set; }

        public string CEP { get; set; }

        public string Logradouro { get; set; }

        public string Numero { get; set; }

        public string Bairro { get; set; }

        public string Municipio { get; set; }

        public string Estado { get; set; }

        public string Celular { get; set; }

        public string Status { get; set; }
    }

    public class LoginUsuario
    {
        public string Email { get; set; }

        public string Senha { get; set; }
    }
}
