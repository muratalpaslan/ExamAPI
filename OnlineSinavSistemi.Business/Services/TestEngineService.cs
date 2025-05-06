using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using OnlineSinavSistemi.Core.Interfaces;
using OnlineSinavSistemi.Core.Models;

namespace OnlineSinavSistemi.Business.Services
{
    /// <summary>
    /// ITestEngine arayüzünün implementasyonu
    /// </summary>
    public class TestEngineService : ITestEngine
    {
        private readonly IUnitOfWork _unitOfWork;

        public TestEngineService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Sonuc SinavOl(int sinavId, SinavCevap cevaplar)
        {
            // Sınav ve öğrenci kontrolü
            var sinav = _unitOfWork.Sinavlar.GetById(sinavId);
            var ogrenci = _unitOfWork.Ogrenciler.GetById(cevaplar.OgrenciId);

            if (sinav == null || ogrenci == null)
                throw new ArgumentException("Sınav veya öğrenci bulunamadı.");

            // Cevapları değerlendir
            int dogruSayisi = 0;
            int yanlisSayisi = 0;

            Console.WriteLine($"Sınav değerlendiriliyor - Sınav ID: {sinavId}, Öğrenci: {ogrenci.Isim}, Toplam Soru: {sinav.Sorular.Count}, Gönderilen Cevap Sayısı: {cevaplar.Cevaplar.Count}");
            
            // Soru listesini indeks bazlı düzenle (1'den başlayarak)
            var sorular = sinav.Sorular.Select((s, index) => new { Soru = s, SiraNo = index + 1 }).ToList();
            
            foreach (var cevap in cevaplar.Cevaplar)
            {
                // SoruNumarasi kullanarak soru indeksini bul (daha güvenilir)
                var siraNo = cevap.SoruNumarasi > 0 ? cevap.SoruNumarasi : -1;
                var soruItem = sorular.FirstOrDefault(s => s.SiraNo == siraNo);
                
                if (soruItem != null)
                {
                    var soru = soruItem.Soru;
                    // Boşlukları kaldır ve büyük/küçük harf duyarlılığını kapat
                    string dogruCevapNormalized = (soru.DogruCevap ?? "").Trim().ToLowerInvariant();
                    string verilenCevapNormalized = (cevap.VerilenCevap ?? "").Trim().ToLowerInvariant();
                    
                    Console.WriteLine($"Cevap Kontrol: Sıra No: {siraNo}, SoruId: {soru.SoruId}, Beklenen: '{dogruCevapNormalized}', Verilen: '{verilenCevapNormalized}'");
                    
                    if (dogruCevapNormalized == verilenCevapNormalized)
                    {
                        dogruSayisi++;
                        Console.WriteLine($"DOĞRU - Soru #{siraNo}, ID: {soru.SoruId}, Metin: {soru.Metin}, Beklenen: '{dogruCevapNormalized}', Verilen: '{verilenCevapNormalized}'");
                    }
                    else
                    {
                        yanlisSayisi++;
                        Console.WriteLine($"YANLIŞ - Soru #{siraNo}, ID: {soru.SoruId}, Metin: {soru.Metin}, Beklenen: '{dogruCevapNormalized}', Verilen: '{verilenCevapNormalized}'");
                    }
                }
                else
                {
                    Console.WriteLine($"HATA - Soru bulunamadı. SoruID: {cevap.SoruId}, Sıra No: {siraNo}");
                    yanlisSayisi++;
                }
            }

            // Sonuç hesapla
            var sonuc = new Sonuc
            {
                Ogrenci = ogrenci,
                OgrenciId = ogrenci.Id,
                SinavId = sinavId,
                DogruSayisi = dogruSayisi,
                YanlisSayisi = yanlisSayisi,
                Puan = sinav.Sorular.Count > 0 ? (double)dogruSayisi * 100 / sinav.Sorular.Count : 0
            };

            // Öğrenciye sınavı ekle
            if (!ogrenci.KatildigiSinavlar.Contains(sinavId))
            {
                ogrenci.KatildigiSinavlar.Add(sinavId);
                _unitOfWork.Ogrenciler.Update(ogrenci);
            }

            // Sonucu veritabanına kaydet
            _unitOfWork.Sonuclar.Add(sonuc);
            _unitOfWork.Complete();

            // Log ekleyelim
            Console.WriteLine($"SinavOl Tamamlandı: ID={sinavId}, Öğrenci={ogrenci.Isim}, Doğru={dogruSayisi}, Yanlış={yanlisSayisi}, Puan={sonuc.Puan}");

            return sonuc;
        }

        public List<Sinav> GetSinavlar()
        {
            return _unitOfWork.Sinavlar.GetAll().ToList();
        }

        public List<Sonuc> GetSonuclar()
        {
            return _unitOfWork.Sonuclar.GetAll().ToList();
        }
    }
} 