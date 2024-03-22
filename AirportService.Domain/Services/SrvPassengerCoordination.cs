using MassTransit;
using Microsoft.Extensions.Logging;


namespace AirportService.Domain.Services
{
    /// <summary>
    /// Служба координирования пассажиров
    /// </summary>
    public class SrvPassengerCoordination : MaintenanceService
    {
        public SrvPassengerCoordination(IBus bus, ILogger<SrvPassengerCoordination> logger) : 
            base(bus, logger, MaintenanceServiceTypes.PassengerCoordination)
        {

        }
    }
}
