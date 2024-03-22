using MassTransit;
using Microsoft.Extensions.Logging;


namespace AirportService.Domain.Services
{
    /// <summary>
    /// Служба подачи топлива
    /// </summary>
    public class SrvFuel : MaintenanceService
    {
        public SrvFuel(IBus bus, ILogger<SrvFuel> logger) : 
            base(bus, logger, MaintenanceServiceTypes.Fuel)
        {

        }
    }
}
