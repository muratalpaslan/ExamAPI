namespace OnlineSinavSistemi.Core.Models
{
    /// <summary>
    /// Sınav varlık sınıfı
    /// </summary>
    public class Sinav
    {
        public int SinavId { get; set; }
        public List<Soru> Sorular { get; set; } = new List<Soru>();
        public string Baslik { get; set; } = string.Empty;
        public DateTime OlusturulmaTarihi { get; set; } = DateTime.Now;
    }
} 