namespace Entity.Dtos
{
    public class MainPageNewsPagingDto : PagingDto
    {
        public int? NewsId { get; set; }
        public string Url { get; set; }
        public MainPageNewsPagingDto()
        {
            Limit = 1;
            PageNumber = 1;
            OrderBy = "Id desc";
        }
    }
}
