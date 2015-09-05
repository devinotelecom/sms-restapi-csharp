namespace RestApiClient
{
    /// <summary>
    /// Статус сообщения
    /// </summary>
    public enum MessageState
    {
        /// <summary>
        /// Отправлено (передано в мобильную сеть)
        /// </summary>
        Sent = -1,

        /// <summary>
        /// В очереди
        /// </summary>
        InQueue = -2,

        /// <summary>
        /// Удалено
        /// </summary>
        Deleted = -97,

        /// <summary>
        /// Остановлено
        /// </summary>
        Stopped = -98,

        /// <summary>
        /// Доставлено абоненту
        /// </summary>
        Delivered = 0,

        /// <summary>
        /// Неверно введён адрес отправителя
        /// </summary>
        WrongSourceAddress = 10,

        /// <summary>
        /// Неверно введён адрес получателя
        /// </summary>
        WrongDestinationAddress = 11,

        /// <summary>
        /// Недопустимый адрес получаателя
        /// </summary>
        InvalidDestinationAddress = 41,

        /// <summary>
        /// Отклонено смс центром
        /// </summary>
        RejectedBySmsCenter = 42,

        /// <summary>
        /// Просрочено (истёк срок жизни сообщения)
        /// </summary>
        ValidityExpired = 46,

        /// <summary>
        /// Отклонено
        /// </summary>
        Rejected = 69,

        /// <summary>
        /// Неизвестный
        /// </summary>
        Unknown = 99,

        /// <summary>
        /// Неизвестный
        /// </summary>
        StateExpired = 255
    }
}
