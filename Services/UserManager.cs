using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class UserManager : IUserService
    {
        private readonly IRepositoryManager _manager;
        //private readonly IServiceManager _service;
        private readonly IReservationInfoService _reservationInfoService;
        public UserManager(IRepositoryManager manager, IReservationInfoService reservationInfoService) //, IServiceManager service)
        {
            _manager = manager;
            _reservationInfoService = reservationInfoService;
                //_service = service;
        }

        public async Task DeleteOneUserAsync(string id, bool trackChanges)
        {
            var user = await _manager.User.GetOneUserByIdAsync(id, false, true);
            if (user is null)
                throw new ArgumentException(nameof(user));



            var userReservations = await _reservationInfoService.GetAllReservationInfosByUserId(false, user.Id);
            foreach (var reservation in userReservations)
            {
                await _reservationInfoService.DeleteOneReservationInfoAsync(reservation.Id, false);
            }


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
