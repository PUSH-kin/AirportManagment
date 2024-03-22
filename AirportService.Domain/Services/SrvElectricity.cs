using MassTransit;
using Microsoft.Extensions.Logging;

namespace AirportService.Domain.Services
{
    /// <summary>
    /// Служба обеспечения электричества
    /// </summary>
    public class SrvElectricity : MaintenanceService
    {
        public SrvElectricity(IBus bus, ILogger<SrvElectricity> logger) : 
            base(bus, logger, MaintenanceServiceTypes.Electricity)
        {

        }
    }

    
}
