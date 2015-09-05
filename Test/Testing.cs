/* У REST-сервиса не предусмотрен demo-режим, все действия совершаются в боевом режиме.
 * То есть при вызове функции SendMessage сообщения реально отправляются.
 * Будьте внимательны при вводе адреса отправителя и номеров получателей.
*/
using System;
using System.Collections.Generic;

namespace Test
{
    using RestApiClient;

    public class Testing
    {
//#error введите ваш логин и пароль для тестирования сервиса
        private const string UserLogin = "логин";
        private const string Password = "пароль";

        /// <summary>
        /// Примеры вызовов функций REST-сервиса
        /// </summary>
        public static void Main()
        {
            var rest = new RestService();

            try
            {
                string sessionId = rest.GetSessionId(UserLogin, Password);

                decimal balance = rest.GetBalance(sessionId);

                List<IncomingMessage> incomingMessages = rest.GetIncomingMessages(sessionId, new DateTime(2012, 5, 1), DateTime.Now);

 //#error введите адрес отправителя и номера получателей
                const string sourceAddress = "адрес отправителя";
                var destinationAddresses = new List<string> { "адрес получателя 1", "адрес получателя 2" };

                List<string> messageIds = rest.SendMessage(sessionId, sourceAddress, destinationAddresses[0], "тест");

                foreach (var id in messageIds)
                {
                    MessageStateInfo messageState = rest.GetState(sessionId, id);
                }

                List<string> bulkIds = rest.SendMessagesBulk(sessionId, sourceAddress, destinationAddresses, "тест");

                List<string> timezoneIds = rest.SendByTimeZone(sessionId, sourceAddress, destinationAddresses[0], "тест", DateTime.Now);

                var startDateTime = new DateTime(2012, 3, 1, 0, 0, 0, 0);
                var endDateTime = new DateTime(2012, 4, 1, 0, 0, 0, 0);

                DeliveryStatistics statistics = rest.GetDeliveryStatistics(sessionId, startDateTime, endDateTime);
            }
            catch (RestApiException ex)
            {
                Console.WriteLine("Код ошибки: {0}\tОписание: {1}", ex.ErrorResult.Code, ex.ErrorResult.Desc);
            }
            Console.ReadKey();
        }
    }
}
