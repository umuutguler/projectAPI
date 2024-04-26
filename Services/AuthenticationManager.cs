using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
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

        public Task<TokenDto> CreateToken(bool populateExp)
        {
            throw new NotImplementedException();
        }

        public Task<TokenDto> RefreshToken(TokenDto tokenDto)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateUser(UserForAuthenticationDto userForAuthDto)
        {
            throw new NotImplementedException();
        }
        /*
public async Task<TokenDto> CreateToken(bool populateExp) // Token üretme
                                                         // Öncelikle kimlik bilgilerini alacağız, sonra rolleri alacağız, Token oluşturma seçeneklerini dikkate alacağız, sonra da oluşturduğumuz token'ı döneceğiz
{
   var signinCredentials = GetSiginCredentials(); // Kimlik bilgilerini aldık
   var claims = await GetClaims(); // Claimsleri aldık
   var tokenOptions = GenerateTokenOptions(signinCredentials, claims); // Token oluşturma optionslarını generate ettik
                                                                       // Kimlik bilgileri ve claims bilgileri ile beraber tokenOptions u oluşturup bu ifadeyi ilgili tokenın oluşması için parametre olarak verdik

   var refreshToken = GenerateRefreshToken(); // refreshToken tanımı
   _user.RefreshToken = refreshToken; //_user'ın RefreshToken alanı var, Bu alana oluşturduğum refreshToken'ı atıyoruz

   if (populateExp)
       _user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7); // Expiry süresini 7 gün daha uzatmış olduk

   await _userManager.UpdateAsync(_user); // Update işleminin asenkron oluşmasını sağlıyoruz

   var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
   return new TokenDto()
   {
       AccessToken = accessToken,
       RefreshToken = refreshToken
   };
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

public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
{
   var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken); // GetPrincipalFromExpiredToken ifadesiyle Kullanıcı bilgilerini alacağız
   var user = await _userManager.FindByNameAsync(principal.Identity.Name); // veritabanına kullanıcının hala varlığını sorguluyoruz.

   if (user is null ||   // user yoksa
       user.RefreshToken != tokenDto.RefreshToken || // user'ın RefreshToken'ı tokenDto dan gelen RefreshToken'a eşit değilse
       user.RefreshTokenExpiryTime <= DateTime.Now)  // Expiry(Son kullanma) tarihi şimdiki zamandan az ise yani tokenın son kullanma tarihi geçmiş ise
       throw new RefreshTokenBadRequestException();

   _user = user; // veritabanında kullanıcı varsa class içerisinde kullandığımız _user'ı user a atayabiliriz
   return await CreateToken(populateExp: false);
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
       new Claim(ClaimTypes.Name, _user.Id) // Listeye username bilgisini ekliyoruz
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

//------------------------------------------

private string GenerateRefreshToken()
{
   var randomNumber = new byte[32]; // randomNumber'ı byte dizisi olarak tanımladık.
                                    // Masraflı işler yaaptığımız zaman using ifadesi kullanırız. blok bittiği zaman bu blok içerisinde kullanılan kaynaklar serbest bırakılır.
   using (var rng = RandomNumberGenerator.Create())
   {
       rng.GetBytes(randomNumber);
       return Convert.ToBase64String(randomNumber);// ilgili ifadeyi string formatında dönüyoruz.
   }
}

// Süresi gemiş olan Tokendan Kullanıcı bilgilerini alacak bir metod
// ClaimsPrincipal ifadesi ile Principal olan yerlerde kullanıcı ilgilerine ihtiyaç olduğunu bilelim.
private ClaimsPrincipal GetPrincipalFromExpiredToken(string token) // tokenı aldık
{
   var jwtSettings = _configuration.GetSection("JwtSettings"); // Ayarları alıyoruz appsettingten
   var secretKey = jwtSettings["secretKey"]; // anahtar bilgilerini alıyoruz

   //Token2ı doğrulamak gerekiyor. Bu tokenı bizim sunucu mu üretti yani issuer kısmı bizi mi işaret ediyor bunu anlamak lazım
   var tokenValidationParameters = new TokenValidationParameters
   {
       ValidateIssuer = true,
       ValidateAudience = true,
       ValidateLifetime = true,
       ValidateIssuerSigningKey = true,
       ValidIssuer = jwtSettings["validIssuer"],
       ValidAudience = jwtSettings["validAudience"],
       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
   }; // Tokenı doğrulama parametreleri oluşturduk 

   var tokenHandler = new JwtSecurityTokenHandler();
   SecurityToken securityToken;

   var principal = tokenHandler.ValidateToken(token, tokenValidationParameters,
       out securityToken); // out-> security token a değer ataması
                           // Tokenı dğruladık primcipal üzerinden kullanıcı bilgilerini almış olduk

   var jwtSecurityToken = securityToken as JwtSecurityToken; // securityToken'a JwtSecurityToken gibi davran
   if (jwtSecurityToken is null ||
       !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, // Headerdaki güvenlik algoritması HmacSha256 değil ise
       StringComparison.InvariantCultureIgnoreCase))
   {
       throw new SecurityTokenException("Invalid token.");
   }
   return principal;
}
*/
    }
}
