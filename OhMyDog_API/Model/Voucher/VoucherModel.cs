namespace OhMyDog_API.Model.Voucher
{
    public class VoucherModel
    {
        public string? IdVoucher { get; set; }

        public string? IdPatrocinador { get; set;}

        public string? DtVencimento { get; set; }

        public string? Cupom { get; set; }

        public string? Valor { get; set; }

        public string? Status { get; set; }
    }

    public class NovoVoucherModel
    {
        public string? IdPatrocinador { get; set; }

        public string? DtVencimento { get; set; }

        public string? Cupom { get; set; }

        public string? Valor { get; set; }
        
    }

    public class AtualizaVoucher
    {
        public string? IdVoucher { get; set; }

        public string? DtVencimento { get; set; }

        public string? Cupom { get; set; }

        public string? Valor { get; set; }

        public string? Status { get; set; }
    }

    public class VoucherRandom
    {
        public string? IdVoucher { get; set; }       

        public string? Cupom { get; set; }
    }
}
