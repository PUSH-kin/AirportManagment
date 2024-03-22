using MassTransit;
using Microsoft.Extensions.Logging;


namespace AirportService.Domain.Services
{
    /// <summary>
    /// Служба клининга
    /// </summary>
    public class SrvCleaning : MaintenanceService
    {
        public SrvCleaning(IBus bus, ILogger<SrvCleaning> logger) : 
            base(bus, logger, MaintenanceServiceTypes.Cleaning)
        {

        }
    }
}
