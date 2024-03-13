namespace Entities.Exceptions
{
    public class RefreshTokenBadRequestException : BadRequestException //  BadRequestException üzerinden kalıtıyoruz
    {
        public RefreshTokenBadRequestException()
            // RefreshToken ya da AccessToken geçersiz ise bu mesajla bildireceğiz
            : base($"Invalid client request. The tokenDto has some invalid values.")
        {
        }
    }
}