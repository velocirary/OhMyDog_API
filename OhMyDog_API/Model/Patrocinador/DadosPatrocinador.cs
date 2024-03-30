namespace OhMyDog_API.Model.Patrocinador
{
    public class DadosPatrocinador
    {
        public string IdPatrocinador { get; set; }
        
        public string IdUsuario { get; set; }

        public string CNPJ { get; set; }

        public string RazaoSocial { get; set; }

        public string Celular { get; set; }

        public string Status { get; set; }
    }

    public class AprovaPatrocinador
    {
        public string IdPatrocinador { get; set; }

        public string Status { get; set; }
    }
}
