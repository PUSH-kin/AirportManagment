
using AirportService.Domain.Contracts;
using AirportService.Domain.Services;

namespace AirportService.Maintenance.Utility
{
    public class TestService : IHostedService
    {
        private readonly SrvDispatcher _srvDispatcher;
        private readonly PlaneService _planeService;

        public TestService(SrvDispatcher srvDispatcher, PlaneService planeService)
        {
            _srvDispatcher = srvDispatcher;
            _planeService = planeService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var plane = _planeService.GetNext();

            List<Message> messages = _srvDispatcher.PlaneServiceStart(plane);

            await _srvDispatcher.Publish(messages);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
