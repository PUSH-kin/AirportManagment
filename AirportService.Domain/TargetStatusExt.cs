namespace AirportService.Domain
{
    /// <summary>
    /// Вспомогательный класс для статусов обслуживания
    /// </summary>
    public static class TargetStatusExt
    {
        /// <summary>
        /// Получить наименование статуса
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string Name(this TargetStatus item)
        {
            switch (item)
            {
                case TargetStatus.AwaitingService:
                    return "Ожидает обслуживания";
                case TargetStatus.InProcess:
                    return "В процессе обслуживания";
                case TargetStatus.Serviced:
                    return "Обслужен";
                default:
                    throw new ArgumentException($"Неизвестный статус: {item.Id()}");
            }
        }


        /// <summary>
        /// Получить числовой код статуса обслуживания
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static int Id(this TargetStatus item)
        {
            return (int)item;
        }
    }
}
