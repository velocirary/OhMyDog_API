namespace OhMyDog_API.Model.Clientes
{
    public class DadosClientes
    {
        public string CodCliente { get; set; }

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
    }

    public class AtualizarCliente
    {
        public string CodCliente { get; set; }

        public string Nome { get; set; }

        public string CPF { get; set; }

        public string DataNasc { get; set; }

        public string CEP { get; set; }

        public string Logradouro { get; set; }

        public string Numero { get; set; }

        public string Bairro { get; set; }

        public string Municipio { get; set; }

        public string Estado { get; set; }

    }

    public class LoginCliente
    {
        public string Email { get; set; }

        public string Senha { get; set; }
    }
}
