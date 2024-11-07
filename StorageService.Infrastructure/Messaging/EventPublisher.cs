using StorageService.Application.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StorageService.Infrastructure.Messaging
{
    public class EventPublisher : IEventPublisher
    {
        private readonly RabbitMqConnectionFactory _connectionFactory;

        public async Task PublishFileAddedEvent(FileAddedEvent fileAddedEvent)
        {
            // Await connection creation
            using var connection = await Task.Run(() => _connectionFactory.CreateConnection());
            using var channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync("file-events", ExchangeType.Fanout); // Fanout exchange

            var messageBody = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(fileAddedEvent));
            await channel.BasicPublishAsync(exchange: "file-events", routingKey: "", body: messageBody);
        }

        public async Task PublishFileDeletedEvent(FileDeletedEvent fileDeletedEvent)
        {
            // Await connection creation
            using var connection = await Task.Run(() => _connectionFactory.CreateConnection());
            using var channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync("file-events", ExchangeType.Fanout); // Fanout exchange

            var messageBody = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(fileDeletedEvent));
            await channel.BasicPublishAsync(exchange: "file-events", routingKey: "", body: messageBody);
        }
    }
}
