namespace MMC.API.DTO
{
    public class DtoDataTable<T>
    {
        public List<T> Data { get; set; }
        public int? totalRecord { get; set; }
        public int? totalFiltred { get; set; }
    }
}
