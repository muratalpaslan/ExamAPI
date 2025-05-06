using System;

namespace OnlineSinavSistemi.Core.Models
{
    /// <summary>
    /// Sınav sonucu
    /// </summary>
    public class Sonuc
    {
        public Ogrenci? Ogrenci { get; set; }
        public int OgrenciId { get; set; }
        public int SinavId { get; set; }
        public int DogruSayisi { get; set; }
        public int YanlisSayisi { get; set; }
        public double Puan { get; set; }
        public DateTime TamamlanmaTarihi { get; set; } = DateTime.Now;
    }
} 