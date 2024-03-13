using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Services.Contracts;

namespace Services
{
    public class AuthenticationManager : IAuthenticationService
    {
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager; // Bu framework tarafından tanımlanıyor.
        private readonly IConfiguration _configuration;
        // Kullanıcı kaydı yaparken UserManager kullanacağız.
        // UserForRegistrationDto'da User'a dönerken mapper kullanağız
        // Konfigürasyon gerektiğinde _configuration kullanacağız

        public AuthenticationManager(ILoggerService logger, IMapper mapper,
            UserManager<User> userManager, IConfiguration configuration)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto)
        {
            // Gelen Dto'da bir User nesnesi elde etmek içim
            var user = _mapper.Map<User>(userForRegistrationDto);

            var result = await _userManager
                .CreateAsync(user, userForRegistrationDto.Password); // User bilgisi yanında password de vermek gerekiyor
            // Yukarıda işlem sonucunda elde ettiğimiz ifade IdentityResult ifadesidir

            if (result.Succeeded) // İşlem başarılıyda
                // User'a hangi rolleri ekliyorsak userForRegistrationDto üzerindeki roles alanı üzerinden ilgili ifadeleri alacağız
                await _userManager.AddToRolesAsync(user, userForRegistrationDto.Roles);
            return result;
        }
    }
}
