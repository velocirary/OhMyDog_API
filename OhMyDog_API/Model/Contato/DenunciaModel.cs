namespace OhMyDog_API.Model.Contato
{
    public class ContatoModel
    {
        public string? EmailDestinatario { get; } = "ohmydogcontato.web@gmail.com";

        public string? EmailRemetente { get; } = "ohmydogcontato.web@gmail.com";

        public string? NomeContato { get; set; }

        public string? EmailContato { get; set; }

        public string? TelefoneContato { get; set; }

        public string? MensagemContato { get; set; }
    }
}
