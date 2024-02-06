using MMC.API.BaseRepository;
using MMC.API.DTO;
using MMC.API.Models;
using MMC.API.Services;

namespace MMC.API.Repository
{
    public interface IUtilisateurRepo : IBaseRepository<Utilisateur>
    {
        //string GenerateToken(Utilisateur token);
        Task<Utilisateur> login(DtoLogin user);
        Task<Utilisateur> connexion(string username, string password);
        Task<DtoDataTable<UtilisateurDTO>> GetAllDtoAsync(DtoFiltreUtilisateur filtreUser, DtoPagination dtoPagination);
    }
}
