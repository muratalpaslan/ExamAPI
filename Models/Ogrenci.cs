namespace OnlineSinavSistemi.Models
{
    public class Ogrenci : Kullanici
    {
        public string? Sinif { get; set; }
        public List<int> KatildigiSinavlar { get; set; }

        public Ogrenci()
        {
            KatildigiSinavlar = new List<int>();
        }
    }
} 