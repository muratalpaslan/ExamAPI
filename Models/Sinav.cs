namespace OnlineSinavSistemi.Models
{
    public class Sinav
    {
        public int SinavId { get; set; }
        public List<Soru> Sorular { get; set; }

        public Sinav()
        {
            Sorular = new List<Soru>();
        }
    }
} 