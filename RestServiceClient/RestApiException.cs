using System;
using System.Web;

namespace RestApiClient
{
    /// <summary>
    /// Исключение, получаемые от REST-API
    /// </summary>
    [Serializable]
    public class RestApiException : HttpException
    {
        public RestApiException(int httpCode, string message, int errorCode)
            : base(httpCode, message)
        {
            ErrorResult = new ErrorResult { Code = errorCode, Desc = message };
        }

        public RestApiException(ErrorResult errorResult)
        {
            ErrorResult = errorResult;
        }

        public ErrorResult ErrorResult { get; set; }
    }
}
