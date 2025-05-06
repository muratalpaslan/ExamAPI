namespace OnlineSinavSistemi.Core.Models
{
    /// <summary>
    /// Sınav sorusu varlık sınıfı
    /// </summary>
    public class Soru
    {
        public int SoruId { get; set; }
        public string Metin { get; set; } = string.Empty;
        public List<string> Secenekler { get; set; } = new List<string>();
        public string DogruCevap { get; set; } = string.Empty;
    }
} 