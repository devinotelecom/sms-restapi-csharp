using System;

namespace RestApiClient
{
    /// <summary>
    /// Статистика отправки сообщений
    /// </summary>
    [Serializable]
    public class DeliveryStatistics
    {
        /// <summary>
        /// Количество отправленных сообщений
        /// </summary>
        public int Sent { get; set; }

        /// <summary>
        /// Количество доставленных сообщений
        /// </summary>
        public int Delivered { get; set; }

        /// <summary>
        /// Ошибки
        /// </summary>
        public int Errors { get; set; }

        /// <summary>
        /// Сообщения, которые сейчас находятся в процессе доставки
        /// </summary>
        public int InProcess { get; set; }

        /// <summary>
        /// Количество просроченных сообщений
        /// </summary>
        public int Expired { get; set; }

        /// <summary>
        /// Количество отклоненных сообщений
        /// </summary>
        public int Rejected { get; set; }        
    }
}
