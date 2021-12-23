using Integration.Partnership.Model;
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
        Boolean isTest;

        public RabbitMQService(INewsRepository newsRepository, IPharmaciesRepository pharmaciesRepository)
        {
            this.newsRepository = newsRepository;
            this.pharmaciesRepository = pharmaciesRepository;
            isTest = true;
        }

        public RabbitMQService()
        {
            newsRepository = new NewsRepository();
            pharmaciesRepository = new PharmaciesRepository();
            isTest = false;
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            foreach (PharmacyProfile pharmacyProfile in pharmaciesRepository.GetAll())
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

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    byte[] body = ea.Body.ToArray();
                    var jsonMessage = Encoding.UTF8.GetString(body);
                    News news;

                    news = JsonConvert.DeserializeObject<News>(jsonMessage);

                    if (isTest) newsRepository.GetAll();
                    news.Posted = false;
                    news.IdEncoded = Generator.GenerateNewsId();
                    news.PharmacyName = pharmacyProfile.Name;
                    newsRepository.Save(news);

                };


                channel.BasicConsume(queue: pharmacyProfile.Name,
                                        autoAck: true,
                                        consumer: consumer);
            }

            foreach (PharmacyProfile pharmacyProfile in pharmaciesRepository.GetAll())
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                connection = factory.CreateConnection();
                channel = connection.CreateModel();

                channel.ExchangeDeclare(exchange: pharmacyProfile.Name + "OffersChannel", type: ExchangeType.Direct);
                var queueName = channel.QueueDeclare(queue: pharmacyProfile.Name + "Offers",
                                       durable: false,
                                       exclusive: false,
                                       autoDelete: false,
                                       arguments: null).QueueName;
                channel.QueueBind(queue: queueName,
                                    exchange: pharmacyProfile.Name + "OffersChannel",
                                    routingKey: pharmacyProfile.Name + "Offers");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    byte[] body = ea.Body.ToArray();
                    var jsonMessage = Encoding.UTF8.GetString(body);
                    TenderOffer offer;

                    offer = JsonConvert.DeserializeObject<TenderOffer>(jsonMessage);

                    offer.PharmacyName = pharmacyProfile.Name;

                    Console.WriteLine(offer.PharmacyName);
                    foreach (OfferItem item in offer.OfferItems)
                    {
                        Console.WriteLine(item.MedicineName + " " + item.Quantity + " " + item.Price);
                    }
                    Console.WriteLine(offer.TotalPrice);


                };


                channel.BasicConsume(queue: pharmacyProfile.Name + "Offers",
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
