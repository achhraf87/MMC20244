using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.API.Models
{
    public class Utilisateur
    {
        public int Id { get; set; }
        public string? Fullname { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public bool IsSpeaker { get; set; }
        public string? Statut { get; set; }
        public string? Token { get; set; }
        public DateTime? DateCreate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public string? ImageUrl { get; set; }
        public string Role { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Gender { get; set; }
        public string? MVP { get; set; }
        public string? MCT { get; set; }
        public string? Description { get; set; }
        public string? Facebook { get; set; }
        public string? Instagram { get; set; }
        public string? LinkedIn { get; set; }


    }
}
