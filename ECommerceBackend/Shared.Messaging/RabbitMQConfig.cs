using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Shared.Messaging
{
    public static class RabbitMQConfig
    {
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration configuration, Action<IBusRegistrationConfigurator>? configureConsumers = null)
        {
            services.AddMassTransit(x =>
            {
                // Đăng ký consumers (nếu có)
                configureConsumers?.Invoke(x);

                x.UsingRabbitMq((context, cfg) =>
                {
                    // Lấy giá trị trực tiếp từ IConfiguration thay vì IConfigurationSection
                    var host = configuration["RabbitMQ:Host"] ?? "localhost";
                    var port = ushort.TryParse(configuration["RabbitMQ:Port"], out var parsedPort) ? parsedPort : (ushort)5672;
                    var virtualHost = configuration["RabbitMQ:VirtualHost"] ?? "/";
                    var username = configuration["RabbitMQ:Username"] ?? "guest";
                    var password = configuration["RabbitMQ:Password"] ?? "guest";

                    cfg.Host(host, port, virtualHost, h =>
                    {
                        h.Username(username);
                        h.Password(password);
                    });
                    // Tự động cấu hình các endpoint dựa trên consumers
                    cfg.ConfigureEndpoints(context);
                });
            });

            return services;
        }
    }
}