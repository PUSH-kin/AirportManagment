using MassTransit;
using Microsoft.Extensions.Logging;


namespace AirportService.Domain.Services
{
    /// <summary>
    /// Служба зауска двигателей
    /// </summary>
    public class SrvEngines : MaintenanceService
    {
        public SrvEngines(IBus bus, ILogger<SrvEngines> logger) : 
            base(bus, logger, MaintenanceServiceTypes.Engines)
        {

        }
    }
}
