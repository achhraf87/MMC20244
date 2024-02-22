using MMC.API.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MMC.API.DTO
{
    public class PagedResponseDTO<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public List<Utilisateur> Demandes { get; }
        public int TotalDemandes { get; }

        //public PagedResponseDTO(UtilisateurDTO data, int page, int pageSize, int totalItems, int totalPages)
        //{
        //    Data = data;
        //    Page = page;
        //    PageSize = pageSize;
        //    TotalItems = totalItems;
        //    TotalPages = totalPages;
        //}

        public PagedResponseDTO(List<Utilisateur> demandes, int page, int pageSize, int totalDemandes, int totalPages)
        {
            Demandes = demandes;
            Page = page;
            PageSize = pageSize;
            TotalDemandes = totalDemandes;
            TotalPages = totalPages;
        }


//        import { HttpClient
//    }
//    from '@angular/common/http';

//@Injectable()
//export class DataService
//    {
//        constructor(private http: HttpClient) {}

//    getDemandes(page: number, pageSize: number)
//        {
//            return this.http.get<PagedResponse<UtilisateurDTO>>(
//            `https://ton-api-url/getToutesLesDemandes?page=${page}&pageSize=${pageSize}`

//            );
//        }
//    }




}

}
