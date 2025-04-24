using OnlineSinavSistemi.Models;

namespace OnlineSinavSistemi.Interfaces
{
    public interface ITestEngine
    {
        Sonuc SinavOl(int sinavId, SinavCevap cevaplar);
        List<Sinav> GetSinavlar();
        List<Sonuc> GetSonuclar();
    }
} 