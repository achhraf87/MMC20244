using AutoMapper;
using MMC.API.DTO;
using MMC.API.Models;
using System.Net;

namespace MMC.API.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //CreateMap<Utilisateur, UtilisateurDTO>().ReverseMap();
        }
    }
}
