namespace MMC.API.DTO
{
    public class DtoPagination
    {
        public int? pageNumber { get; set; }
        public int? pageSize { get; set; }
        public string? sortOrder { get; set; }
        public string? filtreColumn { get; set; }
    }
}
