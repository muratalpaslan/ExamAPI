namespace OnlineSinavSistemi.Models
{
    public class Soru
    {
        public int SoruId { get; set; }
        public required string Metin { get; set; }
        public List<string> Secenekler { get; set; }
        public required string DogruCevap { get; set; }

        public Soru()
        {
            Secenekler = new List<string>();
        }
    }
} 