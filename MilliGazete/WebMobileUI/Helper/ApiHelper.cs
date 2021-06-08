using Core.Utilities.Results;
using Newtonsoft.Json;
using System;
using WebMobileUI.Models;
using static HttpHelper;

public class ApiHelper
{
    /// <summary>
    /// get request for pagination
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static PagingResult<T> GetPagingApi<T>(string url, string token = "") where T : class
    {
        try
        {
            using (HttpHelper http = new HttpHelper())
            {
                var res = http.Request<dynamic>(new RequestObject()
                {
                    Url = LayoutModel.APIUrl + url,
                    Method = "GET",
                    AuthorizationHeaderValue = token.StringNotNullOrEmpty() ? (token = "Bearer " + token) : ""
                });
                T data = default(T);
                int count = 0;
                if (res != null && res.data != null)
                {
                    if (res.data.data != null)
                    {
                        data = JsonConvert.DeserializeObject<T>(res.data.data.ToString());
                    }
                    if (res.data.count != null)
                    {
                        int.TryParse(res.data.count.ToString(), out count);
                    }
                }
                return new PagingResult<T>
                {
                    Data = new PagingData<T>() { Data = data, Count = count },
                    Success = true
                };
            }
        }
        catch (Exception ec)
        {
            return new PagingResult<T>
            {
                Data = new PagingData<T>() { Data = null, Count = 0 },
                Success = false,
                Message = ec.Message
            };
        }
    }

    /// <summary>
    /// get request
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static IDataResult<T> GetApi<T>(string url, string token = "")
    {
        try
        {
            using (HttpHelper http = new HttpHelper())
            {
                var res = http.Request<dynamic>(new RequestObject()
                {
                    Url = LayoutModel.APIUrl + url,
                    Method = "GET",
                    AuthorizationHeaderValue = token.StringNotNullOrEmpty() ? (token = "Bearer " + token) : ""
                });
                T data = default(T);
                if (res != null && res.data != null)
                {
                    data = JsonConvert.DeserializeObject<T>(res.data.ToString());
                }
                return new SuccessDataResult<T>(data: data);
            }
        }
        catch (Exception ec)
        {
            try
            {
                var responseDetails = JsonConvert.DeserializeObject<dynamic>(ec.Message);
                if (responseDetails != null && responseDetails.message != null)
                {
                    return new ErrorDataResult<T>(message: responseDetails.message.ToString());
                }
            }
            catch
            {
            }

            return new ErrorDataResult<T>(message: ec.Message);
        }
    }

    /// <summary>
    /// Post with return data returns both data and message
    /// </summary>
    /// <param name="url"></param>
    /// <param name="body"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static IDataResult<T> PostDataApi<T>(string url, dynamic body, string token = "")
    {
        try
        {
            using (HttpHelper http = new HttpHelper())
            {
                var res = http.Request<dynamic>(new RequestObject()
                {
                    Url = LayoutModel.APIUrl + url,
                    Method = "POST",
                    Body = body,
                    AuthorizationHeaderValue = token.StringNotNullOrEmpty() ? (token = "Bearer " + token) : ""
                });
                T data = default(T);
                if (res != null && res.data != null)
                {
                    data = JsonConvert.DeserializeObject<T>(res.data.ToString());
                }
                return new SuccessDataResult<T>(data: data);
            }
        }
        catch (Exception ec)
        {
            try
            {
                var responseDetails = JsonConvert.DeserializeObject<dynamic>(ec.Message);
                if (responseDetails != null && responseDetails.message != null)
                {
                    return new ErrorDataResult<T>(message: responseDetails.message.ToString());
                }
            }
            catch
            { }

            return new ErrorDataResult<T>(message: ec.Message);
        }
    }

    /// <summary>
    /// Post with no return data only returns message
    /// </summary>
    /// <param name="url"></param>
    /// <param name="body"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static IResult PostNoDataApi(string url, dynamic body, string token = "")
    {
        try
        {
            using (HttpHelper http = new HttpHelper())
            {
                var res = http.Request<dynamic>(new RequestObject()
                {
                    Url = LayoutModel.APIUrl + url,
                    Method = "POST",
                    Body = body,
                    AuthorizationHeaderValue = token.StringNotNullOrEmpty() ? (token = "Bearer " + token) : ""
                });

                if (res != null && res.message != null)
                {
                    return new SuccessResult(message: res.message.ToString());
                }
                return new SuccessResult(message: "");
            }
        }
        catch (Exception ec)
        {
            try
            {
                var responseDetails = JsonConvert.DeserializeObject<dynamic>(ec.Message);
                if (responseDetails != null && responseDetails.message != null)
                {
                    return new ErrorResult(message: responseDetails.message.ToString());
                }
            }
            catch
            { }
            return new ErrorResult(message: ec.Message);
        }
    }
}
