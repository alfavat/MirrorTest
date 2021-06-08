using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

public class HttpHelper : IDisposable
{

    public class RequestObject
    {
        public string Url { get; set; }
        public dynamic Body { get; set; }
        public string AuthorizationHeaderValue { get; set; }
        public string Method { get; set; }
    }

    public T Request<T>(RequestObject requestObject)
    {
        if (string.IsNullOrEmpty(requestObject.Url)) throw new Exception("Url Not Found");
        if (string.IsNullOrEmpty(requestObject.Method)) throw new Exception("Request Method Name Not Found");
        try
        {
            if (WebRequest.Create(requestObject.Url) is WebRequest request)
            {
                request.Method = requestObject.Method;
                request.ContentType = "application/json; charset=utf-8";
                if (!string.IsNullOrEmpty(requestObject.AuthorizationHeaderValue))
                    request.Headers.Add("authorization", requestObject.AuthorizationHeaderValue);

                var param = requestObject.Body != null ? JsonConvert.SerializeObject(requestObject.Body) : "";
                try
                {
                    if (request.Method != "GET")
                        using (var writer = request.GetRequestStream())
                        {
                            byte[] byteArray = Encoding.UTF8.GetBytes(param);
                            writer.Write(byteArray, 0, byteArray.Length);
                        }

                    string responseContent = null;
                    request.Timeout = 10 * 60 * 1000;
                    using (var response = request.GetResponse() as WebResponse)
                    {
                        if (response != null)
                            using (var reader = new StreamReader(response.GetResponseStream()))
                            {
                                responseContent = reader.ReadToEnd();
                            }
                    }
                    return JsonConvert.DeserializeObject<T>(responseContent);
                }
                catch (WebException ex)
                {
                    throw new Exception(ex.Message + " | " + ex.Response != null ? new StreamReader(ex.Response.GetResponseStream()).ReadToEnd() : "");
                }
            }
            throw new Exception("Request Create Exception");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
