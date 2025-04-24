using System;
using System.Collections.Generic;
using System.Linq;
using OnlineSinavSistemi.Models;
using OnlineSinavSistemi.Interfaces;
using OnlineSinavSistemi.Data;

namespace OnlineSinavSistemi.Services
{
    public class TestEngine : ITestEngine
    {
        // Sınav verilerini tutan in-memory listeler
        private readonly List<Sinav> _sinavlar;
        private readonly List<Ogrenci> _ogrenciler;
        private readonly List<Sonuc> _sonuclar;

        public TestEngine()
        {
            _sinavlar = OrnekVeri.Sinavlar;
            _ogrenciler = OrnekVeri.Ogrenciler;
            _sonuclar = new List<Sonuc>();
        }

       
        public Sonuc SinavOl(int sinavId, SinavCevap cevaplar)
        {
            // Sınav ve öğrenci kontrolü
            var sinav = _sinavlar.FirstOrDefault(s => s.SinavId == sinavId);
            var ogrenci = _ogrenciler.FirstOrDefault(o => o.OgrenciId == cevaplar.OgrenciId);

            if (sinav == null || ogrenci == null)
                throw new ArgumentException("Sınav veya öğrenci bulunamadı.");

            // Cevapları değerlendir
            int dogruSayisi = 0;
            int yanlisSayisi = 0;

            foreach (var cevap in cevaplar.Cevaplar)
            {
                var soru = sinav.Sorular.FirstOrDefault(s => s.SoruId == cevap.SoruId);
                if (soru != null)
                {
                    if (soru.DogruCevap == cevap.VerilenCevap)
                        dogruSayisi++;
                    else
                        yanlisSayisi++;
                }
            }

            // Sonuç hesapla
            var sonuc = new Sonuc
            {
                Ogrenci = ogrenci,
                DogruSayisi = dogruSayisi,
                YanlisSayisi = yanlisSayisi,
                Puan = (double)dogruSayisi * 100 / sinav.Sorular.Count
            };

            _sonuclar.Add(sonuc);
            return sonuc;
        }

 
        public List<Sinav> GetSinavlar() => _sinavlar;
        public List<Sonuc> GetSonuclar() => _sonuclar;
    }
} 