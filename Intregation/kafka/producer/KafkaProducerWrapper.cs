using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intregation.kafka.producer
{
    public class KafkaProducerWrapper<TKey, TValue>: IDisposable
    {
        private readonly IProducer<TKey, TValue> _producer;

        public KafkaProducerWrapper(string bootstrapServers)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = bootstrapServers
            };

            _producer = new ProducerBuilder<TKey, TValue>(config).Build();
        }

        public async Task ProduceAsync(string topic, TKey key, TValue value)
        {
            try
            {
            await _producer.ProduceAsync(topic, new Message<TKey, TValue> { Key = key, Value = value });

            }
            catch (Exception e)
            {
                string em = e.Message;
                throw;
            }
        }

        public void Dispose()
        {
            _producer.Dispose();
        }
    }
}
