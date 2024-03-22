using AirportService.Domain;
using AirportService.Domain.Contracts;
using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AirportService.Domain.Services
{
    /// <summary>
    /// Базовый класс службы
    /// </summary>
    public class MaintenanceServiceBase : IConsumer<Message>, IHostedService
    {
        private readonly IBus _bus;

        private Random _rnd;

        private readonly ILogger<MaintenanceServiceBase> _logger;

        public Guid Guid { get; init; }

        /// <summary>
        /// Наименование службы
        /// </summary>
        public string Name { get; init; }

        public MaintenanceServiceTypes Maintenance { get; init; }

        public List<Message> ConsumedMessages { get; set; }

        public MaintenanceServiceBase(IBus bus, ILogger<MaintenanceServiceBase> logger, MaintenanceServiceTypes maintenance)
        {
            _bus = bus;
            _logger = logger;
            Guid = Guid.NewGuid();
            Maintenance = maintenance;
            Name = maintenance.Name();
            ConsumedMessages = new List<Message>();
            _rnd = new Random();
        }


        public async Task Consume(ConsumeContext<Message> context)
        {
            if (context.Message.SrvConsumer == Maintenance)
            {
                DoLogIn(context.Message);
                ConsumedMessages.Add(context.Message);
                await ProcessMessage(context.Message);
            }
        }

        [Obsolete]
        public async Task ConsumeOld(ConsumeContext<Message> context)
        {
            // хотим видеть ветку с диспетчером отдельно
            if (context.Message.SrvConsumer == Maintenance
                && Maintenance == MaintenanceServiceTypes.Dispatcher)
            {
                DoLogPlain4(context.Message);
                if (context.Message.SrvCommand != SrvCommand.Info
                    && context.Message.SrvCommand != SrvCommand.SelfTest)
                {
                    ConsumedMessages.Add(context.Message);
                    await ProcessMessage(context.Message);
                    // Console.WriteLine($"!!! Диспетчер: {ConsumedMessages.Count} сообщений");
                }
                else
                {
                    if (context.Message.SrvCommand == SrvCommand.SelfTest)
                    {
                        ConsumedMessages.Add(context.Message);
                        await ProcessMessage(context.Message);
                        Console.WriteLine($"!!! Диспетчер: {ConsumedMessages.Count} сообщений");
                    }
                }
                return;
            }
            else
            {
                if (context.Message.SrvConsumer == Maintenance)
                {
                    DoLogPlain4(context.Message);
                    if (context.Message.SrvCommand != SrvCommand.Info)
                    {
                        ConsumedMessages.Add(context.Message);
                        await ProcessMessage(context.Message);
                    }
                }
                return;
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Процессирование полученного сообщения
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual async Task ProcessMessage(Message message)
        {
            if (message.SrvCommand == SrvCommand.Info)
            {
                RemoveMessage(message);
                return;
            }

            if (message.SrvCommand == SrvCommand.Start)
            {
                await SendMessageConnected(message.Plane);
                RemoveMessage(message);

                // некоторые службы отработали - не надо ждать команд на отсоединение - сами себя инфрмируем
                if (Maintenance == MaintenanceServiceTypes.Food
                    || Maintenance == MaintenanceServiceTypes.Water
                    || Maintenance == MaintenanceServiceTypes.Cleaning
                    || Maintenance == MaintenanceServiceTypes.Fuel)
                {
                    string comment;

                    switch (Maintenance)
                    {
                        case MaintenanceServiceTypes.Food:
                            comment = "Еда обновлена";
                            break;
                        case MaintenanceServiceTypes.Water:
                            comment = "Вода обновлена";
                            break;
                        case MaintenanceServiceTypes.Cleaning:
                            comment = "Уборка помещений закончина";
                            break;
                        case MaintenanceServiceTypes.Fuel:
                            comment = "ТОПЛИВО ЗАГРУЖЕНО";
                            break;
                        default:
                            throw new NotImplementedException();
                    }

                    var nextMessages = CreateMessageList(
                        message.Plane,
                        new List<MaintenanceServiceTypes> { Maintenance },
                        SrvCommand.Stop, comment);

                    await Publish(nextMessages);
                    return;
                }

                if (Maintenance == MaintenanceServiceTypes.PassengerBridge)
                {
                    await Publish(CreateMessage(message.Plane, MaintenanceServiceTypes.PassengerCoordination, SrvCommand.PassengerOut,
                        "Пассажирам покинуть борт"));
                }

                return;
            }

            if (message.SrvCommand == SrvCommand.Stop)
            {
                await SendMessageCheckPoint(message.Plane);
                RemoveMessage(message);
                return;
            }

            // Блок команд только для службы кординации пассажиров
            if (message.SrvConsumer == MaintenanceServiceTypes.PassengerCoordination)
            {
                if (message.SrvCommand == SrvCommand.PassengerOut)
                {
                    await Publish(CreateMessage(message.Plane, MaintenanceServiceTypes.Fuel, SrvCommand.Start, "Загрузить топливо"));
                    await Publish(CreateMessage(message.Plane, MaintenanceServiceTypes.Cleaning, SrvCommand.Start, "Начать уборку"));
                    await Publish(CreateMessage(message.Plane, MaintenanceServiceTypes.Water, SrvCommand.Start, "Наполнить резервуары с водой"));
                    await Publish(CreateMessage(message.Plane, MaintenanceServiceTypes.Food, SrvCommand.Start, "Обновить продуктовые наборы"));
                    await Publish(CreateMessage(message.Plane, MaintenanceServiceTypes.Engines, SrvCommand.Start, "Присоединить службу двигателей"));
                    await Publish(CreateMessage(message.Plane, MaintenanceServiceTypes.Mover, SrvCommand.Start, "Присоединить машину-транспортировщик"));
                    await Publish(CreateMessage(message.Plane, MaintenanceServiceTypes.BaggageTransport, SrvCommand.Stop, "Закончить выгрузку/погрузку багажа"));
                    await Publish(CreateMessage(message.Plane, MaintenanceServiceTypes.BaggageBeltLoader, SrvCommand.Stop, "Закончить работу ленточного погрузчика"));

                    return;
                }

                if (message.SrvCommand == SrvCommand.PassengerIn)
                {
                    await Publish(CreateMessage(message.Plane, MaintenanceServiceTypes.PassengerBridge, SrvCommand.Stop, "Закончить работу пассажирского мостика"));
                    await Publish(CreateMessage(message.Plane, MaintenanceServiceTypes.Oxygen, SrvCommand.Stop, "Закончить работу службы кислорода"));
                    await Publish(CreateMessage(message.Plane, MaintenanceServiceTypes.Electricity, SrvCommand.Stop, "Закончить работу службы электричества"));
                    return;
                }
            }

            throw new NotImplementedException();
        }

        #region DoLog

        private void DoLogPlain3(Message message)
        {
            Console.WriteLine(MessageLogFormatter.LogMessageCreateV3(message));
        }

        private void DoLogPlain4(Message message)
        {
            if (message.SrvConsumer != MaintenanceServiceTypes.Dispatcher)
                return;
            Console.WriteLine(MessageLogFormatter.LogMessageCreateV3(message));
        }


        private void DoLogIn(Message message)
        {
            Console.WriteLine(MessageLogFormatter.LogMessageCreate(message, Direction.In));
        }

        private void DoLogOut(Message message)
        {
            Console.WriteLine(MessageLogFormatter.LogMessageCreate(message, Direction.Out));
        }

        #endregion



        #region Работа с сообщениями

        /// <summary>
        /// Опубликовать сообщение
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task Publish(Message message)
        {
            // задержка перед отправкой - имитация работы
            var delay = _rnd.Next(3, 5); 
            await Task.Delay(delay * 1000);

            DoLogOut(message);

            await _bus.Publish(message);

            await _bus.Publish(new MessageLog(message));
        }

        /// <summary>
        /// Опубликовать сообщениЯ
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        public async Task Publish(List<Message> messages)
        {
            foreach (var message in messages)
            {
                await Publish(message);
            }
        }

        /// <summary>
        /// Список сообщений в памяти
        /// </summary>
        /// <returns></returns>
        public List<Message> GetConsumedMessages()
        {
            return ConsumedMessages;
        }

        /// <summary>
        /// Список сообщений по заданному ВС
        /// </summary>
        /// <param name="planeId"></param>
        /// <returns></returns>
        public List<Message> GetConsumedMessages(Guid planeId)
        {
            return GetConsumedMessages().Where(x => x.Plane.PlaneId == planeId).ToList();
        }

        /// <summary>
        /// Список сообщений по заданному ВС, заданного типа команды
        /// </summary>
        /// <param name="planeId"></param>
        /// <param name="srvCommand"></param>
        /// <returns></returns>
        public List<MaintenanceServiceTypes> GetConsumedMessages(Guid planeId, SrvCommand srvCommand)
        {
            return GetConsumedMessages()
                .Where(x => x.Plane.PlaneId == planeId && x.SrvCommand == srvCommand)
                .Select(x => x.SrvProducer)
                .ToList();
        }

        public void RemoveMessage(Message message)
        {
            if (Maintenance != MaintenanceServiceTypes.Dispatcher)
            {
                for (int i = 0; i < ConsumedMessages.Count; i++)
                {
                    if (ConsumedMessages[i].MessageGuid == message.MessageGuid)
                        ConsumedMessages.Remove(message);
                }
            }

        }


        /// <summary>
        /// Базовое сообщение
        /// </summary>
        /// <returns></returns>
        public Message CreateMessage()
        {
            return new Message()
            {
                MessageGuid = Guid.NewGuid(),
                ProducerGuid = Guid,
                SrvProducer = Maintenance,
                ProducedAt = DateTime.UtcNow
            };
        }


        /// <summary>
        /// Сообщение с адресатом
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Message CreateMessage(Plane value)
        {
            var message = CreateMessage();
            message.Plane = value;
            return message;
        }

        public async Task SendMessageCheckPoint(Plane plane)
        {
            var message = CreateMessageCheckPoint(plane);
            await Publish(message);
        }

        public async Task SendMessageConnected(Plane plane)
        {
            var message = CreateMessageConnected(plane);
            await Publish(message);
        }

        private Message CreateMessageConnected(Plane plane, MaintenanceServiceTypes target = MaintenanceServiceTypes.Dispatcher, SrvCommand srvCommand = SrvCommand.Connected, string comment = "OK")
        {
            return new Message()
            {
                MessageGuid = Guid.NewGuid(),
                ProducerGuid = Guid,
                SrvProducer = Maintenance,
                ProducedAt = DateTime.UtcNow,
                SrvConsumer = target,
                Plane = plane,
                SrvCommand = srvCommand,
                Comment = comment
            };
        }

        private Message CreateMessageCheckPoint(Plane plane, MaintenanceServiceTypes target = MaintenanceServiceTypes.Dispatcher, SrvCommand srvCommand = SrvCommand.CheckPoint, string comment = "OK")
        {
            return new Message()
            {
                MessageGuid = Guid.NewGuid(),
                ProducerGuid = Guid,
                SrvProducer = Maintenance,
                ProducedAt = DateTime.UtcNow,
                SrvConsumer = target,
                Plane = plane,
                SrvCommand = srvCommand,
                Comment = comment
            };
        }


        public Message CreateMessage(Plane plane, MaintenanceServiceTypes target, SrvCommand srvCommand, string comment)
        {
            return new Message()
            {
                MessageGuid = Guid.NewGuid(),
                ProducerGuid = Guid,
                SrvProducer = Maintenance,
                ProducedAt = DateTime.UtcNow,
                SrvConsumer = target,
                Plane = plane,
                SrvCommand = srvCommand,
                Comment = comment
            };
        }


        public List<Message> CreateMessageList(
            Plane plane,
            List<MaintenanceServiceTypes> targets,
            SrvCommand srvCommand,
            string comment)
        {
            return targets.Select(x => CreateMessage(plane, x, srvCommand, comment)).ToList();
        }

        #endregion


        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Служба запущена {Name} ({Guid})");

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
