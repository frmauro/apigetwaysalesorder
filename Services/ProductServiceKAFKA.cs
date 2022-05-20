using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using ApiGetwaySalesOrder.Dtos;

namespace ApiGetwaySalesOrder.Services
{
    public class ProductServiceKAFKA
    {
        public string SendMsgUpdateAmount(string dto)
        {
            var config = new ProducerConfig { BootstrapServers = "kafka:9092" };
            var result = string.Empty;

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var sendResult = producer
                                        .ProduceAsync("updateamount", new Message<Null, string> { Value = dto })
                                            .GetAwaiter()
                                                .GetResult();

                    result = $"Mensagem '{sendResult.Value}' de '{sendResult.TopicPartitionOffset}'";
                }
                catch (ProduceException<string, UpdateAmountDto> e)
                {
                    //Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                    result = $"Delivery failed: {e.Error.Reason}";
                }
            }

            return result;
        }

    }
}