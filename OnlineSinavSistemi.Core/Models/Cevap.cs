namespace OnlineSinavSistemi.Core.Models
{
    /// <summary>
    /// Öğrencinin bir soruya verdiği cevap
    /// </summary>
    public class Cevap
    {
        public int SoruId { get; set; }
        public int SoruNumarasi { get; set; }
        public string VerilenCevap { get; set; } = string.Empty;
    }
} 