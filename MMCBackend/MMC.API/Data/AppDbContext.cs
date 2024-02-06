using Microsoft.EntityFrameworkCore;
using MMC.API.DTO;
using MMC.API.Models;

namespace MMC.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
        {
            
        }
        public DbSet<Demande> Demandes { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }


    }
}
