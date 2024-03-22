using AirportService.Domain.Contracts;

namespace AirportService.Domain
{
    /// <summary>
    /// Цель обслуживания
    /// </summary>
    public class Target
    {
        /// <summary>
        /// Идентификатор цели
        /// </summary>
        public Plane Plane { get; init; }

        /// <summary>
        /// Статус обслуживания цели
        /// </summary>
        public TargetStatus Status { get; set; }


        /// <summary>
        /// Конструктор для десериализации
        /// </summary>
        public Target()
        {
            
        }


        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="plane"></param>
        /// <param name="status"></param>
        private Target(Plane plane, TargetStatus status)
        {
            Plane = plane;
            Status = status;
        }


        /// <summary>
        /// Создать задание на обслуживание
        /// </summary>
        /// <param name="plane"></param>
        /// <returns></returns>
        public static Target Create(Plane plane)
        {
            Target target = new(plane, TargetStatus.AwaitingService);
            return target;
        }


        /// <summary>
        /// Изменить статус обслуживания
        /// </summary>
        /// <returns>новый статус обслуживания</returns>
        public TargetStatus ChangeStatus()
        {
            if (Status == TargetStatus.AwaitingService)
                return TargetStatus.InProcess;

            if (Status == TargetStatus.InProcess)
                return TargetStatus.Serviced;

            return TargetStatus.Serviced;
        }


        /// <summary>
        /// Текстовое представление
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{this.Plane} в статусе: {this.Status.Name()}";
        }
    }
}
