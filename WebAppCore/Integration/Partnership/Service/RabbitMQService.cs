using Integration.Partnership.Model;
using Integration.Partnership.Repository;
using Integration.Partnership.Repository.RepositoryInterfaces;
using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository;
using Integration.Pharmacy.Repository.RepositoryInterfaces;
using Microsoft.AspNetCore.SignalR;
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
        ITenderRepository tenderRepository;

        public RabbitMQService(INewsRepository newsRepository, IPharmaciesRepository pharmaciesRepository, ITenderRepository tenderRepository)
        {
            this.newsRepository = newsRepository;
            this.pharmaciesRepository = pharmaciesRepository;
            this.tenderRepository = tenderRepository;
        }

        public RabbitMQService()
        {
            
            newsRepository = new NewsRepository(new SharedModel.IntegrationDbContext());
            pharmaciesRepository = new PharmaciesRepository();
            tenderRepository = new TenderRepository();
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            NewsChannelExchange();

            TenderChannelExchange();

            return base.StartAsync(cancellationToken);
        }

        private void TenderChannelExchange()
        {
            foreach (PharmacyProfile pharmacyProfile in pharmaciesRepository.GetAll())
            {
                OpenConnectionTender(pharmacyProfile);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    byte[] body = ea.Body.ToArray();
                    var jsonMessage = Encoding.UTF8.GetString(body);

                    TenderOffer offer = JsonConvert.DeserializeObject<TenderOffer>(jsonMessage);

                    ReceiveTenderOffer(offer, pharmacyProfile);
                };

                channel.BasicConsume(queue: pharmacyProfile.Name + "Offers",
                                        autoAck: true,
                                        consumer: consumer);
            }
        }

        private void OpenConnectionTender(PharmacyProfile pharmacyProfile)
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
        }

        private void ReceiveTenderOffer(TenderOffer offer, PharmacyProfile pharmacyProfile)
        {
            offer.PharmacyName = pharmacyProfile.Name;

            tenderRepository.AddOffer(offer);
        }

        private void NewsChannelExchange()
        {
            foreach (PharmacyProfile pharmacyProfile in pharmaciesRepository.GetAll())
            {
                OpenConnectionNews(pharmacyProfile);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    byte[] body = ea.Body.ToArray();
                    var jsonMessage = Encoding.UTF8.GetString(body);
                   
                    News news = JsonConvert.DeserializeObject<News>(jsonMessage);

                    ReceiveNews(news, pharmacyProfile);
                };

                channel.BasicConsume(queue: pharmacyProfile.Name,
                                        autoAck: true,
                                        consumer: consumer);
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

        private void ReceiveNews(News news, PharmacyProfile pharmacyProfile)
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
