using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository;
using Integration.Pharmacy.Repository.RepositoryInterfaces;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Integration.Pharmacy.Service
{
    public class RabbitMQService : BackgroundService
    {
        IConnection connection;
        IModel channel;
        INewsRepository newsRepository;
        IPharmaciesRepository pharmaciesRepository;

        public RabbitMQService(INewsRepository newsRepository, IPharmaciesRepository pharmaciesRepository)
        {
            this.newsRepository = newsRepository;
            this.pharmaciesRepository = pharmaciesRepository;
        }

        public RabbitMQService()
        {
            newsRepository = new NewsRepository();
            pharmaciesRepository = new PharmaciesRepository();
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            foreach(PharmacyProfile pharmacyProfile in pharmaciesRepository.GetAll())
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                connection = factory.CreateConnection();
                channel = connection.CreateModel();
                channel.QueueDeclare(queue: pharmacyProfile.Name,
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    byte[] body = ea.Body.ToArray();
                    var jsonMessage = Encoding.UTF8.GetString(body);
                    News news;

                    news = JsonConvert.DeserializeObject<News>(jsonMessage);


                    news.Posted = false;
                    news.IdEncoded = Generator.GenerateNewsId();
                    news.PharmacyName = pharmacyProfile.Name;
                    newsRepository.Save(news);

                };


                channel.BasicConsume(queue: pharmacyProfile.Name,
                                        autoAck: true,
                                        consumer: consumer);
            }
            return base.StartAsync(cancellationToken);
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
