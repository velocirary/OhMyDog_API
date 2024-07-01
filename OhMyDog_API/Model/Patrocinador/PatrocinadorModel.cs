namespace OhMyDog_API.Model.Patrocinador
{
    public class PatrocinadorModel
    {
        public string? IdPatrocinador { get; set; }

        public string? IdUsuario { get; set; }

        public string? CNPJ { get; set; }

        public string? RazaoSocial { get; set; }

        public string? InscricaoEstadual { get; set; }

        public string? Observacao { get; set; }

        public string? MsgAdministrador { get; set; }

        public string? Status { get; set; }
    }

    public class NovoPatrocinadorModel
    { 
        public string? IdUsuario { get; set; }

        public string? CNPJ { get; set; }

        public string? RazaoSocial { get; set; }

        public string? InscricaoEstadual { get; set; }

        public string? Observacao { get; set; }        
    }
}
