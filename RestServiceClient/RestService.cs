using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace RestApiClient
{
    /// <summary>
    /// Клиент для взаимодействия с Devino REST API
    /// </summary>
    public class RestService
    {
        private const string Host = "integrationapi.net/rest";
        private const string Scheme = "https";

        /// <summary>
        /// Отправить сообщение
        /// </summary>
        /// <param name="sessionId">Идентификатор сессии</param>
        /// <param name="sourceAddress">Адрес отправителя сообщения</param>
        /// <param name="destinationAddress">Мобильный телефонный номер получателя сообщения</param>
        /// <param name="data">Текст сообщения</param>
        /// <param name="sendDate">Дата и время отправки (для моментальной отправки можно не передавать)</param>
        /// <param name="validity">Время жизни сообщения (в минутах)</param>
        /// <returns>Идентификаторы частей сообщения 
        /// (если сообщение больше 70 символов на кириллице или 160 на латинице, оно разбивается на несколько)</returns>
        public List<string> SendMessage(string sessionId, string sourceAddress, string destinationAddress, string data, DateTime? sendDate = null, int validity = 0)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            queryString["sessionId"] = sessionId;
            queryString["sourceAddress"] = sourceAddress;
            queryString["destinationAddress"] = destinationAddress;
            queryString["data"] = data;
            queryString["validity"] = validity.ToString();

            if (sendDate.HasValue)
            {
                queryString["sendDate"] = sendDate.Value.ToString("yyyy-MM-ddThh:mm:ss");
            }

            string result = Post("/Sms/Send", queryString.ToString());
            return JsonConvert.DeserializeObject<List<string>>(result);
        }

        /// <summary>
        /// Отправить сообщения нескольким получателям 
        /// </summary>
        /// <param name="sessionId">Идентификатор сессии</param>
        /// <param name="sourceAddress">Адрес отправителя сообщения</param>
        /// <param name="destinationAddresses">Мобильные телефонные номера получателей сообщения</param>
        /// <param name="data">Текст сообщения</param>
        /// <param name="sendDate">Дата и время отправки (для моментальной отправки можно не передавать)</param>
        /// <param name="validity">Время жизни сообщения (в минутах)</param>
        /// <returns>Идентификаторы частей сообщений (всех частей всех сообщений для каждого получателя)</returns>
        public List<string> SendMessagesBulk(string sessionId, string sourceAddress, List<string> destinationAddresses, string data, DateTime? sendDate = null, int validity = 0)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            queryString["sessionId"] = sessionId;
            queryString["sourceAddress"] = sourceAddress;
            queryString["data"] = data;
            queryString["validity"] = validity.ToString();

            if (sendDate.HasValue)
            {
                queryString["sendDate"] = sendDate.Value.ToString("yyyy-MM-ddThh:mm:ss");
            }

            var request = new StringBuilder();

            request.Append(queryString);

            foreach (var destinationAddress in destinationAddresses)
            {
                request.AppendFormat("&destinationAddresses={0}", HttpUtility.UrlEncode(destinationAddress));
            }

            var result = Post("/Sms/SendBulk", request.ToString());
            return JsonConvert.DeserializeObject<List<string>>(result);
        }

        /// <summary>
        /// Отправить сообщение
        /// </summary>
        /// <param name="sessionId">Идентификатор сессии</param>
        /// <param name="sourceAddress">Адрес отправителя сообщения</param>
        /// <param name="destinationAddress">Мобильный телефонный номер получателя сообщения</param>
        /// <param name="data">Текст сообщения</param>
        /// <param name="sendDate">Дата и время отправки (для моментальной отправки можно не передавать)</param>
        /// <param name="validity">Время жизни сообщения (в минутах)</param>
        /// <returns>Идентификаторы частей сообщения 
        /// (если сообщение больше 70 символов на кириллице или 160 на латинице, оно разбивается на несколько)</returns>
        public List<string> SendByTimeZone(string sessionId, string sourceAddress, string destinationAddress, string data, DateTime sendDate, int validity = 0)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            queryString["sessionId"] = sessionId;
            queryString["sourceAddress"] = sourceAddress;
            queryString["destinationAddress"] = destinationAddress;
            queryString["data"] = data;
            queryString["validity"] = validity.ToString();
            queryString["sendDate"] = sendDate.ToString("yyyy-MM-ddThh:mm:ss");

            var result = Post("/Sms/SendByTimeZone", queryString.ToString());
            return JsonConvert.DeserializeObject<List<string>>(result);
        }

        /// <summary>
        /// Получить статус сообщения
        /// </summary>
        /// <param name="sessionId">Идентификатор сессии</param>
        /// <param name="messageId">Идентификатор сообщения</param>
        /// <returns>Информация о статусе сообщения</returns>
        public MessageStateInfo GetState(string sessionId, string messageId)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            queryString["sessionId"] = sessionId;
            queryString["messageId"] = messageId;

            var result = Get("/Sms/State", queryString.ToString());
            return JsonConvert.DeserializeObject<MessageStateInfo>(result);
        }

        /// <summary>
        /// Получить входящие сообщения
        /// </summary>
        /// <param name="sessionId">Идентификатор сессии</param>
        /// <param name="minDateUtc">Минимальное значение периода,
        ///  за который происходит выборка входящих сообщени</param>
        /// <param name="maxDateUtc">Максимальное значение периода,
        ///  за который происходит выборка входящих сообщений</param>
        /// <returns>Список входящих сообщений</returns>
        public List<IncomingMessage> GetIncomingMessages(string sessionId, DateTime minDateUtc, DateTime maxDateUtc)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            queryString["sessionId"] = sessionId;
            queryString["minDateUTC"] = minDateUtc.ToString("yyyy-MM-ddThh:mm:ss");
            queryString["maxDateUTC"] = maxDateUtc.ToString("yyyy-MM-ddThh:mm:ss");

            var result = Get("/Sms/In", queryString.ToString());
            return JsonConvert.DeserializeObject<List<IncomingMessage>>(result);
        }

        /// <summary>
        /// Получить идентификатор сессии
        /// </summary>
        /// <param name="login">Логин клиента</param>
        /// <param name="password">Пароль клиента</param>
        /// <returns>Идентификатор сессии</returns>
        public string GetSessionId(string login, string password)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            queryString["login"] = login;
            queryString["password"] = password;

            var result = Get("/User/SessionId", queryString.ToString());
            return JsonConvert.DeserializeObject<string>(result);
        }

        /// <summary>
        /// Получить баланс
        /// </summary>
        /// <param name="sessionId">Идентификатор сессии</param>
        /// <returns>Баланс клиента</returns>
        public decimal GetBalance(string sessionId)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            queryString["sessionId"] = sessionId;

            var result = Get("/User/Balance", queryString.ToString());
            return JsonConvert.DeserializeObject<decimal>(result);
        }

        /// <summary>
        /// Получить статистику смс за заданный промежуток времени
        /// </summary>
        /// <param name="sessionId">Id сессии</param>
        /// <param name="startDateTime">Начало промежутка</param>
        /// <param name="endDateTime">Конец промежутка</param>
        /// <returns>Статистику смс</returns>
        public DeliveryStatistics GetDeliveryStatistics(string sessionId, DateTime startDateTime, DateTime endDateTime)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            queryString["sessionId"] = sessionId;
            queryString["startDateTime"] = startDateTime.ToString("yyyy-MM-ddThh:mm:ss");
            queryString["endDateTime"] = endDateTime.ToString("yyyy-MM-ddThh:mm:ss");

            var result = Get("/Sms/Statistics", queryString.ToString());
            return JsonConvert.DeserializeObject<DeliveryStatistics>(result);
        }

        private string Post(string path, string queryString)
        {
            var uriBuilder = new UriBuilder { Host = Host, Path = path, Scheme = Scheme };

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(uriBuilder.Uri);

            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            if (httpWebRequest.Proxy != null)
            {
                httpWebRequest.Proxy.Credentials = CredentialCache.DefaultCredentials;
            }

            byte[] byteArr = Encoding.UTF8.GetBytes(queryString);
            httpWebRequest.ContentLength = byteArr.Length;

            try
            {
                using (var stream = httpWebRequest.GetRequestStream())
                {
                    stream.Write(byteArr, 0, byteArr.Length);
                }

                var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var responseStream = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8))
                {
                    return responseStream.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    var contentStream = ex.Response.GetResponseStream();
                    if (contentStream != null)
                    {
                        using (var reader = new StreamReader(contentStream))
                        {
                            var content = reader.ReadToEnd();
                            if (!string.IsNullOrEmpty(content))
                                throw new RestApiException(JsonConvert.DeserializeObject<ErrorResult>(content));
                            throw;
                        }
                    }
                }
                throw;
            }
        }

        private static string Get(string path, string queryString)
        {
            var uriBuilder = new UriBuilder { Host = Host, Path = path, Query = queryString, Scheme = Scheme };

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(uriBuilder.Uri);

            httpWebRequest.Method = "GET";
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            if (httpWebRequest.Proxy != null)
            {
                httpWebRequest.Proxy.Credentials = CredentialCache.DefaultCredentials;
            }

            try
            {
                var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var responseStream = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8))
                {
                    return responseStream.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    var contentStream = ex.Response.GetResponseStream();
                    if (contentStream != null)
                    {
                        using (var reader = new StreamReader(contentStream))
                        {
                            var content = reader.ReadToEnd();
                            if (!string.IsNullOrEmpty(content))
                                throw new RestApiException(JsonConvert.DeserializeObject<ErrorResult>(content));
                        }
                    }
                }
                throw;
            }
        }
    }
}