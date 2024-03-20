using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore;
using Services.Contracts;

namespace Services
{
    public class UserManager : IUserService
    {
        private readonly IRepositoryManager _manager;
        private readonly RepositoryContext _context;
        private readonly IReservationInfoService _reservationInfoService;
        public UserManager(IRepositoryManager manager, IReservationInfoService reservationInfoService, RepositoryContext context) //, IServiceManager service)
        {
            _manager = manager;
            _reservationInfoService = reservationInfoService;
                //_service = service;
        }

        public async Task DeleteOneUserAsync(string id, bool trackChanges=false)
        {
            var user = await _manager.User.GetOneUserByIdAsync(id, false, true); // Rezervasyonları da al

            if (user is null)
                throw new ArgumentException(nameof(user));

            foreach (var reservation in user.ReservationInfos)
            {
                _manager.ReservationInfo.DeleteOneReservationInfo(reservation);
            }

            // Tek tek rezervasyon silmeye gerek yok
            _manager.User.DeleteOneUser(user);
            await _manager.SaveAsync();

        }

        public async Task<IEnumerable<User>> GetAllUsersAsync(bool trackChanges) =>
            await _manager.User.GetAllUsersAsync(false, true);

        public async Task<User> GetOneUserByIdAsync(string id, bool trackChanges) =>
            await _manager.User.GetOneUserByIdAsync(id, false, true);

        public async Task UpdateOneUserAsync(string id, User user, bool trackChanges)
        {
            var entity = await _manager.User.GetOneUserByIdAsync(id, false, true);
            if (entity is null)
                throw new Exception($"User with id:{id} could not found.");

            entity.FirstName = user.FirstName ?? entity.FirstName;
            entity.LastName = user.LastName ?? entity.LastName;
            entity.Email = user.Email ?? entity.Email;
            entity.UserName = user.UserName ?? entity.UserName;
            entity.PasswordHash = user.PasswordHash ?? entity.PasswordHash;
            entity.PhoneNumber = user.PhoneNumber ?? entity.PhoneNumber;


            _manager.User.Update(entity);
            await _manager.SaveAsync();
        }
    }
}
