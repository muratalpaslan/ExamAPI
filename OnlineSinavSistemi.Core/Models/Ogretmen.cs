namespace OnlineSinavSistemi.Core.Models
{
    /// <summary>
    /// Öğretmen varlık sınıfı
    /// </summary>
    public class Ogretmen : Kullanici
    {
        public string Brans { get; set; } = string.Empty;
        public int OgretmenId => Id;
        public List<int> HazirladigiSinavlar { get; set; } = new List<int>();
    }
} 