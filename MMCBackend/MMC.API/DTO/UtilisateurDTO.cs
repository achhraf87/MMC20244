using System.Text.Json.Serialization;

namespace MMC.API.DTO
{
    public class UtilisateurDTO
    {
        public int Id { get; set; } 
        public string? Fullname { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public bool? IsSpeaker { get; set; }
        public string? DateCreate { get; set; }
        public string? DateUpdate { get; set; }
        public string Role { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string Gender { get; set; }
        public string? ImageUrl { get; set; }
        public string MVP { get; set; }
        public string MCT { get; set; }
        public string? Description { get; set; }
        public string? Facebook { get; set; }
        public string? Instagram { get; set; }
        public string? LinkedIn { get; set; }

        //[JsonIgnore]
        //public IFormFile? Image { get; set; }

    }
}
