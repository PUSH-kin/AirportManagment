namespace AirportService.Domain
{
    public enum MaintenanceServiceTypes
    {
        /// <summary>
        /// Диспетчерская
        /// </summary>
        Dispatcher = 10,

        /// <summary>
        /// Загрузка топлива
        /// </summary>
        Fuel = 20,

        /// <summary>
        /// Перевозка самолёта
        /// </summary>
        Mover = 30,

        /// <summary>
        /// Ленточный погрузчик
        /// </summary>
        BaggageBeltLoader = 40,

        /// <summary>
        /// Обеспечение кислородом
        /// </summary>
        Oxygen = 50,

        /// <summary>
        /// Запуск двигателей
        /// </summary>
        Engines = 60,

        /// <summary>
        /// Туалеты
        /// </summary>
        Lavatory = 70,

        /// <summary>
        /// Обеспечение еды
        /// </summary>
        Food = 80,

        /// <summary>
        /// Подача воды
        /// </summary>
        Water = 90,

        /// <summary>
        /// Клининг
        /// </summary>
        Cleaning = 100,

        /// <summary>
        /// Электричество
        /// </summary>
        Electricity = 110,

        /// <summary>
        /// Транспорт багажа
        /// </summary>
        BaggageTransport = 120,

        /// <summary>
        /// Пассажирский мостик
        /// </summary>
        PassengerBridge = 130,

        /// <summary>
        /// Координирование пассажиров
        /// </summary>
        PassengerCoordination = 140,
    }


    public static class MaintenanceServiceTypesExt
    {
        public static string Name(this MaintenanceServiceTypes item)
        {
            switch (item)
            {
                case MaintenanceServiceTypes.Dispatcher:
                    return nameof(MaintenanceServiceTypes.Dispatcher);
                case MaintenanceServiceTypes.Fuel:
                    return nameof(MaintenanceServiceTypes.Fuel);
                case MaintenanceServiceTypes.Mover:
                    return nameof(MaintenanceServiceTypes.Mover);
                case MaintenanceServiceTypes.BaggageBeltLoader:
                    return nameof(MaintenanceServiceTypes.BaggageBeltLoader);
                case MaintenanceServiceTypes.Oxygen:
                    return nameof(MaintenanceServiceTypes.Oxygen);
                case MaintenanceServiceTypes.Engines:
                    return nameof(MaintenanceServiceTypes.Engines);
                case MaintenanceServiceTypes.Lavatory:
                    return nameof(MaintenanceServiceTypes.Lavatory);
                case MaintenanceServiceTypes.Food:
                    return nameof(MaintenanceServiceTypes.Food);
                case MaintenanceServiceTypes.Water:
                    return nameof(MaintenanceServiceTypes.Water);
                case MaintenanceServiceTypes.Cleaning:
                    return nameof(MaintenanceServiceTypes.Cleaning);
                case MaintenanceServiceTypes.Electricity:
                    return nameof(MaintenanceServiceTypes.Electricity);
                case MaintenanceServiceTypes.BaggageTransport:
                    return nameof(MaintenanceServiceTypes.BaggageTransport);
                case MaintenanceServiceTypes.PassengerBridge:
                    return nameof(MaintenanceServiceTypes.PassengerBridge);
                case MaintenanceServiceTypes.PassengerCoordination:
                    return nameof(MaintenanceServiceTypes.PassengerCoordination);
                default:
                    throw new ArgumentException($"Неизвестный тип сервиса {item}");
            }
        }
    }
}
