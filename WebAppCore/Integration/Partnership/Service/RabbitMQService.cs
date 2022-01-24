﻿using Integration.Partnership.Model;
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
        Boolean isTest;
        private IHubContext<SignalServer> _hubContext;

        public RabbitMQService(INewsRepository newsRepository, IPharmaciesRepository pharmaciesRepository, ITenderRepository tenderRepository, Boolean isTest)
        {
            this.newsRepository = newsRepository;
            this.pharmaciesRepository = pharmaciesRepository;
            this.tenderRepository = tenderRepository;
            this.isTest = isTest;
        }

        public RabbitMQService()
        {
            
            newsRepository = new NewsRepository();
            pharmaciesRepository = new PharmaciesRepository();
            tenderRepository = new TenderRepository();
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

                    tenderRepository.AddOffer(offer);

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
