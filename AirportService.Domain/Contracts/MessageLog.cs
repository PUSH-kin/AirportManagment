namespace AirportService.Domain.Contracts
{
    public record MessageLog
    {
        public Message? Msg { get; set; }

        public MessageLog()
        {
            
        }

        public MessageLog(Message message)
        {
            Msg = message;
        }
    }
}
