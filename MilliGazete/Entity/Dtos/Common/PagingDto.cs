namespace Entity.Dtos
{
    public class PagingDto
    {
        public string Query { get; set; }
        public int Limit { get; set; }
        public string OrderBy { get; set; }
        public int PageNumber { get; set; }
    }
}
