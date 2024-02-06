namespace MMC.API.DTO
{
    public class DtoDataTableRequest<T>
    {
        public DtoPagination? Pagination { get; set; }
        public T? Filtre { get; set; }
    }
}
