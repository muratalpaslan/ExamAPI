using System;
using System.Collections.Generic;
using System.Linq;
using OnlineSinavSistemi.Core.Interfaces;
using OnlineSinavSistemi.Core.Models;

namespace OnlineSinavSistemi.Business.Services
{
    /// <summary>
    /// Sınav servis implementasyonu
    /// </summary>
    public class SinavService : ISinavService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SinavService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Sinav> GetAllSinavlar()
        {
            return _unitOfWork.Sinavlar.GetAll().ToList();
        }

        public Sinav GetSinavById(int sinavId)
        {
            return _unitOfWork.Sinavlar.GetById(sinavId);
        }

        public void CreateSinav(Sinav sinav)
        {
            if (sinav == null)
                throw new ArgumentNullException(nameof(sinav));

            _unitOfWork.Sinavlar.Add(sinav);
            _unitOfWork.Complete();
        }

        public void UpdateSinav(Sinav sinav)
        {
            if (sinav == null)
                throw new ArgumentNullException(nameof(sinav));

            _unitOfWork.Sinavlar.Update(sinav);
            _unitOfWork.Complete();
        }

        public void DeleteSinav(int sinavId)
        {
            var sinav = _unitOfWork.Sinavlar.GetById(sinavId);
            if (sinav != null)
            {
                _unitOfWork.Sinavlar.Remove(sinav);
                _unitOfWork.Complete();
            }
        }
    }
} 