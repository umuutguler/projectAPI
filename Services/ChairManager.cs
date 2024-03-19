﻿using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ChairManager : IChairService
    {
        private readonly IRepositoryManager _manager;
        public ChairManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public async Task<Chair> CreateOneReservationInfoAsync(Chair chair)
        {
            if (chair is null)
                throw new ArgumentException(nameof(chair));


            _manager.Chair.CreateOneChair(chair);
            await _manager.SaveAsync();

            return chair;
        }

        public async Task<IEnumerable<Chair>> GetAllChairsAsync(bool trackChanges)
        {
            return await _manager
                .Chair
                .GetAllChairsAsync(trackChanges , includeRelated: true);
        }

        // Empty Chairs
        public async Task<IEnumerable<Chair>> GetAllEmptyChairsAsync(bool trackChanges)
        {
            var chairs = await _manager
                .Chair
                .GetAllChairsAsync(trackChanges, includeRelated: true);

            return chairs.Where(c => c.Status == false);
        }

        public async Task<Chair> GetOneChairByIdAsync(int id, bool trackChanges)
        {
            var chair = await _manager
                .Chair
                .GetOneChairByIdAsync(id, trackChanges, includeRelated: true);
            if (chair is null)
                throw new ChairNotFoundException(id);


            return chair;
        }

        public async Task<Chair> UpdateChairByIdAsync(int id, Chair updatedChair, bool trackChanges)
        {
            var chair = await _manager
                .Chair
                .GetOneChairByIdAsync(id, trackChanges, includeRelated: false); // Güncellenmiş sandalyeyi almak için includeRelated: false kullanıyoruz
            if (chair == null)
                throw new ChairNotFoundException(id);

            chair.Status = updatedChair.Status; // Güncelleme işlemleri, updatedChair içindeki özelliklere göre yapılmalıdır
            chair.TableId = updatedChair.TableId; 

            _manager.Chair.UpdateOneChair(chair); // Güncelleme işlemi
            await _manager.SaveAsync(); // Değişiklikleri kaydet

            return chair;
        }
    }
}
