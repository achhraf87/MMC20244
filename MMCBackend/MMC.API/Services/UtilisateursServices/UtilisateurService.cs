using MMC.API.Data;
using MMC.API.DTO;
using MMC.API.Models;
using MMC.API.Repository;

namespace MMC.API.Services.UtilisateursServices
{
    public class UtilisateurService : BaseServices<Utilisateur>, IUtilisateurService
    {
        private readonly IUtilisateurRepo _repo;
        public UtilisateurService(IUtilisateurRepo repo) : base(repo)
        {
            _repo = repo;
        }

        public async Task<DtoDataTable<UtilisateurDTO>> GetAllDtoAsync(DtoFiltreUtilisateur filtreUser, DtoPagination dtoPagination)
        {
            return await _repo.GetAllDtoAsync(filtreUser, dtoPagination);
        }

        public async Task<Utilisateur> LoginAsync(DtoLogin request)
        {
            return await _repo.login(request);
        }
    }
}
