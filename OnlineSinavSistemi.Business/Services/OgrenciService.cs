using System;
using System.Collections.Generic;
using System.Linq;
using OnlineSinavSistemi.Core.Interfaces;
using OnlineSinavSistemi.Core.Models;

namespace OnlineSinavSistemi.Business.Services
{
    /// <summary>
    /// Öğrenci servis implementasyonu
    /// </summary>
    public class OgrenciService : IOgrenciService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OgrenciService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Ogrenci> GetAllOgrenciler()
        {
            return _unitOfWork.Ogrenciler.GetAll().ToList();
        }

        public Ogrenci GetOgrenciById(int ogrenciId)
        {
            return _unitOfWork.Ogrenciler.GetById(ogrenciId);
        }

        public Ogrenci GetOgrenciByEmail(string email)
        {
            return _unitOfWork.Ogrenciler.GetByEmail(email);
        }

        public void CreateOgrenci(Ogrenci ogrenci)
        {
            if (ogrenci == null)
                throw new ArgumentNullException(nameof(ogrenci));

            _unitOfWork.Ogrenciler.Add(ogrenci);
            _unitOfWork.Complete();
        }

        public void UpdateOgrenci(Ogrenci ogrenci)
        {
            if (ogrenci == null)
                throw new ArgumentNullException(nameof(ogrenci));

            _unitOfWork.Ogrenciler.Update(ogrenci);
            _unitOfWork.Complete();
        }

        public void DeleteOgrenci(int ogrenciId)
        {
            var ogrenci = _unitOfWork.Ogrenciler.GetById(ogrenciId);
            if (ogrenci != null)
            {
                _unitOfWork.Ogrenciler.Remove(ogrenci);
                _unitOfWork.Complete();
            }
        }

        public List<Ogrenci> GetOgrencilerBySinavId(int sinavId)
        {
            return _unitOfWork.Ogrenciler.GetOgrencilerBySinavId(sinavId).ToList();
        }
    }
} 