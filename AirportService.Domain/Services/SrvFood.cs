using MassTransit;
using Microsoft.Extensions.Logging;


namespace AirportService.Domain.Services
{
    /// <summary>
    /// Служба обеспечения питания
    /// </summary>
    public class SrvFood : MaintenanceService
    {
        public SrvFood(IBus bus, ILogger<SrvFood> logger) :
            base(bus, logger, MaintenanceServiceTypes.Food)
        {

        }
    }
}
