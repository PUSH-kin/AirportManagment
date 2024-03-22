using AirportService.Domain.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace AirportService.Domain.Services
{
    /// <summary>
    /// Сервисная служба аэропорта
    /// </summary>
    public class MaintenanceService : MaintenanceServiceBase
    {
        private static MaintenanceService? instance;


        public static MaintenanceService getInstance()
        {
            if (instance == null)
                throw new InvalidOperationException("Требуется проинициализировать службу !");
            return instance;
        }


        /// <summary>
        /// Обслуживающие единицы
        /// </summary>
        public List<Worker>? Units { get; private set; }


        /// <summary>
        /// Инициализировать службу
        /// </summary>
        /// <param name="name"></param>
        /// <param name="units"></param>
        public MaintenanceService(IBus bus, ILogger<MaintenanceService> logger, MaintenanceServiceTypes maintenance) :
            base(bus, logger, maintenance)
        {
            
        }


        /// <summary>
        /// Инициализировать службу
        /// </summary>
        /// <param name="name"></param>
        /// <param name="units"></param>
        public MaintenanceService(IBus bus, ILogger<MaintenanceService> logger, MaintenanceServiceTypes maintenance, List<Worker> units) :
            base(bus, logger, maintenance)
        {
            Units = units;
        }


        /// <summary>
        /// Инициализировать службу
        /// </summary>
        /// <param name="name"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static MaintenanceService Create(IBus bus, ILogger<MaintenanceService> logger, MaintenanceServiceTypes maintenance, int count)
        {
            var workers = new List<Worker>();
            do
            {
                workers.Add(Worker.Create());
            } while (workers.Count < count);

            var maintenanceService = new MaintenanceService(bus, logger, maintenance, workers);

            return maintenanceService;
        }


        /// <summary>
        /// Проинициализировать сервис
        /// </summary>
        /// <param name="name"></param>
        /// <param name="count"></param>
        public static void Init(IBus bus, ILogger<MaintenanceService> logger, MaintenanceServiceTypes maintenance, int count = 1)
        {
            instance = Create(bus, logger, maintenance, count);
        }

        /// <summary>
        /// Получить список единиц обслуживания
        /// </summary>
        /// <returns></returns>
        public List<Worker> GetUnits()
        {
            return Units ?? new List<Worker>();
        }


        /// <summary>
        /// Получить список СВОБОДНЫХ единиц обслуживания
        /// </summary>
        /// <returns></returns>
        public List<Worker> GetUnitsFree()
        {
            return GetUnits()
                .Where(x => x.IsFree())
                .ToList();
        }


        /// <summary>
        /// Получить списорк всех служб, кроме этой
        /// </summary>
        /// <returns></returns>
        public List<MaintenanceServiceTypes> GetAllOtherMaintenances()
        {
            var all = GetEnumList<MaintenanceServiceTypes>();
            all.Remove(Maintenance);
            return all;
        }


        /// <summary>
        /// Получить все значения перечисления
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static List<T> GetEnumList<T>()
        {
            T[] array = (T[])Enum.GetValues(typeof(T));
            List<T> list = new List<T>(array);
            return list;
        }

        public List<Message> SelfMessage()
        {
            return CreateMessageList(PlaneService.GetNull(), new List<MaintenanceServiceTypes> { Maintenance }, SrvCommand.SelfTest, "self test");
        }
    }
}
