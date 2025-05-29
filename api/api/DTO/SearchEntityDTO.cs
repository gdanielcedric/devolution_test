namespace api.DTO
{
    public class SearchEntityDto<FilterDtoType>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public FilterDtoType? Filter { get; set; }
    }
}
