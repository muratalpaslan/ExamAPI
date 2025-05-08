public class Ogrenci
{
    public int OgrenciId { get; set; }
    public string Isim { get; set; }
    public string Email { get; set; }
    public string Sinif { get; set; }
    public List<int> KatildigiSinavlar { get; set; } = new List<int>();
} 