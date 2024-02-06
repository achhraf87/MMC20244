using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MMC.API.BaseRepository;
using MMC.API.Data;
using MMC.API.DTO;
using MMC.API.JWT;
using MMC.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MMC.API.Repository
{
    public class UtilisateurRepo : BaseRepository<Utilisateur>, IUtilisateurRepo
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _config;
        public UtilisateurRepo(AppDbContext appDbContext,IConfiguration configuration) : base(appDbContext) 
        { 
            _appDbContext = appDbContext; 
            _config = configuration;
        }

        public async Task<Utilisateur> login(DtoLogin userDTO)
        {
            if (userDTO is null || string.IsNullOrWhiteSpace(userDTO.Email) || string.IsNullOrWhiteSpace(userDTO.Password))
                
            return null;

            var user = await GetBylogin(userDTO.Email);

            if (user is null || !await VerifyPassword(userDTO.Password, user))

            return null;

            user.Token = JWTtoken.GenerateToken(user);

            return user;
        }

        private async Task<bool> VerifyPassword(string pass, Utilisateur check)
        {
            return check.Password.Equals(pass);
        }

        private async Task<Utilisateur> GetBylogin(string login)
        {
            return await _appDbContext.Utilisateurs
                .FirstOrDefaultAsync(x => x.Email == login.Trim() || x.Username == login.Trim());
        }

        private UtilisateurDTO mapSpeakerToDto(Utilisateur utilisateur)
        {
            return new UtilisateurDTO
            {
                Id = utilisateur.Id,
                Fullname = !string.IsNullOrEmpty(utilisateur.Fullname) ? utilisateur.Fullname : "N/D",
                Username = !string.IsNullOrEmpty(utilisateur.Username) ? utilisateur.Username : "N/D",
                IsSpeaker = utilisateur.IsSpeaker,
                DateCreate = utilisateur.DateCreate.HasValue ? utilisateur.DateCreate.Value.ToString("dd/MM/yyyy") : "N/D",
                DateUpdate = utilisateur.DateUpdate.HasValue ? utilisateur.DateUpdate.Value.ToString("dd/MM/yyyy") : "N/D",
                Role = utilisateur.Role = "User",
                Email = !string.IsNullOrEmpty(utilisateur.Email) ? utilisateur.Email : "N/D",
                Phone = !string.IsNullOrEmpty(utilisateur.Phone) ? utilisateur.Phone : "N/D",
                Gender = utilisateur.Gender,
                MVP = utilisateur.MVP,
                MCT = utilisateur.MCT ,
                Description = !string.IsNullOrEmpty(utilisateur.Description) ? utilisateur.Description : "N/D",
                Facebook = !string.IsNullOrEmpty(utilisateur.Facebook) ? utilisateur.Facebook : "N/D",
                Instagram = !string.IsNullOrEmpty(utilisateur.Instagram) ? utilisateur.Instagram : "N/D",
                LinkedIn = !string.IsNullOrEmpty(utilisateur.LinkedIn) ? utilisateur.LinkedIn : "N/D"
                
            };
        }

        public async Task<DtoDataTable<UtilisateurDTO>> GetAllDtoAsync(DtoFiltreUtilisateur filtreUser, DtoPagination pagination)
        {
            int pageNumber = pagination.pageNumber ?? 1;
            int pageSize = pagination.pageSize ?? 5; // Set a default page size

            var query = _appDbContext.Utilisateurs
                .Where(x => x.IsSpeaker != false);

            var response = new DtoDataTable<UtilisateurDTO>
            {
                totalRecord = query.Count(),
            };

            //Apply Filtre
            if (filtreUser is not null)
            {
                if (!string.IsNullOrEmpty(filtreUser.Statut)) { query = query.Where(x => x.IsSpeaker == (filtreUser.Statut == "Approuvée" ? true : false)); };
                if (!string.IsNullOrEmpty(filtreUser.Username)) { query = query.Where(x => x.Username == filtreUser.Username); };
                if (!string.IsNullOrEmpty(filtreUser.NomComplete)) { query = query.Where(x => (x.Fullname).Contains(filtreUser.NomComplete)); };
            }


            // Apply sorting
            if (!string.IsNullOrEmpty(pagination.sortOrder))
            {
                switch (pagination.filtreColumn.ToLower())
                {
                    case "Fullname":
                        query = pagination.sortOrder == "asc" ? query.OrderBy(x => x.Fullname) : query.OrderByDescending(x => x.Fullname);
                        break;
                    //case "prenom":
                    //    query = pagination.sortOrder == "asc" ? query.OrderBy(x => x.Prenom) : query.OrderByDescending(x => x.Prenom);
                    //    break;
                    case "email":
                        query = pagination.sortOrder == "asc" ? query.OrderBy(x => x.Username) : query.OrderByDescending(x => x.Username);
                        break;
                    //case "login":
                    //    query = pagination.sortOrder == "asc" ? query.OrderBy(x => x.Login) : query.OrderByDescending(x => x.Login);
                    //    break;
                    //case "Telephone":
                    //    query = pagination.sortOrder == "asc" ? query.OrderBy(x => x.Telephone) : query.OrderByDescending(x => x.Telephone);
                    //    break;
                    default:
                        query = query.OrderByDescending(x => x.DateCreate);
                        break;
                }
            }
            else
            {
                // Default sorting
                query = query.OrderByDescending(x => x.DateCreate);
            }

            response.totalFiltred = query.Count();

            var users = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            response.Data = users.Select(x => mapSpeakerToDto(x)).ToList();

            return response;
        }

        public void connexion(string username, string password)
        {
           _appDbContext.Utilisateurs.FirstOrDefault(x=> x.Username == username && x.Password == password);
        }

         async Task<Utilisateur> IUtilisateurRepo.connexion(string username, string password)
         {
            var cnx = await _appDbContext.Utilisateurs.FirstOrDefaultAsync(x=>x.Username ==  username && x.Password == password);
            return cnx;
         }

        
    }
}
