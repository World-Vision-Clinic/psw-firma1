using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository;
using Integration.Pharmacy.Repository.RepositoryInterfaces;
using Integration.Pharmacy.Service;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Integration_API.MessageQueue
{
    public class NewsMQConnection : BackgroundService
    {
        IConnection connection;
        IModel channel;
        IPharmaciesRepository pharmaciesRepository;
        INewsRepository newsRepository;

        public NewsMQConnection(IPharmaciesRepository pharmaciesRepository, INewsRepository newsRepository)
        {
            this.pharmaciesRepository = pharmaciesRepository;
            this.newsRepository = newsRepository;
        }

        public NewsMQConnection()
        {
            pharmaciesRepository = new PharmaciesRepository();
            newsRepository = new NewsRepository(new Integration.SharedModel.IntegrationDbContext());
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            NewsChannelExchange();
            return base.StartAsync(cancellationToken);
        }

        private void NewsChannelExchange()
        {
            foreach (PharmacyProfile pharmacyProfile in pharmaciesRepository.GetAll())
            {
                OpenConnectionNews(pharmacyProfile);
                RecieveNews(pharmacyProfile);
            }
        }

        private void OpenConnectionNews(PharmacyProfile pharmacyProfile)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: pharmacyProfile.Name + "NewsChannel", type: ExchangeType.Direct);
            var queueName = channel.QueueDeclare(queue: pharmacyProfile.Name,
                                   durable: false,
                                   exclusive: false,
                                   autoDelete: false,
                                   arguments: null).QueueName;
            channel.QueueBind(queue: queueName,
                                exchange: pharmacyProfile.Name + "NewsChannel",
                                routingKey: pharmacyProfile.Name);
        }

        private void RecieveNews(PharmacyProfile pharmacyProfile)
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                byte[] body = ea.Body.ToArray();
                var jsonMessage = Encoding.UTF8.GetString(body);
                News news = JsonConvert.DeserializeObject<News>(jsonMessage);
                SaveNews(news, pharmacyProfile);
            };

            channel.BasicConsume(queue: pharmacyProfile.Name,
                                        autoAck: true,
                                        consumer: consumer);
        }

        private void SaveNews(News news, PharmacyProfile pharmacyProfile)
        {
            news.Posted = false;
            news.IdEncoded = Generator.GenerateNewsId();
            news.PharmacyName = pharmacyProfile.Name;
            newsRepository.Save(news);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            channel.Close();
            connection.Close();
            return base.StopAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}
