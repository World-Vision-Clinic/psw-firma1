using Integration.Pharmacy.Model;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace IntegrationTests.IntegrationTests.RabbitMQSender
{
    public class Sender
    {

        private ConnectionFactory factory = new ConnectionFactory();

        public void sendMessage(News pieceOfNews, string channelName, string queueName)
        {
            factory.HostName = "localhost";
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {

                channel.ExchangeDeclare(exchange: channelName, type: ExchangeType.Direct);
                channel.QueueDeclare(queue: queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);


                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(pieceOfNews));

                channel.BasicPublish(exchange: channelName,
                                     routingKey: queueName,
                                     basicProperties: null,
                                     body: body);

            }
        }
    }
}
