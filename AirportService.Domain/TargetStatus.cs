namespace AirportService.Domain
{
    /// <summary>
    /// Статус обслуживания
    /// </summary>
    public enum TargetStatus
    {
        /// <summary>
        /// Ожидает обслуживания
        /// </summary>
        AwaitingService = 10,

        /// <summary>
        /// В процессе обслуживания
        /// </summary>
        InProcess = 20,

        /// <summary>
        /// Обслужен
        /// </summary>
        Serviced = 30,
    }
}
