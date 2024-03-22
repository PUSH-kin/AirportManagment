using MassTransit;
using Microsoft.Extensions.Logging;


namespace AirportService.Domain.Services
{
    /// <summary>
    /// Служба подачи кислорода
    /// </summary>
    public class SrvOxygen : MaintenanceService
    {
        public SrvOxygen(IBus bus, ILogger<SrvOxygen> logger) : 
            base(bus, logger, MaintenanceServiceTypes.Oxygen)
        {

        }
    }

    
}
