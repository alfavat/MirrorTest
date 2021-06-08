namespace Core.CrossCuttingConcerns.Logging
{
    public class ErrorLogParameter
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string HelpLink { get; set; }
        public int HResult { get; set; }
        public string Source { get; set; }
    }
}
