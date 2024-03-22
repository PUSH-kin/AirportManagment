using MassTransit;
using Microsoft.Extensions.Logging;


namespace AirportService.Domain.Services
{
    /// <summary>
    /// Служба транспортировки ВС
    /// </summary>
    public class SrvMover : MaintenanceService
    {
        public SrvMover(IBus bus, ILogger<SrvMover> logger) : 
            base(bus, logger, MaintenanceServiceTypes.Mover)
        {

        }
    }
}
