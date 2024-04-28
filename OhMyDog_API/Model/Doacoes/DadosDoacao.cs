namespace OhMyDog_API.Model.Doacoes
{
    public class DadosDoacao
    {
        public string IdDoacao { get; set; }

        public string IdUsuarioDoador { get; set; }       

        public string IdPostagem { get; set; }

        public string IdVoucher { get; set; }

        public string ValorDoacao { get; set; }

        public string Mensagem { get; set; }

        public string ComprovantePix { get; set; }

        public string DtDoacao { get; set; }

        public string Status { get; set; }  
    }
}
