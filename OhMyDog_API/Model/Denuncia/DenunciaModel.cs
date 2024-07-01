namespace OhMyDog_API.Model.Denuncia
{
    public class DenunciaModel
    {
        public int IdDenuncia { get; set; }

        public int IdUsuario { get; set; }

        public int IdPostagem { get; set; }

        public string? TituloDenuncia { get; set; }

        public string? DescricaoDenuncia { get; set; }

        public string? DataDenuncia { get; set; }
    }

    public class  NovaDenunciaModel
    {       
        public int IdUsuario { get; set; }

        public int IdPostagem { get; set; }

        public string? TituloDenuncia { get; set; }

        public string? DescricaoDenuncia { get; set; }        
    }
}
