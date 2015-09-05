namespace RestApiClient
{
    /// <summary>
    /// Коды ошибок возвращаемые сервисом
    /// </summary>
    public enum ApiErrorCode
    {
        /// <summary>
        /// Значение какого-либо аргумента не задано
        /// </summary>        
        ArgumentCanNotBeNullOrEmpty = 1,

        /// <summary>
        /// Значение переданного аргумента задано некорректно 
        /// </summary>        
        InvalidArgument = 2,

        /// <summary>
        /// Некорректное значение сессии либо она просрочилась
        /// </summary>        
        InvalidSessionID = 3,

        /// <summary>
        /// Неверный логин либо пароль
        /// </summary>        
        UnauthorizedAccess = 4,

        /// <summary>
        /// Не хватает денег для проведения операции
        /// </summary>        
        NotEnoughCredits = 5,

        /// <summary>
        /// Ошибка при проведении операции
        /// </summary>        
        InvalidOperation = 6,

        /// <summary>
        /// Анти-спам
        /// </summary>        
        Forbidden = 7,

        /// <summary>
        /// Ошибка от смс-центра Offline, Timeout и т.д.
        /// </summary>        
        GatewayError = 8,

        /// <summary>
        /// Необработанное исключение
        /// </summary>        
        InternalServerError = 9
    }
}
