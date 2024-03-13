using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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

        private User? _user;

        public AuthenticationManager(ILoggerService logger, IMapper mapper,
            UserManager<User> userManager, IConfiguration configuration)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> CreateToken()
        {
            var signinCredentials = GetSiginCredentials(); // Kimlik bilgilerini aldık
            var claims = await GetClaims(); // Claimsleri aldık
            var tokenOptions = GenerateTokenOptions(signinCredentials, claims); // Token oluşturma optionslarını generate ettik
                                                                                // Kimlik bilgileri ve claims bilgileri ile beraber tokenOptions u oluşturup bu ifadeyi ilgili tokenın oluşması için parametre olarak verdik
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
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
        public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuthDto)
        {
            _user = await _userManager.FindByNameAsync(userForAuthDto.UserName); // kullanıcı adı üzerinden ilgii ifadeyi çözümleyeceğiz
            var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuthDto.Password)); // Sonuç olacak ve result true ya da false şeklinde olacak. user null değilse ve kullanıcı adına göre şifre doğruysa
            if (!result)
            {
                _logger.LogWarning($"{nameof(ValidateUser)} : Authentication failed. Wrong username or password.");
            }
            return result; // True/False
        }

        //------------------------------------------

        private SigningCredentials GetSiginCredentials() // Kimlik bilgilerini almak için
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["secretKey"]);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256); // SecurityAlgorithms algoritmasını kullanıyoruz. Duruma göre farklı algoritmalar tercih edilebilir. 
        }

        private async Task<List<Claim>> GetClaims() // Claim nesnelerini içeren liste döneceğiz
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, _user.UserName) // Listeye username bilgisini ekliyoruz
            };

            var roles = await _userManager // rolleri _userManager kullanarak alarak listeye atıyoruz,
                .GetRolesAsync(_user); // Liste interfaceinde ve string türünde dönüş yapıyor.  

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role)); // rolleri claims ifadesine çevirip, listeye claimleri ekliyoruz.
            }
            return claims; // Oluşturduğumuz listeyi dönüyoruz
        }

        // Token optionslarını içeren bir metod
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signinCredentials, // JwtSecurityToken ifadesini döneceğiz
            List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var tokenOptions = new JwtSecurityToken( // bir token oluşturuyoruz
                    issuer: jwtSettings["validIssuer"], // bsstoreapi
                    audience: jwtSettings["validAudience"], // localhost 3000
                    claims: claims,
                    // appsettingte expires 60 değeri vardı. dakika mı saat mi belirteceğiz
                    // Now ile şimdiki zamanı al, ve expires değeri addminutes ile şimdiki zamana ekle
                    // Tokenın geçerlilik süresini bul
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
                    signingCredentials: signinCredentials);

            return tokenOptions; // Oluşan JwtSecurityToken'ı dönüyoruz
        }
    }
}
