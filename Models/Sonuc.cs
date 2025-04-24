namespace OnlineSinavSistemi.Models
{
    public class Sonuc
    {
        public required Ogrenci Ogrenci { get; set; }
        public int DogruSayisi { get; set; }
        public int YanlisSayisi { get; set; }
        public double Puan { get; set; }
    }
} 