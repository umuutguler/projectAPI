using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;

namespace Entities.Models
{
    public class User : IdentityUser
    {
        public ObjectId Id {  get; set; }
        public String? FirstName { get; set; }
        public String? LastName { get; set; }

        // Refresh Token
        public String? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        // Ref: navigation property
        public int DepartmentId { get; set; }
        public ICollection<ReservationInfo> ReservationInfos { get; set; }
    }
}