using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Intregation.kafka.consumer
{
    public class KafkaConsumerWrapper<TKey, TValue> : IDisposable
    {
        private readonly IConsumer<TKey, TValue> _consumer;

        public KafkaConsumerWrapper(string bootstrapServers, string groupId, string topic)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = groupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<TKey, TValue>(config)
                .Build();
            _consumer.Subscribe(topic);
        }

        public async Task ConsumeAsync(Func<ConsumeResult<TKey, TValue>, Task> consumeHandler, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var result = _consumer.Consume(cancellationToken);
                    await consumeHandler(result);
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Error occured: {e.Error.Reason}");
                }
            }
        }

        public void Dispose()
        {
            _consumer.Dispose();
        }
    }
}
