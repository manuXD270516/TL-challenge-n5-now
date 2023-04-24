using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intregation.kafka.consumer;
using Intregation.kafka.producer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Intregation
{
    public static class ServicesRegistration
    {

        public static IServiceCollection AddIntegrations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(sp => new KafkaProducerWrapper<string, string>(configuration["Kafka:BootstrapServers"]));
            services.AddSingleton(sp => new KafkaConsumerWrapper<string, string>(
                    configuration["Kafka:BootstrapServers"],
                    configuration["Kafka:ConsumerGroupId"],
                    configuration["Kafka:ConsumerTopic"])
            );

            return services;
        }
    }
}
