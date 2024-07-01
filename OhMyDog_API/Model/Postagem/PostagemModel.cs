namespace OhMyDog_API.Model.Postagem
{
    public class PostagemModel
    {
        public string? IdPostagem { get; set; }

        public string? IdUsuario { get; set; }

        public string? IdAdministrador { get; set; }
        
        public string? DtPublicacao { get; set; }

        public string? DtAprovacao { get; set; }

        public string? DoacaoPix { get; set; }

        public string? DoacaoRacao { get; set; }

        public string? DoacaoMedicamento { get; set; }

        public string? DoacaoOutros { get; set; }

        public string? Titulo { get; set; }

        public string? Conteudo { get; set; }

        public string? UrlFoto { get; set; }

        public string? ChavePix { get; set; }

        public string? MetaDoacao { get; set; }

        public string? ValorArrecadado { get; set; }

        public string? MsgAdministrador { get; set; }

        public string? Status { get; set; }
    }

    public class NovaPostagemModel
    {
        public string? IdUsuario { get; set; }
    
        public string? DoacaoPix { get; set; }

        public string? DoacaoRacao { get; set; }

        public string? DoacaoMedicamento { get; set; }

        public string? DoacaoOutros { get; set; }

        public string? Titulo { get; set; }

        public string? Conteudo { get; set; }

        public string? UrlFoto { get; set; }

        public string? ChavePix { get; set; }

        public string? MetaDoacao { get; set; }   
    }

    public class AprovarPostagem
    {
        public string? IdPostagem { get; set; }

        public string? DtAprovacao { get; }

        public string? MsgAdministrador { get; set; }

        public string? Status { get; set; }
    }
}
