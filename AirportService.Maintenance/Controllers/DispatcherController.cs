using AirportService.Domain.Contracts;
using AirportService.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static MassTransit.Monitoring.Performance.BuiltInCounters;

namespace AirportService.Maintenance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DispatcherController : ControllerBase
    {
        private readonly SrvDispatcher _srvDispatcher;
        private readonly PlaneService _planeService;

        public DispatcherController(SrvDispatcher srvDispatcher, PlaneService planeService)
        {
            _srvDispatcher = srvDispatcher;
            _planeService = planeService;
        }


        /// <summary>
        /// Получить новый Guid самолёта
        /// </summary>
        /// <param name="targetGuid"></param>
        /// <returns></returns>
        [HttpGet("plane")]
        public ActionResult<Plane> Get() 
        { 
            return Ok(_planeService.GetNext());
        }


        /// <summary>
        /// Начать обслуживание конкретного воздушного судна
        /// </summary>
        /// <param name="plane"></param>
        /// <returns></returns>
        [HttpPost("plane/service/start")]
        public async Task<ActionResult<List<Message>>> PlaneServiceStart(Plane plane)
        {

            List<Message> messages = _srvDispatcher.PlaneServiceStart(plane);

            await _srvDispatcher.Publish(messages);

            return Ok(messages);
        }


        /// <summary>
        /// Начать обслуживание воздушного судна из очереди
        /// </summary>
        /// <param name="plane"></param>
        /// <returns></returns>
        [HttpPost("plane/service/queue/start")]
        public async Task<ActionResult<List<Message>>> PlaneServiceStart()
        {
            var nextPlane = _planeService.GetNext();

            List<Message> messages = _srvDispatcher.PlaneServiceStart(nextPlane);

            await _srvDispatcher.Publish(messages);

            return Ok(messages);
        }


        [HttpGet("SelfTest")]
        public async Task<ActionResult<List<Message>>> SelfTest()
        {

            List<Message> messages = _srvDispatcher.SelfMessage();

            await _srvDispatcher.Publish(messages);

            return Ok(messages);
        }


        [HttpGet("messages/consumed")]
        public ActionResult<List<Message>> GetConsumedMessages() 
        {
            return Ok(_srvDispatcher.GetConsumedMessages());
        }
    }
}
