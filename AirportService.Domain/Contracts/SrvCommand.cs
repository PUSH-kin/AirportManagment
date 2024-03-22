using System.ComponentModel;

namespace AirportService.Domain.Contracts
{
    /// <summary>
    /// Команды для служб
    /// </summary>
    public enum SrvCommand
    {
        /// <summary>
        /// Информирование
        /// </summary>
        [Description("Информирование")]
        Info = 0,

        /// <summary>
        /// Начать выполнение
        /// </summary>
        [Description("Начать выполнение")]
        Start = 1,

        /// <summary>
        /// Закончить выполнение
        /// </summary>
        [Description("Закончить выполнение")]
        Stop = 2,

        /// <summary>
        /// Выгрузка пассажиров
        /// </summary>
        [Description("Выгрузка пассажиров")]
        PassengerOut = 3,

        /// <summary>
        /// Загрузка пассажиров
        /// </summary>
        [Description("Загрузка пассажиров")]
        PassengerIn = 4,

        /// <summary>
        /// Подключен
        /// </summary>
        [Description("Подключен")]
        Connected = 5,

        /// <summary>
        /// Обслуживание завершено
        /// </summary>
        [Description("Обслуживание завершено")]
        CheckPoint = 6,

        SelfTest = 7,
    }



    public static class SrvCommandExt
    {
        public static string Name(this SrvCommand item)
        {
            switch (item)
            {
                case SrvCommand.Info:
                    return "INFO";
                case SrvCommand.Start:
                    return "Обслуживание начать";
                case SrvCommand.Stop:
                    return "Обслуживание закончить";
                case SrvCommand.PassengerOut:
                    return "Пассажиры - покинуть борт";
                case SrvCommand.PassengerIn:
                    return "Пассажиры - взойти на борт";
                case SrvCommand.Connected:
                    return "Присоединился";
                case SrvCommand.CheckPoint:
                    return "Завершено";
                case SrvCommand.SelfTest:
                    return "Самотестирование";
                default:
                    throw new ArgumentException();
            }
        }
    }
}
