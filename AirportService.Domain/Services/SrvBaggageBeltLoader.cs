using MassTransit;
using Microsoft.Extensions.Logging;


namespace AirportService.Domain.Services
{
    /// <summary>
    /// Служба ленточного погрузчика
    /// </summary>
    public class SrvBaggageBeltLoader : MaintenanceService
    {
        public SrvBaggageBeltLoader(IBus bus, ILogger<SrvBaggageBeltLoader> logger) : 
            base(bus, logger, MaintenanceServiceTypes.BaggageBeltLoader)
        {

        }
    }
}
