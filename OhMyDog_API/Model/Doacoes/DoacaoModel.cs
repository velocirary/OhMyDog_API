namespace OhMyDog_API.Model.Doacoes
{
    public class DoacaoModel
    {
        public string? IdDoacao { get; set; }

        public string? IdUsuarioDoador { get; set; }       

        public string? IdPostagem { get; set; }

        public string? IdVoucher { get; set; }

        public string? ValorDoacao { get; set; }

        public string? Mensagem { get; set; }

        public string? ComprovantePix { get; set; }
        
        public string? IdTipoDoacao { get; set; }

        public string? DtDoacao { get; set; }

        public string? Status { get; set; }  
    }

    public class NovaDoacaoModel
    {
        public string? IdUsuarioDoador { get; set; }

        public string? IdPostagem { get; set; }

        public string? ValorDoacao { get; set; }

        public string? Mensagem { get; set; }

        public string? ComprovantePix { get; set; }

        public string? IdTipoDoacao { get; set; }        
    }

    public class DoacaoUsuarioSolicitanteModel
    {
        public string? IdDoacao { get; set; }

        public string? IdUsuarioDoador { get; set; }       

        public string? IdPostagem { get; set; }

        public string? IdVoucher { get; set; }

        public string? ValorDoacao { get; set; }

        public string? Mensagem { get; set; }

        public string? ComprovantePix { get; set; }
        
        public string? IdTipoDoacao { get; set; }

        public string? DtDoacao { get; set; }

        public string? Status { get; set; }

        public string? CupomVoucher { get; set; }

        public string? NomeUsuarioDoador { get; set; }

        public string? TituloPostagem { get; set; }

        public string? StatusPostagem { get; set; }
        
        public string? IdPatrocinador { get; set; }
    }

    public class DoacaoUsuarioDoadorModel
    {
        public string? IdDoacao { get; set; }

        public string? IdUsuarioDoador { get; set; }       

        public string? IdPostagem { get; set; }

        public string? IdVoucher { get; set; }

        public string? ValorDoacao { get; set; }

        public string? Mensagem { get; set; }

        public string? ComprovantePix { get; set; }
        
        public string? IdTipoDoacao { get; set; }

        public string? DtDoacao { get; set; }

        public string? Status { get; set; }

        public string? CupomVoucher { get; set; }

        public string? NomeUsuarioSolicitante { get; set; }

        public string? TituloPostagem { get; set; }

        public string? StatusPostagem { get; set; }
        
        public string? IdPatrocinador { get;  set; }
    }
}
