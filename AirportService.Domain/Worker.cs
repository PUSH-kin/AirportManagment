using AirportService.Domain.Contracts;

namespace AirportService.Domain
{
    /// <summary>
    /// Обслуживающая единица
    /// </summary>
    public class Worker
    {
        /// <summary>
        /// Идентификатор обслуживающей единиы
        /// </summary>
        public Guid Guid { get; init; }

        /// <summary>
        /// Цель обслуживания
        /// </summary>
        public Target? Target { get; private set; }


        /// <summary>
        /// Конструтор для десериализации
        /// </summary>
        public Worker()
        {
            
        }

        /// <summary>
        /// Конструтор
        /// </summary>
        /// <param name="guid"></param>
        private Worker(Guid guid)
        {
            Guid = guid;
        }


        /// <summary>
        /// Создать единицу обслуживания
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static Worker Create()
        {
            return new Worker(Guid.NewGuid());
        }

        /// <summary>
        /// Добавиь цель обслуживания
        /// </summary>
        /// <param name="targetGuid"></param>
        /// <returns></returns>
        public Target? Add(Plane plane)
        {
            if (IsFree())
            {
                SetTarget(Target.Create(plane));
                return GetTarget();
            }
            else
            {
                throw new InvalidOperationException($"Выполняется обслуживания {GetTarget()}");
            }
        }

        public Target? Remove(Target target)
        {
            if (IsFree())
                return null;

            SetTarget();
            return GetTarget();
        }



        /// <summary>
        /// Получить текущую цель обслуживания
        /// </summary>
        /// <returns></returns>
        public Target? GetTarget()
        {
            return this.Target;
        }

        /// <summary>
        /// Отменить назначение
        /// </summary>
        /// <returns></returns>
        private Target? SetTarget()
        {
            this.Target = null;
            return GetTarget();
        }

        /// <summary>
        /// Присвоить назначение
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        private Target? SetTarget(Target target)
        {
            this.Target = target;
            return GetTarget();
        }

        


        /// <summary>
        /// Статус единицы обслуживания
        /// </summary>
        /// <returns></returns>
        public bool IsFree()
        {
            return GetTarget() == null;
        }
    }
}
