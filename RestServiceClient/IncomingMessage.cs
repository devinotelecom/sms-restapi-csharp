using System;

namespace RestApiClient
{
    /// <summary>
    /// Входящее сообщение
    /// </summary>
    public class IncomingMessage
    {
        /// <summary>
        /// Идентификатор сообщения
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Номер отправителя
        /// </summary>
        public string SourceAddress { get; set; }

        /// <summary>
        /// Номер для приёма входящих сообщений
        /// </summary>
        public string DestinationAddress { get; set; }

        /// <summary>
        /// Дата и время получения сообщения (Гринвич GMT00:00)
        /// </summary>
        public DateTime CreationDateUtc { get; set; }
    }
}
