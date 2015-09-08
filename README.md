# restapi-csharp
Devino Telecom REST API csharp client

[![Build status](https://ci.appveyor.com/api/projects/status/w73xlir7idgu1qik?svg=true)](https://ci.appveyor.com/project/devinotelecom/restapi-csharp)

[Общие сведения](http://devinotele.com/resheniya/dokumentaciya-po-api/http_rest_protocol/Obshchiye_svedeniya/) о том, что надо сделать для начала использования API

*У REST-сервиса не предусмотрен demo-режим, все действия совершаются в боевом режиме.*
*То есть при вызове функции SendMessage сообщения реально отправляются.*
*Будьте внимательны при вводе адреса отправителя и номеров получателей.*

#### Аутентификация ([подробнее](http://devinotele.com/resheniya/dokumentaciya-po-api/http_rest_protocol/Autentifikatsiya/))

```C#
var rest = new RestService();
string sessionId = rest.GetSessionId(UserLogin, Password);
```

#### Получение баланса авторизованного пользователя ([подробнее](http://devinotele.com/resheniya/dokumentaciya-po-api/http_rest_protocol/Polucheniye_balansa_avtorizovannogo_polzovatelya/))
```C#
decimal balance = rest.GetBalance(sessionId);
 ```
 
#### Отправка SMS-сообщений ([подробнее](http://devinotele.com/resheniya/dokumentaciya-po-api/http_rest_protocol/Otpravka_SMS-soobshcheny/))
 
Отправка SMS-сообщения на один номер без учета часового пояса получателя  
 ```C#
List<string> messageIds = rest.SendMessage(sessionId, "адрес отправителя", "адрес получателя 1", "тест
```
Отправка SMS-сообщения на несколько номеров без учета часового пояса получателя
  ```C#
var destinationAddresses = new List<string> { "адрес получателя 1", "адрес получателя 2" };
List<string> bulkIds = rest.SendMessagesBulk(sessionId, "адрес отправителя", destinationAddresses, "тест");
  ```
Отправка SMS-сообщения на один номер с учетом часового пояса получателя  
```C#
List<string> timezoneIds = rest.SendByTimeZone(sessionId, "адрес отправителя", "адрес получателя 1", "тест", DateTime.Now);
```

#### Получение статуса отправленного SMS-сообщения ([подробнее](http://devinotele.com/resheniya/dokumentaciya-po-api/http_rest_protocol/Polucheniye_statusa_otpravlennogo_SMS-soobshcheniya/))

```C#
foreach (var id in messageIds)
{
    MessageStateInfo messageState = rest.GetState(sessionId, id);
}
```
  
#### Получение входящих SMS-сообщений за период ([подробнее](http://devinotele.com/resheniya/dokumentaciya-po-api/http_rest_protocol/Polucheniye_SMS-soobshcheny_za_period/))
```C#
 List<IncomingMessage> incomingMessages = rest.GetIncomingMessages(sessionId, new DateTime(2012, 5, 1), DateTime.Now);
```

#### Получение статистики по SMS-рассылкам  ([подробнее](http://devinotele.com/resheniya/dokumentaciya-po-api/http_rest_protocol/Polucheniye_statistiki_po_SMS-rassylkam/))
 
```C#
var startDateTime = new DateTime(2015, 3, 1, 0, 0, 0, 0);
var endDateTime = new DateTime(2015, 4, 1, 0, 0, 0, 0);

DeliveryStatistics statistics = rest.GetDeliveryStatistics(sessionId, startDateTime, endDateTime);
``` 
