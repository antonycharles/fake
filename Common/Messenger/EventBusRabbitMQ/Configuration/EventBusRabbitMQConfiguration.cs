using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Messenger.EventBus.Abstractions;
using RabbitMQ.Client;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Messenger.EventBus.EventBusRabbitMQ.Configuration
{
    public static class EventBusRabbitMQConfiguration
    {
        public static void AddEventBusRabbitMQ(this IServiceCollection services, RabbitMQBaseSettings configuration)
        {
            ValidateConfiguration(configuration);

            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
                var factory = new ConnectionFactory
                {
                    Uri = new Uri(configuration.Endpoint),
                    UserName = configuration.User,
                    Password = configuration.Password,
                    DispatchConsumersAsync = true
                };

                var retryCount = GetRetryConnectionCount(configuration);

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });

            services.AddSingleton<IEventBus, EventBusRabbitMQ>(serviceProvider =>
            {
                var rabbitMQPersistentConnection = serviceProvider.GetRequiredService<IRabbitMQPersistentConnection>();
                var logger = serviceProvider.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = serviceProvider.GetRequiredService<IEventBusSubscriptionsManager>();

                var hasRetry = GetHasRetryConnection(configuration);
                var retryConnectionCount = GetRetryConnectionCount(configuration);

                return new EventBusRabbitMQ(
                    rabbitMQPersistentConnection,
                    logger,
                    serviceProvider,
                    eventBusSubcriptionsManager,
                    configuration.Broker,
                    configuration.Queue,
                    retryConnectionCount,
                    hasRetry);
            });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
        }

        private static void ValidateConfiguration(RabbitMQBaseSettings configuration)
        {
            ValidationContext context = new ValidationContext(configuration, null, null);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            bool valid = Validator.TryValidateObject(configuration, context, validationResults, true);
            if (!valid)
            {
                foreach (ValidationResult validationResult in validationResults)
                {
                    Console.WriteLine("{0}", validationResult.ErrorMessage);
                }

                System.Environment.Exit(validationResults.Count);
            }
        }

        private static int GetRetryConnectionCount(RabbitMQBaseSettings configuration)
        {
            var retryConfig = configuration.ConnRetries;
            var retryCount = 5;

            if (!string.IsNullOrEmpty(retryConfig))
                retryCount = int.Parse(retryConfig);

            return retryCount;
        }
        
        private static bool GetHasRetryConnection(RabbitMQBaseSettings configuration)
        {
            var retryConfig = configuration.HasRetry;

            var hasRetry = false;
            
            if (!string.IsNullOrEmpty(retryConfig))
                hasRetry = bool.Parse(retryConfig);

            return hasRetry;
        }
    }
}