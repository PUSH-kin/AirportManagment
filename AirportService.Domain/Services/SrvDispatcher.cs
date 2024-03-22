using AirportService.Domain.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;


namespace AirportService.Domain.Services
{
    /// <summary>
    /// Служба диспетчеров
    /// </summary>
    public class SrvDispatcher : MaintenanceService
    {
        public SrvDispatcher(IBus bus, ILogger<SrvDispatcher> logger) : 
            base(bus, logger, MaintenanceServiceTypes.Dispatcher)
        {

        }

        public override async Task ProcessMessage(Message message)
        {
            if (message.SrvCommand == SrvCommand.CheckPoint)
            {
                if (message.SrvProducer == MaintenanceServiceTypes.Fuel)
                {
                    await Publish(CreateMessage(message.Plane, MaintenanceServiceTypes.PassengerCoordination, SrvCommand.PassengerIn, "Начать посадку пассажиров"));
                    return;
                }

                var checkPoints = GetConsumedMessages(message.Plane.PlaneId, SrvCommand.CheckPoint);

                if (checkPoints.Count == 0)
                    return;


                if (checkPoints.Contains(MaintenanceServiceTypes.BaggageBeltLoader)
                    && checkPoints.Contains(MaintenanceServiceTypes.BaggageTransport)
                    && checkPoints.Contains(MaintenanceServiceTypes.Fuel)
                    && checkPoints.Contains(MaintenanceServiceTypes.Cleaning)
                    && checkPoints.Contains(MaintenanceServiceTypes.Water)
                    && checkPoints.Contains(MaintenanceServiceTypes.Food)
                    && checkPoints.Contains(MaintenanceServiceTypes.PassengerBridge)
                    && checkPoints.Contains(MaintenanceServiceTypes.Oxygen)
                    && checkPoints.Contains(MaintenanceServiceTypes.Electricity)
                    && checkPoints.Contains(MaintenanceServiceTypes.Engines)
                    && checkPoints.Contains(MaintenanceServiceTypes.Mover))
                {
                    await Publish(CreateMessage(message.Plane, MaintenanceServiceTypes.Dispatcher, SrvCommand.Info, "Взлёт"));
                    return;
                }


                if (checkPoints.Contains(MaintenanceServiceTypes.BaggageBeltLoader)
                    && checkPoints.Contains(MaintenanceServiceTypes.BaggageTransport)
                    && checkPoints.Contains(MaintenanceServiceTypes.Fuel)
                    && checkPoints.Contains(MaintenanceServiceTypes.Cleaning)
                    && checkPoints.Contains(MaintenanceServiceTypes.Water)
                    && checkPoints.Contains(MaintenanceServiceTypes.Food)
                    && checkPoints.Contains(MaintenanceServiceTypes.PassengerBridge)
                    && checkPoints.Contains(MaintenanceServiceTypes.Oxygen)
                    && checkPoints.Contains(MaintenanceServiceTypes.Electricity)
                    && checkPoints.Contains(MaintenanceServiceTypes.Engines))
                {
                    await Publish(CreateMessage(message.Plane, MaintenanceServiceTypes.Mover, SrvCommand.Info, "Начать буксировку на взлётную полосу"));
                    await Publish(CreateMessage(message.Plane, MaintenanceServiceTypes.Mover, SrvCommand.Stop, "Отсоединить машину-буксировщик"));
                    return;
                }



                if (checkPoints.Contains(MaintenanceServiceTypes.BaggageBeltLoader)
                    && checkPoints.Contains(MaintenanceServiceTypes.BaggageTransport)
                    && checkPoints.Contains(MaintenanceServiceTypes.Fuel)
                    && checkPoints.Contains(MaintenanceServiceTypes.Cleaning)
                    && checkPoints.Contains(MaintenanceServiceTypes.Water)
                    && checkPoints.Contains(MaintenanceServiceTypes.Food)
                    && checkPoints.Contains(MaintenanceServiceTypes.PassengerBridge)
                    && checkPoints.Contains(MaintenanceServiceTypes.Oxygen)
                    && checkPoints.Contains(MaintenanceServiceTypes.Electricity))
                {
                    await Publish(CreateMessage(message.Plane, MaintenanceServiceTypes.Engines, SrvCommand.Info, "Запуск двигателей"));
                    await Publish(CreateMessage(message.Plane, MaintenanceServiceTypes.Engines, SrvCommand.Stop, "Отсоединить машину запуска двигателей"));
                    return;
                }
            }


            if (message.SrvCommand == SrvCommand.Connected)
            {
                var checkPoints = GetConsumedMessages(message.Plane.PlaneId, SrvCommand.Connected);

                if (checkPoints.Count == 0)
                    return;

                var check =
                    checkPoints.Contains(MaintenanceServiceTypes.Electricity)
                    && checkPoints.Contains(MaintenanceServiceTypes.PassengerBridge)
                    && checkPoints.Contains(MaintenanceServiceTypes.Oxygen)
                    && checkPoints.Contains(MaintenanceServiceTypes.BaggageBeltLoader)
                    && checkPoints.Contains(MaintenanceServiceTypes.BaggageTransport)
                    && checkPoints.Contains(MaintenanceServiceTypes.Fuel)
                    && checkPoints.Contains(MaintenanceServiceTypes.Cleaning)
                    && checkPoints.Contains(MaintenanceServiceTypes.Water)
                    && checkPoints.Contains(MaintenanceServiceTypes.Food)
                    && checkPoints.Count == 9;

                if (check)
                {
                    var nextMessages1 = CreateMessage(message.Plane, MaintenanceServiceTypes.BaggageBeltLoader, SrvCommand.Stop, "Отключение модулей");
                    var nextMessages2 = CreateMessage(message.Plane, MaintenanceServiceTypes.BaggageTransport, SrvCommand.Stop, "Отключение модулей");

                    await Publish(nextMessages1);
                    await Publish(nextMessages2);

                    return;
                }
            }
        }

            

        public List<Message> PlaneServiceStart(Plane plane)
        {
            var startMessages = new List<MaintenanceServiceTypes>()
            {
                MaintenanceServiceTypes.Electricity,
                MaintenanceServiceTypes.PassengerBridge,
                MaintenanceServiceTypes.Oxygen,
                MaintenanceServiceTypes.BaggageBeltLoader,
                MaintenanceServiceTypes.BaggageTransport,
            };
            

            return CreateMessageList(plane, startMessages, SrvCommand.Start, "начать обслуживание (эл-во, мостик, кислород, багаж)");
        }
    }

    
}
