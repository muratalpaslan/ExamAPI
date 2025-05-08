using System.Collections.Generic;

namespace OnlineSinavSistemi.Core.Models
{
    /// <summary>
    /// Öğrencinin sınav cevapları
    /// </summary>
    public class SinavCevap
    {
        public int SinavId { get; set; }
        public int OgrenciId { get; set; }
        public string OgrenciIsim { get; set; } = string.Empty;
        public string OgrenciEmail { get; set; } = string.Empty;
        public string OgrenciSinif { get; set; } = string.Empty; // Öğrencinin sınıf bilgisi
        public List<Cevap> Cevaplar { get; set; } = new List<Cevap>();
    }
} 