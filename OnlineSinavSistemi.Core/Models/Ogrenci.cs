namespace OnlineSinavSistemi.Core.Models
{
    /// <summary>
    /// Öğrenci varlık sınıfı
    /// </summary>
    public class Ogrenci : Kullanici
    {
        public string Sinif { get; set; } = string.Empty;
        public int OgrenciId => Id;
        public List<int> KatildigiSinavlar { get; set; } = new List<int>();
    }
} 