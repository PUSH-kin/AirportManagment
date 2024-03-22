using Newtonsoft.Json;

namespace AirportService.Domain.Contracts
{
    public record Message
    {
        public Guid MessageGuid { get; init; }

        /// <summary>
        /// Идентификатор сервиса отправителя
        /// </summary>
        public Guid ProducerGuid { get; init; }

        /// <summary>
        /// Время генерации сообщения
        /// </summary>
        public DateTime ProducedAt { get; init; }

        public MaintenanceServiceTypes SrvProducer {  get; init; }

        /// <summary>
        /// Адресат сообщения
        /// </summary>
        public MaintenanceServiceTypes SrvConsumer { get; set; }


        /// <summary>
        /// Управляющая команада
        /// </summary>
        public SrvCommand SrvCommand { get; init; }

        /// <summary>
        /// Примечание
        /// </summary>
        public string? Comment { get; set; }

        // todo передать объект guid + номер самолёта, например RF6734526734
        /// <summary>
        /// Идентификатор цели - воздушного судна
        /// </summary>
        public Plane Plane { get; set; }
    }


    public static class MessageLogFormatter
    {
        public static string LogMessageCreateV0(Message message, Guid consumerGuid)
        {
            return $"Consumer GUID: {consumerGuid}{Environment.NewLine}Received message {message.SrvConsumer.Name()}: {JsonConvert.SerializeObject(message, Formatting.Indented)}";
        }

        public static string LogMessageCreateV1(Message message)
        {
            return $"от {message.SrvProducer.Name()} к {message.SrvConsumer.Name()}: command {message.SrvCommand.Name()} => ВС {message.Plane} => {message.Comment}";
        }
        public static string LogMessageCreateV2(Message message)
        {
            return $"от {message.SrvProducer.Name(),-25} к {message.SrvConsumer.Name(),-25}: command {message.SrvCommand.Name(),-25} => {message.Comment}";
        }

        public static string LogMessageCreateV3(Message message)
        {
            return $"от {message.SrvProducer.Name(),-20} к {message.SrvConsumer.Name(),-20}: command {message.SrvCommand.Name(),-10} => {message.Comment}";
        }

        public static string LogMessageCreate(Message message, Direction direction)
        {
            switch (direction)
            {
                case Direction.Out:
                    return $"{message.SrvProducer.Name(),-25} {direction.Verb(),-13} {message.SrvConsumer.Name(),-25}: {message.SrvCommand.Name(),-28}  {message.Plane} detail: {message.Comment}";
                case Direction.In:
                    return $"{message.SrvConsumer.Name(),-25} {direction.Verb(),-13} {message.SrvProducer.Name(),-25}: {message.SrvCommand.Name(),-28}  {message.Plane} detail: {message.Comment}";
                default:
                    throw new NotImplementedException();
            }
            
            
        }
    }
}
