using MassTransit;
using Microsoft.Extensions.Logging;


namespace AirportService.Domain.Services
{
    /// <summary>
    /// Служба подачи воды
    /// </summary>
    public class SrvWater : MaintenanceService
    {
        public SrvWater(IBus bus, ILogger<SrvWater> logger) : 
            base(bus, logger, MaintenanceServiceTypes.Water)
        {

        }
    }
}
