namespace OhMyDog_API.Model.Postagem
{
    public class DadosPostagem
    {
        public string IdPostagem { get; set; }

        public string Titulo { get; set; }

        public string Conteudo { get; set; }

        public string DtPublicacao { get; set; }

        public string DtAprovacao { get; set; }

        public string IdUsuario { get; set; }

        public string IdAdminstrador { get; set; }

        public string Imagem { get; set; }

        public string TipoDoacao { get; set; }        

        public string ChavePix { get; set; }

        public string Status { get; set; }
    }

    public class AtualizaPostagem
    {
        public string IdPostagem { get; }

        public string Titulo { get; set; }

        public string Conteudo { get; set; }

        public string Foto { get; set; }

        public string ChavePix { get; set; }

        public string Status { get; set; }
    }

    public class AprovaPostagem
    {
        public string IdPostagem { get; }

        public string Status { get; set; }
    }
}
