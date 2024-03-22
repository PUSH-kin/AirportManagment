using MassTransit;
using Microsoft.Extensions.Logging;


namespace AirportService.Domain.Services
{
    /// <summary>
    /// Служба перевозки багажа
    /// </summary>
    public class SrvBaggageTransport : MaintenanceService
    {
        public SrvBaggageTransport(IBus bus, ILogger<SrvBaggageTransport> logger) : 
            base(bus, logger, MaintenanceServiceTypes.BaggageTransport)
        {

        }
    }
}
