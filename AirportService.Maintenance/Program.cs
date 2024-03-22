using AirportService.Domain.Contracts;
using AirportService.Domain.Services;
using AirportService.Maintenance.Models;
using AirportService.Maintenance.Utility;
using MassTransit;
using Microsoft.FeatureManagement;
using System.Security.Authentication;

namespace AirportService.Maintenance
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.

            builder.Services.AddSingleton<PlaneService>();

            var flags = new Flags();
            builder.Configuration.GetSection("FeatureManagement").Bind(flags);

            var brokerConfig = new BrokerConfig();
            builder.Configuration.GetSection("BrokerConfig").Bind(brokerConfig);
            brokerConfig.Validate();

            builder.Services.AddFeatureManagement();


            #region PUBLISHERS

            // добавляем службы аэропортов
            if (flags.IsBaggageBeltLoader)
                builder.Services.AddSingleton<SrvBaggageBeltLoader>();
            if (flags.IsBaggageTransport)
                builder.Services.AddSingleton<SrvBaggageTransport>();
            if (flags.IsCleaning)
                builder.Services.AddSingleton<SrvCleaning>();
            if (flags.IsDispatcher)
                builder.Services.AddSingleton<SrvDispatcher>();
            if (flags.IsElectricity)
                builder.Services.AddSingleton<SrvElectricity>();
            if (flags.IsStartingEngines)
                builder.Services.AddSingleton<SrvEngines>();
            if (flags.IsFood)
                builder.Services.AddSingleton<SrvFood>();
            if (flags.IsFuel)
                builder.Services.AddSingleton<SrvFuel>();
            if (flags.IsMover)
                builder.Services.AddSingleton<SrvMover>();
            if (flags.IsOxygen)
                builder.Services.AddSingleton<SrvOxygen>();
            if (flags.IsPassengerBridge)
                builder.Services.AddSingleton<SrvPassengerBridge>();
            if (flags.IsPassengerCoordination)
                builder.Services.AddSingleton<SrvPassengerCoordination>();
            if (flags.IsWater)
                builder.Services.AddSingleton<SrvWater>();

            #endregion


            // Добавляем MassTransit
            builder.Services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                Guid instanceId = Guid.NewGuid();

                #region CONSUMERS

                // Добавляем подписчиков
                if (flags.IsBaggageBeltLoader)
                {
                    x.AddConsumer<SrvBaggageBeltLoader>().Endpoint(c => c.InstanceId = nameof(SrvBaggageBeltLoader));
                }

                if (flags.IsBaggageTransport)
                {
                    x.AddConsumer<SrvBaggageTransport>().Endpoint(c => c.InstanceId = nameof(SrvBaggageTransport));
                }

                if (flags.IsCleaning)
                {
                    x.AddConsumer<SrvCleaning>().Endpoint(c => c.InstanceId = nameof(SrvCleaning));
                }

                if (flags.IsDispatcher)
                {
                    x.AddConsumer<SrvDispatcher>().Endpoint(c => c.InstanceId = nameof(SrvDispatcher));
                }

                if (flags.IsElectricity)
                {
                    x.AddConsumer<SrvElectricity>().Endpoint(c => c.InstanceId = nameof(SrvElectricity));
                }

                if (flags.IsStartingEngines)
                {
                    x.AddConsumer<SrvEngines>().Endpoint(c => c.InstanceId = nameof(SrvEngines));
                }

                if (flags.IsFood)
                {
                    x.AddConsumer<SrvFood>().Endpoint(c => c.InstanceId = nameof(SrvFood));
                }

                if (flags.IsFuel)
                {
                    x.AddConsumer<SrvFuel>().Endpoint(c => c.InstanceId = nameof(SrvFuel));
                }

                if (flags.IsMover)
                {
                    x.AddConsumer<SrvMover>().Endpoint(c => c.InstanceId = nameof(SrvMover));
                }

                if (flags.IsOxygen)
                {
                    x.AddConsumer<SrvOxygen>().Endpoint(c => c.InstanceId = nameof(SrvOxygen));
                }

                if (flags.IsPassengerBridge)
                {
                    x.AddConsumer<SrvPassengerBridge>().Endpoint(c => c.InstanceId = nameof(SrvPassengerBridge));
                }

                if (flags.IsPassengerCoordination)
                {
                    x.AddConsumer<SrvPassengerCoordination>().Endpoint(c => c.InstanceId = nameof(SrvPassengerCoordination));
                }

                if (flags.IsWater)
                {
                    x.AddConsumer<SrvWater>().Endpoint(c => c.InstanceId = nameof(SrvWater));
                }


                #endregion


                // В данном примере используем InMemory реализацию

                if (brokerConfig.UseLocal)
                {
                    x.UsingInMemory((context, cfg) =>
                    {
                        cfg.ConfigureEndpoints(context);
                    });
                }
                else
                {
                    if (brokerConfig.UseRabbitMq)
                    {
                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.Host(brokerConfig.RabbitMqHost, "/", h =>
                            {
                                h.Username(brokerConfig.RabbitMqLogin);
                                h.Password(brokerConfig.RabbitMqPassword);

                                if (brokerConfig.UseRabbitMqUseSSL)
                                    h.UseSsl(s =>
                                    {
                                        s.Protocol = SslProtocols.Tls12;
                                    });
                            });
                            cfg.ConfigureEndpoints(context);
                        });

                    }
                }
            });





            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();


        }

        private static string CreateNewGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
