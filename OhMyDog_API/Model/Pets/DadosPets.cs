using System.Runtime.ConstrainedExecution;

namespace OhMyDog_API.Model.Pets
{
    public class DadosPets
    {
        public string codPet { get; set; }
        public string Sexo { get; set; }
        public string Cor { get; set; }
        public string Nome { get; set; }
        public string Especie { get; set; }
        public string Peso { get; set; }
        
        private string _castrado;
        public string Castrado
        {
            get { return _castrado; }
            set { _castrado = value == "True" ? "1" : "0"; }
        }
        public string Observacao { get; set; }
        public string codCliente { get; set; }
    }
}
