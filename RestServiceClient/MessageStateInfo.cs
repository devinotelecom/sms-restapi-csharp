using System;

namespace RestApiClient
{
    /// <summary>
    /// Информация о статусе сообщения
    /// </summary>
    public struct MessageStateInfo
    {
        /// <summary>
        /// Статус
        /// </summary>
        public MessageState State { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime? CreationDateUtc { get; set; }

        /// <summary>
        /// Дата отправки
        /// </summary>
        public DateTime? SubmittedDateUtc { get; set; }

        /// <summary>
        /// Дата доставки
        /// </summary>
        public DateTime? ReportedDateUtc { get; set; }

        /// <summary>
        /// Дата и время обновления статуса
        /// </summary>
        public DateTime TimeStampUtc { get; set; }

        /// <summary>
        /// Описание статуса
        /// </summary>
        public string StateDescription { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal? Price { get; set; }
    }
}
