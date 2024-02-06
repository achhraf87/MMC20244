using MMC.API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.API.DTO
{
    public class DemandeDTO
    {
        public int Id { get; set; }
        public int UtilisateurId { get; set; }

        [ForeignKey(nameof(UtilisateurId))]
        public UtilisateurDTO? Utilisateur { get; set; }
        public string? Description { get; set; }
        public string? Statut { get; set; }
    }
}
