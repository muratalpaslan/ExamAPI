namespace OnlineSinavSistemi.Models
{
    public class SinavCevap
    {
        public int OgrenciId { get; set; }
        public int SinavId { get; set; }
        public List<Cevap> Cevaplar { get; set; }

        public SinavCevap()
        {
            Cevaplar = new List<Cevap>();
        }
    }
} 