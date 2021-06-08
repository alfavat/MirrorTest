using Newtonsoft.Json;

namespace Core.Middlewares
{
    public class ApiResponse
    {

        public int statusCode { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string message { get; }

        public ApiResponse(int statusCode, string message = null)
        {
            this.statusCode = statusCode;
            this.message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private static string GetDefaultMessageForStatusCode(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    return "Resource not found";
                case 500:
                    return "An unhandled error occurred";
                case 401:
                    return "Authentication expired";
                case 403:
                    return "Authorization denied";
                default:
                    return null;
            }
        }

    }
}
