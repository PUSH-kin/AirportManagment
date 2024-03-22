
namespace AirportService.Maintenance.Models
{
    public class BrokerConfig
    {
        public bool UseLocal { get; set; }

        public bool UseRabbitMq { get; set; }

        public string? RabbitMqHost { get; set; }

        public string? RabbitMqLogin { get; set; }

        public string? RabbitMqPassword { get; set; }

        public bool UseRabbitMqUseSSL { get; set; }



        public bool IsValid()
        {
            return UseLocal ^ UseRabbitMq;
        }

        internal void Validate()
        {
            if (!IsValid())
                throw new InvalidOperationException("Неверное задание типа брокера");

            if (UseRabbitMq)
            {
                if (string.IsNullOrWhiteSpace(RabbitMqHost)
                    && string.IsNullOrWhiteSpace(RabbitMqLogin)
                    && string.IsNullOrWhiteSpace(RabbitMqPassword))
                    throw new ArgumentException("креды для доступа к RabbitMq не заполнены");
            }
        }
    }
}
