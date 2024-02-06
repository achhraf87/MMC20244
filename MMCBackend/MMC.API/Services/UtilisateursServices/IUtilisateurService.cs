using MMC.API.DTO;
using MMC.API.Models;

namespace MMC.API.Services.UtilisateursServices
{
    public interface IUtilisateurService : IBaseServices<Utilisateur>
    {
        Task<Utilisateur> LoginAsync(DtoLogin request);
        Task<DtoDataTable<UtilisateurDTO>> GetAllDtoAsync(DtoFiltreUtilisateur filtreUser, DtoPagination dtoPagination);
    }
}
