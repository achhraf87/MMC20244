using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMC.API.Data;
using MMC.API.DTO;
using MMC.API.JWT;
using MMC.API.Models;
using MMC.API.Repository;
using MMC.API.Services.UtilisateursServices;
using System;
using System.Security.Claims;

namespace MMC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrationController : ControllerBase
    {
        private readonly AppDbContext _Context;
        private readonly IUtilisateurService _auth;
        public AdministrationController(AppDbContext dbContext, IUtilisateurService auth)
        {    
            _Context = dbContext;
            _auth = auth;
        }

        [HttpPost("login")]
        public async Task<Utilisateur> login([FromBody] DtoLogin adminDTO) => await _auth.LoginAsync(adminDTO);


        [HttpPost("Inscription")]
        public async Task<IActionResult> InscriptionUser([FromBody] Utilisateur etudiantObj)
        {
            if (etudiantObj == null)
            {
                return BadRequest("Veuillez fournir les informations de user");
            }

            try
            {
                if (await CheckEmail(etudiantObj.Email))
                {
                    return BadRequest(new
                    {
                        Message = "Ce mail existe déja"
                    });
                }
                await _Context.Utilisateurs.AddAsync(etudiantObj);
                await _Context.SaveChangesAsync();
                return Ok(new { Message = "user été enregistré avec succès" });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Une erreur s'est produite lors de l'enregistrement de user : {ex.Message}");
            }
        }

        private Task<bool> CheckEmail(string email)
        {
            var check = _Context.Utilisateurs.AnyAsync(x => x.Email == email);
            return check;
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUtulisateur([FromBody]UtilisateurDTO utilisateurDTO)
        {
            var user = new Utilisateur
            {
                Username = utilisateurDTO.Username,
                Password = utilisateurDTO.Password,
                Fullname = utilisateurDTO.Fullname,
                IsSpeaker = false,
                Role = "User",
                Statut = "En attente",
                DateCreate = DateTime.Now,
                DateUpdate = DateTime.Now,
                ImageUrl = utilisateurDTO.ImageUrl,
                Email = utilisateurDTO?.Email,
                Phone = utilisateurDTO?.Phone,
                Gender = utilisateurDTO.Gender,
                MVP= utilisateurDTO.MVP,
                MCT = utilisateurDTO.MCT

            };
            await _Context.Utilisateurs.AddAsync(user);
            await _Context.SaveChangesAsync();
            return Ok(user);
        }

        [Authorize]
        [HttpGet("info")]
        public IActionResult GetUserInfo()
        {
           
            var userID = GetUserID();
            var user = _Context.Utilisateurs.FirstOrDefault(e => e.Id == userID);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [Authorize]
        [HttpGet("GetInfoUsers")]
        public async Task<IActionResult> getinfoUtulisateur()
        {
            var user = GetUserID();
            var userr = await _Context.Utilisateurs.Where(e=>e.Id == user).ToListAsync();
            if(user == null)
            {
                return NotFound();
            }
            return Ok(userr);

        }

        private int GetUserID()
        {
            var id = HttpContext.User.Identity as ClaimsIdentity;
            var etud = id.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (int.TryParse(etud, out int etudId))
            {
                return etudId;
            }
            throw new FormatException("Invalid etudiant ID.");
        }


        


        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _Context.Utilisateurs.SingleOrDefaultAsync(u => u.Id == id);
            if(user is not null)
            _Context.Utilisateurs.Remove(user);
            await _Context.SaveChangesAsync();
            return Ok(user);
        }

        

        
        [HttpPost("AddDemande")]
        public async Task<IActionResult> AddDemande(DemandeDTO demandeDTO)
        {
            var demande = new Demande
            {
                UtilisateurId = demandeDTO.UtilisateurId,
                Description = demandeDTO.Description,
                Statut = "En attente"
            };
             await _Context.Demandes.AddAsync(demande);
            await _Context.SaveChangesAsync();
            return Ok(demande);
        }

       

        [HttpGet("testLOGIN"),Authorize(Roles ="User")]
        public async Task<IActionResult> List()
        {
            DtoLogin dto = new DtoLogin()
            {
                Email = "achraf",
                Password = "azer"
            };
            return Ok(dto);
        }

        [HttpGet("getToutesLesDemandes")/*,Authorize(Roles ="Admin")*/]
        public async Task<ActionResult<IEnumerable<UtilisateurDTO>>> getDemande()
        {
            var demandes = await _Context.Utilisateurs
                .Where(d => d.Statut == "En attente")
                .ToListAsync();
            return Ok(demandes);
        }


        [HttpGet("getToutesLesDemandesS")]
        public async Task<ActionResult<PagedResponseDTO<UtilisateurDTO>>> GetDemandes([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var totalDemandes = await _Context.Utilisateurs
                .Where(d => d.Statut == "En attente")
                .CountAsync();

            var demandes = await _Context.Utilisateurs
                .Where(d => d.Statut == "En attente")
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalPages = (int)Math.Ceiling(totalDemandes / (double)pageSize);

            var response = new PagedResponseDTO<UtilisateurDTO>(demandes, page, pageSize, totalDemandes, totalPages);

            return Ok(response);
        }





        //[HttpGet("getToutesLesDemandes")/*,Authorize(Roles ="Admin")*/]
        //public async Task<ActionResult<IEnumerable<DemandeDTO>>> getDemande()
        //{
        //    var demandes = await _Context.Demandes
        //        .Where(d => d.Statut == "En attente")
        //        .Include(u => u.Utilisateur)
        //        .ToListAsync();

        //    var demandeDTOs =  demandes.Select(d => new DemandeDTO
        //    {
        //        Id = d.Id,
        //        UtilisateurId = d.UtilisateurId,
        //        Utilisateur = new UtilisateurDTO
        //        {
        //            Id = d.Utilisateur.Id,
        //            Username = d.Utilisateur.Username,
        //            // Ajoutez d'autres propriétés d'utilisateur si nécessaire
        //        },
        //        Description = d.Description,
        //        Statut = d.Statut
        //    }).ToList();

        //    return Ok(demandeDTOs);
        //}


        [HttpPost("approuver-demande/{demandeId}")]
        public async Task<IActionResult> ApprouverDemande(int demandeId)
        {
            var demande = await _Context.Utilisateurs.FindAsync(demandeId);

            if (demande == null)
            {
                return NotFound();
            }

            demande.Statut = "Approuvée";
            demande.IsSpeaker = true;

            try
            {
                await _Context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"An error occurred while saving changes to the database.{ex}");
            }
        }

        [HttpPost("Annuler-Demande/{demandeId}")]
        public async Task<IActionResult> AnnulerDemande(int demandeId)
        {
            var demande = await _Context.Utilisateurs.FirstOrDefaultAsync(d => d.Id == demandeId);
            if(demande is not null)
            {
                demande.Statut = "Annulée";
                await _Context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }



    }
}
