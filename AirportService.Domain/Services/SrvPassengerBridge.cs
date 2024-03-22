using MassTransit;
using Microsoft.Extensions.Logging;


namespace AirportService.Domain.Services
{
    /// <summary>
    /// Служба управления пассажирским мостиком
    /// </summary>
    public class SrvPassengerBridge : MaintenanceService
    {
        public SrvPassengerBridge(IBus bus, ILogger<SrvPassengerBridge> logger) : 
            base(bus, logger, MaintenanceServiceTypes.PassengerBridge)
        {

        }
    }
}
