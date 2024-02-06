using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.API.Models
{
    public class Demande
    {
        public int Id { get; set; }
        public int UtilisateurId { get; set; }

        [ForeignKey(nameof(UtilisateurId))]
        public Utilisateur? Utilisateur { get; set; }
        public string? Description { get; set; }
        public string? Statut { get; set; }
    }
}
