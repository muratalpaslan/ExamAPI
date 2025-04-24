namespace OnlineSinavSistemi.Models
{
    public class Ogretmen : Kullanici
    {
        public string? Brans { get; set; }
        public List<int> HazirladigiSinavlar { get; set; }

        public Ogretmen()
        {
            HazirladigiSinavlar = new List<int>();
        }
    }
} 