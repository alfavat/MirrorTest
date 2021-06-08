namespace WebMobileUI.Models
{
    public class PagingResult<T> where T : class
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = "";
        public PagingData<T> Data { get; set; }
    }
    public class PagingData<T> where T : class
    {
        public T Data { get; set; }
        public int Count { get; set; }
    }
}
