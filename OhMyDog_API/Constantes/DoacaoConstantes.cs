namespace OhMyDog_API.Constantes
{
    public class DoacaoConstantes
    {
        public struct Tabela
        {
            public const string NomeTabela = "Doacao";

            public const string IdDoacao = "IdDoacao";
            public const string IdUsuario = "IdUsuario";
            public const string IdPostagem = "IdPostagem";
            public const string IdVoucher = "IdVoucher";
            public const string Valor = "Valor";
            public const string Mensagem = "Mensagem";
            public const string ComprovantePix = "ComprovantePix";
            public const string IdTipoDoacao = "IdTipoDoacao";
            public const string DtDoacao = "DtDoacao";
            public const string Status = "Status";
        }

        public struct TipoDoacao
        {
            public const string NomeTabela = "TipoDoacao";

            public const string IdTipoDoacao = "IdTipoDoacao";
            public const string DescricaoDoacao = "DescricaoDoacao";
            
            public enum TiposDoacoes
            {
                Pix,
                Racao,
                Medicamento,
                Outros
            }
        }
    }
}
