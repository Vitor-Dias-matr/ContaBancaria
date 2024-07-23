namespace Teste.Models
{
    public class Conta
    {
        public Conta(string responsavel)
        {
            Responsavel = responsavel;
        }

        public int NumeroConta { get; set; }
        public string Responsavel { get; set; }
        public decimal Saldo { get; private set; }
        
    }
}
